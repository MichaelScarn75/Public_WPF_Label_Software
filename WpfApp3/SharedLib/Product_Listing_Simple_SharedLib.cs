namespace WpfApp3.SharedLib
{
    using System.Windows;
    using System.Windows.Controls;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.Windows.Controls.Input;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    class Product_Listing_Simple_SharedLib
    {
        internal static bool SpecialPrice_dataTable_HasError = false;
        internal static bool PasteNewRowsProgrammatically = false;
        internal static bool PasteExistingRowsProgrammatically = false;
        internal static int OldRowDataIndex = 0;
        internal static Productlisting_Model OldRowData = new Productlisting_Model();
        internal static Productlisting_Model ErrorRowData = new Productlisting_Model();
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static Border ErrorTextBorder1;
        internal static SfTextBoxExt ErrorTextBox1;
        internal static Product_Listing_ViewModel productlistingViewModel;

        internal static void ErrorTextBox1_Setter(string visibility, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (visibility == "show")
                    {
                        productlistingViewModel.ErrorTextBorder1_Visibility = Visibility.Visible;
                        productlistingViewModel.ErrorTextBox1_Text = message;
                    }
                    else
                    {
                        productlistingViewModel.ErrorTextBorder1_Visibility = Visibility.Hidden;
                        productlistingViewModel.ErrorTextBox1_Text = message;
                    }
                });
        }

        internal static async void Refresh()
        {
            productlistingViewModel.Populate__productlisting_simple_view();
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
    }
}
