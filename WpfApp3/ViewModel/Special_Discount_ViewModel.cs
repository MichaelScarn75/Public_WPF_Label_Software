// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Special_Discount_ViewModel : ViewModelBase
    {
        private ObservableCollection<Specialdiscount_Model> _specialdiscount_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Specialdiscount_Model> SpecialDiscount_IEnum
        {
            get { return this._specialdiscount_IEnum; }

            set
            {
                this._specialdiscount_IEnum = value;
                this.RaisePropertyChanged(nameof(SpecialDiscount_IEnum));
                OCPropertyChanged(this.SpecialDiscount_IEnum);
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

        public Special_Discount_ViewModel()
        {
            this._specialdiscount_IEnum = new ObservableCollection<Specialdiscount_Model>();
            this.Populate__specialdiscount();
        }

        public void Populate__specialdiscount()
        {
            using (var context = new ProductListingContext())
            {
                this._specialdiscount_IEnum = context.SpecialDiscounts.ToObservableCollection<Specialdiscount_Model>();
                this.RaisePropertyChanged(nameof(SpecialDiscount_IEnum));
                OCPropertyChanged(this.SpecialDiscount_IEnum);
            }
        }
    }
}
