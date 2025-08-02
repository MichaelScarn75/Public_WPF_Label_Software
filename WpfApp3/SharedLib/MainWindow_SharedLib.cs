namespace WpfApp3.SharedLib
{
    using System.Windows.Controls;
    using WpfApp3.Model;
    using WpfApp3.ViewModel;

    internal class MainWindow_SharedLib
    {
        internal static MainWindow_ViewModel MainWindow_ViewModel_Proxy = new MainWindow_ViewModel();
        internal static User_Configuration_ViewModel User_Configuration_ViewModel_Proxy = new User_Configuration_ViewModel();
        internal static ContentControl ContentControl1_Proxy = new ContentControl();
        internal static Customermain_Model Choosecustomer_Model_Proxy = new();
        internal static Productlisting_Model Productlisting_simple_Model_Proxy = new();
        internal static PrintingHistory_Model Printinghistory_Model_Proxy = new();
        internal static bool IsScale = false;
    }
}
