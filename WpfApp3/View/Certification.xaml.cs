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
    using Microsoft.EntityFrameworkCore;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using WpfApp3.FilterManager;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;
    using CA_Lib = WpfApp3.SharedLib.Certification_Actual_SharedLib;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    /// <summary>
    /// Interaction logic for InventoryPostingGroup.xaml
    /// </summary>
    public partial class Certification : UserControl
    {
        public Certification()
        {
            this.InitializeComponent();
        }

        private void Certification_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            CA_Lib.certificationactualViewModel = this.DataContext as Certification_Actual_ViewModel;
            CA_Lib.SfDataGrid1_Proxy = this.SfDataGrid1;
            CA_Lib.cellManager = this.SfDataGrid1.SelectionController.CurrentCellManager;
            CA_Lib.ErrorTextBorder1 = this.ErrorTextBorder1;
            CA_Lib.ErrorTextBox1 = this.ErrorTextBox1;
            CA_Lib.ErrorTextBox1_Setter("hidden", string.Empty);
        }

        // Helper to set the error message and validation status
        private void SetErrorMessage(Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e, string message, bool hasError)
        {
            if (hasError)
            {
                e.ErrorMessage = message;
                e.IsValid = false;
                CA_Lib.Certification_Actual_dataTable_HasError = true;
                CA_Lib.ErrorTextBox1_Setter("show", message);
            }
            else
            {
                e.IsValid = true;
                CA_Lib.Certification_Actual_dataTable_HasError = false;
                CA_Lib.ErrorTextBox1_Setter("hidden", message);
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

                if (CA_Lib.PasteNewRowsProgrammatically && e.RowColumnIndex.RowIndex == SfDataGrid1.GetAddNewRowIndex())
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
                    CA_Lib.PasteNewRowsProgrammatically == true ||
                    CA_Lib.PasteExistingRowsProgrammatically == true)
                {
                    return;
                }

                var record = this.SfDataGrid1.GetRecordAtRowIndex(index);
                var context = new ProductListingContext();
                CA_Lib.OldRowDataIndex = index - 1;
                CA_Lib.OldRowData = record as certification_actual_Model;
                certification_Model item1 = context.Certifications.Where(a => a.Id.Equals(CA_Lib.OldRowData.Id)).Single();

                if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                {
                    this.SetErrorMessage(e, string.Empty, false);
                    return;
                }

                if (e.Column.MappingName == nameof(CA_Lib.OldRowData.Code))
                {
                    item1.Code = e.NewValue.ToString();

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
                    else
                    {
                        if (e.OldValue != e.NewValue &&
                                context.Certifications.Where(
                                    i => i.Code.Equals(item1.Code)
                                    ).Count() > 0)
                        {
                            var joined_string = System.String.Join(",",
                                    item1.Code
                                    );

                            this.SetErrorMessage(e, $"{joined_string} is duplicated!", true);
                            return;
                        }
                    }
                }
                else if (e.Column.MappingName == nameof(CA_Lib.OldRowData.Description))
                {
                    item1.Description = e.NewValue.ToString();

                    if (e.OldValue != null && e.NewValue != null && e.OldValue.ToString() == e.NewValue.ToString())
                    {
                        this.SetErrorMessage(e, string.Empty, false);
                        return;
                    }
                    else if (e.NewValue.ToString().Length > 50)
                    {
                        this.SetErrorMessage(e, "Total length cannot exceed 50 characters!", true);
                        return;
                    }
                }

                this.SetErrorMessage(e, null, false);
                context.SaveChanges();
                context.Dispose();
                CA_Lib.Certification_Actual_dataTable_HasError = false;
                CA_Lib.PasteNewRowsProgrammatically = false;
                CA_Lib.PasteExistingRowsProgrammatically = false;
                CA_Lib.OldRowData = new certification_actual_Model();
                CA_Lib.OldRowData = new certification_actual_Model();
                CA_Lib.OldRowDataIndex = 0;
                certification_actual_Model rowView = e.RowData as certification_actual_Model;
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
                    CA_Lib.CustomRowValidating(e.RowData as certification_actual_Model, IsAddNew: true, Programmatically: false, e, out _, out _);
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
                    CA_Lib.Delete();
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
                CA_Lib.Copy();
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
                CA_Lib.Delete();
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
                CA_Lib.Paste();
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
                Certification_Actual_FilterManager filtermanager = new Certification_Actual_FilterManager();
                filtermanager.certificationactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Certification_Actual_ViewModel;
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
                    Certification_Actual_FilterManager filtermanager = new Certification_Actual_FilterManager();
                    filtermanager.certificationactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Certification_Actual_ViewModel;
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
                Certification_Actual_FilterManager filtermanager = new Certification_Actual_FilterManager();
                filtermanager.certificationactualViewModel = this.SfDataGrid1.DataContext as ViewModel.Certification_Actual_ViewModel;
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
    }
}
