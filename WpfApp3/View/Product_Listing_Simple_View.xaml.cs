using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.ScrollAxis;
using WpfApp3.FilterManager;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;
using PL_Lib = WpfApp3.SharedLib.Product_Listing_Simple_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Product_Listing_Simple_View.xaml
    /// </summary>
    public partial class Product_Listing_Simple_View : UserControl
    {
        public Product_Listing_Simple_View()
        {
            InitializeComponent();
        }
        private void Product_Listing_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Product_Listing_ViewModel();
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_New_Enabled = true;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_Enabled = true;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_Edit_Enabled = false;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Page_Enabled = true;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Setups_Printing_Setup_Enabled = true;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Setups_Product_Management_Enabled = true;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled = false;
            MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Setups_CustomerManagement_Enabled = true;
            PL_Lib.productlistingViewModel = this.DataContext as Product_Listing_ViewModel;
            PL_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            PL_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            PL_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            PL_Lib.ErrorTextBox1_Setter("hidden", string.Empty);

            foreach (var item in PL_Lib.productlistingViewModel.ProductListing_IEnum)
            {
                item.IM_productBitmapImage = ToBitmapImage(item.IM_Image);
            }
        }

        public static BitmapImage ToBitmapImage(byte[] _bytes)
        {
            if (_bytes == null)
            {
                return (BitmapImage)Application.Current.Resources["Img_Null_Image"];
            }

            BitmapImage img = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(_bytes))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = stream;
                img.EndInit();
            }

            return img;
        }

        private async void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_PreviewKeyDown error " + ex.Message);
            }
        }

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PL_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
            }
        }

        private void Expander1_Click(object sender, RoutedEventArgs e)
        {
            if (this.Main_Grid.RowDefinitions[1].Height.Value == 90.0)
            {
                this.Main_Grid.RowDefinitions[1].Height = new GridLength(this.Main_Grid.RowDefinitions[1].Height.Value + 175.0);
                this.Main_Grid.RowDefinitions[2].Height = new GridLength(this.Main_Grid.RowDefinitions[2].Height.Value - 250.0);
                this.Expander1.LargeIcon = (BitmapImage)Application.Current.Resources["Img_CollapseAll"];
                this.Expander1.SmallIcon = (BitmapImage)Application.Current.Resources["Img_CollapseAll"];
            }
            else
            {
                this.Main_Grid.RowDefinitions[1].Height = new GridLength(this.Main_Grid.RowDefinitions[1].Height.Value - 175.0);
                this.Main_Grid.RowDefinitions[2].Height = new GridLength(this.Main_Grid.RowDefinitions[2].Height.Value + 250.0);
                this.Expander1.LargeIcon = (BitmapImage)Application.Current.Resources["Img_ExpandAll"];
                this.Expander1.SmallIcon = (BitmapImage)Application.Current.Resources["Img_ExpandAll"];
            }
        }

        private void AddFilter_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Special_Price_FilterManager filtermanager = new Special_Price_FilterManager();
                filtermanager.specialpriceViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Price_ViewModel;
                filtermanager.SfDataGrid1 = this.SfDataGrid1;
                filtermanager.AddFilter(this.Detailed_Filter_Children_Grid);
                this.ScrollViewer_Filter.ScrollToBottom();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddFilter_Button_Click error " + ex.Message);
            }
        }

        private void SimpleFilterTextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Special_Price_FilterManager filtermanager = new Special_Price_FilterManager();
                    filtermanager.specialpriceViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Price_ViewModel;
                    filtermanager.SfDataGrid1 = this.SfDataGrid1;
                    filtermanager.FilterResults(0, new List<string>() { this.SimpleFilterComboBox.Text }, new List<string>() { this.SimpleFilterTextBox1.Text });
                    this.SfDataGrid1.BeginInit();
                    this.SfDataGrid1.MoveCurrentCell(new RowColumnIndex(1, 1), true);
                    this.SfDataGrid1.EndInit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SimpleFilterTextBox1_PreviewKeyDown error " + ex.Message);
            }
        }

        private void SimpleFilterSearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Special_Price_FilterManager filtermanager = new Special_Price_FilterManager();
                filtermanager.specialpriceViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Price_ViewModel;
                filtermanager.SfDataGrid1 = this.SfDataGrid1;
                filtermanager.FilterResults(0, new List<string>() { this.SimpleFilterComboBox.Text }, new List<string>() { this.SimpleFilterTextBox1.Text });
                this.SfDataGrid1.BeginInit();
                this.SfDataGrid1.MoveCurrentCell(new RowColumnIndex(1, 1), true);
                this.SfDataGrid1.EndInit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SimpleFilterSearchButton_Click error " + ex.Message);
            }
        }

        private void SfDataGrid1_CellTapped(object sender, GridCellTappedEventArgs e)
        {
            MW_Lib.Productlisting_simple_Model_Proxy = e.Record as Productlisting_Model;
            MW_Lib.ContentControl1_Proxy.Content = new Product_Info();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MW_Lib.ContentControl1_Proxy.Content = new PacketOrScale();
        }
    }
}
