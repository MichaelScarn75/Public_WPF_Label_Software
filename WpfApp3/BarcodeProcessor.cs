// <copyright file="BarcodeProcessor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Media.Imaging;
    using BarcodeStandard;
    using SkiaSharp;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    public class BarcodeProcessor
    {
        public static void Universal_EAN13(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal temp_price = decimal.Zero;
            decimal temp_priceperkg = decimal.Zero;

            if (weight == decimal.Zero)
            {
                temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100);
                temp_priceperkg = GV_item.SP_UnitPrice / GV_item.IUOM_QtyPerUnitOfMeasure;
            }
            else
            {
                temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100) * weight;
                temp_priceperkg = GV_item.SP_UnitPrice;
            }

            string _price = "RM" + temp_price.ToString("0.00");
            string _price_Barcode = temp_price.ToString("000.00").Replace(".", string.Empty);
            string _barcode = GV_item.SP_ProductBarcode[0..7] + _price_Barcode;
            string _priceperkg = "RM" + temp_priceperkg.ToString("0.00") + "/KG";
            price = _price;
            barcode = EAN13_checksum(_barcode);
            priceperkg = _priceperkg;
        }

        public static void PriceNotGenerative_EAN13(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal temp_price = decimal.Zero;
            decimal temp_priceperkg = decimal.Zero;

            if (weight == decimal.Zero)
            {
                temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100);
                temp_priceperkg = GV_item.SP_UnitPrice / GV_item.IUOM_QtyPerUnitOfMeasure;
            }
            else
            {
                temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100) * weight;
                temp_priceperkg = GV_item.SP_UnitPrice;
            }

            string _price = "RM" + temp_price.ToString("0.00");
            string _price_Barcode = temp_price.ToString("000.00").Replace(".", string.Empty);
            string _barcode = GV_item.SP_ProductBarcode[0..13];
            string _priceperkg = "RM" + temp_priceperkg.ToString("0.00") + "/KG";
            price = _price;
            barcode = EAN13_checksum(_barcode);
            priceperkg = _priceperkg;
        }

        public static void JayaGrocer_WEIGHTED_Code128(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100) * weight;
            decimal temp_priceperkg = GV_item.SP_UnitPrice;
            string _price = "RM" + temp_price.ToString("0.00");
            string _price_Barcode = temp_price.ToString("000.00").Replace(".", string.Empty);
            string _barcode = GV_item.SP_ProductBarcode[0..7] + "0" + _price_Barcode;
            string _priceperkg = "RM" + temp_priceperkg.ToString("0.00") + "/KG";
            price = _price;
            barcode = _barcode;
            priceperkg = _priceperkg;
        }

        public static void VillageGrocer_WEIGHTED_EAN13(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal _weight = weight;
            string _priceperkg = "RM" + GV_item.SP_UnitPrice.ToString("0.00") + "/KG";
            string _price = "RM" + (GV_item.SP_UnitPrice * _weight).ToString("0.00");
            string _weight_barcode = _weight.ToString("00.000").Replace(".", string.Empty);
            string _barcode = GV_item.SP_ProductBarcode[0..7] + _weight_barcode;
            price = _price;
            barcode = EAN13_checksum(_barcode);
            priceperkg = _priceperkg;
        }

        public static void ThePasar_WEIGHTED_EAN18(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal _weight = weight;
            decimal temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100) * weight;
            string _price_barcode = temp_price.ToString("000.00").Replace(".", string.Empty);
            string _price = "RM" + temp_price.ToString("0.00");
            string _weight_barcode = _weight.ToString("00.000").Replace(".", string.Empty);
            string _priceperkg = "RM" + GV_item.SP_UnitPrice.ToString("0.00") + "/KG";
            string _barcode = GV_item.SP_ProductBarcode[0..7] + _price_barcode + _weight_barcode;
            price = _price;
            barcode = EAN18_checksum(_barcode);
            priceperkg = _priceperkg;
        }

        public static void Aeon_WEIGHTED_EAN13(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero) // weight, barcode, price
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            var GV_customer = MW_Lib.Choosecustomer_Model_Proxy;
            decimal _weight = weight;
            decimal temp_price = GV_item.SP_UnitPrice * (GV_item.SD_LineDiscount / 100) * weight;
            string _price = "RM" + temp_price.ToString("0.00");
            string _price_barcode = temp_price.ToString("000.00").Replace(".", string.Empty);
            string _barcode = GV_item.SP_ProductBarcode[0..7] + _price_barcode;
            string _priceperkg = "RM" + GV_item.SP_UnitPrice.ToString("0.00") + "/KG";
            price = _price;
            barcode = EAN13_checksum(_barcode);
            priceperkg = _priceperkg;
        }

        public static string EAN13_checksum(string barcode)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                {
                    sum += Convert.ToInt32(barcode[i].ToString());
                }
                else
                {
                    sum += Convert.ToInt32(barcode[i].ToString()) * 3;
                }
            }

            int checksum = (10 - (sum % 10)) % 10;
            return (barcode + checksum.ToString());
        }

        public static string EAN18_checksum(string barcode)
        {
            int odd = 0;
            int even = 0;
            int checksum = 0;

            for (int i = 1; i <= barcode.Length; i++)
            {
                if (i % 2 != 0)
                {
                    odd += Convert.ToInt32(barcode[i - 1].ToString());
                }
                else
                {
                    even += Convert.ToInt32(barcode[i - 1].ToString());
                }
            }

            checksum = 10 - (((odd * 3) + even) % 10);

            if (checksum == 10)
            {
                checksum = 0;
            }

            return (barcode + checksum.ToString());
        }

        public static BitmapImage Generate_Barcode(string barcode)
        {
            // bitmap width here must be bigger than BarcodeLib source code in order for the actual bitmap to be bigger
            string BarcodeFormat;
            string ItemInfoArrayBarcode;
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;
            BarcodeStandard.Type BarcodeType = new BarcodeStandard.Type();
            SkiaSharp.SKTypeface customfontfile = SkiaSharp.SKTypeface.FromFile("Fonts\\ARIALN.TTF");
            SkiaSharp.SKFont skFont = new SKFont(customfontfile, 45f);
            BarcodeStandard.Barcode TemplateBarcode = new BarcodeStandard.Barcode()
            {
                Width = 450,
                IncludeLabel = true,
                Alignment = AlignmentPositions.Center,
                LabelFont = skFont
            };


            if (GV_item.SP_Barcode_Format == "Code128")
            {
                BarcodeType = BarcodeStandard.Type.Code128;
            }
            else if (GV_item.SP_Barcode_Format == "EAN13")
            {
                BarcodeType = BarcodeStandard.Type.Ean13;
            }
            else if (GV_item.SP_Barcode_Format == "EAN18") //EAN18 / GS1 - 128 / SSCC18
            {
                BarcodeType = BarcodeStandard.Type.Interleaved2Of5;
            }

            using SKImage skImage = TemplateBarcode.Encode(BarcodeType, barcode);
            using SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100); // Encode to PNG
            using MemoryStream ms = new MemoryStream(data.ToArray());

            // Convert to BitmapImage
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            bitmap.Freeze(); // Optional: makes it cross-thread accessible

            return bitmap;
        }


        public static void GetBarcodeString(out string price, out string barcode, out string priceperkg, decimal weight = decimal.Zero)
        {
            var GV_item = MW_Lib.Productlisting_simple_Model_Proxy;

            if (!GV_item.SP_WeightItem && !GV_item.SP_WeightScale)
            {
                switch (GV_item.SD_SalesCode)
                {
                    case "C000001":
                    case "C000002":
                    case "C000003":
                    case "C000004":
                    case "C000006":
                    case "PANDAMART":
                        PriceNotGenerative_EAN13(out price, out barcode, out priceperkg, weight);
                        break;
                    default:
                        Universal_EAN13(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                }
            }
            else if (GV_item.SP_WeightItem && GV_item.SP_WeightScale)
            {
                switch (GV_item.SD_SalesCode)
                {
                    case "C000001":
                        Aeon_WEIGHTED_EAN13(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                    case "C000002":
                        JayaGrocer_WEIGHTED_Code128(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                    case "C000003":
                        ThePasar_WEIGHTED_EAN18(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                    case "C000006":
                        VillageGrocer_WEIGHTED_EAN13(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                    default:
                        Universal_EAN13(out price, out barcode, out priceperkg, weight); // weight, barcode, price
                        break;
                }
            }
            else
            {
                Universal_EAN13(out price, out barcode, out priceperkg, weight); // weight, barcode, price
            }
        }
    }
}
