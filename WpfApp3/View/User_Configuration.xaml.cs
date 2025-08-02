using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModel;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Product_Info.xaml
    /// </summary>
    public partial class User_Configuration : UserControl
    {
        public User_Configuration()
        {
            InitializeComponent();
        }

        private void User_Configuration_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = MW_Lib.User_Configuration_ViewModel_Proxy;
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var datacontext = this.DataContext as User_Configuration_ViewModel;
                var item = datacontext.User_Configuration;
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(item, options);
                File.WriteAllTextAsync(Path.Combine(AppContext.BaseDirectory, @"userconfig.json"), json);
                MessageBox.Show($"Saved successfully.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_Reload_Click(object sender, RoutedEventArgs e)
        {
            var datacontext = this.DataContext as User_Configuration_ViewModel;
            datacontext.Populate__user_configuration();
        }
    }
}
