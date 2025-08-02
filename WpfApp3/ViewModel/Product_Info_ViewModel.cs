// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    public class Product_Info_ViewModel : ViewModelBase
    {
        private Productlisting_Model _productlisting_simple_Model;
        private ObservableCollection<certification_actual_Model> _certification_actual_IEnum;
        private ObservableCollection<Myorgcertification_Model> _myorgcertification_IEnum;

        public ObservableCollection<certification_actual_Model> Certification_Actual_IEnum
        {
            get { return this._certification_actual_IEnum; }

            set
            {
                this._certification_actual_IEnum = value;
                this.RaisePropertyChanged(nameof(Certification_Actual_IEnum));
                OCPropertyChanged(this.Certification_Actual_IEnum);
            }
        }

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

        public Productlisting_Model Productlisting_simple_Model
        {
            get { return this._productlisting_simple_Model; }

            set
            {
                this._productlisting_simple_Model = value;
                this.RaisePropertyChanged(nameof(Productlisting_simple_Model));
                OCPropertyChanged(this.Productlisting_simple_Model);
            }
        }

        public Product_Info_ViewModel()
        {
            this._productlisting_simple_Model = MW_Lib.Productlisting_simple_Model_Proxy;
            this._productlisting_simple_Model.IM_productBitmapImage = IUOM_Lib.CustomBitmapImage(this._productlisting_simple_Model.IM_Image);
            this.RaisePropertyChanged(nameof(Productlisting_simple_Model));
            OCPropertyChanged(this.Productlisting_simple_Model);
            this.Populate__certification();
            this.Populate__myorgcertification();
        }

        public void Populate__certification()
        {
            using (var context = new ProductListingContext())
            {
                var result1 = from e in context.Certifications
                              join f in context.CertificationImages
                              on new { e.Code }
                              equals new { f.Code }
                              select new certification_actual_Model
                              {
                                  Code = e.Code,
                                  Image = f.Image
                              };

                this._certification_actual_IEnum = result1.ToObservableCollection<certification_actual_Model>();
                this.RaisePropertyChanged(nameof(Certification_Actual_IEnum));
                OCPropertyChanged(this.Certification_Actual_IEnum);
            }
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
