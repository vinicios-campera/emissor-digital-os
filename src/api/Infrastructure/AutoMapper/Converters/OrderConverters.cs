using AutoMapper;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Kernel.Toolkit.Extensions;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Constants;

namespace OrderService.Infrastructure.AutoMapper.Converters
{
    public static class OrderConverters
    {
        public class OrderInsertToOrder : ITypeConverter<OrderInsert, Order>
        {
            public Order Convert(OrderInsert source, Order destination, ResolutionContext context)
            {
                var user = context.Items[AutomapperConstants.PARAM_USER].ConvertTo<User>();
                var client = context.Items[AutomapperConstants.PARAM_CLIENT].ConvertTo<ClientResponse>();
                var lastOrder = context.Items[AutomapperConstants.PARAM_LAST_ORDER].ConvertTo<Order>();

                destination = new Order
                {
                    Start = source.Start,
                    Finish = source.Finish,
                    Note = source.Note!.ToUppercaseFirst(),
                    Amount = source.Products!.Sum(x => x.Subtotal),
                    Discount = source.Discount,
                    Identifier = lastOrder != null ? lastOrder.Identifier + 1 : 1,
                    State = OrderState.None,
                    UserEmail = user!.Email
                };

                if (client != null)
                    destination.ClientId = client!.Id;

                destination.SetInsertedDate();
                destination.Pdf = GeneratePdf(user, client, destination, source.Products!);
                return destination;
            }

            private static byte[] GeneratePdf(User user, ClientResponse? client, Order order, IEnumerable<OrderProductInsert> products)
            {
                var stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                document.SetMargins(2, 5, 70, 5);
                var separator = new LineSeparator(new DashedLine());

                if (user.AddPictureInOrder)
                {
                    var logo = new Image(ImageDataFactory
                    .Create(user.Picture))
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetHeight(100)
                    .SetWidth(100)
                    .SetBorderRadius(new BorderRadius(20));
                    document.Add(logo);
                }

                if (client != null)
                {
                    var header = new Paragraph($"Ordem de Serviço - N° {order.Identifier}").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
                    document.Add(header);
                }

                var _client = new List<string>();

                if (client != null && !string.IsNullOrEmpty(client.Name))
                    _client.Add($"Cliente: {client.Name}");

                if (client != null && !string.IsNullOrEmpty(client.Document))
                {
                    var prefixDocument = "Documento";
                    if (client.Document.IsPhysicalDocument())
                        prefixDocument = "CPF";

                    if (client.Document.IsLegalDocument())
                        prefixDocument = "CNPJ";

                    _client.Add($"{prefixDocument}: {client.Document}");
                }

                if (_client.Count > 0)
                    document.Add(new Paragraph(string.Join(" - ", _client)).SetTextAlignment(TextAlignment.LEFT));

                var _phone = new List<string>();

                if (client != null && !string.IsNullOrEmpty(client.Cellphone))
                    _phone.Add($"Celular: {client.Cellphone}");

                if (_phone.Count > 0)
                    document.Add(new Paragraph(string.Join(" ", _phone)).SetTextAlignment(TextAlignment.LEFT));

                if (_client.Count > 0 || _phone.Count > 0)
                    document.Add(separator);

                document.Add(new Paragraph($"Observação: {order.Note}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph($"Data emissão: {order.Inserted:dd/MM/yyyy}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph($"Data inicio: {order.Start:dd/MM/yyyy}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph($"Data término: {order.Finish:dd/MM/yyyy}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(separator);
                document.Add(new Paragraph());
                document.Add(GetTableItems(products));
                document.Add(new Paragraph());
                document.Add(separator);
                document.Add(new Paragraph($"Total: {order.Amount:N2}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph($"Desconto: {order.Discount:N2}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph($"Subtotal: {order.Amount - order.Discount:N2}").SetTextAlignment(TextAlignment.LEFT));
                document.Add(separator);

                Paragraph GetParagraphNameAndEmail() => !string.IsNullOrEmpty(user.NameFull) ? new Paragraph($"{user.NameFull}") : new Paragraph($"{user.Name}");

                (bool, Paragraph) GetParagraphContact()
                {
                    var contact = new List<string>();

                    if (!string.IsNullOrEmpty(user.Telephone))
                        contact.Add($"{user.Telephone}");

                    if (!string.IsNullOrEmpty(user.Cellphone))
                        contact.Add($"{user.Cellphone}");

                    var result = contact.Count > 0 ? $"Contato: {user.Email} - {string.Join(" / ", contact)}" : $"Contato: {user.Email}";

                    return (true, new Paragraph(result));
                }

                (bool, Paragraph) GetParagraphCpfOrCnpfAndAdress()
                {
                    var cpfOrCnpfAndAdress = new List<string>();

                    if (!string.IsNullOrEmpty(user.Document))
                    {
                        var prefixDocument = "Documento";
                        if (user.Document.IsPhysicalDocument())
                            prefixDocument = "CPF";

                        if (user.Document.IsLegalDocument())
                            prefixDocument = "CNPJ";

                        cpfOrCnpfAndAdress.Add($"{prefixDocument}: {user.Document}");
                    }

                    if (!string.IsNullOrEmpty(user.Address))
                        cpfOrCnpfAndAdress.Add($"{user.Address}");

                    if (!string.IsNullOrEmpty(user.City))
                        cpfOrCnpfAndAdress.Add($"{user.City}");

                    if (!string.IsNullOrEmpty(user.State))
                        cpfOrCnpfAndAdress.Add($"{user.State}");

                    return (cpfOrCnpfAndAdress.Count > 0, new Paragraph(string.Join(" - ", cpfOrCnpfAndAdress)));
                }

                for (int i = 1; i <= document.GetPdfDocument().GetNumberOfPages(); i++)
                {
                    int spacing = 20;
                    int currentMultiplySpacing = 1;
                    Rectangle pageSize = document.GetPdfDocument().GetPage(i).GetPageSize();
                    float x = pageSize.GetWidth() / 2;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    float y = pageSize.GetTop() - (pageSize.GetTop() - spacing * currentMultiplySpacing);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                    if (GetParagraphCpfOrCnpfAndAdress().Item1)
                    {
                        y = pageSize.GetTop() - (pageSize.GetTop() - spacing * currentMultiplySpacing);
                        document.ShowTextAligned(GetParagraphCpfOrCnpfAndAdress().Item2, x, y, i, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);
                        currentMultiplySpacing++;
                    }

                    if (GetParagraphContact().Item1)
                    {
                        y = pageSize.GetTop() - (pageSize.GetTop() - spacing * currentMultiplySpacing);
                        document.ShowTextAligned(GetParagraphContact().Item2, x, y, i, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);
                        currentMultiplySpacing++;
                    }

                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        y = pageSize.GetTop() - (pageSize.GetTop() - spacing * currentMultiplySpacing);
                        document.ShowTextAligned(GetParagraphNameAndEmail(), x, y, i, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);
                    }
                }

                document.Add(separator);

                document.Close();

                return stream.ToArray();
            }

            private static Table GetTableItems(IEnumerable<OrderProductInsert> products)
            {
                var listItems = new Table(5, true);
                string[] header = new string[5] { "Descrição", "Quantidade", "Valor Unitario", "Medida", "SubTotal" };

                for (int i = 0; i < header.Length; i++)
                    listItems.AddCell(new Cell(1, 1).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(header[i])));

                foreach (var product in products)
                {
                    Cell cellDescricao = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(product.Description!.ToUppercaseFirst()));
                    Cell cellQuantidade = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(product.Amount.ToString("N2")));
                    Cell cellValorUnitario = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(product.UnitaryValue.ToString("N2")));
                    Cell cellMedida = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(product.Measure.Description()));
                    Cell cellSubtotal = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((product.Amount * product.UnitaryValue).ToString("N2")));

                    listItems.AddCell(cellDescricao);
                    listItems.AddCell(cellQuantidade);
                    listItems.AddCell(cellValorUnitario);
                    listItems.AddCell(cellMedida);
                    listItems.AddCell(cellSubtotal);
                }
                return listItems;
            }
        }

        public class OrderToOrderResponse : ITypeConverter<Order, OrderResponse>
        {
            public OrderResponse Convert(Order source, OrderResponse destination, ResolutionContext context)
            {
                destination = new OrderResponse
                {
                    Id = source.Id,
                    Client = new ClientResponse { Id = source.ClientId },
                    Start = source.Start,
                    Finish = source.Finish,
                    Note = source.Note,
                    Amount = source.Amount,
                    Discount = source.Discount,
                    Identifier = source.Identifier,
                    State = source.State,
                    Inserted = source.Inserted
                };
                return destination;
            }
        }
    }
}