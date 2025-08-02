using System;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp3.Model;
using WpfApp3.View;
using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

namespace WpfApp3
{
    internal class LabelProcessor
    {
        public static void BigLabelProcessor(BitmapImage tempBitmapImage, decimal weight)
        {
            var GV_PHModel = MW_Lib.Printinghistory_Model_Proxy;
            var userconfig = MW_Lib.User_Configuration_ViewModel_Proxy.User_Configuration;
            using (ProductListingContext context = new())
            {
                string price, barcode, priceperkg;
                BarcodeProcessor.GetBarcodeString(out price, out barcode, out priceperkg, GV_PHModel.PH_WeighingScaleData);

                BigLabel biglabel = new BigLabel();
                biglabel.TextBox1.Text = GV_PHModel.PH_OrgCertText;
                biglabel.TextBox2.Text = GV_PHModel.SP_EnglishLabelDescription;
                biglabel.TextBox3.Text = GV_PHModel.SP_MalayLabelDescription;
                biglabel.TextBox4.Text = GV_PHModel.SP_ChineseLabelDescription;
                biglabel.TextBox5.Text = price;
                if (weight == decimal.Zero)
                {
                    biglabel.TextBox6.Text = "(" + GV_PHModel.SP_LabelUnitOfMeasure + ") when packed";
                }
                else
                {
                    biglabel.TextBox6.Text = "(" + weight + "KG" + ") when packed";
                }
                biglabel.TextBox7.Text = priceperkg;
                biglabel.TextBox8.Text = GV_PHModel.PH_MyOrgCertText;
                biglabel.TextBox9.Text = GV_PHModel.CUST_CustomerLabelCode;
                string EncryptedQRData = QRProcessor.RandomStringGenerator();
                biglabel.Image1.Source = IUOM_Lib.CustomBitmapImage(QRProcessor.Generate_QR(EncryptedQRData));
                biglabel.Image2.Source = tempBitmapImage;
                TransformedBitmap rotatedtempImage = new TransformedBitmap(BarcodeProcessor.Generate_Barcode(barcode), new RotateTransform(270));
                biglabel.Image4.Source = rotatedtempImage;
                biglabel.TextBox10.Text = "Produce " + GV_PHModel.IT_Country;
                biglabel.TextBox11.Text = "Hasil " + GV_PHModel.IT_Country;
                biglabel.TextBox12.Text = QRProcessor.DateToAlphabet(GV_PHModel.PH_PrintingDate);
                GV_PHModel.PH_Price = price;
                GV_PHModel.PH_ProductBarcode = barcode;
                GV_PHModel.PH_EncryptedQrdata = EncryptedQRData;
                GV_PHModel.PH_PricePerKgText = priceperkg;
                GV_PHModel.PH_DateAsAlphabetText = biglabel.TextBox12.Text;
                GV_PHModel.PH_IpAddress = IUOM_Lib.GetLocalIPAddress();

                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueue printer = printServer.GetPrintQueue(userconfig.BigLabelPrinter);
                PrintDialog dialog = new PrintDialog();
                dialog.PrintQueue = printer;
                dialog.PrintVisual(biglabel.stackPanel, "A Great Image.");
            };
        }

        public static void MediumLabelProcessor(decimal weight)
        {
            var GV_PHModel = MW_Lib.Printinghistory_Model_Proxy;
            var userconfig = MW_Lib.User_Configuration_ViewModel_Proxy.User_Configuration;
            using (ProductListingContext context = new())
            {
                string price, barcode, priceperkg;
                string EncryptedQRData = QRProcessor.RandomStringGenerator();
                BarcodeProcessor.GetBarcodeString(out price, out barcode, out priceperkg, GV_PHModel.PH_WeighingScaleData);

                MediumLabel mediumlabel = new MediumLabel();
                mediumlabel.TextBox1.Text = GV_PHModel.PH_OrgCertText + " " + GV_PHModel.SP_EnglishLabelDescription;
                mediumlabel.TextBox2.Text = GV_PHModel.SP_MalayLabelDescription;
                mediumlabel.TextBox3.Text = GV_PHModel.SP_ChineseLabelDescription;
                mediumlabel.TextBox4.Text = "Produce " + GV_PHModel.IT_Country;
                mediumlabel.TextBox5.Text = "Hasil " + GV_PHModel.IT_Country;
                mediumlabel.TextBox6.Text = price;
                if (weight == decimal.Zero)
                {
                    mediumlabel.TextBox6.Text = "(" + GV_PHModel.SP_LabelUnitOfMeasure + ") when packed";
                }
                else
                {
                    mediumlabel.TextBox6.Text = weight + "KG";
                }
                mediumlabel.Image1.Source = IUOM_Lib.CustomBitmapImage(QRProcessor.Generate_QR(EncryptedQRData));
                mediumlabel.TextBox8.Text = QRProcessor.DateToAlphabet(GV_PHModel.PH_PrintingDate);
                TransformedBitmap rotatedtempImage = new TransformedBitmap(BarcodeProcessor.Generate_Barcode(barcode), new RotateTransform(270));
                mediumlabel.Image4.Source = rotatedtempImage;
                GV_PHModel.PH_Price = price;
                GV_PHModel.PH_ProductBarcode = barcode;
                GV_PHModel.PH_EncryptedQrdata = EncryptedQRData;
                GV_PHModel.PH_MyOrgCertText = "";
                GV_PHModel.PH_PricePerKgText = priceperkg;
                GV_PHModel.PH_DateAsAlphabetText = mediumlabel.TextBox8.Text;
                GV_PHModel.PH_IpAddress = IUOM_Lib.GetLocalIPAddress();

                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueue printer = printServer.GetPrintQueue(userconfig.BigLabelPrinter);
                PrintDialog dialog = new PrintDialog();
                dialog.PrintQueue = printer;
                dialog.PrintVisual(mediumlabel.stackPanel, "A Great Image.");
            };
        }

        public static void SmallLabelProcessor(decimal weight)
        {
            var GV_PHModel = MW_Lib.Printinghistory_Model_Proxy;
            var userconfig = MW_Lib.User_Configuration_ViewModel_Proxy.User_Configuration;
            using (ProductListingContext context = new())
            {
                string price, barcode, priceperkg;
                string EncryptedQRData = QRProcessor.RandomStringGenerator();
                BarcodeProcessor.GetBarcodeString(out price, out barcode, out priceperkg, GV_PHModel.PH_WeighingScaleData);

                MediumLabel smalllabel = new MediumLabel();
                smalllabel.TextBox1.Text = GV_PHModel.PH_OrgCertText;
                smalllabel.TextBox2.Text = GV_PHModel.SP_EnglishLabelDescription;
                smalllabel.TextBox4.Text = "Produce " + GV_PHModel.IT_Country;
                smalllabel.TextBox5.Text = price;
                if (weight == decimal.Zero)
                {
                    smalllabel.TextBox6.Text = "(" + GV_PHModel.SP_LabelUnitOfMeasure + ") when packed";
                }
                else
                {
                    smalllabel.TextBox6.Text = weight + "KG";
                }
                smalllabel.Image1.Source = IUOM_Lib.CustomBitmapImage(QRProcessor.Generate_QR(EncryptedQRData));
                smalllabel.TextBox7.Text = QRProcessor.DateToAlphabet(GV_PHModel.PH_PrintingDate);
                smalllabel.Image4.Source = BarcodeProcessor.Generate_Barcode(barcode);
                GV_PHModel.PH_Price = price;
                GV_PHModel.PH_ProductBarcode = barcode;
                GV_PHModel.PH_EncryptedQrdata = EncryptedQRData;
                GV_PHModel.PH_MyOrgCertText = "";
                GV_PHModel.PH_PricePerKgText = priceperkg;
                GV_PHModel.PH_DateAsAlphabetText = smalllabel.TextBox8.Text;
                GV_PHModel.PH_IpAddress = IUOM_Lib.GetLocalIPAddress();

                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueue printer = printServer.GetPrintQueue(userconfig.BigLabelPrinter);
                PrintDialog dialog = new PrintDialog();
                dialog.PrintQueue = printer;
                dialog.PrintVisual(smalllabel.stackPanel, "A Great Image.");
            };
        }

        public static void MainProcessor(BitmapImage tempBitmapImage, decimal weight = decimal.Zero)
        {
            var GV_PHModel = MW_Lib.Printinghistory_Model_Proxy;
            GV_PHModel.PH_IpAddress = IUOM_Lib.GetLocalIPAddress();
            GV_PHModel.PH_PrintingDate = DateTime.Now;

            if (GV_PHModel.SP_LabelSize == "BIG LABEL")
            {
                BigLabelProcessor(tempBitmapImage, weight);
            }
            else if (GV_PHModel.SP_LabelSize == "MEDIUM LABEL")
            {
                MediumLabelProcessor(weight);
            }
            else if (GV_PHModel.SP_LabelSize == "SMALL LABEL")
            {
                SmallLabelProcessor(weight);
            }
        }
    }
}
