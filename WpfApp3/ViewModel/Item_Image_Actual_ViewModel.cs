// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

    public class Item_Image_Actual_ViewModel : ViewModelBase
    {
        private ObservableCollection<item_image_actual_Model> _item_image_IEnum;

        public ObservableCollection<item_image_actual_Model> Item_Image_Actual_IEnum
        {
            get { return this._item_image_IEnum; }

            set
            {
                this._item_image_IEnum = value;
                this.RaisePropertyChanged(nameof(Item_Image_Actual_IEnum));
                OCPropertyChanged(this.Item_Image_Actual_IEnum);
            }
        }

        public Item_Image_Actual_ViewModel()
        {
            this._item_image_IEnum = new ObservableCollection<item_image_actual_Model>();
            this.Populate__item_image_actual();
        }

        public void Populate__item_image_actual()
        {
            using (var context = new ProductListingContext())
            {
                var result1 = context.ItemImages.ToObservableCollection<item_image_Model>();

                foreach (var item in result1)
                {
                    this._item_image_IEnum.Add(new item_image_actual_Model()
                    {
                        Code = item.Code,
                        Image = item.Image,
                        ImageActual = IUOM_Lib.CustomBitmapImage(item.Image)
                    });
                }

                this.RaisePropertyChanged(nameof(Item_Image_Actual_IEnum));
                OCPropertyChanged(this.Item_Image_Actual_IEnum);
            }
        }

        public void Filter__itemimageactual(item_image_actual_Model temp)
        {
            using (var context = new ProductListingContext())
            {
                var result1 = from e in context.ItemImages
                              where e.Code == temp.Code
                              select new item_image_actual_Model
                              {
                                  Code = e.Code,
                                  Image = e.Image,
                                  ImageActual = IUOM_Lib.CustomBitmapImage(e.Image)
                              };

                this._item_image_IEnum = result1.ToObservableCollection<item_image_actual_Model>();

                this.RaisePropertyChanged(nameof(Item_Image_Actual_IEnum));
                OCPropertyChanged(this.Item_Image_Actual_IEnum);
            }
        }
    }
}
