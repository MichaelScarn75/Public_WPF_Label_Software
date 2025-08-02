using System;

namespace WpfApp3.Model
{
    public partial class PrintingHistory_Model : ModelBase, ICloneable
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
        private Decimal _IUOM_qtyperunitofMeasure = 0;
        private string _CUST_customerlabelcode;
        private DateTime _PH_PrintingDate;
        private string? _PH_Price;
        private string? _PH_ProductBarcode;
        private decimal _PH_WeighingScaleData;
        private string _PH_EncryptedQrdata;
        private string _PH_OrgCertText;
        private string _PH_MyOrgCertText;
        private string _PH_OrgCertText2;
        private string _PH_MyOrgCertText2;
        private string _PH_PricePerKgText;
        private string _PH_DateAsAlphabetText;
        private string _PH_IpAddress;
        private string _PH_Location;
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

        public string? SP_ProductBarcode
        {
            get { return this._SP_productbarcode; }

            set
            {
                this._SP_productbarcode = value;
                this.RaisePropertyChanged(nameof(this.SP_ProductBarcode));
            }
        }

        public string? SP_BarcodeFormat
        {
            get { return this._SP_barcode_format; }

            set
            {
                this._SP_barcode_format = value;
                this.RaisePropertyChanged(nameof(this.SP_BarcodeFormat));
            }
        }

        public string? SP_CustomerSku
        {
            get { return this._SP_customersku; }

            set
            {
                this._SP_customersku = value;
                this.RaisePropertyChanged(nameof(this.SP_CustomerSku));
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

        public string SP_Currencycode
        {
            get { return this._SP_currencycode; }

            set
            {
                this._SP_currencycode = value;
                this.RaisePropertyChanged(nameof(this.SP_Currencycode));
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

        public string? IT_Description
        {
            get { return this._IT_description; }

            set
            {
                this._IT_description = value;
                this.RaisePropertyChanged(nameof(this.IT_Description));
            }
        }

        public string? IT_InventoryPostingGroup
        {
            get { return this._IT_inventorypostinggroup; }

            set
            {
                this._IT_inventorypostinggroup = value;
                this.RaisePropertyChanged(nameof(this.IT_InventoryPostingGroup));
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

        public byte[]? IM_Image
        {
            get { return this._IM_productImageBytes; }

            set
            {
                this._IM_productImageBytes = value;
                this.RaisePropertyChanged(nameof(this.IM_Image));
            }
        }

        public decimal IUOM_QtyPerUnitOfMeasure
        {
            get { return this._IUOM_qtyperunitofMeasure; }

            set
            {
                this._IUOM_qtyperunitofMeasure = value;
                this.RaisePropertyChanged(nameof(this.IUOM_QtyPerUnitOfMeasure));
            }
        }

        public string? CUST_CustomerLabelCode
        {
            get { return this._CUST_customerlabelcode; }

            set
            {
                this._CUST_customerlabelcode = value;
                this.RaisePropertyChanged(nameof(this.CUST_CustomerLabelCode));
            }
        }

        public DateTime PH_PrintingDate
        {
            get { return this._PH_PrintingDate; }

            set
            {
                this._PH_PrintingDate = value;
                this.RaisePropertyChanged(nameof(this.PH_PrintingDate));
            }
        }

        public string? PH_Price
        {
            get { return this._PH_Price; }

            set
            {
                this._PH_Price = value;
                this.RaisePropertyChanged(nameof(this.PH_Price));
            }
        }

        public string? PH_ProductBarcode
        {
            get { return this._PH_ProductBarcode; }

            set
            {
                this._PH_ProductBarcode = value;
                this.RaisePropertyChanged(nameof(this.PH_ProductBarcode));
            }
        }

        public decimal PH_WeighingScaleData
        {
            get { return this._PH_WeighingScaleData; }

            set
            {
                this._PH_WeighingScaleData = value;
                this.RaisePropertyChanged(nameof(this.PH_WeighingScaleData));
            }
        }

        public string PH_EncryptedQrdata
        {
            get { return this._PH_EncryptedQrdata; }

            set
            {
                this._PH_EncryptedQrdata = value;
                this.RaisePropertyChanged(nameof(this.PH_EncryptedQrdata));
            }
        }

        public string PH_OrgCertText
        {
            get { return this._PH_OrgCertText; }

            set
            {
                this._PH_OrgCertText = value;
                this.RaisePropertyChanged(nameof(this.PH_OrgCertText));
            }
        }

        public string PH_MyOrgCertText
        {
            get { return this._PH_MyOrgCertText; }

            set
            {
                this._PH_MyOrgCertText = value;
                this.RaisePropertyChanged(nameof(this.PH_MyOrgCertText));
            }
        }

        public string PH_OrgCertText2
        {
            get { return this._PH_OrgCertText2; }

            set
            {
                this._PH_OrgCertText2 = value;
                this.RaisePropertyChanged(nameof(this.PH_OrgCertText2));
            }
        }

        public string PH_MyOrgCertText2
        {
            get { return this._PH_MyOrgCertText2; }

            set
            {
                this._PH_MyOrgCertText2 = value;
                this.RaisePropertyChanged(nameof(this.PH_MyOrgCertText2));
            }
        }

        public string PH_PricePerKgText
        {
            get { return this._PH_PricePerKgText; }

            set
            {
                this._PH_PricePerKgText = value;
                this.RaisePropertyChanged(nameof(this.PH_PricePerKgText));
            }
        }

        public string PH_DateAsAlphabetText
        {
            get { return this._PH_DateAsAlphabetText; }

            set
            {
                this._PH_DateAsAlphabetText = value;
                this.RaisePropertyChanged(nameof(this.PH_DateAsAlphabetText));
            }
        }

        public string PH_IpAddress
        {
            get { return this._PH_IpAddress; }

            set
            {
                this._PH_IpAddress = value;
                this.RaisePropertyChanged(nameof(this.PH_IpAddress));
            }
        }

        public string PH_Location
        {
            get { return this._PH_Location; }

            set
            {
                this._PH_Location = value;
                this.RaisePropertyChanged(nameof(this.PH_Location));
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

        public object Clone()
        {
            return new PrintingHistory_Model()
            {
                SP_SalesCode = this.SP_SalesCode,
                SP_ItemNo = this.SP_ItemNo,
                SP_UnitOfMeasureCode = this.SP_UnitOfMeasureCode,
                SP_UnitPrice = this.SP_UnitPrice,
                SP_StartingDate = this.SP_StartingDate,
                SP_EndingDate = this.SP_EndingDate,
                SP_ProductBarcode = this.SP_ProductBarcode,
                SP_BarcodeFormat = this.SP_BarcodeFormat,
                SP_CustomerSku = this.SP_CustomerSku,
                SP_Hidden = this.SP_Hidden,
                SP_EnglishLabelDescription = this.SP_EnglishLabelDescription,
                SP_MalayLabelDescription = this.SP_MalayLabelDescription,
                SP_ChineseLabelDescription = this.SP_ChineseLabelDescription,
                SP_LabelUnitOfMeasure = this.SP_LabelUnitOfMeasure,
                SP_LabelSize = this.SP_LabelSize,
                SP_Currencycode = this.SP_Currencycode,
                SP_WeightItem = this.SP_WeightItem,
                SP_WeightScale = this.SP_WeightScale,
                SD_SalesCode = this.SD_SalesCode,
                SD_ItemNo = this.SD_ItemNo,
                SD_UnitOfMeasureCode = this.SD_UnitOfMeasureCode,
                SD_LineDiscount = this.SD_LineDiscount,
                SD_StartingDate = this.SD_StartingDate,
                SD_EndingDate = this.SD_EndingDate,
                IT_Description = this.IT_Description,
                IT_InventoryPostingGroup = this.IT_InventoryPostingGroup,
                IT_Country = this.IT_Country,
                IM_Image = this.IM_Image,
                IUOM_QtyPerUnitOfMeasure = this.IUOM_QtyPerUnitOfMeasure,
                CUST_CustomerLabelCode = this.CUST_CustomerLabelCode,
                PH_PrintingDate = this.PH_PrintingDate,
                PH_Price = this.PH_Price,
                PH_ProductBarcode = this.PH_ProductBarcode,
                PH_WeighingScaleData = this.PH_WeighingScaleData,
                PH_EncryptedQrdata = this.PH_EncryptedQrdata,
                PH_OrgCertText = this.PH_OrgCertText,
                PH_MyOrgCertText = this.PH_MyOrgCertText,
                PH_OrgCertText2 = this.PH_OrgCertText2,
                PH_MyOrgCertText2 = this.PH_MyOrgCertText2,
                PH_PricePerKgText = this.PH_PricePerKgText,
                PH_DateAsAlphabetText = this.PH_DateAsAlphabetText,
                PH_IpAddress = this.PH_IpAddress,
                PH_Location = this.PH_Location,
            };
        }
    }
}
