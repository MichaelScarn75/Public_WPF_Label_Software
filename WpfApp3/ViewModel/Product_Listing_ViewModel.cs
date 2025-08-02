// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    public class Product_Listing_ViewModel : ViewModelBase
    {
        private ObservableCollection<Productlisting_Model> _productlisting_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Productlisting_Model> ProductListing_IEnum
        {
            get { return this._productlisting_IEnum; }

            set
            {
                this._productlisting_IEnum = value;
                this.RaisePropertyChanged(nameof(ProductListing_IEnum));
                OCPropertyChanged(this.ProductListing_IEnum);
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

        public Product_Listing_ViewModel()
        {
            this._productlisting_IEnum = new ObservableCollection<Productlisting_Model>();
            this.Populate__productlisting_simple_view();
        }

        public void Populate__productlisting_simple_view()
        {
            using (var context = new ProductListingContext())
            {
                var result1 = context.SpecialPrices
                                    .Where(a => a.StartingDate <= DateTime.Today.Date)
                                    .Where(a => a.EndingDate > DateTime.Today.Date)
                                    .Where(a => a.SalesCode.Equals(MW_Lib.Choosecustomer_Model_Proxy.CustomerId))
                                    .Where(a => a.WeightScale == MW_Lib.IsScale)
                                    .Where(a => a.Hidden.Equals(false))
                                    .GroupBy(b => new { b.SalesCode, b.ItemNo, b.UnitOfMeasureCode })
                                    .Select(c => new
                                    {
                                        c.Key.SalesCode,
                                        c.Key.ItemNo,
                                        c.Key.UnitOfMeasureCode,
                                        FilteredMaxStartingDate = c.Max(d => d.StartingDate),
                                        FilteredMaxEndingDate = c.Max(d => d.EndingDate)

                                    });


                var result2 = context.SpecialDiscounts
                                    .Where(a => a.StartingDate <= DateTime.Today.Date)
                                    .Where(a => a.EndingDate > DateTime.Today.Date)
                                    .GroupBy(b => new { b.SalesCode, b.ItemNo, b.UnitOfMeasureCode })
                                    .Select(c => new
                                    {
                                        c.Key.SalesCode,
                                        c.Key.ItemNo,
                                        c.Key.UnitOfMeasureCode,
                                        FilteredMaxStartingDate = c.Max(d => d.StartingDate),
                                        FilteredMaxEndingDate = c.Max(d => d.EndingDate)

                                    });

                var result3 = from e in context.SpecialPrices
                              join f in result1
                              on new { e.SalesCode, e.ItemNo, e.UnitOfMeasureCode, e.StartingDate, e.EndingDate }
                              equals new { f.SalesCode, f.ItemNo, f.UnitOfMeasureCode, StartingDate = f.FilteredMaxStartingDate, EndingDate = f.FilteredMaxEndingDate }
                              select e;

                var result4 = from e in context.SpecialDiscounts
                              join f in result2
                              on new { e.SalesCode, e.ItemNo, e.UnitOfMeasureCode, e.StartingDate, e.EndingDate }
                              equals new { f.SalesCode, f.ItemNo, f.UnitOfMeasureCode, StartingDate = f.FilteredMaxStartingDate, EndingDate = f.FilteredMaxEndingDate }
                              select e;

                var result5 = from e in result3
                              join f in result4
                              on new { e.SalesCode, e.ItemNo, e.UnitOfMeasureCode, e.StartingDate, e.EndingDate }
                              equals new { f.SalesCode, f.ItemNo, f.UnitOfMeasureCode, f.StartingDate, f.EndingDate }

                              join g in context.Items
                              on new { e.ItemNo }
                              equals new { g.ItemNo }

                              join h in context.ItemImages
                              on new { V = e.ItemNo }
                              equals new { V = h.Code }

                              join i in context.ItemUnitsOfMeasures
                              on new { e.UnitOfMeasureCode }
                              equals new { i.UnitOfMeasureCode }

                              join j in context.Customers
                              on new { V = e.SalesCode }
                              equals new { V = j.CustomerId }

                              where h.Image != null
                              orderby g.Description, g.Country
                              select new Productlisting_Model
                              {
                                  SP_SalesCode = e.SalesCode,
                                  SP_ItemNo = e.ItemNo,
                                  SP_UnitOfMeasureCode = e.UnitOfMeasureCode,
                                  SP_UnitPrice = e.UnitPrice,
                                  SP_StartingDate = e.StartingDate,
                                  SP_EndingDate = e.EndingDate,
                                  SP_ProductBarcode = e.ProductBarcode,
                                  SP_Barcode_Format = e.Barcode_Format,
                                  SP_CustomerSKU = e.CustomerSKU,
                                  SP_Hidden = e.Hidden,
                                  SP_EnglishLabelDescription = e.EnglishLabelDescription,
                                  SP_MalayLabelDescription = e.MalayLabelDescription,
                                  SP_ChineseLabelDescription = e.ChineseLabelDescription,
                                  SP_LabelUnitOfMeasure = e.LabelUnitOfMeasure,
                                  SP_LabelSize = e.LabelSize,
                                  SP_CurrencyCode = e.CurrencyCode,
                                  SP_WeightItem = e.WeightItem,
                                  SP_WeightScale = e.WeightScale,
                                  SD_SalesCode = f.SalesCode,
                                  SD_ItemNo = f.ItemNo,
                                  SD_UnitOfMeasureCode = f.UnitOfMeasureCode,
                                  SD_LineDiscount = f.LineDiscount,
                                  SD_StartingDate = f.StartingDate,
                                  SD_EndingDate = f.EndingDate,
                                  IT_description = g.Description,
                                  IT_inventorypostinggroup = g.InventoryPostingGroup,
                                  IT_Country = g.Country,
                                  IM_Image = h.Image,
                                  IUOM_QtyPerUnitOfMeasure = i.QtyPerUnitOfMeasure,
                                  CUST_CustomerLabelCode = j.CustomerLabelCode ?? "",
                              };

                if (result5.Count() == 0)
                {
                    Debug.WriteLine("< = zero");
                    result5 = from e in result3

                              join g in context.Items
                              on new { e.ItemNo }
                              equals new { g.ItemNo }

                              join h in context.ItemImages
                              on new { V = e.ItemNo }
                              equals new { V = h.Code }

                              join i in context.ItemUnitsOfMeasures
                              on new { e.UnitOfMeasureCode }
                              equals new { i.UnitOfMeasureCode }

                              join j in context.Customers
                              on new { V = e.SalesCode }
                              equals new { V = j.CustomerId }

                              where h.Image != null
                              orderby g.Description, g.Country
                              select new Productlisting_Model
                              {
                                  SP_SalesCode = e.SalesCode,
                                  SP_ItemNo = e.ItemNo,
                                  SP_UnitOfMeasureCode = e.UnitOfMeasureCode,
                                  SP_UnitPrice = e.UnitPrice,
                                  SP_StartingDate = e.StartingDate,
                                  SP_EndingDate = e.EndingDate,
                                  SP_ProductBarcode = e.ProductBarcode,
                                  SP_Barcode_Format = e.Barcode_Format,
                                  SP_CustomerSKU = e.CustomerSKU,
                                  SP_Hidden = e.Hidden,
                                  SP_EnglishLabelDescription = e.EnglishLabelDescription,
                                  SP_MalayLabelDescription = e.MalayLabelDescription,
                                  SP_ChineseLabelDescription = e.ChineseLabelDescription,
                                  SP_LabelUnitOfMeasure = e.LabelUnitOfMeasure,
                                  SP_LabelSize = e.LabelSize,
                                  SP_CurrencyCode = e.CurrencyCode,
                                  SP_WeightItem = e.WeightItem,
                                  SP_WeightScale = e.WeightScale,
                                  SD_SalesCode = e.SalesCode,
                                  SD_ItemNo = e.ItemNo,
                                  SD_UnitOfMeasureCode = e.UnitOfMeasureCode,
                                  SD_LineDiscount = 100,
                                  SD_StartingDate = e.StartingDate,
                                  SD_EndingDate = e.EndingDate,
                                  IT_description = g.Description,
                                  IT_inventorypostinggroup = g.InventoryPostingGroup,
                                  IT_Country = g.Country,
                                  IM_Image = h.Image,
                                  IUOM_QtyPerUnitOfMeasure = i.QtyPerUnitOfMeasure,
                                  CUST_CustomerLabelCode = j.CustomerLabelCode ?? "",
                              };
                }

                var result6 = result5.ToObservableCollection<Productlisting_Model>()
                    .DistinctBy(a => new { a.SP_SalesCode, a.SP_ItemNo, a.SP_UnitOfMeasureCode, a.SP_StartingDate });

                this._productlisting_IEnum = result6.ToObservableCollection<Productlisting_Model>();

                this.RaisePropertyChanged(nameof(ProductListing_IEnum));
                OCPropertyChanged(this.ProductListing_IEnum);
            }
        }

        /*
        public static byte[] ToByte()
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(
                (BitmapImage)Application.Current.Resources["Img_Null_Image"])
                );
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }
        */
    }
}
