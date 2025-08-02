namespace WpfApp3.SharedLib
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using Microsoft.IdentityModel.Tokens;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using Syncfusion.Windows.Controls.Input;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    class Item_Units_Of_Measure_SharedLib
    {
        internal static bool Itemunitsofmeasure_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex;
        internal static Itemunitsofmeasure_Model OldRowData = new Itemunitsofmeasure_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static GridMultiColumnDropDownList GridMultiColumnDropDownList_Proxy = new GridMultiColumnDropDownList();
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static Item_Units_Of_Measure_ViewModel itemunitsofmeasureViewModel = new Item_Units_Of_Measure_ViewModel();

        internal static void Reset()
        {
            Itemunitsofmeasure_dataTable_HasError = false;
            PasteNewRowsProgrammatically = false;
            PasteExistingRowsProgrammatically = false;
            OldRowData = new Itemunitsofmeasure_Model();
            OldRowData = new Itemunitsofmeasure_Model();
            OldRowDataIndex = 0;
            ErrorTextBox1_Setter("hidden", string.Empty);
        }
        internal static Int32 CustomInt(string value)
        {
            Int32 temp;
            var temp2 = Int32.TryParse(value, out temp);
            return temp;
        }

        internal static decimal CustomDecimal(string value)
        {
            decimal temp;
            var temp2 = decimal.TryParse(value, out temp);
            return Math.Round(temp, 5);
        }

        internal static bool CustomBool(string value)
        {
            bool temp;
            bool.TryParse(value, out temp);
            return temp;
        }

        internal static DateTime CustomDateTime(string value)
        {
            return Convert.ToDateTime(value).Date;
        }

        internal static byte[] CustomByte(string? value)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            if (value.IsNullOrEmpty())
            {
                encoder.Frames.Add(BitmapFrame.Create(
                    (BitmapImage)Application.Current.Resources["Img_Null_Image"]
                    ));
            }
            else
            {
                encoder.Frames.Add(BitmapFrame.Create(
                    new System.Windows.Media.Imaging.BitmapImage(
                        new Uri(string.Format($"{value}"),
                        UriKind.Relative)))
                    );
            }

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        internal static BitmapImage CustomBitmapImage(byte[] value)
        {
            if (value == null || value.Length == 0)
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

        public static byte[] ToByte(string? _filepath)
        {
            byte[] _bytes;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            if (_filepath.IsNullOrEmpty())
            {
                encoder.Frames.Add(BitmapFrame.Create(
                    (BitmapImage)Application.Current.Resources["Img_Null_Image"])
                    );
            }
            else
            {
                encoder.Frames.Add(BitmapFrame.Create(
                    new System.Windows.Media.Imaging.BitmapImage(new Uri(string.Format($"{_filepath}"), UriKind.Relative)))
                    );
            }

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                _bytes = ms.ToArray();
            }

            return _bytes;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No network adapters with an IPv4 address in the system!";
        }

        internal static void ErrorTextBox1_Setter(string visibility, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (visibility == "show")
                    {
                        itemunitsofmeasureViewModel.ErrorTextBorder1_Visibility = Visibility.Visible;
                        itemunitsofmeasureViewModel.ErrorTextBox1_Text = message;
                    }
                    else
                    {
                        itemunitsofmeasureViewModel.ErrorTextBorder1_Visibility = Visibility.Hidden;
                        itemunitsofmeasureViewModel.ErrorTextBox1_Text = message;
                    }
                });
        }

        internal static void Copy()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            if (SfDataGrid1_Proxy.SelectedItems.Count == 0 && cellManager.HasCurrentCell == false)
            {
                MessageBoxResult result1 = MessageBox.Show("No row(s) selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SfDataGrid1_Proxy.GridCopyPaste.Copy();
        }

        internal static async void Refresh()
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            itemunitsofmeasureViewModel.Populate__itemunitsofmeasure();
        }

        internal static void Delete()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            ViewModel.Item_Units_Of_Measure_ViewModel viewModel1 = SfDataGrid1_Proxy.DataContext as ViewModel.Item_Units_Of_Measure_ViewModel;
            List<Itemunitsofmeasure_Model> todelete = new();

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

            var toreturn = Itemunitsofmeasure_dataTable_HasError;

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
                foreach (Itemunitsofmeasure_Model item in SfDataGrid1_Proxy.SelectedItems)
                {
                    todelete.Add(item);
                }

                foreach (Itemunitsofmeasure_Model item in todelete)
                {
                    if (context.SpecialPrices.Where(a => a.ItemNo.Equals(item.ItemNo))
                        .Where(a => a.UnitOfMeasureCode.Equals(item.UnitOfMeasureCode))
                        .Count() > 0)
                    {
                        MessageBox.Show($"{nameof(item.ItemNo)},{nameof(item.UnitOfMeasureCode)} exists in {nameof(context.SpecialPrices)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (context.SpecialDiscounts.Where(a => a.ItemNo.Equals(item.ItemNo))
                        .Where(a => a.UnitOfMeasureCode.Equals(item.UnitOfMeasureCode))
                        .Count() > 0)
                    {
                        MessageBox.Show($"{nameof(item.ItemNo)},{nameof(item.UnitOfMeasureCode)} exists in {nameof(context.SpecialDiscounts)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    viewModel1.Itemunitsofmeasure_IEnum.Remove(item);
                    context.ItemUnitsOfMeasures.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        internal static void New()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            var currentCell = cellManager.CurrentCell;
            int row_index = SfDataGrid1_Proxy.GetAddNewRowController().GetAddNewRowIndex();
            int col_index = InventoryPostingGroup_SharedLib.SfDataGrid1_Proxy.GetFirstColumnIndex();

            if (Itemunitsofmeasure_dataTable_HasError)
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
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;

            if (Itemunitsofmeasure_dataTable_HasError == true)
            {
                MessageBoxResult result = MessageBox.Show("Resolve the error(s) first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cellManager.CurrentCell != null)
            {
                cellManager.EndEdit(false);
                SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
            }

            if (SfDataGrid1_Proxy.IsAddNewIndex(cellManager.CurrentRowColumnIndex.RowIndex))
            {
                SfDataGrid1_Proxy.GetAddNewRowController().CancelAddNew();
            }

            Item_Units_Of_Measure_FilterManager item_units_of_measure_filtermanager = new Item_Units_Of_Measure_FilterManager();
            item_units_of_measure_filtermanager.Grid1 = Detailed_Filter_Children_Grid;
            item_units_of_measure_filtermanager.SimplyRemoveAllFilter();
            itemunitsofmeasureViewModel.Populate__itemunitsofmeasure();
            itemunitsofmeasureViewModel.Populate__unitsofmeasure();
        }

        public static async void Paste()
        {
            if (Itemunitsofmeasure_dataTable_HasError)
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


            int clipboardSplitByTab_Counter = 0;
            int column_counter = 0;
            Itemunitsofmeasure_Model model = new();
            ObservableCollection<Itemunitsofmeasure_Model> clipboard_collection = new();

            for (clipboardSplitByTab_Counter = 0; clipboardSplitByTab_Counter < clipboardSplitByTab.Count(); clipboardSplitByTab_Counter++)
            {
                if (SfDataGrid1_Proxy.Columns[column_counter].MappingName == nameof(model.QtyPerUnitOfMeasure))
                {
                    model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, CustomDecimal(clipboardSplitByTab[clipboardSplitByTab_Counter]));
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
                List<Itemunitsofmeasure_Model> selecteditems = new();
                foreach (var item in SfDataGrid1_Proxy.SelectedItems)
                {
                    selecteditems.Add(item as Itemunitsofmeasure_Model);
                }

                for (int i = 0; i < clipboard_collection.Count; i++)
                {
                    var temp_model = clipboard_collection[i];
                    CustomRowValidating(temp_model, IsAddNew: false, Programmatically: true, null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    PasteExistingRowsProgrammatically = true;
                    var datacontext_collection = itemunitsofmeasureViewModel.Itemunitsofmeasure_IEnum;
                    datacontext_collection.Insert(datacontext_collection.IndexOf(selecteditems[i]), item);
                    datacontext_collection.Remove(selecteditems[i]);
                    using (ProductListingContext context = new())
                    {
                        context.ItemUnitsOfMeasures.Remove(selecteditems[i]);
                        context.ItemUnitsOfMeasures.Add(item);
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
                    CustomRowValidating(temp_model, IsAddNew: true, Programmatically: true, e: null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    itemunitsofmeasureViewModel.Itemunitsofmeasure_IEnum.Add(item);
                    var rowIndex = itemunitsofmeasureViewModel.Itemunitsofmeasure_IEnum.IndexOf(item) + 1;
                    var colIndex = SfDataGrid1_Proxy.Columns.IndexOf(SfDataGrid1_Proxy.Columns.FirstOrDefault());
                    RowColumnIndex rowColumnIndex = new RowColumnIndex(rowIndex, colIndex);
                    await Task.Delay(100);
                    SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
                    SfDataGrid1_Proxy.SelectionController.MoveCurrentCell(rowColumnIndex);
                    SfDataGrid1_Proxy.ScrollInView(rowColumnIndex);
                    await Task.Delay(100);

                    using (ProductListingContext context = new())
                    {
                        context.ItemUnitsOfMeasures.Add(item);
                        context.SaveChanges();
                    }

                    Reset();
                }

                return;
            }
        }

        public static void CustomRowValidating(
            Itemunitsofmeasure_Model rowView,
            bool IsAddNew,
            bool Programmatically,
            Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e,
            out bool success,
            out Itemunitsofmeasure_Model temp_model)
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
                    rowView.ItemNo == null &&
                    rowView.UnitOfMeasureCode == null &&
                    rowView.QtyPerUnitOfMeasure == 0
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
                else if (rowView.ItemNo == string.Empty)
                {
                    string message = $"Column \"{rowView.ItemNo}\" cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.ItemNo), Programmatically, e, success);
                    return;
                }
                else if (rowView.ItemNo.Length > 10)
                {
                    string message = $"The value \"{rowView.ItemNo}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.ItemNo), Programmatically, e, success);
                    return;
                }
                else if (rowView.UnitOfMeasureCode == null || rowView.UnitOfMeasureCode == string.Empty)
                {
                    string message = $"Column \"{rowView.UnitOfMeasureCode}\" cannot be null";
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
                else if (context.UnitsOfMeasures.Where(i => i.Code.Equals(rowView.UnitOfMeasureCode)).Count() == 0)
                {
                    string message = $"The value \"{rowView.UnitOfMeasureCode.ToString()}\" doesn't exist in Item Units Of Measure table!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }
                else if (context.ItemUnitsOfMeasures.Where(
                    i => i.ItemNo.Equals(rowView.ItemNo) &&
                    i.UnitOfMeasureCode.Equals(rowView.UnitOfMeasureCode)
                    ).Count() > 0)
                {
                    var joined_string = System.String.Join(",",
                                            rowView.ItemNo,
                                            rowView.UnitOfMeasureCode,
                                            "is duplicated!"
                                            );

                    string message = joined_string;
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.ItemNo), Programmatically, e, success);
                    ShowError(message, nameof(rowView.UnitOfMeasureCode), Programmatically, e, success);
                    return;
                }
                else if (rowView.QtyPerUnitOfMeasure.ToString().Length > 64)
                {
                    string message = $"The value \"{Convert.ToDouble(rowView.QtyPerUnitOfMeasure.ToString())}\" exceeds the limit of 64 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.QtyPerUnitOfMeasure), Programmatically, e, success);
                    return;
                }
                if (rowView.QtyPerUnitOfMeasure == null)
                {
                    rowView.QtyPerUnitOfMeasure = Math.Round(Convert.ToDecimal(0.00000));
                }

                Debug.WriteLine("update");
                success = false;
                temp_model = rowView;
                if (IsAddNew && !Programmatically)
                {
                    context.ItemUnitsOfMeasures.Add(rowView);
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
                Itemunitsofmeasure_dataTable_HasError = true;
            }
            else
            {
                e.ErrorMessages.Add(columnName, message);
                e.IsValid = !success;
                Itemunitsofmeasure_dataTable_HasError = true;
                ErrorTextBox1_Setter("show", message);
            }
            return;
        }
    }
}
