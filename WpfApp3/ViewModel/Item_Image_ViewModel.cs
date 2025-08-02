// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Item_Image_ViewModel : ViewModelBase
    {
        private ObservableCollection<item_image_actual_Model> _item_image_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<item_image_actual_Model> Item_Image_IEnum
        {
            get { return this._item_image_IEnum; }

            set
            {
                this._item_image_IEnum = value;
                this.RaisePropertyChanged(nameof(Item_Image_IEnum));
                OCPropertyChanged(this.Item_Image_IEnum);
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

        public Item_Image_ViewModel()
        {
            this._item_image_IEnum = new ObservableCollection<item_image_actual_Model>();
            this.Populate__item_image();
        }

        public void Populate__item_image()
        {
            using (var context = new ProductListingContext())
            {
                var result1 = context.ItemImages.ToObservableCollection<item_image_Model>();

                foreach (var item in result1)
                {
                    this._item_image_IEnum.Add(new item_image_actual_Model()
                    {
                        Code = item.Code,
                        Image = item.Image
                    });
                }

                this.RaisePropertyChanged(nameof(Item_Image_IEnum));
                OCPropertyChanged(this.Item_Image_IEnum);
            }
        }
    }
}
