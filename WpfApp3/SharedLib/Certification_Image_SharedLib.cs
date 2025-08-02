namespace WpfApp3.SharedLib
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Syncfusion.UI.Xaml.Grid;
    using WpfApp3.FilterManager;
    using WpfApp3.ViewModel;

    class Certification_Image_SharedLib
    {
        internal static SfDataGrid SfDataGrid1_Proxy = new SfDataGrid();
        internal static GridCurrentCellManager cellManager;
        internal static Certification_Image_ViewModel certificationimageViewModel = new Certification_Image_ViewModel();

        internal static async void Refresh()
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            certificationimageViewModel.Populate__certification_image();
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

        internal static async void ClearFilter(Grid Detailed_Filter_Children_Grid)
        {
            if (SfDataGrid1_Proxy.View.CurrentEditItem != null)
            {
                SfDataGrid1_Proxy.View.CancelEdit();
                cellManager.EndEdit();
                await Task.Delay(100);
            }

            Certification_Image_FilterManager certification_image_filtermanager = new Certification_Image_FilterManager();
            certification_image_filtermanager.Grid1 = Detailed_Filter_Children_Grid;
            certification_image_filtermanager.SimplyRemoveAllFilter();
            certificationimageViewModel.Populate__certification_image();
        }
    }
}
