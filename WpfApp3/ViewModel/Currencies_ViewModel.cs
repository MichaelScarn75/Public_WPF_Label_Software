// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Currencies_ViewModel : ViewModelBase
    {
        private ObservableCollection<Currencies_Model> _currencies_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

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

        public Currencies_ViewModel()
        {
            this._currencies_IEnum = new ObservableCollection<Currencies_Model>();
            this.Populate__currencies();
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
    }
}
