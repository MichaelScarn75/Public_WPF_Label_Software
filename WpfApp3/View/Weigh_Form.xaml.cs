using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;
using PH_Lib = WpfApp3.SharedLib.Printing_History_SharedLib;

namespace WpfApp3.View
{
    /// <summary>
    /// Interaction logic for Product_Info.xaml
    /// </summary>
    public partial class Weigh_Form : UserControl
    {
        internal Thread thread_check_scale_status;
        internal bool IsStopThread = true;
        public Weigh_Form()
        {
            InitializeComponent();
        }

        private void Weigh_Form_User_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Weigh_Form_ViewModel();
            var viewModel1 = (this.DataContext as Weigh_Form_ViewModel);
            PH_Lib.SfDataGrid1_Proxy = SfDataGrid1;
            IsStopThread = true;
            viewModel1.Printinghistory_Model = MW_Lib.Printinghistory_Model_Proxy;
            SfTextBoxExt4.Text = "RM" + MW_Lib.Printinghistory_Model_Proxy.SP_UnitPrice.ToString("0.00") + @"/KG";
            Print();
        }

        private void Print()
        {
            thread_check_scale_status = new Thread(() =>
            {
                using (SerialPort _serialport = COM_Port_Handler.Initial_Serial_Port())
                {
                    Tuple<string, string> temp = COM_Port_Handler.Check_Scale_Status(_serialport);
                    Thread.Sleep(200);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SfTextBoxExt6.Text = temp.Item1;
                        SfTextBoxExt6.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(temp.Item2);
                    });

                    while (IsStopThread)
                    {
                        object item = MW_Lib.Printinghistory_Model_Proxy.Clone();
                        MW_Lib.Printinghistory_Model_Proxy = item as PrintingHistory_Model;
                        MW_Lib.Printinghistory_Model_Proxy.PH_WeighingScaleData = COM_Port_Handler.Start_Weighing(_serialport);

                        if (!MW_Lib.Printinghistory_Model_Proxy.PH_WeighingScaleData.Equals(0))
                        {
                            using (ProductListingContext context = new())
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    BitmapImage tempBitmapImage = IUOM_Lib.CustomBitmapImage(
                                        context.CertificationImages
                                        .Where(a => a.Code.Equals(MW_Lib.Printinghistory_Model_Proxy.PH_OrgCertText2))
                                        .Select(a => a.Image).First()
                                        );
                                    MW_Lib.Printinghistory_Model_Proxy.PH_PrintingDate = DateTime.Now;
                                    LabelProcessor.MainProcessor(tempBitmapImage, MW_Lib.Printinghistory_Model_Proxy.PH_WeighingScaleData);
                                    (this.DataContext as Weigh_Form_ViewModel).PrintingHistory_IEnum.Add(MW_Lib.Printinghistory_Model_Proxy);
                                    context.PrintingHistories.Add(MW_Lib.Printinghistory_Model_Proxy);
                                    context.SaveChanges();
                                });
                            }
                        }
                    }

                    _serialport.Close();
                    Thread.Sleep(300);
                }
            });
            thread_check_scale_status.Start();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            IsStopThread = false;
            thread_check_scale_status.Join();
            SharedLib.MainWindow_SharedLib.ContentControl1_Proxy.Content = new Product_Info();
        }

        private void button_DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            PH_Lib.Delete();
        }

        private void SfDataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete) ||
                    e.Key == Key.Delete)
                {
                    PH_Lib.Delete();
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
                PH_Lib.Copy();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CopyRow_Click error " + ex.Message);
            }
        }
    }
}
