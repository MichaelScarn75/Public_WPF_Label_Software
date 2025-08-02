// <copyright file="Units_Of_Measure.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.View
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using Microsoft.EntityFrameworkCore;
    using Syncfusion.UI.Xaml.Charts;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using II_Lib = WpfApp3.SharedLib.Item_Image_Actual_SharedLib;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

    /// <summary>
    /// Interaction logic for InventoryPostingGroup.xaml
    /// </summary>
    public partial class Item_Image : UserControl
    {
        public Item_Image()
        {
            this.InitializeComponent();
        }

        private void Item_Image_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            II_Lib.itemimageactualViewModel = this.DataContext as Item_Image_Actual_ViewModel;
            II_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            II_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;

            var filepath = Path.Combine(AppContext.BaseDirectory, "img");

            /*
            foreach (var item in II_Lib.itemimageactualViewModel.Item_Image_Actual_IEnum)
            {
                byte[] bitmap = item.Image;
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bitmap)))
                {
                    image.Save($"{filepath}\\{item.Code}.png", ImageFormat.Png);  // Or Png
                }
            }
            */
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
                Item_Image_Actual_FilterManager filtermanager = new Item_Image_Actual_FilterManager();
                filtermanager.itemimageactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Image_Actual_ViewModel;
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
                    Item_Image_Actual_FilterManager filtermanager = new Item_Image_Actual_FilterManager();
                    filtermanager.itemimageactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Image_Actual_ViewModel;
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
                Item_Image_Actual_FilterManager filtermanager = new Item_Image_Actual_FilterManager();
                filtermanager.itemimageactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Image_Actual_ViewModel;
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

        private void SfDataGrid1_CellTapped(object sender, GridCellTappedEventArgs e)
        {
            Import_Button.IsEnabled = true;
            var item = e.Record as item_image_actual_Model;
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
            var item = SfDataGrid1.View.GetRecordAt(index - 1).Data as item_image_actual_Model;
            item.Image = IUOM_Lib.CustomByte(result);
            ImageControl1.Source = IUOM_Lib.CustomBitmapImage(item.Image);
            result = result.Replace("\\", "\\\\");
            using (ProductListingContext context = new())
            {
                context.Database.ExecuteSqlRaw(
                    $"UPDATE item_image SET {nameof(item.Image)} = LOAD_FILE(\"{result}\") WHERE {nameof(item.Code)} = \"{item.Code}\";"
                );
                //MessageBox.Show("Successfully added image!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
