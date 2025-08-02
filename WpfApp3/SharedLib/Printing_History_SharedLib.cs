namespace WpfApp3.SharedLib
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.Windows.Controls.Input;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    class Printing_History_SharedLib
    {
        internal static bool PrintingHistory_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex = 0;
        internal static PrintingHistory_Model OldRowData = new PrintingHistory_Model();
        internal static PrintingHistory_Model ErrorRowData = new PrintingHistory_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static Weigh_Form_ViewModel weighformViewModel = new Weigh_Form_ViewModel();
        internal static async void Refresh()
        {
            weighformViewModel.populate__printinghistory();
        }

        internal static void Delete()
        {
            var cellManager = SfDataGrid1_Proxy.SelectionController.CurrentCellManager;
            Weigh_Form_ViewModel viewModel1 = SfDataGrid1_Proxy.DataContext as Weigh_Form_ViewModel;
            List<PrintingHistory_Model> todelete = new();

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

            using (ProductListingContext context = new())
            {
                foreach (PrintingHistory_Model item in SfDataGrid1_Proxy.SelectedItems)
                {
                    todelete.Add(item);
                }

                foreach (PrintingHistory_Model item in todelete)
                {
                    viewModel1.PrintingHistory_IEnum.Remove(item);
                    context.PrintingHistories.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        internal static void Copy()
        {
            if (SfDataGrid1_Proxy.SelectedItems.Count == 0 && SfDataGrid1_Proxy.SelectionController.CurrentCellManager.HasCurrentCell == false)
            {
                MessageBoxResult result1 = MessageBox.Show("No row(s) selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SfDataGrid1_Proxy.GridCopyPaste.Copy();
            return;
        }
    }
}
