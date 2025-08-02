// <copyright file="SpecialDiscount.xaml.cs" company="PlaceholderCompany">
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
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;
    using SD_Lib = WpfApp3.SharedLib.Special_Discount_SharedLib;

    /// <summary>
    /// Interaction logic for Customer_Branch.xaml.
    /// </summary>
    public partial class Special_Discount : UserControl
    {

        public Special_Discount()
        {
            this.InitializeComponent();
        }

        private void Special_Discount_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SD_Lib.specialdiscountViewModel = this.DataContext as Special_Discount_ViewModel;
            SD_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            SD_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            SD_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            SD_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            SD_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
            DateWidget1.AllowNullValue = true;
            DateWidget1.NullValue = IUOM_Lib.CustomDateTime("1990-01-01");
            DateWidget1.MinDateTime = IUOM_Lib.CustomDateTime("1990-01-01");
            DateWidget1.MaxDateTime = DateTime.Now.Date.AddYears(200);
            DateWidget2.AllowNullValue = true;
            DateWidget2.NullValue = IUOM_Lib.CustomDateTime("1990-01-01");
            DateWidget2.MinDateTime = IUOM_Lib.CustomDateTime("1990-01-01");
            DateWidget2.MaxDateTime = DateTime.Now.Date.AddYears(200);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                SD_Lib.SpecialDiscount_dataTable_HasError = true;
                SD_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                SD_Lib.SpecialDiscount_dataTable_HasError = false;
                SD_Lib.ErrorTextBox1_Setter("hidden", message);
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
                    SD_Lib.PasteNewRowsProgrammatically == true ||
                    SD_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                SD_Lib.OldRowDataIndex = index - 1;
                SD_Lib.OldRowData = record as Specialdiscount_Model;
                Specialdiscount_Model item1 = context.SpecialDiscounts.Where(a => a.Id.Equals(SD_Lib.OldRowData.Id)).Single();

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.SalesCode))
                {
                    item1.SalesCode = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                    else if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.Customers.Where(i => i.CustomerId.Equals(item1.SalesCode)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.SalesCode);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.Customers)}!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.ItemNo))
                {
                    item1.ItemNo = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                    else if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.Items.Where(i => i.ItemNo.Equals(item1.ItemNo)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.ItemNo);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.Items)}!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.UnitOfMeasureCode))
                {
                    item1.UnitOfMeasureCode = e.NewValue.ToString();

                    if (e.NewValue != null && e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                    else if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.UnitsOfMeasures.Where(i => i.Code.Equals(item1.UnitOfMeasureCode)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.UnitOfMeasureCode);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.UnitsOfMeasures)}!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.LineDiscount))
                {
                    item1.LineDiscount = IUOM_Lib.CustomDecimal(e.NewValue.ToString());

                    if (e.NewValue != null && e.NewValue.ToString().Length > 64)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 64 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.StartingDate))
                {
                    item1.StartingDate = IUOM_Lib.CustomDateTime(e.NewValue.ToString());
                }

                else if (e.Column.MappingName == nameof(SD_Lib.OldRowData.EndingDate))
                {
                    item1.EndingDate = IUOM_Lib.CustomDateTime(e.NewValue.ToString());
                }

                else
                {
                    if (e.OldValue != e.NewValue &&
                            context.SpecialDiscounts.Where(
                                i => i.SalesCode.Equals(item1.SalesCode) &&
                                i.ItemNo.Equals(item1.ItemNo) &&
                                i.UnitOfMeasureCode.Equals(item1.UnitOfMeasureCode) &&
                                i.LineDiscount.Equals(item1.LineDiscount) &&
                                i.StartingDate.Equals(item1.StartingDate)
                                ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                item1.SalesCode,
                                item1.ItemNo,
                                item1.UnitOfMeasureCode,
                                item1.LineDiscount,
                                item1.StartingDate
                                );

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                }

                // if no error
                // Update block, after validation is passed
                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                SD_Lib.SpecialDiscount_dataTable_HasError = false;
                SD_Lib.PasteNewRowsProgrammatically = false;
                SD_Lib.PasteExistingRowsProgrammatically = false;
                SD_Lib.OldRowData = new Specialdiscount_Model();
                SD_Lib.OldRowData = new Specialdiscount_Model();
                SD_Lib.OldRowDataIndex = 0;
                Specialdiscount_Model rowView = e.RowData as Specialdiscount_Model;
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
                    SD_Lib.CustomRowValidating(e.RowData as Specialdiscount_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_RowValidating error: " + ex.Message);
            }
        }

        private async void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete) ||
                    e.Key == Key.Delete)
                {
                    SD_Lib.Delete();
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
                SD_Lib.Copy();
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
                SD_Lib.Delete();
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
                SD_Lib.Paste();
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
                Special_Discount_FilterManager filtermanager = new Special_Discount_FilterManager();
                filtermanager.specialdiscountViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Discount_ViewModel;
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
                    Special_Discount_FilterManager filtermanager = new Special_Discount_FilterManager();
                    filtermanager.specialdiscountViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Discount_ViewModel;
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
                Special_Discount_FilterManager filtermanager = new Special_Discount_FilterManager();
                filtermanager.specialdiscountViewModel = this.SfDataGrid1.DataContext as ViewModel.Special_Discount_ViewModel;
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
