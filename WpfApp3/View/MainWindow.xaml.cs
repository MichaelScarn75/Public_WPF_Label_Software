namespace WpfApp3.View
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using WpfApp3.SharedLib;
    using WpfApp3.ViewModel;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal UserControl newUserControlWindow = new UserControl();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MW_Lib.MainWindow_ViewModel_Proxy = this.DataContext as MainWindow_ViewModel;
            MW_Lib.ContentControl1_Proxy = this.ContentControl1;
            MW_Lib.User_Configuration_ViewModel_Proxy = new User_Configuration_ViewModel();
            MW_Lib.MainWindow_ViewModel_Proxy.DisableAll();
        }

        private void RibbonTab_Home_New_New_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MW_Lib.MainWindow_ViewModel_Proxy.AddNew();
                switch (ContentControl1.Content.GetType().Name)
                {
                    case "Certification":
                        Certification_Actual_SharedLib.New();
                        break;
                    case "Choose Customer":
                        Customer_Main_SharedLib.New();
                        break;
                    case "Country":
                        Country_SharedLib.New();
                        break;
                    case "Currencies":
                        Currencies_SharedLib.New();
                        break;
                    case "Customer Branch":
                        Customer_Branch_SharedLib.New();
                        break;
                    case "Inventory Posting Group":
                        InventoryPostingGroup_SharedLib.New();
                        break;
                    case "Item Units Of Measure":
                        Item_Units_Of_Measure_SharedLib.New();
                        break;
                    case "Special Discount":
                        Special_Discount_SharedLib.New();
                        break;
                    case "Special Price":
                        Special_Price_SharedLib.New();
                        break;
                    case "Units Of Measure":
                        Units_Of_Measure_SharedLib.New();
                        break;
                    case "Items":
                        Item_SharedLib.New();
                        break;
                    case "MyOrg Certificate":
                        MyOrg_Certification_SharedLib.New();
                        break;
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_New_New_Click error " + ex.Message);
            }
        }

        private void RibbonTab_Home_Manage_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MW_Lib.MainWindow_ViewModel_Proxy.BeginEdit();

                switch (ContentControl1.Content.GetType().Name)
                {
                    case "Certification":
                        Certification_Actual_SharedLib.Edit();
                        break;
                    case "Choose Customer":
                        Customer_Main_SharedLib.Edit();
                        break;
                    case "Country":
                        Country_SharedLib.Edit();
                        break;
                    case "Currencies":
                        Currencies_SharedLib.Edit();
                        break;
                    case "Customer Branch":
                        Customer_Branch_SharedLib.Edit();
                        break;
                    case "Inventory Posting Group":
                        InventoryPostingGroup_SharedLib.Edit();
                        break;
                    case "Item Units Of Measure":
                        Item_Units_Of_Measure_SharedLib.Edit();
                        break;
                    case "Special Discount":
                        Special_Discount_SharedLib.Edit();
                        break;
                    case "Special Price":
                        Special_Price_SharedLib.Edit();
                        break;
                    case "Units Of Measure":
                        Units_Of_Measure_SharedLib.Edit();
                        break;
                    case "Items":
                        Item_SharedLib.Edit();
                        break;
                    case "MyOrg Certificate":
                        MyOrg_Certification_SharedLib.Edit();
                        break;
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Manage_Edit_Click error " + ex.Message);
            }
        }

        private void RibbonTab_Home_Manage_View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MW_Lib.MainWindow_ViewModel_Proxy.View();

                switch (ContentControl1.Content.GetType().Name)
                {
                    case "Certification":
                        Certification_Actual_SharedLib.View();
                        break;
                    case "Choose Customer":
                        Customer_Main_SharedLib.View();
                        break;
                    case "Country":
                        Country_SharedLib.View();
                        break;
                    case "Currencies":
                        Currencies_SharedLib.View();
                        break;
                    case "Customer Branch":
                        Customer_Branch_SharedLib.View();
                        break;
                    case "Inventory Posting Group":
                        InventoryPostingGroup_SharedLib.View();
                        break;
                    case "Item Units Of Measure":
                        Item_Units_Of_Measure_SharedLib.View();
                        break;
                    case "Special Discount":
                        Special_Discount_SharedLib.View();
                        break;
                    case "Special Price":
                        Special_Price_SharedLib.View();
                        break;
                    case "Units Of Measure":
                        Units_Of_Measure_SharedLib.View();
                        break;
                    case "Items":
                        Item_SharedLib.View();
                        break;
                    case "MyOrg Certificate":
                        MyOrg_Certification_SharedLib.View();
                        break;
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Manage_View_Click error " + ex.Message);
            }
        }

        private void RibbonTab_Home_Manage_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (ContentControl1.Content.GetType().Name)
                {
                    case "Certification":
                        Certification_Actual_SharedLib.Delete();
                        break;
                    case "Choose Customer":
                        Customer_Main_SharedLib.Delete();
                        break;
                    case "Country":
                        Country_SharedLib.Delete();
                        break;
                    case "Currencies":
                        Currencies_SharedLib.Delete();
                        break;
                    case "Customer Branch":
                        Customer_Branch_SharedLib.Delete();
                        break;
                    case "Inventory Posting Group":
                        InventoryPostingGroup_SharedLib.Delete();
                        break;
                    case "Item Units Of Measure":
                        Item_Units_Of_Measure_SharedLib.Delete();
                        break;
                    case "Special Discount":
                        Special_Discount_SharedLib.Delete();
                        break;
                    case "Special Price":
                        Special_Price_SharedLib.Delete();
                        break;
                    case "Units Of Measure":
                        Units_Of_Measure_SharedLib.Delete();
                        break;
                    case "Items":
                        Item_SharedLib.Delete();
                        break;
                    case "MyOrg Certificate":
                        MyOrg_Certification_SharedLib.Delete();
                        break;
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Manage_Delete_Click error " + ex.Message);
            }
        }

        private void RibbonTab_Home_Page_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result1 = MessageBox.Show("ClearFilter the page?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result1 == MessageBoxResult.OK)
                {
                    switch (ContentControl1.Content.GetType().Name)
                    {
                        case "Certification":
                            Certification_Actual_SharedLib.Refresh();
                            break;
                        case "Certification Image":
                            Certification_Image_SharedLib.Refresh();
                            break;
                        case "Choose Customer":
                            Customer_Main_SharedLib.Refresh();
                            break;
                        case "Country":
                            Country_SharedLib.Refresh();
                            break;
                        case "Currencies":
                            Currencies_SharedLib.Refresh();
                            break;
                        case "Customer Branch":
                            Customer_Branch_SharedLib.Refresh();
                            break;
                        case "Inventory Posting Group":
                            InventoryPostingGroup_SharedLib.Refresh();
                            break;
                        case "Item Image":
                            Item_Image_Actual_SharedLib.Refresh();
                            break;
                        case "Item Units Of Measure":
                            Item_Units_Of_Measure_SharedLib.Refresh();
                            break;
                        case "Special Discount":
                            Special_Discount_SharedLib.Refresh();
                            break;
                        case "Special Price":
                            Special_Price_SharedLib.Refresh();
                            break;
                        case "Units Of Measure":
                            Units_Of_Measure_SharedLib.Refresh();
                            break;
                        case "Items":
                            Item_SharedLib.Refresh();
                            break;
                        case "MyOrg Certificate":
                            MyOrg_Certification_SharedLib.Refresh();
                            break;
                    }
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Page_Refresh_Click error: " + ex.Message);
            }
        }

        private void RibbonTab_Home_Page_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (ContentControl1.Content.GetType().Name)
                {
                    case "Certificate":
                        Certification_Actual_SharedLib.ClearFilter((ContentControl1.Content as Certification).Detailed_Filter_Children_Grid);
                        break;
                    case "Certification Image":
                        Certification_Image_SharedLib.ClearFilter((ContentControl1.Content as Certification_Image).Detailed_Filter_Children_Grid);
                        break;
                    case "Choose_Customer":
                        Customer_Main_SharedLib.ClearFilter((ContentControl1.Content as Customer_Main).Detailed_Filter_Children_Grid);
                        break;
                    case "Country":
                        Country_SharedLib.ClearFilter((ContentControl1.Content as Country).Detailed_Filter_Children_Grid);
                        break;
                    case "Currencies":
                        Currencies_SharedLib.ClearFilter((ContentControl1.Content as Currencies).Detailed_Filter_Children_Grid);
                        break;
                    case "Customer_Branch":
                        Customer_Branch_SharedLib.ClearFilter((ContentControl1.Content as Customer_Branch).Detailed_Filter_Children_Grid);
                        break;
                    case "InventoryPostingGroup":
                        InventoryPostingGroup_SharedLib.ClearFilter((ContentControl1.Content as InventoryPostingGroup).Detailed_Filter_Children_Grid);
                        break;
                    case "Item Image":
                        Item_Image_Actual_SharedLib.ClearFilter((ContentControl1.Content as Item).Detailed_Filter_Children_Grid);
                        break;
                    case "Item_Units_Of_Measure":
                        Item_Units_Of_Measure_SharedLib.ClearFilter((ContentControl1.Content as Item_Units_Of_Measure).Detailed_Filter_Children_Grid);
                        break;
                    case "Special_Discount":
                        Special_Discount_SharedLib.ClearFilter((ContentControl1.Content as Special_Discount).Detailed_Filter_Children_Grid);
                        break;
                    case "Special_Price":
                        Special_Price_SharedLib.ClearFilter((ContentControl1.Content as Special_Price).Detailed_Filter_Children_Grid);
                        break;
                    case "Units_Of_Measure":
                        Units_Of_Measure_SharedLib.ClearFilter((ContentControl1.Content as Units_Of_Measure).Detailed_Filter_Children_Grid);
                        break;
                    case "Items":
                        Item_SharedLib.ClearFilter((ContentControl1.Content as Item).Detailed_Filter_Children_Grid);
                        break;
                    case "MyOrg Certificate":
                        MyOrg_Certification_SharedLib.ClearFilter((ContentControl1.Content as Item).Detailed_Filter_Children_Grid);
                        break;
                }
                this.ContentControl1.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Page_ClearFilter_Click error " + ex.Message);
            }
        }

        private void SfTreeView_ItemTapped(object sender, Syncfusion.UI.Xaml.TreeView.ItemTappedEventArgs e)
        {
            MW_Lib.MainWindow_ViewModel_Proxy.EnableAll();
            switch (e.Node.Content.ToString())
            {
                case "Certificate":
                    this.ContentControl1.Content = new View.Certification();
                    break;
                case "Certificate Image":
                    this.ContentControl1.Content = new View.Certification_Image();
                    MW_Lib.MainWindow_ViewModel_Proxy.DisableCRUD();
                    break;
                case "Choose Customer":
                    this.ContentControl1.Content = new View.Customer_Main();
                    break;
                case "Country":
                    this.ContentControl1.Content = new View.Country();
                    break;
                case "Currencies":
                    this.ContentControl1.Content = new View.Currencies();
                    break;
                case "Customer Branch":
                    this.ContentControl1.Content = new View.Customer_Branch();
                    break;
                case "Print Label":
                    this.ContentControl1.Content = new View.Customer_To_Print();
                    break;
                case "Inventory Posting Group":
                    this.ContentControl1.Content = new View.InventoryPostingGroup();
                    break;
                case "Item Image":
                    this.ContentControl1.Content = new View.Item_Image();
                    MW_Lib.MainWindow_ViewModel_Proxy.DisableCRUD();
                    break;
                case "Item Units Of Measure":
                    this.ContentControl1.Content = new View.Item_Units_Of_Measure();
                    break;
                case "Special Discount":
                    this.ContentControl1.Content = new View.Special_Discount();
                    break;
                case "Special Price":
                    this.ContentControl1.Content = new View.Special_Price();
                    break;
                case "Units Of Measure":
                    this.ContentControl1.Content = new View.Units_Of_Measure();
                    break;
                case "Product Listing Simple View":
                    this.ContentControl1.Content = new View.Product_Listing_Simple_View();
                    break;
                case "Items":
                    this.ContentControl1.Content = new View.Item();
                    break;
                case "MyOrg Certificate":
                    this.ContentControl1.Content = new View.MyOrg_Certification();
                    break;
                case "User Configuration":
                    this.ContentControl1.Content = new View.User_Configuration();
                    break;
            }
            this.ContentControl1.Focus();
        }

        private void Ribbon_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
