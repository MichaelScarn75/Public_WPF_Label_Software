// <copyright file="Country.xaml.cs" company="PlaceholderCompany">
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
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for Country.xaml
    /// </summary>
    public partial class Country : UserControl
    {
        public Country()
        {
            this.InitializeComponent();
        }

        private void Country_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            Country_SharedLib.countryViewModel = this.DataContext as Country_ViewModel;
            Country_SharedLib.SfDataGrid1_Proxy = this.SfDataGrid1;
            Country_SharedLib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            Country_SharedLib.ErrorTextBorder1 = this.ErrorTextBorder1;
            Country_SharedLib.ErrorTextBox1 = this.ErrorTextBox1;
            Country_SharedLib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                Country_SharedLib.Country_dataTable_HasError = true;
                Country_SharedLib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                Country_SharedLib.Country_dataTable_HasError = false;
                Country_SharedLib.ErrorTextBox1_Setter("hidden", message);
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
                    Country_SharedLib.PasteNewRowsProgrammatically == true ||
                    Country_SharedLib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                Country_SharedLib.OldRowDataIndex = index - 1;
                Country_SharedLib.OldRowData = record as Country_Model;
                Country_Model item1 = context.Countries.Where(a => a.Id.Equals(Country_SharedLib.OldRowData.Id)).Single();
                var item2 = context.Items.Where(a => a.Country.Equals(Country_SharedLib.OldRowData.Code));

                if (e.Column.MappingName == nameof(Country_SharedLib.OldRowData.Code))
                {
                    item1.Code = e.NewValue.ToString();
                    item2.ToList().ForEach(a => a.Country = e.NewValue.ToString());

                    if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                    {
                        this.SetErrorMessage(e, string.Empty, false);
                        return;
                    }
                    else if (e.NewValue == null || e.NewValue.ToString() == string.Empty)
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
                    else
                    {
                        if (e.OldValue != e.NewValue &&
                            context.Countries.Where(
                            i => i.Code.Equals(Country_SharedLib.OldRowData.Code)
                            ).Count() > 0)
                        {
                            var joined_string = System.String.Join(",",
                                                    Country_SharedLib.OldRowData.Code
                                                    );

                            this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                            return;
                        }
                    }
                }

                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                Country_SharedLib.Country_dataTable_HasError = false;
                Country_SharedLib.PasteNewRowsProgrammatically = false;
                Country_SharedLib.PasteExistingRowsProgrammatically = false;
                Country_SharedLib.OldRowData = new Country_Model();
                Country_SharedLib.OldRowData = new Country_Model();
                Country_SharedLib.OldRowDataIndex = 0;
                Country_Model rowView = e.RowData as Country_Model;
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
                    Country_SharedLib.CustomRowValidating(e.RowData as Country_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    Country_SharedLib.Delete();
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
                Country_SharedLib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RibbonTab_Home_Manage_Copy_Click error " + ex.Message);
            }
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country_SharedLib.Delete();
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
                Country_SharedLib.Paste();
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
                Country_FilterManager filtermanager = new Country_FilterManager();
                filtermanager.countryViewModel = this.SfDataGrid1.DataContext as ViewModel.Country_ViewModel;
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
                    Country_FilterManager filtermanager = new Country_FilterManager();
                    filtermanager.countryViewModel = this.SfDataGrid1.DataContext as ViewModel.Country_ViewModel;
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
                Country_FilterManager filtermanager = new Country_FilterManager();
                filtermanager.countryViewModel = this.SfDataGrid1.DataContext as ViewModel.Country_ViewModel;
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
