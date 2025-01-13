using OrderService.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OrderService.View.Order
{
    public class OrderSelectProductView : INotifyPropertyChanged
    {
        private double _amount;
        private string? _description;
        private MeasureType _measureType;
        private List<ItemResponseCustom>? _products;
        private ItemResponseCustom? _selectedProduct;
        private double _unitaryValue;

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

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public bool ExistProducts
        {
            get => Products != null && Products.Count() > 0;
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

        public List<ItemResponseCustom>? Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExistProducts));
            }
        }

        public ItemResponseCustom? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                Description = _selectedProduct!.Description;
                MeasureTypeSelected = _selectedProduct.Measure;
                UnitaryValue = _selectedProduct.UnitaryValue;

                OnPropertyChanged();
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(MeasureTypeSelected));
                OnPropertyChanged(nameof(UnitaryValue));
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