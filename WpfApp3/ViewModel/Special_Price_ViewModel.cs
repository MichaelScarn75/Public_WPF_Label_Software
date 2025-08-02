// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Special_Price_ViewModel : ViewModelBase
    {
        private ObservableCollection<Specialprice_Model> _specialprice_IEnum;
        private ObservableCollection<Itemunitsofmeasure_Model> _itemunitsofmeasure_IEnum;
        private ObservableCollection<Unitsofmeasure_Model> _unitofMeasurecode_IEnum;
        private ObservableCollection<Currencies_Model> _currencies_IEnum;
        private ObservableCollection<Customermain_Model> _choosecustomer_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Specialprice_Model> SpecialPrice_IEnum
        {
            get { return this._specialprice_IEnum; }

            set
            {
                this._specialprice_IEnum = value;
                this.RaisePropertyChanged(nameof(SpecialPrice_IEnum));
                OCPropertyChanged(this.SpecialPrice_IEnum);
            }
        }

        public ObservableCollection<Itemunitsofmeasure_Model> Itemunitsofmeasure_IEnum
        {
            get { return this._itemunitsofmeasure_IEnum; }

            set
            {
                this._itemunitsofmeasure_IEnum = value;
                this.RaisePropertyChanged(nameof(Itemunitsofmeasure_IEnum));
                OCPropertyChanged(this.Itemunitsofmeasure_IEnum);
            }
        }

        public ObservableCollection<Unitsofmeasure_Model> UnitofMeasurecode_IEnum
        {
            get { return this._unitofMeasurecode_IEnum; }

            set
            {
                this._unitofMeasurecode_IEnum = value;
                this.RaisePropertyChanged(nameof(UnitofMeasurecode_IEnum));
                OCPropertyChanged(this.UnitofMeasurecode_IEnum);
            }
        }
        public ObservableCollection<Currencies_Model> Currencies_IEnum
        {
            get { return this._currencies_IEnum; }

            set
            {
                this._currencies_IEnum = value;
                this.RaisePropertyChanged(nameof(Currencies_IEnum));
                OCPropertyChanged(this.Currencies_IEnum);
            }
        }
        public ObservableCollection<Customermain_Model> Choosecustomer_IEnum
        {
            get { return this._choosecustomer_IEnum; }

            set
            {
                this._choosecustomer_IEnum = value;
                this.RaisePropertyChanged(nameof(Choosecustomer_IEnum));
                OCPropertyChanged(this.Choosecustomer_IEnum);
            }
        }


        public Visibility ErrorTextBorder1_Visibility
        {
            get { return this._errortextborder1_Visibility; }

            set
            {
                this._errortextborder1_Visibility = value;
                this.RaisePropertyChanged(nameof(ErrorTextBorder1_Visibility));
            }
        }

        public string ErrorTextBox1_Text
        {
            get { return this._errortextbox1_Text; }

            set
            {
                this._errortextbox1_Text = value;
                this.RaisePropertyChanged(nameof(ErrorTextBox1_Text));
            }
        }

        public Special_Price_ViewModel()
        {
            this._specialprice_IEnum = new ObservableCollection<Specialprice_Model>();
            this.Populate__specialprice();
            this.Populate__itemunitsofmeasure();
            this.Populate__unitsofmeasure();
            this.Populate__currencies();
            this.Populate__choosecustomer();
        }

        public void Populate__specialprice()
        {
            using (var context = new ProductListingContext())
            {
                this._specialprice_IEnum = context.SpecialPrices.ToObservableCollection<Specialprice_Model>();
                this.RaisePropertyChanged(nameof(SpecialPrice_IEnum));
                OCPropertyChanged(this.SpecialPrice_IEnum);
            }
        }
        public void Populate__itemunitsofmeasure()
        {
            using (var context = new ProductListingContext())
            {
                this._itemunitsofmeasure_IEnum = context.ItemUnitsOfMeasures.ToObservableCollection<Itemunitsofmeasure_Model>();
                this.RaisePropertyChanged(nameof(Itemunitsofmeasure_IEnum));
                OCPropertyChanged(this.Itemunitsofmeasure_IEnum);
            }
        }
        public void Populate__unitsofmeasure()
        {
            using (var context = new ProductListingContext())
            {
                this._unitofMeasurecode_IEnum = context.UnitsOfMeasures.ToObservableCollection<Unitsofmeasure_Model>();
                this.RaisePropertyChanged(nameof(UnitofMeasurecode_IEnum));
                OCPropertyChanged(this.UnitofMeasurecode_IEnum);
            }
        }
        public void Populate__currencies()
        {
            using (var context = new ProductListingContext())
            {
                this._currencies_IEnum = context.Currencies.ToObservableCollection<Currencies_Model>();
                this.RaisePropertyChanged(nameof(Currencies_IEnum));
                OCPropertyChanged(this.Currencies_IEnum);
            }
        }
        public void Populate__choosecustomer()
        {
            using (var context = new ProductListingContext())
            {
                this._choosecustomer_IEnum = context.Customers.ToObservableCollection<Customermain_Model>();
                this.RaisePropertyChanged(nameof(Choosecustomer_IEnum));
                OCPropertyChanged(this.Choosecustomer_IEnum);
            }
        }
    }
}
