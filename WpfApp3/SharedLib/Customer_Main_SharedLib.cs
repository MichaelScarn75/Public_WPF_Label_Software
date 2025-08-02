namespace WpfApp3.SharedLib
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
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

    class Customer_Main_SharedLib
    {
        internal static bool Choosecustomer_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex;
        internal static Customermain_Model OldRowData = new Customermain_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static Customer_Main_ViewModel choosecustomerViewModel = new Customer_Main_ViewModel();

        internal static void ErrorTextBox1_Setter(string visibility, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (visibility == "show")
                    {
                        choosecustomerViewModel.ErrorTextBorder1_Visibility = Visibility.Visible;
                        choosecustomerViewModel.ErrorTextBox1_Text = message;
                    }
                    else
                    {
                        choosecustomerViewModel.ErrorTextBorder1_Visibility = Visibility.Hidden;
                        choosecustomerViewModel.ErrorTextBox1_Text = message;
                    }
                });
        }

        internal static void Reset()
        {
            Choosecustomer_dataTable_HasError = false;
            PasteNewRowsProgrammatically = false;
            PasteExistingRowsProgrammatically = false;
            OldRowData = new Customermain_Model();
            OldRowData = new Customermain_Model();
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

            choosecustomerViewModel.Populate__choosecustomer();
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
            ViewModel.Customer_Main_ViewModel viewModel1 = SfDataGrid1_Proxy.DataContext as ViewModel.Customer_Main_ViewModel;
            List<Customermain_Model> todelete = new();

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

            var toreturn = Choosecustomer_dataTable_HasError;

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
                foreach (Customermain_Model item in SfDataGrid1_Proxy.SelectedItems)
                {
                    todelete.Add(item);
                }

                foreach (Customermain_Model item in todelete)
                {
                    if (context.SpecialPrices.Where(a => a.SalesCode.Equals(item.CustomerId)).Count() > 0)
                    {
                        MessageBox.Show($"{nameof(item.CustomerId)} exists in {nameof(context.SpecialPrices)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    viewModel1.Choosecustomer_IEnum.Remove(item);
                    context.Customers.Remove(item);
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

            if (Choosecustomer_dataTable_HasError)
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
            if (Choosecustomer_dataTable_HasError == true)
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

            Customer_Main_FilterManager choose_customer_filtermanager = new Customer_Main_FilterManager();
            choose_customer_filtermanager.Grid1 = Detailed_Filter_Children_Grid;
            choose_customer_filtermanager.SimplyRemoveAllFilter();
            choosecustomerViewModel.Populate__choosecustomer();
        }

        public static async void Paste()
        {
            if (Choosecustomer_dataTable_HasError)
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
            Customermain_Model model = new();
            ObservableCollection<Customermain_Model> clipboard_collection = new();

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
                List<Customermain_Model> selecteditems = new();
                foreach (var item in SfDataGrid1_Proxy.SelectedItems)
                {
                    selecteditems.Add(item as Customermain_Model);
                }

                for (int i = 0; i < clipboard_collection.Count; i++)
                {
                    var temp_model = clipboard_collection[i];
                    CustomRowValidating(temp_model, true, true, null, out var success, out var item);

                    if (success)
                    {
                        break;
                    }

                    var datacontext_collection = choosecustomerViewModel.Choosecustomer_IEnum;
                    datacontext_collection.Insert(datacontext_collection.IndexOf(selecteditems[i]), item);
                    datacontext_collection.Remove(selecteditems[i]);
                    using (ProductListingContext context = new())
                    {
                        context.Customers.Remove(selecteditems[i]);
                        context.Customers.Add(item);
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

                    choosecustomerViewModel.Choosecustomer_IEnum.Add(item);
                    var rowIndex = choosecustomerViewModel.Choosecustomer_IEnum.IndexOf(item) + 1;
                    var colIndex = SfDataGrid1_Proxy.Columns.IndexOf(SfDataGrid1_Proxy.Columns.FirstOrDefault());
                    RowColumnIndex rowColumnIndex = new RowColumnIndex(rowIndex, colIndex);
                    await Task.Delay(100);
                    SfDataGrid1_Proxy.SelectionController.ClearSelections(false);
                    SfDataGrid1_Proxy.SelectionController.MoveCurrentCell(rowColumnIndex);
                    SfDataGrid1_Proxy.ScrollInView(rowColumnIndex);
                    await Task.Delay(100);
                    using (ProductListingContext context = new())
                    {
                        context.Customers.Add(item);
                        context.SaveChanges();
                    }
                    Reset();
                }

                return;
            }
        }

        public static void CustomRowValidating(
            Customermain_Model rowView,
            bool IsAddNew,
            bool Programmatically,
            Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e,
            out bool success,
            out Customermain_Model temp_model)
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
                    rowView.CustomerId == null &&
                    rowView.Description == null &&
                    rowView.CustomerLabelCode == null &&
                    rowView.Address1 == null &&
                    rowView.Address2 == null &&
                    rowView.Address3 == null &&
                    rowView.ContactPerson == null &&
                    rowView.PostalCode == null &&
                    rowView.Phone1 == null &&
                    rowView.Phone2 == null &&
                    rowView.Fax == null &&
                    rowView.Email == null &&
                    rowView.Website == null &&
                    rowView.GSTRegNo == null &&
                    rowView.CompanyRegNo == null &&
                    rowView.VehicleNo == null &&
                    rowView.LabelStyle == null
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

                if (rowView.CustomerId.ToString() == null || rowView.CustomerId.ToString() == string.Empty)
                {
                    string message = $"Column Customer ID cannot be null";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.CustomerId), Programmatically, e, success);
                    return;
                }

                else if (rowView.CustomerId != null && rowView.CustomerId.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.CustomerId}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.CustomerId), Programmatically, e, success);
                    return;
                }

                else if (rowView.Description != null && rowView.Description.ToString().Length > 40)
                {
                    string message = $"The value \"{rowView.Description}\" exceeds the limit of 40 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Description), Programmatically, e, success);
                    return;
                }

                else if (rowView.CustomerLabelCode != null && rowView.CustomerLabelCode.ToString().Length > 2)
                {
                    string message = $"The value \"{rowView.CustomerLabelCode}\" exceeds the limit of 2 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.CustomerLabelCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.Address1 != null && rowView.Address1.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.Address1}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Address1), Programmatically, e, success);
                    return;
                }

                else if (rowView.Address2 != null && rowView.Address2.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.Address2}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Address2), Programmatically, e, success);
                    return;
                }

                else if (rowView.Address3 != null && rowView.Address3.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.Address3}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Address3), Programmatically, e, success);
                    return;
                }

                else if (rowView.ContactPerson != null && rowView.ContactPerson.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.ContactPerson}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.ContactPerson), Programmatically, e, success);
                    return;
                }

                else if (rowView.PostalCode != null && rowView.PostalCode.ToString().Length > 10)
                {
                    string message = $"The value \"{rowView.PostalCode}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.PostalCode), Programmatically, e, success);
                    return;
                }

                else if (rowView.Phone1 != null && rowView.Phone1.ToString().Length > 20)
                {
                    string message = $"The value \"{rowView.Phone1}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Phone1), Programmatically, e, success);
                    return;
                }

                else if (rowView.Phone2 != null && rowView.Phone2.ToString().Length > 20)
                {
                    string message = $"The value \"{rowView.Phone2}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Phone2), Programmatically, e, success);
                    return;
                }

                else if (rowView.Fax != null && rowView.Fax.ToString().Length > 20)
                {
                    string message = $"The value \"{rowView.Fax}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Fax), Programmatically, e, success);
                    return;
                }

                else if (rowView.Email != null && rowView.Email.ToString().Length > 30)
                {
                    string message = $"The value \"{rowView.Email}\" exceeds the limit of 30 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Email), Programmatically, e, success);
                    return;
                }

                else if (rowView.Website != null && rowView.Website.ToString().Length > 80)
                {
                    string message = $"The value \"{rowView.Website}\" exceeds the limit of 80 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.Website), Programmatically, e, success);
                    return;
                }

                else if (rowView.GSTRegNo != null && rowView.GSTRegNo.ToString().Length > 20)
                {
                    string message = $"The value \"{rowView.GSTRegNo}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.GSTRegNo), Programmatically, e, success);
                    return;
                }

                else if (rowView.CompanyRegNo != null && rowView.CompanyRegNo.ToString().Length > 20)
                {
                    string message = $"The value \"{rowView.CompanyRegNo}\" exceeds the limit of 20 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.CompanyRegNo), Programmatically, e, success);
                    return;
                }

                else if (rowView.VehicleNo != null && rowView.VehicleNo.ToString().Length > 10)
                {
                    string message = $"The value \"{rowView.VehicleNo}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.VehicleNo), Programmatically, e, success);
                    return;
                }

                else if (rowView.LabelStyle != null && rowView.LabelStyle.ToString().Length > 10)
                {
                    string message = $"The value \"{rowView.LabelStyle}\" exceeds the limit of 10 characters!";
                    success = true;
                    temp_model = rowView;
                    ShowError(message, nameof(rowView.LabelStyle), Programmatically, e, success);
                    return;
                }
                else
                {
                    if (context.Customers.Where(
                        i => i.CustomerId.Equals(OldRowData.CustomerId)
                        ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                OldRowData.CustomerId,
                                "is duplicated!"
                                );

                        string message = joined_string;
                        success = true;
                        temp_model = rowView;
                        ShowError(message, nameof(rowView.CustomerId), Programmatically, e, success);
                        return;
                    }
                }

                success = false;
                temp_model = rowView;
                if (IsAddNew && !Programmatically)
                {
                    context.Customers.Add(rowView);
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
                Choosecustomer_dataTable_HasError = true;
            }
            else
            {
                e.ErrorMessages.Add(columnName, message);
                e.IsValid = !success;
                Choosecustomer_dataTable_HasError = true;
                ErrorTextBox1_Setter("show", message);
            }
            return;
        }
    }
}
