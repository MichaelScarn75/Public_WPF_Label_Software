// <copyright file="SpecialPrice.xaml.cs" company="PlaceholderCompany">
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
    using System.Threading.Tasks;
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
    using SP_Lib = WpfApp3.SharedLib.Special_Price_SharedLib;

    /// <summary>
    /// Interaction logic for Customer_Branch.xaml.
    /// </summary>
    public partial class Special_Price : UserControl
    {

        public Special_Price()
        {
            this.InitializeComponent();
        }

        private void Special_Price_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SP_Lib.specialpriceViewModel = this.DataContext as Special_Price_ViewModel;
            SP_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            SP_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            SP_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            SP_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            SP_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
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
                SP_Lib.SpecialPrice_dataTable_HasError = true;
                SP_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                SP_Lib.SpecialPrice_dataTable_HasError = false;
                SP_Lib.ErrorTextBox1_Setter("hidden", message);
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
                        SP_Lib.PasteNewRowsProgrammatically == true ||
                        SP_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                SP_Lib.OldRowDataIndex = index - 1;
                SP_Lib.OldRowData = record as Specialprice_Model;
                Specialprice_Model item1 = context.SpecialPrices.Where(a => a.Id.Equals(SP_Lib.OldRowData.Id)).Single();

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                else if (e.Column.MappingName == nameof(item1.SalesCode))
                {
                    item1.SalesCode = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 20)
                    {
                        item1.SalesCode = e.NewValue.ToString();
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
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

                else if (e.Column.MappingName == nameof(item1.ItemNo))
                {
                    item1.ItemNo = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 10)
                    {
                        item1.ItemNo = e.NewValue.ToString();
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
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

                else if (e.Column.MappingName == nameof(item1.UnitOfMeasureCode))
                {
                    item1.UnitOfMeasureCode = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 10)
                    {
                        item1.UnitOfMeasureCode = e.NewValue.ToString();
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
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

                else if (e.Column.MappingName == nameof(item1.UnitPrice))
                {
                    item1.UnitPrice = IUOM_Lib.CustomDecimal(e.NewValue.ToString());
                    if (e.Column.MappingName == "UnitPrice" && (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty))
                    {
                        e.NewValue = Decimal.Zero;
                    }

                    else if (e.NewValue.ToString().Length > 64)
                    {
                        item1.UnitPrice = Convert.ToDecimal(e.NewValue);
                        this.SetErrorMessage(e, "Total length cannot exceed 64 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.StartingDate))
                {
                    item1.StartingDate = IUOM_Lib.CustomDateTime(e.NewValue.ToString());
                    DateTime temp_datetime;
                    bool converted = DateTime.TryParse(e.NewValue.ToString(), out temp_datetime);
                    if (e.Column.MappingName == "StartingDate" && (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty))
                    {
                        // do nothing, date can be null
                    }

                    else if (converted == false)
                    {
                        this.SetErrorMessage(e, "cannot be converted into date!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.EndingDate))
                {
                    item1.EndingDate = IUOM_Lib.CustomDateTime(e.NewValue.ToString());
                    DateTime temp_datetime;
                    bool converted = DateTime.TryParse(e.NewValue.ToString(), out temp_datetime);

                    if (converted == false)
                    {
                        this.SetErrorMessage(e, "cannot be converted into date!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.ProductBarcode))
                {
                    item1.ProductBarcode = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 18)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 18 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.Barcode_Format))
                {
                    item1.Barcode_Format = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.CustomerSKU))
                {
                    item1.CustomerSKU = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 18)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 18 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.Hidden))
                {
                    item1.Hidden = Convert.ToBoolean(e.NewValue);
                }

                else if (e.Column.MappingName == nameof(item1.EnglishLabelDescription))
                {
                    item1.EnglishLabelDescription = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 60)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 60 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.MalayLabelDescription))
                {
                    item1.MalayLabelDescription = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 60)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 60 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.ChineseLabelDescription))
                {
                    item1.ChineseLabelDescription = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.LabelUnitOfMeasure))
                {
                    item1.LabelUnitOfMeasure = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 15)
                    {
                        item1.UnitPrice = Convert.ToDecimal(e.NewValue);
                        this.SetErrorMessage(e, "Total length cannot exceed 15 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.LabelSize))
                {
                    item1.LabelSize = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 45)
                    {
                        item1.UnitPrice = Convert.ToDecimal(e.NewValue);
                        this.SetErrorMessage(e, "Total length cannot exceed 45 characters!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.Labelsizes.Where(i => i.Code.Equals(item1.LabelSize)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.LabelSize);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.Labelsizes)}!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.CurrencyCode))
                {
                    item1.CurrencyCode = e.NewValue.ToString();
                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    else if (e.NewValue.ToString().Length > 10)
                    {
                        item1.UnitPrice = Convert.ToDecimal(e.NewValue);
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.Currencies.Where(i => i.Code.Equals(item1.CurrencyCode)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.CurrencyCode);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.Currencies)}!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(item1.WeightItem))
                {
                    item1.WeightItem = Convert.ToBoolean(e.NewValue);
                }

                else if (e.Column.MappingName == nameof(item1.WeightScale))
                {
                    item1.WeightScale = Convert.ToBoolean(e.NewValue);
                }

                else
                {
                    if (e.OldValue != e.NewValue &&
                            context.SpecialPrices.Where(
                                i => i.SalesCode.Equals(item1.SalesCode) &&
                                i.ItemNo.Equals(item1.ItemNo) &&
                                i.UnitOfMeasureCode.Equals(item1.UnitOfMeasureCode) &&
                                i.UnitPrice.Equals(item1.UnitPrice) &&
                                i.StartingDate.Equals(item1.StartingDate)
                                ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                item1.SalesCode,
                                item1.ItemNo,
                                item1.UnitOfMeasureCode,
                                item1.UnitPrice,
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
                SP_Lib.SpecialPrice_dataTable_HasError = false;
                SP_Lib.PasteNewRowsProgrammatically = false;
                SP_Lib.PasteExistingRowsProgrammatically = false;
                SP_Lib.OldRowData = new Specialprice_Model();
                SP_Lib.OldRowData = new Specialprice_Model();
                SP_Lib.OldRowDataIndex = 0;
                Specialprice_Model rowView = e.RowData as Specialprice_Model;
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
                    SP_Lib.CustomRowValidating(e.RowData as Specialprice_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    SP_Lib.Delete();
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
                SP_Lib.Copy();
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
                SP_Lib.Delete();
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
                SP_Lib.Paste();
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

        private async void SfDataGrid1_CurrentCellValueChanged(object sender, CurrentCellValueChangedEventArgs e)
        {
            try
            {
                int columnIndex = this.SfDataGrid1.ResolveToGridVisibleColumnIndex(e.RowColumnIndex.ColumnIndex);
                var cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;

                //Enabling the RowValidating, CellValidating event if the changes happen in GridCheckBoxColumn

                if (this.SfDataGrid1.Columns[columnIndex].CellType == "CheckBox")
                {
                    this.SfDataGrid1.GetValidationHelper().SetCurrentRowValidated(false);
                    this.SfDataGrid1.GetValidationHelper().SetCurrentCellValidated(false);
                    cellManager.CheckValidationAndEndEdit();
                    await Task.Delay(100);
                    this.SfDataGrid1.ClearSelections(false);
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellValueChanged error " + ex.Message);
            }
        }
    }
}

