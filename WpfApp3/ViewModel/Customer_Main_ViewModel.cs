// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Customer_Main_ViewModel : ViewModelBase
    {
        private ObservableCollection<Customermain_Model> _choosecustomer_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

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

        public Customer_Main_ViewModel()
        {
            this._choosecustomer_IEnum = new ObservableCollection<Customermain_Model>();
            this.Populate__choosecustomer();
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
