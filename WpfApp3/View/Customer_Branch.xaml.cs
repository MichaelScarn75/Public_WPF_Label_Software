// <copyright file="Customer_Branch.xaml.cs" company="PlaceholderCompany">
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
    using CB_Lib = WpfApp3.SharedLib.Customer_Branch_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Customer_Branch.xaml.
    /// </summary>
    public partial class Customer_Branch : UserControl
    {

        public Customer_Branch()
        {
            this.InitializeComponent();
        }

        private void Customer_Branch_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CB_Lib.customerbranchViewModel = this.DataContext as Customer_Branch_ViewModel;
            CB_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CB_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            CB_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            CB_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            CB_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                CB_Lib.CustomerBranch_dataTable_HasError = true;
                CB_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                CB_Lib.CustomerBranch_dataTable_HasError = false;
                CB_Lib.ErrorTextBox1_Setter("hidden", message);
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
                        CB_Lib.PasteNewRowsProgrammatically == true ||
                        CB_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                CB_Lib.OldRowDataIndex = index - 1;
                CB_Lib.OldRowData = record as Customerbranch_Model;
                Customerbranch_Model item1 = context.CustomerBranches.Where(a => a.Id.Equals(CB_Lib.OldRowData.Id)).Single();
                var item2 = context.Customers.Where(a => a.CustomerId.Equals(CB_Lib.OldRowData.CustomerId));

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.BranchId))
                {
                    item1.BranchId = e.NewValue.ToString();

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
                    else
                    {
                        if (e.OldValue != e.NewValue &&
                            context.CustomerBranches.Where(
                            i => i.BranchId.Equals(CB_Lib.OldRowData.BranchId)
                            ).Count() > 0)
                        {
                            var joined_string = System.String.Join(",",
                                                    CB_Lib.OldRowData.BranchId);

                            this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                            return;
                        }
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.CustomerId))
                {
                    item1.CustomerId = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.CustomerId = e.NewValue.ToString());

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.CustomerDescription))
                {
                    item1.CustomerDescription = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.Description = e.NewValue.ToString());

                    if (e.NewValue.ToString().Length > 50)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 50 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.BranchDescription))
                {
                    item1.BranchDescription = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 50)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 50 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Address1))
                {
                    item1.Address1 = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Address2))
                {
                    item1.Address2 = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Address3))
                {
                    item1.Address3 = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.ContactPerson))
                {
                    item1.ContactPerson = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.PostalCode))
                {
                    item1.PostalCode = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Phone1))
                {
                    item1.Phone1 = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Phone2))
                {
                    item1.Phone2 = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Fax))
                {
                    item1.Fax = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Email))
                {
                    item1.Email = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 30)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 30 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.Website))
                {
                    item1.Website = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 80)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 80 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.GSTRegNo))
                {
                    item1.GSTRegNo = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.CompanyRegNo))
                {
                    item1.CompanyRegNo = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.VehicleNo))
                {
                    item1.VehicleNo = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CB_Lib.OldRowData.LabelStyle))
                {
                    item1.LabelStyle = e.NewValue.ToString();

                    if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }

                // if no error
                // Update block, after validation is passed
                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                CB_Lib.CustomerBranch_dataTable_HasError = false;
                CB_Lib.PasteNewRowsProgrammatically = false;
                CB_Lib.PasteExistingRowsProgrammatically = false;
                CB_Lib.OldRowData = new Customerbranch_Model();
                CB_Lib.OldRowData = new Customerbranch_Model();
                CB_Lib.OldRowDataIndex = 0;
                Customerbranch_Model rowView = e.RowData as Customerbranch_Model;
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
                    CB_Lib.CustomRowValidating(e.RowData as Customerbranch_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    CB_Lib.Delete();
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
                CB_Lib.Copy();
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
                CB_Lib.Delete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteRow_Click error " + ex.Message);
            }
        }

        private void PasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CB_Lib.Paste();
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
                Customer_Branch_FilterManager filtermanager = new Customer_Branch_FilterManager();
                filtermanager.customerbranchViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Branch_ViewModel;
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
                    Customer_Branch_FilterManager filtermanager = new Customer_Branch_FilterManager();
                    filtermanager.customerbranchViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Branch_ViewModel;
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
                Customer_Branch_FilterManager filtermanager = new Customer_Branch_FilterManager();
                filtermanager.customerbranchViewModel = this.SfDataGrid1.DataContext as ViewModel.Customer_Branch_ViewModel;
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

