// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class InventoryPostingGroup_ViewModel : ViewModelBase
    {
        private ObservableCollection<Inventorypostinggroup_Model> _inventorypostingGroup_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Inventorypostinggroup_Model> InventoryPostingGroup_IEnum
        {
            get { return this._inventorypostingGroup_IEnum; }

            set
            {
                this._inventorypostingGroup_IEnum = value;
                this.RaisePropertyChanged(nameof(InventoryPostingGroup_IEnum));
                OCPropertyChanged(this.InventoryPostingGroup_IEnum);
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

        public InventoryPostingGroup_ViewModel()
        {
            this._inventorypostingGroup_IEnum = new ObservableCollection<Inventorypostinggroup_Model>();
            this.Populate__inventorypostinggroup();
        }

        public void Populate__inventorypostinggroup()
        {
            using (var context = new ProductListingContext())
            {
                this._inventorypostingGroup_IEnum = context.InventoryPostingGroups.ToObservableCollection<Inventorypostinggroup_Model>();
                this.RaisePropertyChanged(nameof(InventoryPostingGroup_IEnum));
                OCPropertyChanged(this.InventoryPostingGroup_IEnum);
            }
        }
    }
}
