namespace WpfApp3.SharedLib
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using Syncfusion.Windows.Controls.Input;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

    class Special_Price_SharedLib
    {
        internal static bool SpecialPrice_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex = 0;
        internal static Specialprice_Model OldRowData = new Specialprice_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static Special_Price_ViewModel specialpriceViewModel = new Special_Price_ViewModel();

        internal static void ErrorTextBox1_Setter(string visibility, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (visibility == "show")
                    {
                        specialpriceViewModel.ErrorTextBorder1_Visibility = Visibility.Visible;
                        specialpriceViewModel.ErrorTextBox1_Text = message;
                    }
                    else
                    {
                        specialpriceViewModel.ErrorTextBorder1_Visibility = Visibility.Hidden;
                        specialpriceViewModel.ErrorTextBox1_Text = message;
                    }
                });
        }

        internal static void Reset()
        {
            SpecialPrice_dataTable_HasError = false;
            PasteNewRowsProgrammatically = false;
            PasteExistingRowsProgrammatically = false;
            OldRowData = new Specialprice_Model();
            OldRowData = new Specialprice_Model();
            OldRowDataIndex = 0;
            ErrorTextBox1_Setter("hidden", string.Empty);
        }

        internal static async void Refresh()
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            specialpriceViewModel.Populate__specialprice();
        }

        internal static bool Copy()
        {
            if (SfDataGrid1_Proxy.SelectedItems.Count == 0 && SfDataGrid1_Proxy.SelectionController.CurrentCellManager.HasCurrentCell == false)
            {
                MessageBoxResult result1 = MessageBox.Show("No row(s) selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            SfDataGrid1_Proxy.GridCopyPaste.Copy();
            return true;
        }

        internal static void Delete()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            ViewModel.Special_Price_ViewModel viewModel1 = SfDataGrid1_Proxy.DataContext as Special_Price_ViewModel;
            List<Specialprice_Model> todelete = new();

            MessageBoxResult result2 = MessageBox.Show("Delete selected rows?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result2 != MessageBoxResult.OK)
            {
                return;
            }

            if (MainWindow_SharedLib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_View_Enabled == false)
            {
                MessageBoxResult result1 = MessageBox.Show("Can't delete in View Mode, change to Edit Mode first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ((SfDataGrid1_Proxy.IsAddNewIndex(cellManager.CurrentRowColumnIndex.RowIndex) &&
                !PasteExistingRowsProgrammatically) ||
                PasteNewRowsProgrammatically)
            {
                SfDataGrid1_Proxy.GetAddNewRowController().CancelAddNew();
                SfDataGrid1_Proxy.GetValidationHelper().SetCurrentCellValidated(true);
                Reset();
                return;
            }

            var toreturn = SpecialPrice_dataTable_HasError;

            if (cellManager.CurrentCell.IsEditing)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                if (toreturn)
                {
                    return;
                }
            }

            using (ProductListingContext context = new())
            {
                foreach (Specialprice_Model item in SfDataGrid1_Proxy.SelectedItems)
                {
                    todelete.Add(item);
                }

                foreach (Specialprice_Model item in todelete)
                {
                    viewModel1.SpecialPrice_IEnum.Remove(item);
                    context.SpecialPrices.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        internal static void New()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            var currentCell = SfDataGrid1_Proxy.SelectionController.CurrentCellManager.CurrentCell;
            int row_index = SfDataGrid1_Proxy.GetAddNewRowController().GetAddNewRowIndex();
            int col_index = SfDataGrid1_Proxy.GetFirstColumnIndex();

            if (SpecialPrice_dataTable_HasError)
            {
                MessageBoxResult result = MessageBox.Show("Resolve the error(s) first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentCell != null && currentCell.RowIndex == row_index)
            {
                return;
            }

            if (currentCell != null && currentCell.IsEditing == true)
            {
                cellManager.EndEdit(false);
            }

            if (SfDataGrid1_Proxy.GetAddNewRowController().GetAddNewRowIndex() != -1)
            {
                SfDataGrid1_Proxy.MoveCurrentCell(new RowColumnIndex(row_index, col_index), true);
                SfDataGrid1_Proxy.ScrollInView(new RowColumnIndex(row_index, col_index));
                if (currentCell != null && currentCell.IsEditing == false)
                {
                    cellManager.BeginEdit();
                }
            }
        }

        internal static async void Edit()
        {
            SfDataGrid1_Proxy.AllowEditing = true;
            SfDataGrid1_Proxy.AllowDeleting = false;
            SfDataGrid1_Proxy.AddNewRowPosition = AddNewRowPosition.Bottom;
            if (cellManager.HasCurrentCell && !SfDataGrid1_Proxy.View.IsEditingItem)
            {
                SfDataGrid1_Proxy.SelectionController.CurrentCellManager.BeginEdit();
                await Task.Delay(100);
            }
        }

        internal static async void View()
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            SfDataGrid1_Proxy.AllowEditing = false;
            SfDataGrid1_Proxy.AddNewRowPosition = AddNewRowPosition.None;
        }

        internal static void ClearFilter(Grid Detailed_Filter_Children_Grid)
        {
            if (SpecialPrice_dataTable_HasError == true)
            {
                MessageBoxResult result = MessageBox.Show("Resolve the error(s) first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SfDataGrid1_Proxy.SelectionController.CurrentCellManager.CurrentCell != null)
            {
                SfDataGrid1_Proxy.SelectionController.CurrentCellManager.EndEdit(false);
                SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
            }

            if (SfDataGrid1_Proxy.IsAddNewIndex(SfDataGrid1_Proxy.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex))
            {
                SfDataGrid1_Proxy.GetAddNewRowController().CancelAddNew();
            }

            Special_Price_FilterManager specialprice_filtermanager = new Special_Price_FilterManager();
            specialprice_filtermanager.Grid1 = Detailed_Filter_Children_Grid;
            specialprice_filtermanager.SimplyRemoveAllFilter();
            specialpriceViewModel.Populate__specialprice();
        }

        public static async void Paste()
        {
            if (SpecialPrice_dataTable_HasError)
            {
                MessageBox.Show("Resolve the error(s) first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Clipboard.GetText() == null || Clipboard.GetText() == string.Empty)
            {
                MessageBox.Show("No row(s) read from the clipboard!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> clipboardSplitByNewLine = Regex.Split(Clipboard.GetText().Replace("\r", string.Empty).Trim(), @"\n").ToList();
            List<string> clipboardSplitByTab = new List<string>();
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;

            // split clipboard rows into clipboard cells
            foreach (string item in clipboardSplitByNewLine)
            {
                clipboardSplitByTab.AddRange(Regex.Split(item, @"\t").ToList());
            }


            // if selected rows are more than or equal to clipboard rows
            if (SfDataGrid1_Proxy.SelectedItems.Count > 0 && SfDataGrid1_Proxy.SelectedItems.Count != clipboardSplitByNewLine.Count())
            {
                MessageBox.Show($"Can't paste {clipboardSplitByNewLine.Count()} rows into {SfDataGrid1_Proxy.SelectedItems.Count} rows!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //for each datagrid column
            //fill in the corresponding field in model
            //if reached the end of datagrid
            //shove model into collection
            //restart
            int clipboardSplitByTab_Counter = 0;
            int column_counter = 0;
            Specialprice_Model model = new();
            ObservableCollection<Specialprice_Model> clipboard_collection = new();

            for (clipboardSplitByTab_Counter = 0; clipboardSplitByTab_Counter < clipboardSplitByTab.Count(); clipboardSplitByTab_Counter++)
            {
                if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.UnitPrice))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomDecimal(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.StartingDate))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomDateTime(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.EndingDate))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomDateTime(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.Hidden))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomBool(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.WeightItem))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomBool(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.WeightScale))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, IUOM_Lib.CustomBool(clipboardSplitByTab[clipboardSplitByTab_Counter]));
                }
                else
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, clipboardSplitByTab[clipboardSplitByTab_Counter]);
                }

                column_counter++;

                if (column_counter == SfDataGrid1_Proxy.Columns.Count() || clipboardSplitByTab_Counter == clipboardSplitByTab.Count() - 1)
                {
                    clipboard_collection.Add(model);
                    model = new();
                    column_counter = 0;
                }

            }


            // if there are selected lines and count is equal to clipboard line count
            if (SfDataGrid1_Proxy.SelectedItems.Count == clipboardSplitByNewLine.Count())
            {
                List<Specialprice_Model> selecteditems = new();
                foreach (var item in SfDataGrid1_Proxy.SelectedItems)
                {
                    selecteditems.Add(item as Specialprice_Model);
                }

                for (int i = 0; i < clipboard_collection.Count; i++)
                {
                    var temp_model = clipboard_collection[i];
                    CustomRowValidating(temp_model, true, true, null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    var datacontext_collection = specialpriceViewModel.SpecialPrice_IEnum;
                    datacontext_collection.Insert(datacontext_collection.IndexOf(selecteditems[i]), item);
                    datacontext_collection.Remove(selecteditems[i]);
                    using (ProductListingContext context = new())
                    {
                        context.SpecialPrices.Remove(selecteditems[i]);
                        context.SpecialPrices.Add(item);
                        context.SaveChanges();
                    }
                    SfDataGrid1_Proxy.SelectedItems.Add(item);
                }

                return;
            }


            // if paste at add new row
            if (SfDataGrid1_Proxy.SelectedItems.Count == 0 && cellManager.CurrentCell.RowIndex == SfDataGrid1_Proxy.GetAddNewRowController().GetAddNewRowIndex())
            {
                foreach (var temp_model in clipboard_collection)
                {
                    CustomRowValidating(temp_model, true, true, null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    specialpriceViewModel.SpecialPrice_IEnum.Add(item);
                    var rowIndex = specialpriceViewModel.SpecialPrice_IEnum.IndexOf(item) + 1;
                    var colIndex = SfDataGrid1_Proxy.Columns.IndexOf(SfDataGrid1_Proxy.Columns.FirstOrDefault());
                    RowColumnIndex rowColumnIndex = new RowColumnIndex(rowIndex, colIndex);
                    await Task.Delay(100);
                    SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
                    SfDataGrid1_Proxy.SelectionController.MoveCurrentCell(rowColumnIndex);
                    SfDataGrid1_Proxy.ScrollInView(rowColumnIndex);
                    await Task.Delay(100);
                    using (ProductListingContext context = new())
                    {
                        context.SpecialPrices.Add(item);
                        context.SaveChanges();
                    }
                    Reset();
                }

                return;
            }
        }

        public static void CustomRowValidating(
            Specialprice_Model rowView,
            bool IsAddNew,
            bool Programmatically,
            Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e,
            out bool success,
            out Specialprice_Model temp_model)
        {
            if (IsAddNew == false
                && PasteNewRowsProgrammatically == false
                && PasteExistingRowsProgrammatically == false)
            {
                success = true;
                temp_model = rowView;
                return;
            }

            using (ProductListingContext context = new())
            {
                if (
                    rowView.SalesCode == null &&
                    rowView.ItemNo == null &&
                    rowView.UnitOfMeasureCode == null &&
                    rowView.UnitPrice == 0 &&
                    rowView.StartingDate == Convert.ToDateTime("01/01/0001 12:00:00 AM") &&
                    rowView.EndingDate == null &&
                    rowView.ProductBarcode == null &&
                    rowView.Barcode_Format == null &&
                    rowView.CustomerSKU == null &&
                    rowView.Hidden == null &&
                    rowView.EnglishLabelDescription == null &&
                    rowView.MalayLabelDescription == null &&
                    rowView.ChineseLabelDescription == null &&
                    rowView.LabelUnitOfMeasure == null &&
                    rowView.LabelSize == null &&
                    rowView.CurrencyCode == null &&
                    rowView.WeightItem == false &&
                    rowView.WeightScale == false
                    )
                {
                    success = true;
                    temp_model = rowView;
                    if (IsAddNew)
                    {
                        SfDataGrid1_Proxy.View.CancelNew();
                    }
                    return;
                }

                else if (rowView.SalesCode.ToString() == null || rowView.SalesCode.ToString() == string.Empty)
                {
                    string message = $"Column Sales Code cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.ItemNo.ToString() == null || rowView.ItemNo.ToString() == string.Empty)
                {
                    string message = $"Column Item No cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.UnitOfMeasureCode.ToString() == null || rowView.UnitOfMeasureCode.ToString() == string.Empty)
                {
                    string message = $"Column Unit Of Measure Code cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.UnitPrice.ToString() == null || rowView.UnitPrice.ToString() == string.Empty)
                {
                    rowView.UnitPrice = Decimal.Zero;
                }

                else if (rowView.StartingDate.ToString() == null || rowView.StartingDate.ToString() == string.Empty)
                {
                    // do nothing, date can be null
                }

                else if (rowView.EndingDate.ToString() == null || rowView.EndingDate.ToString() == string.Empty)
                {
                    // do nothing, date can be null
                }

                else if (rowView.ProductBarcode.ToString() == null || rowView.ProductBarcode.ToString() == string.Empty)
                {
                    // do nothing, can be null
                }

                else if (rowView.Barcode_Format.ToString() == null || rowView.Barcode_Format.ToString() == string.Empty)
                {
                    string message = $"Column Barcode Format cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.CustomerSKU.ToString() == null || rowView.CustomerSKU.ToString() == string.Empty)
                {
                    // do nothing, can be null
                }

                else if (rowView.Hidden.ToString() == null || rowView.Hidden.ToString() == string.Empty)
                {
                    rowView.Hidden = false;
                }

                else if (rowView.EnglishLabelDescription.ToString() == null || rowView.EnglishLabelDescription.ToString() == string.Empty)
                {
                    // do nothing, can be null
                }

                else if (rowView.MalayLabelDescription.ToString() == null || rowView.MalayLabelDescription.ToString() == string.Empty)
                {
                    // do nothing, can be null
                }

                else if (rowView.ChineseLabelDescription.ToString() == null || rowView.ChineseLabelDescription.ToString() == string.Empty)
                {
                    // do nothing, can be null
                }

                else if (rowView.LabelUnitOfMeasure.ToString() == null || rowView.LabelUnitOfMeasure.ToString() == string.Empty)
                {
                    string message = $"Column Label Unit Of Measure cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.LabelSize.ToString() == null || rowView.LabelSize.ToString() == string.Empty)
                {
                    string message = $"Column Label Size cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.CurrencyCode.ToString() == null || rowView.CurrencyCode.ToString() == string.Empty)
                {
                    string message = $"Column Currency Code cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.SalesCode.Length > 20)
                {
                    string message = $"The value \"{rowView.SalesCode}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.ItemNo.Length > 10)
                {
                    string message = $"The value \"{rowView.ItemNo}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.UnitOfMeasureCode.Length > 10)
                {
                    string message = $"The value \"{rowView.UnitOfMeasureCode}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.ProductBarcode.Length > 18)
                {
                    string message = $"The value \"{rowView.ProductBarcode}\" exceeds the limit of 18 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.Barcode_Format.Length > 26)
                {
                    string message = $"The value \"{rowView.Barcode_Format}\" exceeds the limit of 26 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.CustomerSKU.Length > 18)
                {
                    string message = $"The value \"{rowView.CustomerSKU}\" exceeds the limit of 18 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.EnglishLabelDescription.Length > 60)
                {
                    string message = $"The value \"{rowView.EnglishLabelDescription}\" exceeds the limit of 60 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.MalayLabelDescription.Length > 60)
                {
                    string message = $"The value \"{rowView.MalayLabelDescription}\" exceeds the limit of 60 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.ChineseLabelDescription.Length > 20)
                {
                    string message = $"The value \"{rowView.ChineseLabelDescription}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.LabelUnitOfMeasure.Length > 15)
                {
                    string message = $"The value \"{rowView.LabelUnitOfMeasure}\" exceeds the limit of 15 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.LabelSize.Length > 45)
                {
                    string message = $"The value \"{rowView.LabelSize}\" exceeds the limit of 45 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.CurrencyCode.Length > 10)
                {
                    string message = $"The value \"{rowView.CurrencyCode}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }

                else
                {
                    if (context.SpecialPrices.Where(
                        i => i.SalesCode.Equals(OldRowData.SalesCode) &&
                        i.ItemNo.Equals(OldRowData.ItemNo) &&
                        i.UnitOfMeasureCode.Equals(OldRowData.UnitOfMeasureCode) &&
                        i.UnitPrice.Equals(OldRowData.UnitPrice) &&
                        i.StartingDate.Equals(OldRowData.StartingDate)
                        ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                OldRowData.SalesCode,
                                OldRowData.ItemNo,
                                OldRowData.UnitOfMeasureCode,
                                OldRowData.UnitPrice,
                                OldRowData.StartingDate,
                                "is duplicated!"
                                );

                        string message = joined_string;
                        success = true;
                        temp_model = rowView;
                        ShowError(message, nameof(rowView.SalesCode), Programmatically, e, success);
                        ShowError(message, nameof(rowView.ItemNo), Programmatically, e, success);
                        ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                        ShowError(message, nameof(rowView.UnitPrice), Programmatically, e, success);
                        ShowError(message, nameof(rowView.StartingDate), Programmatically, e, success);
                        return;
                    }
                }

                rowView.Hidden = Convert.ToBoolean(rowView.Hidden.ToString());
                rowView.WeightItem = Convert.ToBoolean(rowView.WeightItem.ToString());
                rowView.WeightScale = Convert.ToBoolean(rowView.WeightScale.ToString());
                success = false;
                temp_model = rowView;
                if (IsAddNew && !Programmatically)
                {
                    context.SpecialPrices.Add(rowView);
                    context.SaveChanges();
                }
                Reset();
                return;
            }
        }

        public static void ShowError(
            string message,
            string columnName,
            bool Programmatically,
            Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e,
            bool success
            )
        {
            if (Programmatically)
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SpecialPrice_dataTable_HasError = true;
            }
            else
            {
                e.ErrorMessages.Add(columnName, message);
                e.IsValid = !success;
                SpecialPrice_dataTable_HasError = true;
                ErrorTextBox1_Setter("show", message);
            }
            return;
        }
    }
}
