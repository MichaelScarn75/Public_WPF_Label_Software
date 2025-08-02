using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for PacketOrScale.xaml
    /// </summary>
    public partial class PacketOrScale : UserControl
    {
        public PacketOrScale()
        {
            InitializeComponent();
        }

        private void Button_Packet_Click(object sender, RoutedEventArgs e)
        {
            MW_Lib.IsScale = false;
            MW_Lib.ContentControl1_Proxy.Content = new Product_Listing_Simple_View();
        }

        private void Button_Scale_Click(object sender, RoutedEventArgs e)
        {
            MW_Lib.IsScale = true;
            MW_Lib.ContentControl1_Proxy.Content = new Product_Listing_Simple_View();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            MW_Lib.ContentControl1_Proxy.Content = new Customer_To_Print();
        }
    }
}
