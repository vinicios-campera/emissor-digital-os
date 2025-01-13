using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderService.View.Order
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private string? _clientName;
        private string? _id;
        private int _identifier;
        private string? _note;
        private bool _pay;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged();
            }
        }

        public string? Id
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

        public string? Note
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

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}