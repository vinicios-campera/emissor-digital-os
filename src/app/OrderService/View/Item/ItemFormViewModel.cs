using OrderService.Models.Item;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderService.View.Item
{
    public class ItemFormViewModel : INotifyPropertyChanged
    {
        private readonly bool _isEditing;
        private string _id;
        private string _description;
        private MeasureType _measureType;
        private double _unitaryValue;

        public ItemFormViewModel()
        {
            _isEditing = false;
            _id = string.Empty;
            _description = string.Empty;
            _measureType = MeasureType.Unidade;
            _unitaryValue = 0;
        }

        public ItemFormViewModel(string id, string description, MeasureType measureType, double unitaryValue)
        {
            _isEditing = true;
            _id = id;
            _description = description;
            _measureType = measureType;
            _unitaryValue = unitaryValue;
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

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidated { get; set; }

        public Array MeasureTypes
        {
            get => Enum.GetValues(typeof(MeasureType));
        }

        public MeasureType MeasureTypeSelected
        {
            get => _measureType;
            set
            {
                _measureType = value;
                OnPropertyChanged();
            }
        }

        public double UnitaryValue
        {
            get => _unitaryValue;
            set
            {
                _unitaryValue = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}