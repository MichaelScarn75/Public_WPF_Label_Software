// <copyright file="Item_Units_Of_Measure.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.View
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using Item_Lib = WpfApp3.SharedLib.Item_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Item_Units_Of_Measure.xaml.
    /// </summary>

    public partial class Item : UserControl
    {
        public Item()
        {
            this.InitializeComponent();

            // Adds Custom MultiColumnDropDown renderer to the CellRenderers.
            this.SfDataGrid1.CellRenderers.Remove("MultiColumnDropDown");
            this.SfDataGrid1.CellRenderers.Add("MultiColumnDropDown", new GridCellMultiColumnDropDownRendererExt());
        }

        private void Item_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Item_Lib.itemViewModel = this.DataContext as Item_ViewModel;
            Item_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            Item_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            Item_Lib.GridMultiColumnDropDownList_Proxy = GridMultiColumnDropdownList1;
            Item_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            Item_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            Item_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                Item_Lib.Item_dataTable_HasError = true;
                Item_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                Item_Lib.Item_dataTable_HasError = false;
                Item_Lib.ErrorTextBox1_Setter("hidden", message);
            }
        }

        private void SfDataGrid1_CurrentCellBeginEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellBeginEditEventArgs e)
        {
            try
            {
                if (MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_View_Enabled == false)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellBeginEdit error " + ex.Message);
            }
        }

        private void SfDataGrid1_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellEndEdit error " + ex.Message);
            }
        }

        private void SfDataGrid1_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
        {
            try
            {
                int index = this.SfDataGrid1.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex;

                if (this.SfDataGrid1.IsAddNewIndex(index) ||
                    Item_Lib.PasteNewRowsProgrammatically == true ||
                    Item_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                Item_Lib.OldRowDataIndex = index - 1;
                Item_Lib.OldRowData = record as item_Model;
                item_Model item1 = context.Items.Where(a => a.Id.Equals(Item_Lib.OldRowData.Id)).Single();
                var item2 = context.SpecialDiscounts.Where(a => a.ItemNo.Equals(Item_Lib.OldRowData.ItemNo));
                var item3 = context.SpecialPrices.Where(a => a.ItemNo.Equals(Item_Lib.OldRowData.ItemNo));
                var item4 = context.ItemImages.Where(a => a.Code.Equals(Item_Lib.OldRowData.ItemNo));
                var item5 = context.ItemUnitsOfMeasures.Where(a => a.ItemNo.Equals(Item_Lib.OldRowData.ItemNo));

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                if (e.Column.MappingName == nameof(Item_Lib.OldRowData.ItemNo))
                {
                    item1.ItemNo = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());
                    item3.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());
                    item4.ToList().ForEach(a => a.Code = e.NewValue.ToString());
                    item5.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());

                    if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }
                else if (e.Column.MappingName == nameof(Item_Lib.OldRowData.Description))
                {
                    item1.Description = e.NewValue.ToString();

                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 50)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 50 characters!", true);
                        return;
                    }
                    // sql returns 1 or more results, old value != new value
                    else if (e.OldValue != e.NewValue &&
                            context.Items.Where(
                                i => i.ItemNo.Equals(item1.ItemNo)
                                ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                item1.ItemNo
                                );

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                }
                else if (e.Column.MappingName == nameof(Item_Lib.OldRowData.InventoryPostingGroup))
                {
                    item1.InventoryPostingGroup = e.NewValue.ToString();

                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 20)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 20 characters!", true);
                        return;
                    }
                    else if (context.InventoryPostingGroups.Where(i => i.Code.Equals(e.NewValue)).Count() == 0)
                    {
                        this.SetErrorMessage(e, "Value doesn't exist in Inventory Posting Group table!", true);
                        return;
                    }
                }
                else if (e.Column.MappingName == nameof(Item_Lib.OldRowData.Country))
                {
                    item1.Country = e.NewValue.ToString();

                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 12)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 12 characters!", true);
                        return;
                    }
                    else if (context.Countries.Where(i => i.Code.Equals(e.NewValue)).Count() == 0)
                    {
                        this.SetErrorMessage(e, "Value doesn't exist in Country table!", true);
                        return;
                    }
                }

                // if no error
                // Update block, after validation is passed
                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                Item_Lib.Item_dataTable_HasError = false;
                Item_Lib.PasteNewRowsProgrammatically = false;
                Item_Lib.PasteExistingRowsProgrammatically = false;
                Item_Lib.OldRowData = new item_Model();
                Item_Lib.OldRowData = new item_Model();
                Item_Lib.OldRowDataIndex = 0;
                item_Model rowView = e.RowData as item_Model;
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellValidating error: " + ex.Message);
            }
        }

        public void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e)
        {
            try
            {
                if (SfDataGrid1.GetAddNewRowController().GetAddNewRowIndex() == e.RowIndex)
                {
                    Item_Lib.CustomRowValidating(e.RowData as item_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_RowValidating error: " + ex.Message);
            }
        }

        private async void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete) ||
                    e.Key == Key.Delete)
                {
                    Item_Lib.Delete();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_PreviewKeyDown error " + ex.Message);
            }
        }

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
            }
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item_Lib.Delete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteRow_Click error " + ex.Message);
            }
        }

        private void PasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item_Lib.Paste();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PasteRow_Click error " + ex.Message);
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
                Item_FilterManager filtermanager = new Item_FilterManager();
                filtermanager.ItemViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_ViewModel;
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
                    Item_FilterManager filtermanager = new Item_FilterManager();
                    filtermanager.ItemViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_ViewModel;
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
                Item_FilterManager filtermanager = new Item_FilterManager();
                filtermanager.ItemViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_ViewModel;
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
    }
}
