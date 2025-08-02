// <copyright file="unitsofmeasure_Model.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;

    public class Productlisting_Model : ModelBase
    {
        private string _SP_salescode;
        private string _SP_itemno;
        private string _SP_unitofmeasurecode;
        private decimal _SP_unitprice;
        private DateTime _SP_startingdate;
        private DateTime? _SP_endingdate;
        private string _SP_productbarcode;
        private string _SP_barcode_format;
        private string? _SP_customersku;
        private bool? _SP_hidden;
        private string? _SP_englishlabeldescription;
        private string? _SP_malaylabeldescription;
        private string? _SP_chineselabeldescription;
        private string _SP_labelunitofmeasure;
        private string _SP_labelsize;
        private string _SP_currencycode;
        private bool _SP_weightitem;
        private bool _SP_weightscale;
        private string _SD_salescode;
        private string _SD_itemno;
        private string _SD_unitofmeasurecode;
        private decimal _SD_linediscount;
        private DateTime _SD_startingdate;
        private DateTime? _SD_endingdate;
        private string _IT_description;
        private string _IT_inventorypostinggroup;
        private string _IT_country;
        private byte[] _IM_productImageBytes;
        private BitmapImage _IM_productBitmapImage;
        private Decimal _IUOM_qtyperunitofMeasure = 0;
        private string _CUST_customerlabelcode;
        private int _id;
        public bool ChoosecustomerModelHasError;



        public string SP_SalesCode
        {
            get { return this._SP_salescode; }

            set
            {
                this._SP_salescode = value;
                this.RaisePropertyChanged(nameof(this.SP_SalesCode));
            }
        }

        public string SP_ItemNo
        {
            get { return this._SP_itemno; }

            set
            {
                this._SP_itemno = value;
                this.RaisePropertyChanged(nameof(this.SP_ItemNo));
            }
        }

        public string SP_UnitOfMeasureCode
        {
            get { return this._SP_unitofmeasurecode; }

            set
            {
                this._SP_unitofmeasurecode = value;
                this.RaisePropertyChanged(nameof(this.SP_UnitOfMeasureCode));
            }
        }

        public decimal SP_UnitPrice
        {
            get { return this._SP_unitprice; }

            set
            {
                this._SP_unitprice = value;
                this.RaisePropertyChanged(nameof(this.SP_UnitPrice));
            }
        }

        public DateTime SP_StartingDate
        {
            get { return this._SP_startingdate; }

            set
            {
                this._SP_startingdate = value;
                this.RaisePropertyChanged(nameof(this.SP_StartingDate));
            }
        }

        public DateTime? SP_EndingDate
        {
            get { return this._SP_endingdate; }

            set
            {
                this._SP_endingdate = value;
                this.RaisePropertyChanged(nameof(this.SP_EndingDate));
            }
        }

        public string SP_ProductBarcode
        {
            get { return this._SP_productbarcode; }

            set
            {
                this._SP_productbarcode = value;
                this.RaisePropertyChanged(nameof(this.SP_ProductBarcode));
            }
        }

        public string SP_Barcode_Format
        {
            get { return this._SP_barcode_format; }

            set
            {
                this._SP_barcode_format = value;
                this.RaisePropertyChanged(nameof(this.SP_Barcode_Format));
            }
        }

        public string? SP_CustomerSKU
        {
            get { return this._SP_customersku; }

            set
            {
                this._SP_customersku = value;
                this.RaisePropertyChanged(nameof(this.SP_CustomerSKU));
            }
        }

        public bool? SP_Hidden
        {
            get { return this._SP_hidden; }

            set
            {
                this._SP_hidden = value;
                this.RaisePropertyChanged(nameof(this.SP_Hidden));
            }
        }

        public string? SP_EnglishLabelDescription
        {
            get { return this._SP_englishlabeldescription; }

            set
            {
                this._SP_englishlabeldescription = value;
                this.RaisePropertyChanged(nameof(this.SP_EnglishLabelDescription));
            }
        }

        public string? SP_MalayLabelDescription
        {
            get { return this._SP_malaylabeldescription; }

            set
            {
                this._SP_malaylabeldescription = value;
                this.RaisePropertyChanged(nameof(this.SP_MalayLabelDescription));
            }
        }

        public string? SP_ChineseLabelDescription
        {
            get { return this._SP_chineselabeldescription; }

            set
            {
                this._SP_chineselabeldescription = value;
                this.RaisePropertyChanged(nameof(this.SP_ChineseLabelDescription));
            }
        }

        public string SP_LabelUnitOfMeasure
        {
            get { return this._SP_labelunitofmeasure; }

            set
            {
                this._SP_labelunitofmeasure = value;
                this.RaisePropertyChanged(nameof(this.SP_LabelUnitOfMeasure));
            }
        }

        public string SP_LabelSize
        {
            get { return this._SP_labelsize; }

            set
            {
                this._SP_labelsize = value;
                this.RaisePropertyChanged(nameof(this.SP_LabelSize));
            }
        }

        public string SP_CurrencyCode
        {
            get { return this._SP_currencycode; }

            set
            {
                this._SP_currencycode = value;
                this.RaisePropertyChanged(nameof(this.SP_CurrencyCode));
            }
        }

        public bool SP_WeightItem
        {
            get { return this._SP_weightitem; }

            set
            {
                this._SP_weightitem = value;
                this.RaisePropertyChanged(nameof(this.SP_WeightItem));
            }
        }

        public bool SP_WeightScale
        {
            get { return this._SP_weightscale; }

            set
            {
                this._SP_weightscale = value;
                this.RaisePropertyChanged(nameof(this.SP_WeightScale));
            }
        }

        public string SD_SalesCode
        {
            get { return this._SD_salescode; }

            set
            {
                this._SD_salescode = value;
                this.RaisePropertyChanged(nameof(this.SD_SalesCode));
            }
        }

        public string SD_ItemNo
        {
            get { return this._SD_itemno; }

            set
            {
                this._SD_itemno = value;
                this.RaisePropertyChanged(nameof(this.SD_ItemNo));
            }
        }

        public string SD_UnitOfMeasureCode
        {
            get { return this._SD_unitofmeasurecode; }

            set
            {
                this._SD_unitofmeasurecode = value;
                this.RaisePropertyChanged(nameof(this.SD_UnitOfMeasureCode));
            }
        }

        public decimal SD_LineDiscount
        {
            get { return this._SD_linediscount; }

            set
            {
                this._SD_linediscount = value;
                this.RaisePropertyChanged(nameof(this.SD_LineDiscount));
            }
        }

        public DateTime SD_StartingDate
        {
            get { return this._SD_startingdate; }

            set
            {
                this._SD_startingdate = value;
                this.RaisePropertyChanged(nameof(this.SD_StartingDate));
            }
        }

        public DateTime? SD_EndingDate
        {
            get { return this._SD_endingdate; }

            set
            {
                this._SD_endingdate = value;
                this.RaisePropertyChanged(nameof(this.SD_EndingDate));
            }
        }

        public string IT_description
        {
            get { return this._IT_description; }

            set
            {
                this._IT_description = value;
                this.RaisePropertyChanged(nameof(this.IT_description));
            }
        }

        public string IT_inventorypostinggroup
        {
            get { return this._IT_inventorypostinggroup; }

            set
            {
                this._IT_inventorypostinggroup = value;
                this.RaisePropertyChanged(nameof(this.IT_inventorypostinggroup));
            }
        }

        public string IT_Country
        {
            get { return this._IT_country; }

            set
            {
                this._IT_country = value;
                this.RaisePropertyChanged(nameof(this.IT_Country));
            }
        }

        public byte[] IM_Image
        {
            get { return this._IM_productImageBytes; }
            set
            {
                this._IM_productImageBytes = value;
                this.RaisePropertyChanged(nameof(this.IM_Image));
            }
        }

        public BitmapImage IM_productBitmapImage
        {
            get { return this._IM_productBitmapImage; }
            set
            {
                this._IM_productBitmapImage = value;
                this.RaisePropertyChanged(nameof(this.IM_productBitmapImage));
            }
        }

        public Decimal IUOM_QtyPerUnitOfMeasure
        {
            get { return this._IUOM_qtyperunitofMeasure; }

            set
            {
                this._IUOM_qtyperunitofMeasure = value;
                this.RaisePropertyChanged(nameof(this.IUOM_QtyPerUnitOfMeasure));
            }
        }

        public string CUST_CustomerLabelCode
        {
            get { return this._CUST_customerlabelcode; }

            set
            {
                this._CUST_customerlabelcode = value;
                this.RaisePropertyChanged(nameof(this.CUST_CustomerLabelCode));
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.RaisePropertyChanged(nameof(this.Id));
            }
        }

        public Productlisting_Model()
        {
            this.IM_productBitmapImage = (BitmapImage)Application.Current.Resources["Img_product_image"];
        }
    }
}
