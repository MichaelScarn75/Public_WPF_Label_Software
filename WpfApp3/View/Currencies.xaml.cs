// <copyright file="Currencies.xaml.cs" company="PlaceholderCompany">
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
    using WpfApp3.SharedLib;
    using WpfApp3.ViewModel;
    using CU_Lib = WpfApp3.SharedLib.Currencies_SharedLib;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Currencies.xaml
    /// </summary>
    public partial class Currencies : UserControl
    {
        public Currencies()
        {
            this.InitializeComponent();
        }

        private void Currencies_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            CU_Lib.currenciesViewModel = this.DataContext as Currencies_ViewModel;
            CU_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CU_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            CU_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            CU_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            CU_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                CU_Lib.Currencies_dataTable_HasError = true;
                CU_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                CU_Lib.Currencies_dataTable_HasError = false;
                CU_Lib.ErrorTextBox1_Setter("hidden", message);
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

                if (this.SfDataGrid1.IsAddNewIndex(this.SfDataGrid1.SelectionController.CurrentCellManager.CurrentRowColumnIndex.RowIndex) ||
                    CU_Lib.PasteNewRowsProgrammatically == true ||
                    CU_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                CU_Lib.OldRowDataIndex = index - 1;
                CU_Lib.OldRowData = record as Currencies_Model;
                Currencies_Model item1 = context.Currencies.Where(a => a.Id.Equals(CU_Lib.OldRowData.Id)).Single();
                var item2 = context.SpecialPrices.Where(a => a.CurrencyCode.Equals(CU_Lib.OldRowData.Code));

                if (e.Column.MappingName == nameof(CU_Lib.OldRowData.Code))
                {
                    item1.Code = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.CurrencyCode = e.NewValue.ToString());

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

                    // sql returns 1 or more results, old value != new value
                    else
                    {
                        if (e.OldValue != e.NewValue &&
                            context.Currencies.Where(
                            i => i.Code.Equals(CU_Lib.OldRowData.Code)
                            ).Count() > 0)
                        {
                            var joined_string = System.String.Join(",",
                                                    CU_Lib.OldRowData.Code
                                                    );

                            this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                            return;
                        }
                    }
                }

                else if (e.Column.MappingName == nameof(CU_Lib.OldRowData.Description))
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
                    else if (e.NewValue.ToString().Length > 50)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 50 characters!", true);
                        return;
                    }
                }

                else if (e.Column.MappingName == nameof(CU_Lib.OldRowData.Symbol))
                {
                    item1.Symbol = e.NewValue.ToString();

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

                else if (e.Column.MappingName == nameof(CU_Lib.OldRowData.Exchange_Rate))
                {
                    item1.Exchange_Rate = IUOM_Lib.CustomDecimal(e.NewValue.ToString());
                }

                else if (e.Column.MappingName == nameof(CU_Lib.OldRowData.ExchangeRateDate))
                {
                    item1.ExchangeRateDate = IUOM_Lib.CustomDateTime(e.NewValue.ToString());
                }

                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                CU_Lib.Currencies_dataTable_HasError = false;
                CU_Lib.PasteNewRowsProgrammatically = false;
                CU_Lib.PasteExistingRowsProgrammatically = false;
                CU_Lib.OldRowData = new Currencies_Model();
                CU_Lib.OldRowData = new Currencies_Model();
                CU_Lib.OldRowDataIndex = 0;
                Currencies_Model rowView = e.RowData as Currencies_Model;
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
                    Currencies_SharedLib.CustomRowValidating(e.RowData as Currencies_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_RowValidating error: " + ex.Message + "\n" + ex.ToString());
            }
        }

        private async void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete) ||
                    e.Key == Key.Delete)
                {
                    CU_Lib.Delete();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SfDataGrid1_PreviewKeyDown error " + ex.Message);
            }
        }

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            CU_Lib.Copy();
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CU_Lib.Delete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Manage_Delete_Click error " + ex.Message);
            }
        }

        private async void PasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CU_Lib.Paste();
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
                Currencies_FilterManager filtermanager = new Currencies_FilterManager();
                filtermanager.currenciesViewModel = this.SfDataGrid1.DataContext as ViewModel.Currencies_ViewModel;
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
                    Currencies_FilterManager filtermanager = new Currencies_FilterManager();
                    filtermanager.currenciesViewModel = this.SfDataGrid1.DataContext as ViewModel.Currencies_ViewModel;
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
                Currencies_FilterManager filtermanager = new Currencies_FilterManager();
                filtermanager.currenciesViewModel = this.SfDataGrid1.DataContext as ViewModel.Currencies_ViewModel;
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
