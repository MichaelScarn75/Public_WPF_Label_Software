// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

    public class Certification_Actual_ViewModel : ViewModelBase
    {
        private ObservableCollection<certification_actual_Model> _certification_actual_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

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

        public Certification_Actual_ViewModel()
        {
            this._certification_actual_IEnum = new ObservableCollection<certification_actual_Model>();
            this.Populate__certification();
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
                                  Code = f.Code,
                                  Description = e.Description,
                                  Image = f.Image,
                                  ImageActual = IUOM_Lib.CustomBitmapImage(f.Image),
                                  Id = f.Id,
                              };

                this._certification_actual_IEnum = result1.ToObservableCollection<certification_actual_Model>();

                this.RaisePropertyChanged(nameof(Certification_Actual_IEnum));
                OCPropertyChanged(this.Certification_Actual_IEnum);
            }
        }
        public void Filter__certification(certification_actual_Model temp)
        {
            using (var context = new ProductListingContext())
            {
                var result1 = from e in context.Certifications
                              join f in context.CertificationImages
                              on new { e.Code }
                              equals new { f.Code }
                              where e.Code == temp.Code
                              select new certification_actual_Model
                              {
                                  Code = e.Code,
                                  Description = e.Description,
                                  Image = f.Image,
                                  ImageActual = IUOM_Lib.CustomBitmapImage(f.Image),
                                  Id = f.Id,
                              };

                this._certification_actual_IEnum = result1.ToObservableCollection<certification_actual_Model>();

                this.RaisePropertyChanged(nameof(Certification_Actual_IEnum));
                OCPropertyChanged(this.Certification_Actual_IEnum);
            }
        }
    }
}
