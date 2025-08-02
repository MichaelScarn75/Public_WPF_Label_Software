// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class MyOrg_Certification_ViewModel : ViewModelBase
    {
        private ObservableCollection<Myorgcertification_Model> _myorgcertification_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Myorgcertification_Model> Myorgcertification_IEnum
        {
            get { return this._myorgcertification_IEnum; }

            set
            {
                this._myorgcertification_IEnum = value;
                this.RaisePropertyChanged(nameof(Myorgcertification_IEnum));
                OCPropertyChanged(this.Myorgcertification_IEnum);
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

        public MyOrg_Certification_ViewModel()
        {
            this._myorgcertification_IEnum = new ObservableCollection<Myorgcertification_Model>();
            this.Populate__myorgcertification();
        }

        public void Populate__myorgcertification()
        {
            using (var context = new ProductListingContext())
            {
                this._myorgcertification_IEnum = context.MyorgCertifications.ToObservableCollection<Myorgcertification_Model>();
                this.RaisePropertyChanged(nameof(Myorgcertification_IEnum));
                OCPropertyChanged(this.Myorgcertification_IEnum);
            }
        }
    }
}
