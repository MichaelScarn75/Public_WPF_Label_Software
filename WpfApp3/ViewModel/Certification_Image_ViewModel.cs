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

    public class Certification_Image_ViewModel : ViewModelBase
    {
        private ObservableCollection<certification_image_Model> _certification_image_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<certification_image_Model> Certification_Image_IEnum
        {
            get { return this._certification_image_IEnum; }

            set
            {
                this._certification_image_IEnum = value;
                this.RaisePropertyChanged(nameof(Certification_Image_IEnum));
                OCPropertyChanged(this.Certification_Image_IEnum);
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

        public Certification_Image_ViewModel()
        {
            this._certification_image_IEnum = new ObservableCollection<certification_image_Model>();
            this.Populate__certification_image();
        }

        public void Populate__certification_image()
        {
            using (var context = new ProductListingContext())
            {
                var result1 = from e in context.CertificationImages
                              select new certification_image_Model
                              {
                                  Code = e.Code,
                                  Image = e.Image,
                                  ImageActual = IUOM_Lib.CustomBitmapImage(e.Image),
                                  Id = e.Id,
                              };

                this._certification_image_IEnum = result1.ToObservableCollection<certification_image_Model>();

                this.RaisePropertyChanged(nameof(Certification_Image_IEnum));
                OCPropertyChanged(this.Certification_Image_IEnum);
            }
        }
        public void Filter__certification_image(certification_image_Model temp)
        {
            using (var context = new ProductListingContext())
            {
                var result1 = from e in context.CertificationImages
                              select new certification_image_Model
                              {
                                  Code = e.Code,
                                  Image = e.Image,
                                  ImageActual = IUOM_Lib.CustomBitmapImage(e.Image),
                                  Id = e.Id,
                              };

                this._certification_image_IEnum = result1.ToObservableCollection<certification_image_Model>();

                this.RaisePropertyChanged(nameof(Certification_Image_IEnum));
                OCPropertyChanged(this.Certification_Image_IEnum);
            }
        }
    }
}
