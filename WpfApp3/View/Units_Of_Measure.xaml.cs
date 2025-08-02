// <copyright file="Units_Of_Measure.xaml.cs" company="PlaceholderCompany">
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
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;
    using UOM_Lib = WpfApp3.SharedLib.Units_Of_Measure_SharedLib;

    /// <summary>
    /// Interaction logic for InventoryPostingGroup.xaml
    /// </summary>
    public partial class Units_Of_Measure : UserControl
    {
        public Units_Of_Measure()
        {
            this.InitializeComponent();
        }

        private void Units_Of_Measure_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            UOM_Lib.unitsofmeasureViewModel = this.DataContext as Units_Of_Measure_ViewModel;
            UOM_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            UOM_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            UOM_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            UOM_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            UOM_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                UOM_Lib.Unitsofmeasure_dataTable_HasError = true;
                UOM_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                UOM_Lib.Unitsofmeasure_dataTable_HasError = false;
                UOM_Lib.ErrorTextBox1_Setter("hidden", message);
            }
        }

        private void SfDataGrid1_CurrentCellBeginEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellBeginEditEventArgs e)
        {
            try
            {
                if (MW_Lib.MainWindow_ViewModel_Proxy.RibbonTab_Home_Manage_View_Enabled == false)
                {
                    e.Cancel = true;
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

                if (this.SfDataGrid1.IsAddNewIndex(this.SfDataGrid1.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex) ||
                    UOM_Lib.PasteNewRowsProgrammatically == true ||
                    UOM_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                UOM_Lib.OldRowDataIndex = index - 1;
                UOM_Lib.OldRowData = record as Unitsofmeasure_Model;
                Unitsofmeasure_Model item1 = context.UnitsOfMeasures.Where(a => a.Id.Equals(UOM_Lib.OldRowData.Id)).Single();
                var item2 = context.ItemUnitsOfMeasures.Where(a => a.UnitOfMeasureCode.Equals(UOM_Lib.OldRowData.Code));
                var item3 = context.SpecialDiscounts.Where(a => a.UnitOfMeasureCode.Equals(UOM_Lib.OldRowData.Code));
                var item4 = context.SpecialPrices.Where(a => a.UnitOfMeasureCode.Equals(UOM_Lib.OldRowData.Code));

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                if (e.Column.MappingName == nameof(item1.Code))
                {
                    item1.Code = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.UnitOfMeasureCode = e.NewValue.ToString());
                    item3.ToList().ForEach(a => a.UnitOfMeasureCode = e.NewValue.ToString());
                    item4.ToList().ForEach(a => a.UnitOfMeasureCode = e.NewValue.ToString());

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
                }
                else if (e.Column.MappingName == nameof(item1.Description))
                {
                    item1.Description = e.NewValue.ToString();

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

                    else if (e.NewValue.ToString().Length > 10)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 10 characters!", true);
                        return;
                    }
                }
                else
                {
                    if (e.OldValue != e.NewValue &&
                            context.UnitsOfMeasures.Where(i => i.Code.Equals(item1.Code)).Count() > 0)
                    {
                        var joined_string = System.String.Join(",", item1.Code);

                        this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                        return;
                    }
                }

                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                UOM_Lib.Unitsofmeasure_dataTable_HasError = false;
                UOM_Lib.PasteNewRowsProgrammatically = false;
                UOM_Lib.PasteExistingRowsProgrammatically = false;
                UOM_Lib.OldRowData = new Unitsofmeasure_Model();
                UOM_Lib.OldRowData = new Unitsofmeasure_Model();
                UOM_Lib.OldRowDataIndex = 0;
                Unitsofmeasure_Model rowView = e.RowData as Unitsofmeasure_Model;
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_CurrentCellValidating error: " + ex.Message);
            }
        }

        private void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e)
        {
            try
            {
                if (SfDataGrid1.GetAddNewRowController().GetAddNewRowIndex() == e.RowIndex)
                {
                    UOM_Lib.CustomRowValidating(e.RowData as Unitsofmeasure_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    UOM_Lib.Delete();
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
                UOM_Lib.Copy();
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
                UOM_Lib.Delete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteRow_Click error " + ex.Message);
            }
        }

        private async void PasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UOM_Lib.Paste();
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
                Units_Of_Measure_FilterManager filtermanager = new Units_Of_Measure_FilterManager();
                filtermanager.unitsofmeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Units_Of_Measure_ViewModel;
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
                    Units_Of_Measure_FilterManager filtermanager = new Units_Of_Measure_FilterManager();
                    filtermanager.unitsofmeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Units_Of_Measure_ViewModel;
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
                Units_Of_Measure_FilterManager filtermanager = new Units_Of_Measure_FilterManager();
                filtermanager.unitsofmeasureViewModel = this.SfDataGrid1.DataContext as ViewModel.Units_Of_Measure_ViewModel;
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
