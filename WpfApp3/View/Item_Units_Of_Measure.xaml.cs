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
    using BenchmarkDotNet.Attributes;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Item_Units_Of_Measure.xaml.
    /// </summary>

    public partial class Item_Units_Of_Measure : UserControl
    {
        public Item_Units_Of_Measure()
        {
            this.InitializeComponent();

            // Adds Custom MultiColumnDropDown renderer to the CellRenderers.
            this.SfDataGrid1.CellRenderers.Remove("MultiColumnDropDown");
            this.SfDataGrid1.CellRenderers.Add("MultiColumnDropDown", new GridCellMultiColumnDropDownRendererExt());
        }

        private void Item_Units_Of_Measure_UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IUOM_Lib.itemunitsofmeasureViewModel = this.DataContext as Item_Units_Of_Measure_ViewModel;
            IUOM_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            IUOM_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            IUOM_Lib.GridMultiColumnDropDownList_Proxy = GridMultiColumnDropdownList1;
            IUOM_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            IUOM_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            IUOM_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                IUOM_Lib.Itemunitsofmeasure_dataTable_HasError = true;
                IUOM_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                IUOM_Lib.Itemunitsofmeasure_dataTable_HasError = false;
                IUOM_Lib.ErrorTextBox1_Setter("hidden", message);
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
                    IUOM_Lib.PasteNewRowsProgrammatically == true ||
                    IUOM_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index) as Itemunitsofmeasure_Model;
                var context = new ProductListingContext();
                IUOM_Lib.OldRowDataIndex = index - 1;
                IUOM_Lib.OldRowData = record as Itemunitsofmeasure_Model;
                Itemunitsofmeasure_Model item1 = context.ItemUnitsOfMeasures.Where(a => a.Id.Equals(record.Id)).Single();
                var item2 = context.Items.Where(a => a.ItemNo.Equals(IUOM_Lib.OldRowData.ItemNo));
                var item3 = context.ItemImages.Where(a => a.Code.Equals(IUOM_Lib.OldRowData.ItemNo));
                var item4 = context.SpecialDiscounts.Where(a => a.ItemNo.Equals(IUOM_Lib.OldRowData.ItemNo));
                var item5 = context.SpecialPrices.Where(a => a.ItemNo.Equals(IUOM_Lib.OldRowData.ItemNo));

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                if (e.Column.MappingName == nameof(item1.ItemNo))
                {
                    item1.ItemNo = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());
                    item3.ToList().ForEach(a => a.Code = e.NewValue.ToString());
                    item4.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());
                    item5.ToList().ForEach(a => a.ItemNo = e.NewValue.ToString());

                    if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                    // sql returns 1 or more results, old value != new value
                    else if (e.OldValue != e.NewValue &&
                        context.ItemUnitsOfMeasures.Where(
                        i => i.ItemNo.Equals(item1.ItemNo) &&
                        i.UnitOfMeasureCode.Equals(item1.UnitOfMeasureCode)
                        ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                                item1.ItemNo,
                                                item1.UnitOfMeasureCode);

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.Items.Where(
                        i => i.ItemNo.Equals(item1.ItemNo)
                        ).Count() == 0)
                    {
                        var joined_string = System.String.Join(",", item1.ItemNo);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.Items)}!", true);
                        return;
                    }
                }
                else if (e.Column.MappingName == nameof(item1.UnitOfMeasureCode))
                {
                    item1.UnitOfMeasureCode = e.NewValue.ToString();

                    if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                    {
                        this.SetErrorMessage(e, string.Empty, false);
                        return;
                    }
                    else if (e.NewValue.ToString() == null || e.NewValue.ToString() == string.Empty)
                    {
                        this.SetErrorMessage(e, "Value cannot be null!", true);
                        return;
                    }

                    if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                    else if (context.UnitsOfMeasures.Where(i => i.Code.Equals(e.NewValue)).Count() == 0)
                    {
                        this.SetErrorMessage(e, "Value doesn't exist in Item Units Of Measure table!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.ItemUnitsOfMeasures.Where(
                        i => i.ItemNo.Equals(item1.ItemNo) &&
                        i.UnitOfMeasureCode.Equals(item1.UnitOfMeasureCode)
                        ).Count() > 0)
                    {
                        var joined_string = System.String.Join(",",
                                                item1.ItemNo,
                                                item1.UnitOfMeasureCode);

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                    else if (e.OldValue != e.NewValue &&
                        context.UnitsOfMeasures.Where(
                        i => i.Code.Equals(item1.UnitOfMeasureCode)
                        ).Count() == 0)
                    {
                        var joined_string = System.String.Join(",", item1.UnitOfMeasureCode);

                        this.SetErrorMessage(e, $"{joined_string} doesn't exist in {nameof(context.UnitsOfMeasures)}!", true);
                        return;
                    }
                }
                else if (e.Column.MappingName == nameof(item1.QtyPerUnitOfMeasure))
                {
                    item1.QtyPerUnitOfMeasure = IUOM_Lib.CustomDecimal(e.NewValue.ToString());

                    if (e.NewValue == null)
                    {
                        e.NewValue = 0.00000;
                        this.SetErrorMessage(e, string.Empty, false);
                    }
                    else if (e.NewValue.ToString().Length > 64)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 64 characters!", true);
                        return;
                    }
                }

                // if no error
                // Update block, after validation is passed
                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                IUOM_Lib.Reset();
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellValidating error: " + ex.Message);
            }
        }


        [Benchmark]
        public void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e)
        {
            try
            {
                if (SfDataGrid1.GetAddNewRowController().GetAddNewRowIndex() == e.RowIndex)
                {
                    IUOM_Lib.CustomRowValidating(e.RowData as Itemunitsofmeasure_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    IUOM_Lib.Delete();
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
                IUOM_Lib.Copy();
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
                IUOM_Lib.Delete();
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
                IUOM_Lib.Paste();
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
                Item_Units_Of_Measure_FilterManager filtermanager = new Item_Units_Of_Measure_FilterManager();
                filtermanager.ItemUnitsOfMeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Units_Of_Measure_ViewModel;
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
                    Item_Units_Of_Measure_FilterManager filtermanager = new Item_Units_Of_Measure_FilterManager();
                    filtermanager.ItemUnitsOfMeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Units_Of_Measure_ViewModel;
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
                Item_Units_Of_Measure_FilterManager filtermanager = new Item_Units_Of_Measure_FilterManager();
                filtermanager.ItemUnitsOfMeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Item_Units_Of_Measure_ViewModel;
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

    public class GridCellMultiColumnDropDownRendererExt : Syncfusion.UI.Xaml.Grid.Cells.GridCellMultiColumnDropDownRenderer
    {
        protected override void OnEditElementLoaded(object sender, RoutedEventArgs e)
        {
            // Enables the AllowImmediatePopup and IsDropDownOpen when set to true
            (sender as SfMultiColumnDropDownControl).AllowImmediatePopup = true;
            (sender as SfMultiColumnDropDownControl).IsDropDownOpen = true;

            // Assigns the input text to the SfMultiColumnDropDownControl's text property
            (sender as SfMultiColumnDropDownControl).Text = this.PreviewInputText;
            base.OnEditElementLoaded(sender, e);
        }
    }
}
