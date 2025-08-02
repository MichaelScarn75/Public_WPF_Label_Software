// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing.Printing;
    using System.IO;
    using System.IO.Ports;
    using System.Linq;
    using System.Reflection;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;

    public class User_Configuration_ViewModel : ViewModelBase
    {
        private userconfiguration_Model _user_configuration;
        private List<string> biglabelprinters_collection = new();
        private List<string> mediumlabelprinters_collection = new();
        private List<string> smalllabelprinters_collection = new();
        private List<string> basecurrency_collection = GetBaseCurrencies();
        private List<int> databits_collection = new() { 4, 5, 6, 7, 8 };
        private List<Parity> parity_collection = new()
        {
            Parity.None,
            Parity.Odd,
            Parity.Even,
            Parity.Mark,
            Parity.Space
        };
        private Dictionary<string, StopBits> stopbits_collection = new()
        {
            ["1"] = StopBits.One,
            ["1.5"] = StopBits.OnePointFive,
            ["2"] = StopBits.Two,
        };
        private List<int> baudrate_collection = new()
        {
            75,
            110,
            134,
            150,
            300,
            600,
            1200,
            1800,
            2400,
            4800,
            7200,
            9600,
            14400,
            19200,
            38400,
            57600,
            115200,
            128000
        };


        public List<int> DataBits_Collection
        {
            get { return this.databits_collection; }

            set
            {
                this.databits_collection = value;
                this.RaisePropertyChanged(nameof(DataBits_Collection));
                OCPropertyChanged(this.DataBits_Collection);
            }
        }

        public List<Parity> Parity_Collection
        {
            get { return this.parity_collection; }

            set
            {
                this.parity_collection = value;
                this.RaisePropertyChanged(nameof(Parity_Collection));
                OCPropertyChanged(this.Parity_Collection);
            }
        }

        public Dictionary<string, StopBits> StopBits_Collection
        {
            get { return this.stopbits_collection; }

            set
            {
                this.stopbits_collection = value;
                this.RaisePropertyChanged(nameof(StopBits_Collection));
                OCPropertyChanged(this.StopBits_Collection);
            }
        }

        public List<int> Baudrate_Collection
        {
            get { return this.baudrate_collection; }

            set
            {
                this.baudrate_collection = value;
                this.RaisePropertyChanged(nameof(Baudrate_Collection));
                OCPropertyChanged(this.Baudrate_Collection);
            }
        }

        public List<string> BigLabelPrinters_Collection
        {
            get { return this.biglabelprinters_collection; }

            set
            {
                this.biglabelprinters_collection = value;
                this.RaisePropertyChanged(nameof(BigLabelPrinters_Collection));
                OCPropertyChanged(this.BigLabelPrinters_Collection);
            }
        }

        public List<string> MediumLabelPrinters_Collection
        {
            get { return this.mediumlabelprinters_collection; }

            set
            {
                this.mediumlabelprinters_collection = value;
                this.RaisePropertyChanged(nameof(MediumLabelPrinters_Collection));
                OCPropertyChanged(this.MediumLabelPrinters_Collection);
            }
        }

        public List<string> SmallLabelPrinters_Collection
        {
            get { return this.smalllabelprinters_collection; }

            set
            {
                this.smalllabelprinters_collection = value;
                this.RaisePropertyChanged(nameof(SmallLabelPrinters_Collection));
                OCPropertyChanged(this.SmallLabelPrinters_Collection);
            }
        }

        public List<string> BaseCurrency_Collection
        {
            get { return this.basecurrency_collection; }

            set
            {
                this.basecurrency_collection = value;
                this.RaisePropertyChanged(nameof(BaseCurrency_Collection));
                OCPropertyChanged(this.BaseCurrency_Collection);
            }
        }

        public userconfiguration_Model User_Configuration
        {
            get { return this._user_configuration; }

            set
            {
                this._user_configuration = value;
                this.RaisePropertyChanged(nameof(User_Configuration));
                OCPropertyChanged(this.User_Configuration);
            }
        }

        public User_Configuration_ViewModel()
        {
            BigLabelPrinters_Collection = GetAllAvailablePrinters();
            MediumLabelPrinters_Collection = GetAllAvailablePrinters();
            SmallLabelPrinters_Collection = GetAllAvailablePrinters();
            BaseCurrency_Collection = GetBaseCurrencies();
            this.User_Configuration = new userconfiguration_Model();
            this.Populate__user_configuration();
        }

        public void Populate__user_configuration()
        {
            try
            {

                string resourceName = "WpfApp3.userconfig.json";
                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                using (StreamReader r = new StreamReader(stream))
                {
                    string json = r.ReadToEnd();
                    userconfiguration_Model items = JsonConvert.DeserializeObject<userconfiguration_Model>(json);

                    this.User_Configuration.Com_Port_Number = IUOM_Lib.CustomInt(NullSafeToString(items.Com_Port_Number));
                    this.User_Configuration.DataBit = IUOM_Lib.CustomInt(items.DataBit.ToString()) == 0 ? DataBits_Collection[0] : IUOM_Lib.CustomInt(items.DataBit.ToString());
                    this.User_Configuration.Parity = items.Parity;
                    this.User_Configuration.Baudrate = Baudrate_Collection
                        .Where(a => a.Equals(IUOM_Lib.CustomInt(NullSafeToString(items.Baudrate))))
                        .FirstOrDefault();
                    this.User_Configuration.Baudrate = this.User_Configuration.Baudrate == 0 ? 9600 : this.User_Configuration.Baudrate;
                    this.User_Configuration.StopBits = items.StopBits;
                    this.User_Configuration.BaseCurrency = BaseCurrency_Collection.Where(a => a.Equals(NullSafeToString(items.BaseCurrency))).FirstOrDefault();
                    var item1 = BigLabelPrinters_Collection
                        .Where(a => a
                        .Equals(NullSafeToString(items.BigLabelPrinter)))
                        .FirstOrDefault();
                    var item2 = MediumLabelPrinters_Collection
                        .Where(a => a
                        .Equals(NullSafeToString(items.MediumLabelPrinter)))
                        .FirstOrDefault();
                    var item3 = SmallLabelPrinters_Collection
                        .Where(a => a
                        .Equals(NullSafeToString(items.SmallLabelPrinter)))
                        .FirstOrDefault();
                    this.User_Configuration.BigLabelPrinter = item1.IsNullOrEmpty() ? BigLabelPrinters_Collection[0] : item1;
                    this.User_Configuration.MediumLabelPrinter = item2.IsNullOrEmpty() ? MediumLabelPrinters_Collection[0] : item2;
                    this.User_Configuration.SmallLabelPrinter = item3.IsNullOrEmpty() ? SmallLabelPrinters_Collection[0] : item3;
                }
            }
            catch (Exception ex)
            {
                //use default settings
                this.User_Configuration.Com_Port_Number = 0;
                this.User_Configuration.DataBit = 4;
                this.User_Configuration.Parity = Parity.None;
                this.User_Configuration.Baudrate = 9600;
                this.User_Configuration.StopBits = StopBits.One;
                this.User_Configuration.BigLabelPrinter = BigLabelPrinters_Collection[0];
                this.User_Configuration.MediumLabelPrinter = MediumLabelPrinters_Collection[0];
                this.User_Configuration.SmallLabelPrinter = SmallLabelPrinters_Collection[0];
                this.User_Configuration.BaseCurrency = string.Empty;
            }

            this.RaisePropertyChanged(nameof(User_Configuration));
            OCPropertyChanged(this.User_Configuration);
        }

        private static List<string> GetAllAvailablePrinters()
        {
            List<string> printers = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            return printers;
        }

        private static List<string> GetBaseCurrencies()
        {
            List<string> currencies = new List<string>();

            using (ProductListingContext context = new())
            {
                currencies = context.Currencies.Select(a => a.Code).ToList();
            }

            return currencies;
        }

        public static string NullSafeToString(object obj)
        {
            return obj != null ? obj.ToString() : string.Empty;
        }
    }
}
