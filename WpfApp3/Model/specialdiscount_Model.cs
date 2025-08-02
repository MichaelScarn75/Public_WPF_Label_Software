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

    public class Specialdiscount_Model : ModelBase, IEditableObject
    {
        private string _salescode;
        private string _itemno;
        private string _unitofmeasurecode;
        private decimal _linediscount;
        private DateTime _startingdate;
        private DateTime? _endingdate;
        private int _id;
        public bool SpecialdiscountModelHasError;
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

        public decimal LineDiscount
        {
            get { return this._linediscount; }

            set
            {
                this._linediscount = value;
                this.RaisePropertyChanged(nameof(this.LineDiscount));
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

        public Specialdiscount_Model()
        {
        }
    }
}
