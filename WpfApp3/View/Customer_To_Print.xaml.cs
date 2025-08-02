using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.ScrollAxis;
using WpfApp3.FilterManager;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using CC_Lib = WpfApp3.SharedLib.Customer_Main_SharedLib;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Customer_Selection.xaml
    /// </summary>
    public partial class Customer_To_Print : UserControl
    {
        public Customer_To_Print()
        {
            InitializeComponent();
        }

        private void Customer_To_Print_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Customer_Main_ViewModel();
            CC_Lib.choosecustomerViewModel = this.DataContext as Customer_Main_ViewModel;
            CC_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CC_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            CC_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        private void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MW_Lib.Choosecustomer_Model_Proxy = SfDataGrid1.SelectedItem as Customermain_Model;
                MW_Lib.ContentControl1_Proxy.Content = new PacketOrScale();
            }
        }

        private void SimpleFilterSearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer_Main_FilterManager filtermanager = new Customer_Main_FilterManager();
                filtermanager.Choose_CustomerViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Main_ViewModel;
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

        private void SimpleFilterTextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Customer_Main_FilterManager filtermanager = new Customer_Main_FilterManager();
                    filtermanager.Choose_CustomerViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Main_ViewModel;
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

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CC_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
            }
        }

        private void SfDataGrid1_CellTapped(object sender, GridCellTappedEventArgs e)
        {
            MW_Lib.Choosecustomer_Model_Proxy = e.Record as Customermain_Model;
            MW_Lib.ContentControl1_Proxy.Content = new PacketOrScale();
        }
    }
}
