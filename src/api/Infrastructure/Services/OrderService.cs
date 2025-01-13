using AutoMapper;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using NPOI.Util;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Constants;
using System.Linq.Expressions;
using Kernel.Data.AutoMapper.Extensions;
using Kernel.Net.Http.Interfaces;
using Kernel.Toolkit.Extensions;
using OrderService.Infrastructure.Repositories.MongoDb;
using Kernel.Net.Http.User;
using OrderService.Infrastructure.AutoMapper.Mappers;

namespace OrderService.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _defaultFilename = $"OrdemDeServico_{DateTime.Now:dd-MM-yyyy_HH-mm}.pdf";
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly IUserAccessor _userAccessor;
        private readonly Expression<Func<Order, bool>> _defaultExpression;

        public OrderService(IMapper mapper, IUserAccessor userAccessor, IOrderRepository orderRepository, IProductService productService, IClientService clientService, IUserService userService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productService = productService;
            _clientService = clientService;
            _userService = userService;
            _userAccessor = userAccessor;
            _defaultExpression = x => x.UserEmail == userAccessor.GetUserEmail();
        }

        public OrderPdfResponse? AddOrder(OrderInsert payload)
        {
            var result = new OrderPdfResponse
            {
                FileName = _defaultFilename
            };

            //GET CLIENT
            var client = payload.ClientId.HasValue ? _clientService.GetClientById(payload.ClientId.Value) : null;

            //GET USER
            var user = _userService.GetUserDatabaseLogged();

            //LAST ORDER
            var order = _orderRepository.Find(x => true)!.LastOrDefault();

            var mapped = _mapper.Map<Order>(payload, GetMapperUserAndClientAndOrder(user, client, order));

            var created = false;

            if (client == null)
            {
                result.Pdf = mapped.Pdf!;
                return result;
            }
            else
            {
                created = _orderRepository.Insert(mapped);
            }

            //ADD UNEXISTING PRODUCTS TO DATABASE
            if (created)
                foreach (var newProduct in payload.Products!.Where(x => x.IsNew))
                    if (!_productService.GetProducts(x => true)!.Where(x => newProduct.Description!.Contains(x.Description!, StringComparison.CurrentCultureIgnoreCase) && x.Measure == newProduct.Measure && x.UnitaryValue == newProduct.UnitaryValue)!.Any())
                        _productService.AddProduct(new ProductInsert { Description = newProduct.Description!.ToUppercaseFirst(), Measure = newProduct.Measure, UnitaryValue = newProduct.UnitaryValue });

            string[] ids = { mapped.Id.ToString() };

            if (created)
                return GetOrderAsPdf(ids);

            throw new Exception("Não foi possivel gerar a order de serviço");
        }

        public bool DeleteOrder(Guid id) => _orderRepository.Delete(id);

        public IEnumerable<OrderResponse>? GetOrders(Expression<Func<Order, bool>> expression)
        {
            var data = _mapper.Map<IEnumerable<OrderResponse>>(_orderRepository.Find(_defaultExpression.And(expression)));
            foreach (var item in data)
                item.Client = _clientService.GetClientById(item.Client!.Id);
            return data;
        }

        public bool UpdateOrderState(string[] ids, OrderState state)
        {
            var result = new List<bool>();
            var idsAsGuid = ids.ToList().ConvertAll(x => x.ToGuid()).ToArray();
            foreach (var id in idsAsGuid)
            {
                var existing = _orderRepository.FindById(id).ValidateIsNull();
                existing!.State = state;
                existing.SetModifiedDate();
                result.Add(_orderRepository.Update(existing!));
            }

            return result.All(x => x);
        }

        public OrderPdfResponse? GetOrderAsPdf(string[] ids)
        {
            var result = new OrderPdfResponse
            {
                FileName = _defaultFilename
            };

            ids.ValidateIsNull();

            var idsAsGuid = ids.ToList().ConvertAll(x => x.ToGuid()).ToArray();

            var orders = _orderRepository.Find(x => idsAsGuid.Contains(x.Id));

            using var s = new MemoryStream();
            var pdfMerged = new PdfDocument(new PdfWriter(s));
            var merger = new PdfMerger(pdfMerged);

            foreach (var order in orders!)
            {
                var pdfDoc = new PdfDocument(new PdfReader(new ByteArrayInputStream(order.Pdf)));
                merger.Merge(pdfDoc, 1, pdfDoc.GetNumberOfPages());
                pdfDoc.Close();
            }
            pdfMerged.Close();
            result.Pdf = s.ToArray();
            return result;
        }

        public byte[]? GetOrderPdfById(Guid id)
        {
            var order = _orderRepository.FindById(id);
            if (order == null)
                return null;

            return order.Pdf;
        }

        public OrderDetailResponse? GetOrderDetailById(Guid id)
        {
            var order = _orderRepository.FindById(id);
            var orderMapped = _mapper.Map<OrderDetailResponse>(order);
            var request = _userAccessor.GetContext().Request;
            orderMapped.PdfUrl = $"https{Uri.SchemeDelimiter}osdigital.vintech.dev.br{request.PathBase.Add(request.Path.Value.Split("detail")[0])}document/{orderMapped.Id}";
            return orderMapped;
        }

        private static Action<IMappingOperationOptions> GetMapperUserAndClientAndOrder(User? user, ClientResponse? client, Order? order)
            => new Dictionary<string, object?>
            {
                {
                    AutomapperConstants.PARAM_USER,
                    user
                },
                {
                    AutomapperConstants.PARAM_CLIENT,
                    client
                },
                {
                    AutomapperConstants.PARAM_LAST_ORDER,
                    order
                },
            }.GetParams();
    }
}