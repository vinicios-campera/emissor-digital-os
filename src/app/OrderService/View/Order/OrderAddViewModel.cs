using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderService.View.Order
{
    public class OrderAddViewModel : INotifyPropertyChanged
    {
        private string _clientId;
        private string _clientName;
        private double _discount;
        private string _finishIn;
        private string _note;
        private string _startIn;

        public OrderAddViewModel()
        {
            _clientId = string.Empty;
            _clientName = string.Empty;
            _discount = 0;
            _finishIn = DateTime.Now.Date.ToString("dd/MM/yyyy");
            _note = string.Empty;
            _startIn = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
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

        public string FinishIn
        {
            get => _finishIn;
            set
            {
                _finishIn = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public string StartIn
        {
            get => _startIn;
            set
            {
                _startIn = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}