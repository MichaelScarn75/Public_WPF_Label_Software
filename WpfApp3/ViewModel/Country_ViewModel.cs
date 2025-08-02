// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Country_ViewModel : ViewModelBase
    {
        private ObservableCollection<Country_Model> _country_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Country_Model> Country_IEnum
        {
            get { return this._country_IEnum; }

            set
            {
                this._country_IEnum = value;
                this.RaisePropertyChanged(nameof(Country_IEnum));
                OCPropertyChanged(this.Country_IEnum);
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

        public Country_ViewModel()
        {
            this._country_IEnum = new ObservableCollection<Country_Model>();
            this.Populate__country();
        }

        public void Populate__country()
        {
            using (var context = new ProductListingContext())
            {
                this._country_IEnum = context.Countries.ToObservableCollection<Country_Model>();
                this.RaisePropertyChanged(nameof(Country_IEnum));
                OCPropertyChanged(this.Country_IEnum);
            }
        }
    }
}
