// <copyright file="unitsofmeasure_Model.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public class Specialprice_Model : ModelBase, IEditableObject
    {
        private string _salescode;
        private string _itemno;
        private string _unitofmeasurecode;
        private decimal _unitprice;
        private DateTime _startingdate;
        private DateTime? _endingdate;
        private string _productbarcode;
        private string _barcode_format;
        private string? _customersku;
        private bool? _hidden;
        private string? _englishlabeldescription;
        private string? _malaylabeldescription;
        private string? _chineselabeldescription;
        private string _labelunitofmeasure;
        private string _labelsize;
        private string _currencycode;
        private bool _weightitem;
        private bool _weightscale;
        private int _id;
        public bool ChoosecustomerModelHasError;
        private Dictionary<string, object> storedValues;

        public string SalesCode
        {
            get { return this._salescode; }

            set
            {
                this._salescode = value;
                this.RaisePropertyChanged(nameof(this.SalesCode));
            }
        }

        public string ItemNo
        {
            get { return this._itemno; }

            set
            {
                this._itemno = value;
                this.RaisePropertyChanged(nameof(this.ItemNo));
            }
        }

        public string UnitOfMeasureCode
        {
            get { return this._unitofmeasurecode; }

            set
            {
                this._unitofmeasurecode = value;
                this.RaisePropertyChanged(nameof(this.UnitOfMeasureCode));
            }
        }

        public decimal UnitPrice
        {
            get { return this._unitprice; }

            set
            {
                this._unitprice = value;
                this.RaisePropertyChanged(nameof(this.UnitPrice));
            }
        }

        public DateTime StartingDate
        {
            get { return this._startingdate; }

            set
            {
                this._startingdate = value;
                this.RaisePropertyChanged(nameof(this.StartingDate));
            }
        }

        public DateTime? EndingDate
        {
            get { return this._endingdate; }

            set
            {
                this._endingdate = value;
                this.RaisePropertyChanged(nameof(this.EndingDate));
            }
        }

        public string ProductBarcode
        {
            get { return this._productbarcode; }

            set
            {
                this._productbarcode = value;
                this.RaisePropertyChanged(nameof(this.ProductBarcode));
            }
        }

        public string Barcode_Format
        {
            get { return this._barcode_format; }

            set
            {
                this._barcode_format = value;
                this.RaisePropertyChanged(nameof(this.Barcode_Format));
            }
        }

        public string? CustomerSKU
        {
            get { return this._customersku; }

            set
            {
                this._customersku = value;
                this.RaisePropertyChanged(nameof(this.CustomerSKU));
            }
        }

        public bool? Hidden
        {
            get { return this._hidden; }

            set
            {
                this._hidden = value;
                this.RaisePropertyChanged(nameof(this.Hidden));
            }
        }

        public string? EnglishLabelDescription
        {
            get { return this._englishlabeldescription; }

            set
            {
                this._englishlabeldescription = value;
                this.RaisePropertyChanged(nameof(this.EnglishLabelDescription));
            }
        }

        public string? MalayLabelDescription
        {
            get { return this._malaylabeldescription; }

            set
            {
                this._malaylabeldescription = value;
                this.RaisePropertyChanged(nameof(this.MalayLabelDescription));
            }
        }

        public string? ChineseLabelDescription
        {
            get { return this._chineselabeldescription; }

            set
            {
                this._chineselabeldescription = value;
                this.RaisePropertyChanged(nameof(this.ChineseLabelDescription));
            }
        }

        public string LabelUnitOfMeasure
        {
            get { return this._labelunitofmeasure; }

            set
            {
                this._labelunitofmeasure = value;
                this.RaisePropertyChanged(nameof(this.LabelUnitOfMeasure));
            }
        }

        public string LabelSize
        {
            get { return this._labelsize; }

            set
            {
                this._labelsize = value;
                this.RaisePropertyChanged(nameof(this.LabelSize));
            }
        }

        public string CurrencyCode
        {
            get { return this._currencycode; }

            set
            {
                this._currencycode = value;
                this.RaisePropertyChanged(nameof(this.CurrencyCode));
            }
        }

        public bool WeightItem
        {
            get { return this._weightitem; }

            set
            {
                this._weightitem = value;
                this.RaisePropertyChanged(nameof(this.WeightItem));
            }
        }

        public bool WeightScale
        {
            get { return this._weightscale; }

            set
            {
                this._weightscale = value;
                this.RaisePropertyChanged(nameof(this.WeightScale));
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

        protected Dictionary<string, object> BackUp()
        {
            var dict = new Dictionary<string, object>();
            var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;

            foreach (var pDescriptor in itemProperties)
            {

                if (pDescriptor.CanWrite)
                    dict.Add(pDescriptor.Name, pDescriptor.GetValue(this));
            }
            return dict;
        }

        public void BeginEdit()
        {
            this.storedValues = this.BackUp();
        }

        public void CancelEdit()
        {
            if (this.storedValues == null)
            {
                return;
            }

            foreach (var item in this.storedValues)
            {
                var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
                var pDesc = itemProperties.FirstOrDefault(p => p.Name == item.Key);

                if (pDesc != null)
                {
                    pDesc.SetValue(this, item.Value);
                }
            }
        }

        public void EndEdit()
        {

            if (this.storedValues != null)
            {
                this.storedValues.Clear();
                this.storedValues = null;
            }
        }

        public Specialprice_Model()
        {
        }
    }
}
