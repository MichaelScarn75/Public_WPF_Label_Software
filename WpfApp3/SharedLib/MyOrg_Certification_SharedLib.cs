namespace WpfApp3.SharedLib
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

    class MyOrg_Certification_SharedLib
    {
        internal static bool Myorg_certification_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex = 0;
        internal static Myorgcertification_Model OldRowData = new Myorgcertification_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static MyOrg_Certification_ViewModel myorgcertificationViewModel = new MyOrg_Certification_ViewModel();

        internal static void ErrorTextBox1_Setter(string visibility, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (visibility == "show")
                        {
                            myorgcertificationViewModel.ErrorTextBorder1_Visibility = Visibility.Visible;
                            myorgcertificationViewModel.ErrorTextBox1_Text = message;
                        }
                        else
                        {
                            myorgcertificationViewModel.ErrorTextBorder1_Visibility = Visibility.Hidden;
                            myorgcertificationViewModel.ErrorTextBox1_Text = message;
                        }
                    });
        }

        internal static void Reset()
        {
            Myorg_certification_dataTable_HasError = false;
            PasteNewRowsProgrammatically = false;
            PasteExistingRowsProgrammatically = false;
            OldRowData = new Myorgcertification_Model();
            OldRowData = new Myorgcertification_Model();
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

            myorgcertificationViewModel.Populate__myorgcertification();
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
            ViewModel.MyOrg_Certification_ViewModel viewModel1 = SfDataGrid1_Proxy.DataContext as ViewModel.MyOrg_Certification_ViewModel;
            List<Myorgcertification_Model> todelete = new();

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

            var toreturn = Myorg_certification_dataTable_HasError;

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
                foreach (Myorgcertification_Model item in SfDataGrid1_Proxy.SelectedItems)
                {
                    todelete.Add(item);
                }

                foreach (Myorgcertification_Model item in todelete)
                {
                    viewModel1.Myorgcertification_IEnum.Remove(item);
                    context.MyorgCertifications.Remove(item);
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

            if (Myorg_certification_dataTable_HasError)
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
            if (Myorg_certification_dataTable_HasError == true)
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

            Units_Of_Measure_FilterManager units_of_measure_filtermanager = new Units_Of_Measure_FilterManager();
            units_of_measure_filtermanager.Grid1 = Detailed_Filter_Children_Grid;
            units_of_measure_filtermanager.SimplyRemoveAllFilter();
            myorgcertificationViewModel.Populate__myorgcertification();
        }

        public static async void Paste()
        {
            if (Myorg_certification_dataTable_HasError)
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
            Myorgcertification_Model model = new();
            ObservableCollection<Myorgcertification_Model> clipboard_collection = new();

            for (clipboardSplitByTab_Counter = 0; clipboardSplitByTab_Counter < clipboardSplitByTab.Count(); clipboardSplitByTab_Counter++)
            {
                model.GetType().GetProperty(SfDataGrid1_Proxy.Columns[column_counter].MappingName).SetValue(model, clipboardSplitByTab[clipboardSplitByTab_Counter]);

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
                List<Myorgcertification_Model> selecteditems = new();
                foreach (var item in SfDataGrid1_Proxy.SelectedItems)
                {
                    selecteditems.Add(item as Myorgcertification_Model);
                }

                for (int i = 0; i < clipboard_collection.Count; i++)
                {
                    var temp_model = clipboard_collection[i];
                    CustomRowValidating(temp_model, true, true, null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    var datacontext_collection = myorgcertificationViewModel.Myorgcertification_IEnum;
                    datacontext_collection.Insert(datacontext_collection.IndexOf(selecteditems[i]), item);
                    datacontext_collection.Remove(selecteditems[i]);
                    using (ProductListingContext context = new())
                    {
                        context.MyorgCertifications.Remove(selecteditems[i]);
                        context.MyorgCertifications.Add(item);
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

                    myorgcertificationViewModel.Myorgcertification_IEnum.Add(item);
                    var rowIndex = myorgcertificationViewModel.Myorgcertification_IEnum.IndexOf(item) + 1;
                    var colIndex = SfDataGrid1_Proxy.Columns.IndexOf(SfDataGrid1_Proxy.Columns.FirstOrDefault());
                    RowColumnIndex rowColumnIndex = new RowColumnIndex(rowIndex, colIndex);
                    await Task.Delay(100);
                    SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
                    SfDataGrid1_Proxy.SelectionController.MoveCurrentCell(rowColumnIndex);
                    SfDataGrid1_Proxy.ScrollInView(rowColumnIndex);
                    await Task.Delay(100);
                    using (ProductListingContext context = new())
                    {
                        context.MyorgCertifications.Add(item);
                        context.SaveChanges();
                    }
                    Reset();
                }

                return;
            }
        }

        public static void CustomRowValidating(
            Myorgcertification_Model rowView,
            bool IsAddNew,
            bool Programmatically,
            Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e,
            out bool success,
            out Myorgcertification_Model temp_model)
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
                    rowView.Code == null &&
                    rowView.Description == null
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
                else if (rowView.Code.ToString() == null || rowView.Code.ToString() == string.Empty)
                {
                    string message = "Column Code cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Code), Programmatically, e, success);
                    return;
                }
                else if (rowView.Code.ToString().Length > 50)
                {
                    string message = $"The value \"{rowView.Code}\" exceeds the limit of 50 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Code), Programmatically, e, success);
                    return;
                }
                else if (rowView.Description.ToString() == null || rowView.Description.ToString() == string.Empty)
                {
                    string message = "Column Description cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Description), Programmatically, e, success);
                    return;
                }
                else if (rowView.Description.ToString().Length > 50)
                {
                    string message = $"The value \"{rowView.Description}\" exceeds the limit of 50 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Description), Programmatically, e, success);
                    return;
                }
                else
                {
                    if (context.MyorgCertifications.Where(
                        i => i.Code.Equals(OldRowData.Code)
                        ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                OldRowData.Code,
                                "is duplicated!"
                                );

                        string message = joined_string;
                        success = true;
                        temp_model = rowView;
                        ShowError(message, nameof(rowView.Code), Programmatically, e, success);
                        return;
                    }
                }

                success = false;
                temp_model = rowView;
                if (IsAddNew && !Programmatically)
                {
                    context.MyorgCertifications.Add(rowView);
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
                Myorg_certification_dataTable_HasError = true;
            }
            else
            {
                e.ErrorMessages.Add(columnName, message);
                e.IsValid = !success;
                Myorg_certification_dataTable_HasError = true;
                ErrorTextBox1_Setter("show", message);
            }
            return;
        }
    }
}
