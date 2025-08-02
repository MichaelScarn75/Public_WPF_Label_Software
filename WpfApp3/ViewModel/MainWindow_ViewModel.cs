namespace WpfApp3.ViewModel
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private bool _RibbonTab_Home_New_Enabled = true;
        private bool _RibbonTab_Home_New_New_Enabled = true;
        private bool _RibbonTab_Home_Manage_Enabled = true;
        private bool _RibbonTab_Home_Manage_Edit_Enabled = false;
        private bool _RibbonTab_Home_Manage_View_Enabled = true;
        private bool _RibbonTab_Home_Manage_Delete_Enabled = true;
        private bool _RibbonTab_Home_Page_Enabled = true;
        private bool _RibbonTab_Home_Page_Refresh_Enabled = true;
        private bool _RibbonTab_Home_Page_ClearFilter_Enabled = true;
        private bool _RibbonTab_Setups_Printing_Setup_Enabled = true;
        private bool _RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_Items_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_UnitsOfMeasure_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_SpecialPrice_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled = true;
        private bool _RibbonTab_Setups_Product_Management_Country_Enabled = true;
        private bool _RibbonTab_Setups_CustomerManagement_Enabled = true;
        private bool _RibbonTab_Setups_CustomerManagement_Customer_Enabled = true;
        private bool _RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled = true;

        public MainWindow_ViewModel()
        {
        }

        public bool RibbonTab_Home_New_Enabled
        {
            get { return _RibbonTab_Home_New_Enabled; }

            set
            {
                _RibbonTab_Home_New_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_New_Enabled));
                _RibbonTab_Home_New_New_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_New_New_Enabled));
            }
        }

        public bool RibbonTab_Home_New_New_Enabled
        {
            get { return _RibbonTab_Home_New_New_Enabled; }

            set
            {
                _RibbonTab_Home_New_New_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_New_New_Enabled));
            }
        }

        public bool RibbonTab_Home_Manage_Enabled
        {
            get { return _RibbonTab_Home_Manage_Enabled; }

            set
            {
                _RibbonTab_Home_Manage_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_Enabled));
                _RibbonTab_Home_Manage_Edit_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_Edit_Enabled));
                _RibbonTab_Home_Manage_View_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_View_Enabled));
                _RibbonTab_Home_Manage_Delete_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_Delete_Enabled));
            }
        }

        public bool RibbonTab_Home_Manage_Edit_Enabled
        {
            get { return _RibbonTab_Home_Manage_Edit_Enabled; }

            set
            {
                _RibbonTab_Home_Manage_Edit_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_Edit_Enabled));
            }
        }

        public bool RibbonTab_Home_Manage_View_Enabled
        {
            get { return _RibbonTab_Home_Manage_View_Enabled; }

            set
            {
                _RibbonTab_Home_Manage_View_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_View_Enabled));
            }
        }

        public bool RibbonTab_Home_Manage_Delete_Enabled
        {
            get { return _RibbonTab_Home_Manage_Delete_Enabled; }

            set
            {
                _RibbonTab_Home_Manage_Delete_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Manage_Delete_Enabled));
            }
        }

        public bool RibbonTab_Home_Page_Enabled
        {
            get { return _RibbonTab_Home_Page_Enabled; }

            set
            {
                _RibbonTab_Home_Page_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Page_Enabled));
                _RibbonTab_Home_Page_Refresh_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Page_Refresh_Enabled));
                _RibbonTab_Home_Page_ClearFilter_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Page_ClearFilter_Enabled));
            }
        }

        public bool RibbonTab_Home_Page_Refresh_Enabled
        {
            get { return _RibbonTab_Home_Page_Refresh_Enabled; }

            set
            {
                _RibbonTab_Home_Page_Refresh_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Page_Refresh_Enabled));
            }
        }

        public bool RibbonTab_Home_Page_ClearFilter_Enabled
        {
            get { return _RibbonTab_Home_Page_ClearFilter_Enabled; }

            set
            {
                _RibbonTab_Home_Page_ClearFilter_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Home_Page_ClearFilter_Enabled));
            }
        }

        public bool RibbonTab_Setups_Printing_Setup_Enabled
        {
            get { return _RibbonTab_Setups_Printing_Setup_Enabled; }

            set
            {
                _RibbonTab_Setups_Printing_Setup_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Printing_Setup_Enabled));
                _RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled));
            }
        }

        public bool RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled
        {
            get { return _RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled; }

            set
            {
                _RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Printing_Setup_PrinterConfig_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_Enabled));
                _RibbonTab_Setups_Product_Management_Items_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_Items_Enabled));
                _RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled));
                _RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled));
                _RibbonTab_Setups_Product_Management_SpecialPrice_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_SpecialPrice_Enabled));
                _RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled));
                _RibbonTab_Setups_Product_Management_Country_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_Country_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_Items_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_Items_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_Items_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_Items_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_ItemUnitsOfMeasure_Enabled));
            }
        }
        public bool RibbonTab_Setups_Product_Management_UnitsOfMeasure_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_UnitsOfMeasure_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_UnitsOfMeasure_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_UnitsOfMeasure_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_InventoryPostingGroup_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_SpecialPrice_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_SpecialPrice_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_SpecialPrice_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_SpecialPrice_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_SpecialDiscount_Enabled));
            }
        }

        public bool RibbonTab_Setups_Product_Management_Country_Enabled
        {
            get { return _RibbonTab_Setups_Product_Management_Country_Enabled; }

            set
            {
                _RibbonTab_Setups_Product_Management_Country_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_Product_Management_Country_Enabled));
            }
        }

        public bool RibbonTab_Setups_CustomerManagement_Enabled
        {
            get { return _RibbonTab_Setups_CustomerManagement_Enabled; }

            set
            {
                _RibbonTab_Setups_CustomerManagement_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_CustomerManagement_Enabled));
                _RibbonTab_Setups_CustomerManagement_Customer_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_CustomerManagement_Customer_Enabled));
                _RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled));
            }
        }

        public bool RibbonTab_Setups_CustomerManagement_Customer_Enabled
        {
            get { return _RibbonTab_Setups_CustomerManagement_Customer_Enabled; }

            set
            {
                _RibbonTab_Setups_CustomerManagement_Customer_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_CustomerManagement_Customer_Enabled));
            }
        }

        public bool RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled
        {
            get { return _RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled; }

            set
            {
                _RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled = value;
                this.RaisePropertyChanged(nameof(RibbonTab_Setups_CustomerManagement_CustomerBranch_Enabled));
            }
        }

        public void BeginEdit()
        {
            RibbonTab_Home_Manage_Edit_Enabled = false;
            RibbonTab_Home_Manage_View_Enabled = true;
            RibbonTab_Home_New_New_Enabled = true;
        }

        public void View()
        {
            RibbonTab_Home_Manage_Edit_Enabled = true;
            RibbonTab_Home_Manage_View_Enabled = false;
            RibbonTab_Home_New_New_Enabled = false;
        }

        public void AddNew()
        {
            RibbonTab_Home_New_New_Enabled = false;
        }

        public void DisableCRUD()
        {
            RibbonTab_Home_New_New_Enabled = false;
            RibbonTab_Home_Manage_Edit_Enabled = false;
            RibbonTab_Home_Manage_View_Enabled = false;
            RibbonTab_Home_Manage_Delete_Enabled = false;
        }

        public void EnableCRUD()
        {
            RibbonTab_Home_New_New_Enabled = true;
            RibbonTab_Home_Manage_Edit_Enabled = true;
            RibbonTab_Home_Manage_View_Enabled = true;
            RibbonTab_Home_Manage_Delete_Enabled = true;
        }

        public void DisableAll()
        {
            RibbonTab_Home_New_New_Enabled = false;
            RibbonTab_Home_Manage_Edit_Enabled = false;
            RibbonTab_Home_Manage_View_Enabled = false;
            RibbonTab_Home_Manage_Delete_Enabled = false;
            RibbonTab_Home_Page_Refresh_Enabled = false;
            RibbonTab_Home_Page_ClearFilter_Enabled = false;
        }

        public void EnableAll()
        {
            RibbonTab_Home_New_New_Enabled = true;
            RibbonTab_Home_Manage_Edit_Enabled = true;
            RibbonTab_Home_Manage_View_Enabled = true;
            RibbonTab_Home_Manage_Delete_Enabled = true;
            RibbonTab_Home_Page_Refresh_Enabled = true;
            RibbonTab_Home_Page_ClearFilter_Enabled = true;
        }
    }

}
