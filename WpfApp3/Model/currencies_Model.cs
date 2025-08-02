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

    public class Currencies_Model : ModelBase, IEditableObject
    {
        private string _code = null!;
        private string? _description;
        private string? _symbol;
        private DateTime? _exchangeratedate;
        private Decimal? _exchange_rate;
        private int _id;
        private Dictionary<string, object> storedValues;

        public string Code
        {
            get { return this._code; }

            set
            {
                this._code = value;
                this.RaisePropertyChanged(nameof(this.Code));
            }
        }

        public string? Description
        {
            get { return this._description; }

            set
            {
                this._description = value;
                this.RaisePropertyChanged(nameof(this.Description));
            }
        }

        public string? Symbol
        {
            get { return this._symbol; }

            set
            {
                this._symbol = value;
                this.RaisePropertyChanged(nameof(this.Symbol));
            }
        }

        public DateTime? ExchangeRateDate
        {
            get { return this._exchangeratedate; }

            set
            {
                this._exchangeratedate = value;
                this.RaisePropertyChanged(nameof(this.ExchangeRateDate));
            }
        }

        public Decimal? Exchange_Rate
        {
            get { return this._exchange_rate; }

            set
            {
                this._exchange_rate = value;
                this.RaisePropertyChanged(nameof(this.Exchange_Rate));
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

        public Currencies_Model()
        {
        }
    }
}
