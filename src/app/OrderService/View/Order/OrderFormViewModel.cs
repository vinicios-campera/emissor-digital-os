using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderService.View.Order
{
    public class OrderFormViewModel : INotifyPropertyChanged
    {
        private double _amount;
        private string _client;
        private double _discount;
        private string _id;
        private int _identifier;
        private string _note;
        private bool _pay;
        private bool _pdfSend;
        private double _totalLiquid;

        public OrderFormViewModel()
        {
            _id = string.Empty;
            _pay = false;
            _identifier = 0;
            _client = string.Empty;
            _amount = 0;
            _discount = 0;
            _totalLiquid = 0;
            _note = string.Empty;
            _pdfSend = false;
        }

        public OrderFormViewModel(string id, bool pay, int identifier, string client, double amount, double discount, double totalLiquid, string note, bool pdfSend)
        {
            _id = id;
            _pay = pay;
            _identifier = identifier;
            _client = client;
            _amount = amount;
            _discount = discount;
            _totalLiquid = totalLiquid;
            _note = note;
            _pdfSend = pdfSend;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public string Client
        {
            get => _client;
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }

        public double Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged();
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public int Identifier
        {
            get => _identifier;
            set
            {
                _identifier = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidated { get; set; }

        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public bool Pay
        {
            get => _pay;
            set
            {
                _pay = value;
                OnPropertyChanged();
            }
        }

        public bool PdfSend
        {
            get => _pdfSend;
            set
            {
                _pdfSend = value;
                OnPropertyChanged();
            }
        }

        public double TotalLiquid
        {
            get => _totalLiquid;
            set
            {
                _totalLiquid = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}