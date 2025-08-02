namespace WpfApp3.SharedLib
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    class Item_Image_Actual_SharedLib
    {
        internal static bool Item_Image_Actual_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex = 0;
        internal static item_image_actual_Model OldRowData = new item_image_actual_Model();
        internal static item_image_actual_Model ErrorRowData = new item_image_actual_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static Item_Image_Actual_ViewModel itemimageactualViewModel = new Item_Image_Actual_ViewModel();

        internal static async void Refresh()
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            itemimageactualViewModel.Populate__item_image_actual();
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

        internal static void ClearFilter(Grid Detailed_Filter_Children_Grid)
        {
            if (Item_Image_Actual_dataTable_HasError == true)
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
            itemimageactualViewModel.Populate__item_image_actual();
        }
    }
}
