using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Product_Info.xaml
    /// </summary>
    public partial class Product_Info : UserControl
    {
        public Product_Info()
        {
            InitializeComponent();
        }

        private void UpDown1_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this.UpDown1.Value < 0)
            {
                this.UpDown1.Value = 0;
            }
        }

        private void Product_Info_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Product_Info_ViewModel();
            var viewModel1 = (this.DataContext as Product_Info_ViewModel);

            if (viewModel1.Certification_Actual_IEnum.Count > 0)
            {
                MultiColumnDropDown1.SelectedIndex = 0;
            }

            if (viewModel1.Myorgcertification_IEnum.Count > 0)
            {
                MultiColumnDropDown2.SelectedIndex = 0;
            }

            foreach (var item in viewModel1.Certification_Actual_IEnum)
            {
                item.ImageActual = CustomBitmapImage(item.Image);
            }

            Image1.Source = (this.DataContext as Product_Info_ViewModel).Productlisting_simple_Model.IM_productBitmapImage;

            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            using (ProductListingContext context = new())
            {
                MW_Lib.Printinghistory_Model_Proxy = new PrintingHistory_Model()
                {
                    SP_SalesCode = GV_item.SP_SalesCode,
                    SP_ItemNo = GV_item.SP_ItemNo,
                    SP_UnitOfMeasureCode = GV_item.SP_UnitOfMeasureCode,
                    SP_UnitPrice = GV_item.SP_UnitPrice,
                    SP_StartingDate = GV_item.SP_StartingDate,
                    SP_EndingDate = GV_item.SP_EndingDate,
                    SP_ProductBarcode = GV_item.SP_ProductBarcode,
                    SP_BarcodeFormat = GV_item.SP_Barcode_Format,
                    SP_CustomerSku = GV_item.SP_CustomerSKU,
                    SP_Hidden = GV_item.SP_Hidden,
                    SP_EnglishLabelDescription = GV_item.SP_EnglishLabelDescription,
                    SP_MalayLabelDescription = GV_item.SP_MalayLabelDescription,
                    SP_ChineseLabelDescription = GV_item.SP_ChineseLabelDescription,
                    SP_LabelUnitOfMeasure = GV_item.SP_LabelUnitOfMeasure,
                    SP_LabelSize = GV_item.SP_LabelSize,
                    SP_Currencycode = GV_item.SP_CurrencyCode,
                    SP_WeightItem = GV_item.SP_WeightItem,
                    SP_WeightScale = GV_item.SP_WeightScale,
                    SD_SalesCode = GV_item.SD_SalesCode,
                    SD_ItemNo = GV_item.SD_ItemNo,
                    SD_UnitOfMeasureCode = GV_item.SD_UnitOfMeasureCode,
                    SD_LineDiscount = GV_item.SD_LineDiscount,
                    SD_StartingDate = GV_item.SD_StartingDate,
                    SD_EndingDate = GV_item.SD_EndingDate,
                    IT_Description = GV_item.IT_description,
                    IT_InventoryPostingGroup = GV_item.IT_inventorypostinggroup,
                    IT_Country = GV_item.IT_Country,
                    IM_Image = GV_item.IM_Image,
                    IUOM_QtyPerUnitOfMeasure = GV_item.IUOM_QtyPerUnitOfMeasure,
                    CUST_CustomerLabelCode = GV_item.CUST_CustomerLabelCode,
                    PH_PrintingDate = DateTime.Now,
                    PH_Price = "",
                    PH_ProductBarcode = "",
                    PH_WeighingScaleData = decimal.Zero,
                    PH_EncryptedQrdata = "",
                    PH_OrgCertText = context.Certifications
                        .Where(a => a.Code.Equals(MultiColumnDropDown1.SelectedValue.ToString()))
                        .Select(a => a.Description)
                        .First(),
                    PH_MyOrgCertText = context.MyorgCertifications
                        .Where(a => a.Code.Equals(MultiColumnDropDown2.SelectedValue.ToString()))
                        .Select(a => a.Description)
                        .First(),
                    PH_OrgCertText2 = MultiColumnDropDown1.SelectedValue.ToString(),
                    PH_MyOrgCertText2 = MultiColumnDropDown2.SelectedValue.ToString(),
                    PH_PricePerKgText = "",
                    PH_DateAsAlphabetText = QRProcessor.DateToAlphabet(DateTimePicker1.DateTime),
                    PH_IpAddress = IUOM_Lib.GetLocalIPAddress(),
                    PH_Location = "",
                };

                if (GV_item.SP_WeightItem == true && GV_item.SP_WeightScale == true) //Yes, Yes
                {
                    UpDown1.Visibility = Visibility.Hidden;
                    UpDown1.IsEnabled = false;
                    QuantityLabel.Visibility = Visibility.Hidden;
                    QuantityLabel.IsEnabled = false;
                    button_Print.Label = "Scale";
                }
            }
        }

        internal static BitmapImage CustomBitmapImage(byte[] value)
        {
            if (value == null)
            {
                return (BitmapImage)Application.Current.Resources["Img_Null_Image"];
            }

            using (var ms = new System.IO.MemoryStream(value))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void button_Print_Click(object sender, RoutedEventArgs e)
        {
            using (ProductListingContext context = new())
            {
                var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
                object item = MW_Lib.Printinghistory_Model_Proxy.Clone();
                MW_Lib.Printinghistory_Model_Proxy = item as PrintingHistory_Model;
                MW_Lib.Printinghistory_Model_Proxy.PH_PrintingDate = DateTime.Now;
                MW_Lib.Printinghistory_Model_Proxy.PH_OrgCertText = context.Certifications
                    .Where(a => a.Code.Equals(MultiColumnDropDown1.SelectedValue.ToString()))
                    .Select(a => a.Description)
                    .First();
                MW_Lib.Printinghistory_Model_Proxy.PH_MyOrgCertText = context.MyorgCertifications
                    .Where(a => a.Code.Equals(MultiColumnDropDown2.SelectedValue.ToString()))
                    .Select(a => a.Description)
                    .First();
                MW_Lib.Printinghistory_Model_Proxy.PH_OrgCertText2 = MultiColumnDropDown1.SelectedValue.ToString();
                MW_Lib.Printinghistory_Model_Proxy.PH_MyOrgCertText2 = MultiColumnDropDown2.SelectedValue.ToString();
                MW_Lib.Printinghistory_Model_Proxy.PH_DateAsAlphabetText = QRProcessor.DateToAlphabet(DateTimePicker1.DateTime);

                if (GV_item.SP_WeightItem == true && GV_item.SP_WeightScale == true) //Yes, Yes
                {
                    MW_Lib.ContentControl1_Proxy.Content = new Weigh_Form();
                    return;
                }
                else
                {
                    BitmapImage tempBitmapImage = IUOM_Lib.CustomBitmapImage(
                        context.CertificationImages
                        .Where(a => a.Code.Equals(MultiColumnDropDown1.SelectedValue.ToString()))
                        .Select(a => a.Image)
                        .First()
                        );

                    for (int i = 0; i < UpDown1.Value; i++)
                    {
                        LabelProcessor.MainProcessor(tempBitmapImage);
                        context.PrintingHistories.Add(MW_Lib.Printinghistory_Model_Proxy);
                        context.SaveChanges();
                    }
                }
            }
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MW_Lib.ContentControl1_Proxy.Content = new Product_Listing_Simple_View();
        }

        private void MultiColumnDropDown1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!MultiColumnDropDown1.IsDropDownOpen)
            {
                MultiColumnDropDown1.IsDropDownOpen = true;
            }
        }

        private void MultiColumnDropDown2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!MultiColumnDropDown2.IsDropDownOpen)
            {
                MultiColumnDropDown2.IsDropDownOpen = true;
            }
        }
    }
}
