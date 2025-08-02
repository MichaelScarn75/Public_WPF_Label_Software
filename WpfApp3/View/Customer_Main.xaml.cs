// <copyright file="Choose_Customer.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.View
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using CC_Lib = WpfApp3.SharedLib.Customer_Main_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Choose_Customer.xaml.
    /// </summary>
    public partial class Customer_Main : UserControl
    {

        public Customer_Main()
        {
            this.InitializeComponent();
        }

        private void Customer_Main_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CC_Lib.choosecustomerViewModel = this.DataContext as Customer_Main_ViewModel;
            CC_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CC_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            CC_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            CC_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            CC_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                CC_Lib.Choosecustomer_dataTable_HasError = true;
                CC_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                CC_Lib.Choosecustomer_dataTable_HasError = false;
                CC_Lib.ErrorTextBox1_Setter("hidden", message);
            }
        }

        private void SfDataGrid1_CurrentCellBeginEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellBeginEditEventArgs e)
        {
            try
            {
                if (MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_View_Enabled == false)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellBeginEdit error " + ex.Message);
            }
        }

        private void SfDataGrid1_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellEndEdit error " + ex.Message);
            }
        }

        private void SfDataGrid1_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
        {
            try
            {
                int index = this.SfDataGrid1.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex;

                if (this.SfDataGrid1.IsAddNewIndex(index) ||
                    CC_Lib.PasteNewRowsProgrammatically == true ||
                    CC_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                CC_Lib.OldRowDataIndex = index - 1;
                CC_Lib.OldRowData = record as Customermain_Model;
                Customermain_Model item1 = context.Customers.Where(a => a.Id.Equals(CC_Lib.OldRowData.Id)).Single();
                var item2 = context.CustomerBranches.Where(a => a.CustomerId.Equals(CC_Lib.OldRowData.CustomerId));
                var item3 = context.SpecialDiscounts.Where(a => a.SalesCode.Equals(CC_Lib.OldRowData.CustomerId));
                var item4 = context.SpecialPrices.Where(a => a.SalesCode.Equals(CC_Lib.OldRowData.CustomerId));

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                if (e.Column.MappingName == nameof(CC_Lib.OldRowData.CustomerId))
                {
                    item1.CustomerId = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.CustomerId = e.NewValue.ToString());
                    item3.ToList().ForEach(a => a.SalesCode = e.NewValue.ToString());
                    item4.ToList().ForEach(a => a.SalesCode = e.NewValue.ToString());

                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                    // sql returns 1 or more results, old value != new value
                    else if (e.OldValue != e.NewValue &&
                        context.Customers.Where(i => i.CustomerId.Equals(item1.CustomerId)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.CustomerId);

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Description))
                {
                    item1.Description = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 40)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 40 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.CustomerLabelCode))
                {
                    item1.CustomerLabelCode = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 2)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 2 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Address1))
                {
                    item1.Address1 = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Address2))
                {
                    item1.Address2 = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Address3))
                {
                    item1.Address3 = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.ContactPerson))
                {
                    item1.ContactPerson = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.PostalCode))
                {
                    item1.PostalCode = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Phone1))
                {
                    item1.Phone1 = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Phone2))
                {
                    item1.Phone2 = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Fax))
                {
                    item1.Fax = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Email))
                {
                    item1.Email = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.Website))
                {
                    item1.Website = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 80)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 80 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.GSTRegNo))
                {
                    item1.GSTRegNo = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.CompanyRegNo))
                {
                    item1.CompanyRegNo = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.VehicleNo))
                {
                    item1.VehicleNo = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CC_Lib.OldRowData.LabelStyle))
                {
                    item1.LabelStyle = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                CC_Lib.Choosecustomer_dataTable_HasError = false;
                CC_Lib.PasteNewRowsProgrammatically = false;
                CC_Lib.PasteExistingRowsProgrammatically = false;
                CC_Lib.OldRowData = new Customermain_Model();
                CC_Lib.OldRowData = new Customermain_Model();
                CC_Lib.OldRowDataIndex = 0;
                Customermain_Model rowView = e.RowData as Customermain_Model;
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellValidating error: " + ex.Message);
            }
        }

        private void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e)
        {
            try
            {
                if (SfDataGrid1.GetAddNewRowController().GetAddNewRowIndex() == e.RowIndex)
                {
                    CC_Lib.CustomRowValidating(e.RowData as Customermain_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_RowValidating error: " + ex.Message + "\n" + ex.ToString());
            }
        }

        private async void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete) ||
                    e.Key == Key.Delete)
                {
                    CC_Lib.Delete();
                }
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
                CC_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
            }
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CC_Lib.Delete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteRow_Click error " + ex.Message);
            }
        }

        private async void PasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SfDataGrid1.GridCopyPaste.Paste();
                //CC_Lib.Paste();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PasteRow_Click error " + ex.Message);
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
                Customer_Main_FilterManager filtermanager = new Customer_Main_FilterManager();
                filtermanager.Choose_CustomerViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Main_ViewModel;
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
    }
}
