// <copyright file="itemunitsofmeasure_Model.cs" company="PlaceholderCompany">
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

    public class Itemunitsofmeasure_Model : ModelBase, IEditableObject
    {
        private string _itemNo = null!;
        private string _unitofmeasureCode = null!;
        private Decimal _qtyperunitofMeasure = 0;
        private int _id;
        public bool ItemunitsofmeasureModelHasError;
        private Dictionary<string, object> storedValues;


        public string ItemNo
        {
            get { return this._itemNo; }

            set
            {
                this._itemNo = value;
                this.RaisePropertyChanged(nameof(this.ItemNo));
            }
        }

        public string UnitOfMeasureCode
        {
            get { return this._unitofmeasureCode; }

            set
            {
                this._unitofmeasureCode = value;
                this.RaisePropertyChanged(nameof(this.UnitOfMeasureCode));
            }
        }

        public Decimal QtyPerUnitOfMeasure
        {
            get { return this._qtyperunitofMeasure; }

            set
            {
                this._qtyperunitofMeasure = value;
                this.RaisePropertyChanged(nameof(this.QtyPerUnitOfMeasure));
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

        public Itemunitsofmeasure_Model()
        {
        }
    }
}
