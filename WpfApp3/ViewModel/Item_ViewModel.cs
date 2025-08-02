// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Item_ViewModel : ViewModelBase
    {
        private ObservableCollection<item_Model> _item_IEnum;
        private ObservableCollection<Inventorypostinggroup_Model> _inventorypostinggroup_IEnum;
        private ObservableCollection<Country_Model> _country_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<item_Model> Item_IEnum
        {
            get { return this._item_IEnum; }

            set
            {
                this._item_IEnum = value;
                this.RaisePropertyChanged(nameof(Item_IEnum));
                OCPropertyChanged(this.Item_IEnum);
            }
        }

        public ObservableCollection<Inventorypostinggroup_Model> InventoryPostingGroup_IEnum
        {
            get { return this._inventorypostinggroup_IEnum; }

            set
            {
                this._inventorypostinggroup_IEnum = value;
                this.RaisePropertyChanged(nameof(InventoryPostingGroup_IEnum));
                OCPropertyChanged(this.InventoryPostingGroup_IEnum);
            }
        }

        public ObservableCollection<Country_Model> Country_IEnum
        {
            get { return this._country_IEnum; }

            set
            {
                this._country_IEnum = value;
                this.RaisePropertyChanged(nameof(Country_IEnum));
                OCPropertyChanged(this.Country_IEnum);
            }
        }

        public Visibility ErrorTextBorder1_Visibility
        {
            get { return this._errortextborder1_Visibility; }

            set
            {
                this._errortextborder1_Visibility = value;
                this.RaisePropertyChanged(nameof(ErrorTextBorder1_Visibility));
            }
        }

        public string ErrorTextBox1_Text
        {
            get { return this._errortextbox1_Text; }

            set
            {
                this._errortextbox1_Text = value;
                this.RaisePropertyChanged(nameof(ErrorTextBox1_Text));
            }
        }

        public Item_ViewModel()
        {
            this._item_IEnum = new ObservableCollection<item_Model>();
            this.Populate__item();
            this.Populate__inventorypostinggroup();
            this.Populate__country();
        }

        public void Populate__item()
        {
            using (var context = new ProductListingContext())
            {
                this._item_IEnum = context.Items.ToObservableCollection<item_Model>();
                this.RaisePropertyChanged(nameof(Item_IEnum));
                OCPropertyChanged(this.Item_IEnum);
            }
        }

        public void Populate__inventorypostinggroup()
        {
            using (var context = new ProductListingContext())
            {
                this._inventorypostinggroup_IEnum = context.InventoryPostingGroups.ToObservableCollection<Inventorypostinggroup_Model>();
                this.RaisePropertyChanged(nameof(InventoryPostingGroup_IEnum));
                OCPropertyChanged(this.InventoryPostingGroup_IEnum);
            }
        }

        public void Populate__country()
        {
            using (var context = new ProductListingContext())
            {
                this._country_IEnum = context.Countries.ToObservableCollection<Country_Model>();
                this.RaisePropertyChanged(nameof(Country_IEnum));
                OCPropertyChanged(this.Country_IEnum);
            }
        }
    }
}
