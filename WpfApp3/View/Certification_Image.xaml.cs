using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.ScrollAxis;
using WpfApp3.FilterManager;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using CI_Lib = WpfApp3.SharedLib.Certification_Image_SharedLib;
using II_Lib = WpfApp3.SharedLib.Item_Image_Actual_SharedLib;
using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Certification_Image.xaml
    /// </summary>
    public partial class Certification_Image : UserControl
    {
        public Certification_Image()
        {
            InitializeComponent();
        }

        private void Certification_Image_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            CI_Lib.certificationimageViewModel = this.DataContext as Certification_Image_ViewModel;
            CI_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CI_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
        }

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                II_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
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
                Certification_Image_FilterManager filtermanager = new Certification_Image_FilterManager();
                filtermanager.certificationimageViewModel = this.SfDataGrid1.DataContext as ViewModel.Certification_Image_ViewModel;
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
                    Certification_Image_FilterManager filtermanager = new Certification_Image_FilterManager();
                    filtermanager.certificationimageViewModel = this.SfDataGrid1.DataContext as Certification_Image_ViewModel;
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
                Certification_Image_FilterManager filtermanager = new Certification_Image_FilterManager();
                filtermanager.certificationimageViewModel = this.SfDataGrid1.DataContext as Certification_Image_ViewModel;
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

        private string Dialog()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            dlg.Multiselect = false;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                return dlg.FileName;
            }

            return string.Empty;
        }

        private void SfDataGrid1_CellTapped(object sender, Syncfusion.UI.Xaml.Grid.GridCellTappedEventArgs e)
        {
            Import_Button.IsEnabled = true;
            var item = e.Record as certification_image_Model;
            ImageControl1.Source = IUOM_Lib.CustomBitmapImage(item.Image);
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            string result = Dialog();
            if (result == string.Empty)
            {
                return;
            }

            var index = SfDataGrid1.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex;
            var item = SfDataGrid1.View.GetRecordAt(index - 1).Data as certification_image_Model;
            item.Image = IUOM_Lib.CustomByte(result);
            ImageControl1.Source = IUOM_Lib.CustomBitmapImage(item.Image);
            result = result.Replace("\\", "\\\\");
            using (ProductListingContext context = new())
            {
                context.Database.ExecuteSqlRaw(
                    $"UPDATE certification_image SET {nameof(item.Image)} = LOAD_FILE(\"{result}\") WHERE {nameof(item.Code)} = \"{item.Code}\";"
                );
                MessageBox.Show("Successfully added image!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
