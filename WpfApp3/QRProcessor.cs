using System;
using System.Collections.Generic;
using QRCoder;

namespace WpfApp3
{
    internal class QRProcessor
    {
        public static byte[] Generate_QR(string idenfitier)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(idenfitier, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20, false);
                return qrCodeImage;
            }
        }

        public static string RandomStringGenerator()
        {
            List<char> sample = new()
            {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
            };

            string result = string.Empty;
            Random r = new Random();

            for (int i = 0; i < 9; i++)
            {
                result += sample[r.Next(0, 51)];
            }

            return "==" + result + "==";
        }

        public static string DateToAlphabet(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }

            Dictionary<string, string> alphabet_day = new Dictionary<string, string>()
            {
                { "01", "ZA" },
                { "02", "ZB" },
                { "03", "ZC" },
                { "04", "ZD" },
                { "05", "ZE" },
                { "06", "ZF" },
                { "07", "ZG" },
                { "08", "ZH" },
                { "09", "ZI" },
                { "10", "AZ" },
                { "11", "AA" },
                { "12", "AB" },
                { "13", "AC" },
                { "14", "AD" },
                { "15", "AE" },
                { "16", "AF" },
                { "17", "AG" },
                { "18", "AH" },
                { "19", "AI" },
                { "20", "BZ" },
                { "21", "BA" },
                { "22", "BB" },
                { "23", "BC" },
                { "24", "BB" },
                { "25", "BE" },
                { "26", "BF" },
                { "27", "BG" },
                { "28", "BH" },
                { "29", "BI" },
                { "30", "CZ" },
                { "31", "CA" }
            };

            Dictionary<string, string> alphabet_month = new Dictionary<string, string>()
            {
                { "1", "A" },
                { "2", "B" },
                { "3", "C" },
                { "4", "D" },
                { "5", "E" },
                { "6", "F" },
                { "7", "G" },
                { "8", "H" },
                { "9", "I" },
                { "0", "J" },
                { "11", "K" },
                { "12", "L" }
            };

            alphabet_day.TryGetValue(dateTime?.Day.ToString(), out var day);
            alphabet_month.TryGetValue(dateTime?.Month.ToString(), out var month);

            return "H" + day + month;
        }
    }
}
