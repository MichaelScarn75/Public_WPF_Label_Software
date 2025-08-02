// <copyright file="FilterManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.FilterManager
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Syncfusion.Linq;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.Windows.Controls.Input;
    using Syncfusion.Windows.Tools.Controls;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    public class Customer_Branch_FilterManager
    {
        internal List<UIElement> UIElementsList = new List<UIElement>();
        internal Grid Grid1 = null;
        internal Customer_Branch_ViewModel customerbranchViewModel = null;
        internal SfDataGrid SfDataGrid1 = null;

        internal void AddFilter(Grid Detailed_Filter_Children_Grid)
        {
            var uIElement_addfilterbutton = Detailed_Filter_Children_Grid.Children
                .OfType<ButtonAdv>()
                .FirstOrDefault(btn => btn.Name == "AddFilter_Button");
            int newRow = Detailed_Filter_Children_Grid.RowDefinitions.Count - 1;
            Grid1 = Detailed_Filter_Children_Grid;

            Detailed_Filter_Children_Grid.RowDefinitions.Insert(newRow, new RowDefinition
            {
                Height = new GridLength(60)
            });

            Detailed_Filter_Children_Grid.RowDefinitions[newRow].Height = new GridLength(30);

            if (uIElement_addfilterbutton != null)
            {
                Grid.SetRow(uIElement_addfilterbutton, newRow + 1);
            }

            ButtonAdv button1 = new ButtonAdv
            {
                Tag = "Cancel Button",
                Width = 20,
                Height = 30,
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                BorderThickness = new Thickness(0),
                IconHeight = 15,
                IconWidth = 15,
                Label = null,
                LargeIcon = (BitmapImage)Application.Current.Resources["Img_Cancel"],
                SmallIcon = (BitmapImage)Application.Current.Resources["Img_Cancel"]
            };
            button1.Click += RemoveFilter_Click;

            var label = new Label
            {
                Tag = "first column label",
                Content = "And",
                FontSize = 13,
                Margin = new Thickness(15, 0, 0, 0),
                FontWeight = FontWeights.SemiBold,
                Foreground = Brushes.MediumBlue,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Left
            };

            if (Detailed_Filter_Children_Grid.RowDefinitions.Count == 2)
            {
                label.Content = "Where";
            }

            var comboBox = new ComboBoxAdv
            {
                Tag = "combobox",
                Width = 280,
                Height = 28,
                FontSize = 13,
                SelectedIndex = 0
            };

            foreach (var col in SfDataGrid1.Columns)
            {
                comboBox.Items.Add(new ComboBoxItemAdv { Content = col.HeaderText });
            }

            var label2 = new Label
            {
                Tag = "second column label",
                Content = "is",
                FontSize = 13,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var textBox = new SfTextBoxExt
            {
                Tag = "textbox",
                Width = 280,
                Height = 28,
                FontSize = 13,
                VerticalContentAlignment = VerticalAlignment.Center,
                Watermark = "Enter a Value"
            };
            textBox.PreviewKeyDown += Textbox1_PreviewKeyDown;

            UIElementsList.Add(button1);
            UIElementsList.Add(label);
            UIElementsList.Add(comboBox);
            UIElementsList.Add(label2);
            UIElementsList.Add(textBox);

            Grid.SetRow(button1, newRow);
            Grid.SetColumn(button1, 0);
            Detailed_Filter_Children_Grid.Children.Add(button1);

            Grid.SetRow(label, newRow);
            Grid.SetColumn(label, 1);
            Detailed_Filter_Children_Grid.Children.Add(label);

            Grid.SetRow(comboBox, newRow);
            Grid.SetColumn(comboBox, 2);
            Detailed_Filter_Children_Grid.Children.Add(comboBox);

            Grid.SetRow(label2, newRow);
            Grid.SetColumn(label2, 3);
            Detailed_Filter_Children_Grid.Children.Add(label2);

            Grid.SetRow(textBox, newRow);
            Grid.SetColumn(textBox, 4);
            Detailed_Filter_Children_Grid.Children.Add(textBox);
        }

        internal void RemoveFilter_Click(object sender, RoutedEventArgs e)
        {
            int rowIndex = 0;
            foreach (UIElement element in UIElementsList)
            {
                Grid1.Children.Remove(element);
                rowIndex = Grid.GetRow(element);
            }

            Grid1.RowDefinitions.RemoveAt(rowIndex);

            // offset everything after current row down one row
            foreach (UIElement element1 in Grid1.Children)
            {
                int currentRow = Grid.GetRow(element1);
                if (currentRow > rowIndex)
                {
                    Grid.SetRow(element1, currentRow - 1);
                }
            }

            // Make sure the first row is labeled "Where"
            foreach (UIElement element1 in Grid1.Children)
            {
                if (Grid.GetRow(element1) == 0 && Grid.GetColumn(element1) == 1)
                {
                    if (element1.GetType() == typeof(Label))
                    {
                        Label element2 = element1 as Label;
                        if (element2.Content == "And")
                        {
                            element2.Content = "Where";
                            break;
                        }

                        break;
                    }
                }
            }

            // re-filter everytime a filter is deleted
            OrganizeFilter();
        }

        internal void SimplyRemoveAllFilter()
        {
            UIElement temp__addfilterButton = new UIElement();

            for (int i = 0; i < Grid1.Children.Count; i++)
            {
                if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "AddFilter_Button")
                {
                    temp__addfilterButton = Grid1.Children[i];
                    break;
                }
            }

            Grid1.Children.Clear();
            Grid1.RowDefinitions.Clear();

            // add back "add filter" button
            Grid1.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            Grid1.Children.Add(temp__addfilterButton);
            Grid.SetRow(temp__addfilterButton, Grid1.RowDefinitions.Count);
            Grid.SetRowSpan(temp__addfilterButton, 1);
            Grid.SetColumn(temp__addfilterButton, 0);
            Grid.SetColumnSpan(temp__addfilterButton, 2);
        }

        internal void Textbox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OrganizeFilter();
            }
        }

        internal void OrganizeFilter()
        {
            List<string> temp__temp_combobox_values = new List<string>();
            List<string> temp__temp_textbox_values = new List<string>();
            List<UIElement> temp__temp_CancelButton_list = new List<UIElement>();
            List<UIElement> temp__temp_firstcolumnlabel_list = new List<UIElement>();
            List<UIElement> temp__temp_combobox_list = new List<UIElement>();
            List<UIElement> temp__temp_secondcolumnlabel_list = new List<UIElement>();
            List<UIElement> temp__temp_textbox_list = new List<UIElement>();

            List<string> temp_combobox_values = new List<string>();
            List<string> temp_textbox_values = new List<string>();
            List<UIElement> temp_CancelButton_list = new List<UIElement>();
            List<UIElement> temp_firstcolumnlabel_list = new List<UIElement>();
            List<UIElement> temp_combobox_list = new List<UIElement>();
            List<UIElement> temp_secondcolumnlabel_list = new List<UIElement>();
            List<UIElement> temp_textbox_list = new List<UIElement>();

            List<string> combobox_values = new List<string>();
            List<string> textbox_values = new List<string>();
            List<UIElement> cancelButton_list = new List<UIElement>();
            List<UIElement> firstcolumnlabel_list = new List<UIElement>();
            List<UIElement> combobox_list = new List<UIElement>();
            List<UIElement> secondcolumnlabel_list = new List<UIElement>();
            List<UIElement> textbox_list = new List<UIElement>();

            UIElement temp__addfilterButton = new UIElement();
            List<Tuple<UIElement, UIElement, UIElement, UIElement, UIElement>> uIElement_List = new List<Tuple<UIElement, UIElement, UIElement, UIElement, UIElement>>();

            // store all widgets from the grid
            for (int i = 0; i < Grid1.Children.Count; i++)
            {
                if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "Cancel Button")
                {
                    temp__temp_CancelButton_list.Add(Grid1.Children[i]);
                }
                else if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "first column label")
                {
                    temp__temp_firstcolumnlabel_list.Add(Grid1.Children[i]);
                }
                else if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "combobox")
                {
                    temp__temp_combobox_list.Add(Grid1.Children[i]);
                    temp__temp_combobox_values.Add(((Grid1.Children[i] as ComboBoxAdv).SelectedItem as ComboBoxItemAdv).Content.ToString());
                }
                else if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "second column label")
                {
                    temp__temp_secondcolumnlabel_list.Add(Grid1.Children[i]);
                }
                else if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "textbox")
                {
                    temp__temp_textbox_list.Add(Grid1.Children[i]);
                    temp__temp_textbox_values.Add((Grid1.Children[i] as SfTextBoxExt).Text.ToString());
                }
                else if ((Grid1.Children[i] as FrameworkElement).Tag.ToString() == "AddFilter_Button")
                {
                    temp__addfilterButton = Grid1.Children[i];
                }
            }

            // if there is no filter row at all (which shouldn't be possible)
            if (temp__temp_textbox_list.Count == 0)
            {
                customerbranchViewModel.Populate__customerbranch();
                return;
            }

            // filter out empty textboxes
            for (int j = 0; j < temp__temp_textbox_list.Count; j++)
            {
                if (((SfTextBoxExt)temp__temp_textbox_list[j]).Text != string.Empty)
                {
                    temp_combobox_values.Add(temp__temp_combobox_values[j]);
                    temp_textbox_values.Add(temp__temp_textbox_values[j]);
                    temp_CancelButton_list.Add(temp__temp_CancelButton_list[j]);
                    temp_firstcolumnlabel_list.Add(temp__temp_firstcolumnlabel_list[j]);
                    temp_combobox_list.Add(temp__temp_combobox_list[j]);
                    temp_secondcolumnlabel_list.Add(temp__temp_secondcolumnlabel_list[j]);
                    temp_textbox_list.Add(temp__temp_textbox_list[j]);
                }
            }

            // if all textboxes are empty
            if (temp_combobox_values.Count == 0)
            {
                customerbranchViewModel.Populate__customerbranch();
                return;
            }

            // remove duplicated filters
            for (int i = 0; i < temp_combobox_values.Count; i++)
            {
                if (combobox_values.Contains(temp_combobox_values[i]) == false)
                {
                    combobox_values.Add(temp_combobox_values[i]);
                    textbox_values.Add(temp_textbox_values[i]);
                    cancelButton_list.Add(temp_CancelButton_list[i]);
                    firstcolumnlabel_list.Add(temp_firstcolumnlabel_list[i]);
                    combobox_list.Add(temp_combobox_list[i]);
                    secondcolumnlabel_list.Add(temp_secondcolumnlabel_list[i]);
                    textbox_list.Add(temp_textbox_list[i]);
                    uIElement_List.Add(new Tuple<UIElement, UIElement, UIElement, UIElement, UIElement>(
                         temp_CancelButton_list[i],
                         temp_firstcolumnlabel_list[i],
                         temp_combobox_list[i],
                         temp_secondcolumnlabel_list[i],
                         temp_textbox_list[i]));
                }
            }

            // redraw grid
            Grid1.Children.Clear();
            Grid1.RowDefinitions.Clear();

            // add all controls back
            for (int i = 0; i < combobox_values.Count; i++)
            {
                Grid1.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                Grid1.Children.Add(cancelButton_list[i]);
                Grid1.Children.Add(firstcolumnlabel_list[i]);
                Grid1.Children.Add(combobox_list[i]);
                Grid1.Children.Add(secondcolumnlabel_list[i]);
                Grid1.Children.Add(textbox_list[i]);

                Grid.SetRow(cancelButton_list[i], i);
                Grid.SetRow(firstcolumnlabel_list[i], i);
                Grid.SetRow(combobox_list[i], i);
                Grid.SetRow(secondcolumnlabel_list[i], i);
                Grid.SetRow(textbox_list[i], i);

                Grid.SetRowSpan(cancelButton_list[i], 1);
                Grid.SetRowSpan(firstcolumnlabel_list[i], 1);
                Grid.SetRowSpan(combobox_list[i], 1);
                Grid.SetRowSpan(secondcolumnlabel_list[i], 1);
                Grid.SetRowSpan(textbox_list[i], 1);

                Grid.SetColumn(cancelButton_list[i], 0);
                Grid.SetColumn(firstcolumnlabel_list[i], 1);
                Grid.SetColumn(combobox_list[i], 2);
                Grid.SetColumn(secondcolumnlabel_list[i], 3);
                Grid.SetColumn(textbox_list[i], 4);

                Grid.SetColumnSpan(cancelButton_list[i], 1);
                Grid.SetColumnSpan(firstcolumnlabel_list[i], 1);
                Grid.SetColumnSpan(combobox_list[i], 1);
                Grid.SetColumnSpan(secondcolumnlabel_list[i], 1);
                Grid.SetColumnSpan(textbox_list[i], 1);
            }

            // add back "add filter" button
            Grid1.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            Grid1.Children.Add(temp__addfilterButton);
            Grid.SetRow(temp__addfilterButton, Grid1.RowDefinitions.Count);
            Grid.SetRowSpan(temp__addfilterButton, 1);
            Grid.SetColumn(temp__addfilterButton, 0);
            Grid.SetColumnSpan(temp__addfilterButton, 2);

            // Make sure the first row is labeled "Where"
            foreach (UIElement element1 in Grid1.Children)
            {
                if (Grid.GetRow(element1) == 0 &&
                    Grid.GetColumn(element1) == 1 &&
                    element1.GetType() == typeof(Label))
                {
                    Label element2 = element1 as Label;
                    if (element2.Content == "And")
                    {
                        element2.Content = "Where";
                        break;
                    }

                    break;
                }
            }

            FilterResults(1, combobox_values, textbox_values);
        }

        // fetch new data from server and feed it to sfdatagrid
        internal void FilterResults(int search_mode, List<string> combobox_values, List<string> textbox_values)
        {
            if (search_mode == 1)       //advanced search
            {
                using (var context = new ProductListingContext())
                {
                    var query = context.CustomerBranches.AsQueryable();
                    var model = new Customerbranch_Model();

                    for (int i = 0; i < combobox_values.Count; i++)
                    {
                        foreach (var gridColumn in SfDataGrid1.Columns)
                        {
                            if (gridColumn.HeaderText == combobox_values[i])
                            {
                                switch (gridColumn.MappingName)
                                {
                                    case nameof(Customerbranch_Model.BranchId):
                                        model.BranchId = textbox_values[i];

                                        query = query.Where(i => i.BranchId.Equals(model.BranchId));
                                        break;
                                    case nameof(Customerbranch_Model.CustomerId):
                                        model.CustomerId = textbox_values[i];
                                        query = query.Where(i => i.CustomerId.Equals(model.CustomerId));
                                        break;
                                    case nameof(Customerbranch_Model.CustomerDescription):
                                        model.CustomerDescription = textbox_values[i];
                                        query = query.Where(i => i.CustomerDescription.Equals(model.CustomerDescription));
                                        break;
                                    case nameof(Customerbranch_Model.BranchDescription):
                                        model.BranchDescription = textbox_values[i];
                                        query = query.Where(i => i.BranchDescription.Equals(model.BranchDescription));
                                        break;
                                    case nameof(Customerbranch_Model.Address1):
                                        model.Address1 = textbox_values[i];
                                        query = query.Where(i => i.Address1.Equals(model.Address1));
                                        break;
                                    case nameof(Customerbranch_Model.Address2):
                                        model.Address2 = textbox_values[i];
                                        query = query.Where(i => i.Address2.Equals(model.Address2));
                                        break;
                                    case nameof(Customerbranch_Model.Address3):
                                        model.Address3 = textbox_values[i];
                                        query = query.Where(i => i.Address3.Equals(model.Address3));
                                        break;
                                    case nameof(Customerbranch_Model.ContactPerson):
                                        model.ContactPerson = textbox_values[i];
                                        query = query.Where(i => i.ContactPerson.Equals(model.ContactPerson));
                                        break;
                                    case nameof(Customerbranch_Model.PostalCode):
                                        model.PostalCode = textbox_values[i];
                                        query = query.Where(i => i.PostalCode.Equals(model.PostalCode));
                                        break;
                                    case nameof(Customerbranch_Model.Phone1):
                                        model.Phone1 = textbox_values[i];
                                        query = query.Where(i => i.Phone1.Equals(model.Phone1));
                                        break;
                                    case nameof(Customerbranch_Model.Phone2):
                                        model.Phone2 = textbox_values[i];
                                        query = query.Where(i => i.Phone2.Equals(model.Phone2));
                                        break;
                                    case nameof(Customerbranch_Model.Fax):
                                        model.Fax = textbox_values[i];
                                        query = query.Where(i => i.Fax.Equals(model.Fax));
                                        break;
                                    case nameof(Customerbranch_Model.Email):
                                        model.Email = textbox_values[i];
                                        query = query.Where(i => i.Email.Equals(model.Email));
                                        break;
                                    case nameof(Customerbranch_Model.Website):
                                        model.Website = textbox_values[i];
                                        query = query.Where(i => i.Website.Equals(model.Website));
                                        break;
                                    case nameof(Customerbranch_Model.GSTRegNo):
                                        model.GSTRegNo = textbox_values[i];
                                        query = query.Where(i => i.GSTRegNo.Equals(model.GSTRegNo));
                                        break;
                                    case nameof(Customerbranch_Model.CompanyRegNo):
                                        model.CompanyRegNo = textbox_values[i];
                                        query = query.Where(i => i.CompanyRegNo.Equals(model.CompanyRegNo));
                                        break;
                                    case nameof(Customerbranch_Model.VehicleNo):
                                        model.VehicleNo = textbox_values[i];
                                        query = query.Where(i => i.VehicleNo.Equals(model.VehicleNo));
                                        break;
                                    case nameof(Customerbranch_Model.LabelStyle):
                                        model.LabelStyle = textbox_values[i];
                                        query = query.Where(i => i.LabelStyle.Equals(model.LabelStyle));
                                        break;
                                }
                            }
                        }
                    }

                    customerbranchViewModel.CustomerBranch_IEnum = query.ToObservableCollection<Customerbranch_Model>();
                }
            }
            else       //simple search
            {
                using (var context = new ProductListingContext())
                {
                    var query = context.CustomerBranches.AsQueryable();
                    var model = new Customerbranch_Model();

                    for (int i = 0; i < combobox_values.Count; i++)
                    {
                        foreach (var gridColumn in SfDataGrid1.Columns)
                        {
                            if (gridColumn.HeaderText == combobox_values[i])
                            {
                                switch (gridColumn.MappingName)
                                {
                                    case nameof(Customerbranch_Model.BranchId):
                                        model.BranchId = textbox_values[i];
                                        query = query.Where(i => i.BranchId.Contains(model.BranchId));
                                        break;
                                    case nameof(Customerbranch_Model.CustomerId):
                                        model.CustomerId = textbox_values[i];
                                        query = query.Where(i => i.CustomerId.Contains(model.CustomerId));
                                        break;
                                    case nameof(Customerbranch_Model.CustomerDescription):
                                        model.CustomerDescription = textbox_values[i];
                                        query = query.Where(i => i.CustomerDescription.Contains(model.CustomerDescription));
                                        break;
                                    case nameof(Customerbranch_Model.BranchDescription):
                                        model.BranchDescription = textbox_values[i];
                                        query = query.Where(i => i.BranchDescription.Contains(model.BranchDescription));
                                        break;
                                    case nameof(Customerbranch_Model.Address1):
                                        model.Address1 = textbox_values[i];
                                        query = query.Where(i => i.Address1.Contains(model.Address1));
                                        break;
                                    case nameof(Customerbranch_Model.Address2):
                                        model.Address2 = textbox_values[i];
                                        query = query.Where(i => i.Address2.Contains(model.Address2));
                                        break;
                                    case nameof(Customerbranch_Model.Address3):
                                        model.Address3 = textbox_values[i];
                                        query = query.Where(i => i.Address3.Contains(model.Address3));
                                        break;
                                    case nameof(Customerbranch_Model.ContactPerson):
                                        model.ContactPerson = textbox_values[i];
                                        query = query.Where(i => i.ContactPerson.Contains(model.ContactPerson));
                                        break;
                                    case nameof(Customerbranch_Model.PostalCode):
                                        model.PostalCode = textbox_values[i];
                                        query = query.Where(i => i.PostalCode.Contains(model.PostalCode));
                                        break;
                                    case nameof(Customerbranch_Model.Phone1):
                                        model.Phone1 = textbox_values[i];
                                        query = query.Where(i => i.Phone1.Contains(model.Phone1));
                                        break;
                                    case nameof(Customerbranch_Model.Phone2):
                                        model.Phone2 = textbox_values[i];
                                        query = query.Where(i => i.Phone2.Contains(model.Phone2));
                                        break;
                                    case nameof(Customerbranch_Model.Fax):
                                        model.Fax = textbox_values[i];
                                        query = query.Where(i => i.Fax.Contains(model.Fax));
                                        break;
                                    case nameof(Customerbranch_Model.Email):
                                        model.Email = textbox_values[i];
                                        query = query.Where(i => i.Email.Contains(model.Email));
                                        break;
                                    case nameof(Customerbranch_Model.Website):
                                        model.Website = textbox_values[i];
                                        query = query.Where(i => i.Website.Contains(model.Website));
                                        break;
                                    case nameof(Customerbranch_Model.GSTRegNo):
                                        model.GSTRegNo = textbox_values[i];
                                        query = query.Where(i => i.GSTRegNo.Contains(model.GSTRegNo));
                                        break;
                                    case nameof(Customerbranch_Model.CompanyRegNo):
                                        model.CompanyRegNo = textbox_values[i];
                                        query = query.Where(i => i.CompanyRegNo.Contains(model.CompanyRegNo));
                                        break;
                                    case nameof(Customerbranch_Model.VehicleNo):
                                        model.VehicleNo = textbox_values[i];
                                        query = query.Where(i => i.VehicleNo.Contains(model.VehicleNo));
                                        break;
                                    case nameof(Customerbranch_Model.LabelStyle):
                                        model.LabelStyle = textbox_values[i];
                                        query = query.Where(i => i.LabelStyle.Contains(model.LabelStyle));
                                        break;
                                }
                            }
                        }
                    }

                    customerbranchViewModel.CustomerBranch_IEnum = query.ToObservableCollection<Customerbranch_Model>();
                }
            }
        }
    }
}
