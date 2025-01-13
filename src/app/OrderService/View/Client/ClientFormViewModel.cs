using OrderService.Models.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderService.View.Client
{
    public class ClientFormViewModel : INotifyPropertyChanged
    {
        private readonly bool _isEditing;
        private string _cep;
        private ClientType _clientType;
        private string _document;
        private string _id;
        private string _name;
        private string _cellphone;
        private string _city;
        private string _state;

        public ClientFormViewModel()
        {
            _isEditing = false;
            _cep = string.Empty;
            _clientType = ClientType.Fisica;
            _document = string.Empty;
            _id = string.Empty;
            _name = string.Empty;
            _cellphone = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
        }

        public ClientFormViewModel(string cep, ClientType clientType, string document, string id, string name, string cellphone, string city, string state)
        {
            _isEditing = true;
            _cep = cep;
            _clientType = clientType;
            _document = document;
            _id = id;
            _name = name;
            _cellphone = cellphone;
            _city = city;
            _state = state;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsEditing => _isEditing;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Cep
        {
            get => _cep;
            set
            {
                _cep = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public ClientType ClientType
        {
            get => _clientType;
            set
            {
                _clientType = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(NameLabel));
                OnPropertyChanged(nameof(DocumentLabel));
            }
        }

        public string Document
        {
            get => _document;
            set
            {
                _document = value;
                OnPropertyChanged();
            }
        }

        public string DocumentLabel
        {
            get
            {
                return ClientType switch
                {
                    ClientType.Fisica => "CPF",
                    ClientType.Juridica => "CNPJ",
                    _ => "CPF",
                };
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string NameLabel
        {
            get
            {
                return ClientType switch
                {
                    ClientType.Fisica => "Nome completo",
                    ClientType.Juridica => "Razão social",
                    _ => "Nome completo",
                };
            }
        }

        public string Cellphone
        {
            get => _cellphone;
            set
            {
                _cellphone = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidated { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}