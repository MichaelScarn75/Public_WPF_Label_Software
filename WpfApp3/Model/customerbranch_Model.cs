// <copyright file="unitsofmeasure_Model.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.Model
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public class Customerbranch_Model : ModelBase, IEditableObject
    {
        private string _branchID = null!;
        private string _customerID = null!;
        private string? _customerDescription;
        private string? _branchDescription;
        private string? _address1;
        private string? _address2;
        private string? _address3;
        private string? _contactPerson;
        private string? _postalCode;
        private string? _phone1;
        private string? _phone2;
        private string? _fax;
        private string? _email;
        private string? _website;
        private string? _gSTRegNo;
        private string? _companyRegNo;
        private string? _vehicleNo;
        private string? _labelStyle;
        private int _id;
        public bool ChoosecustomerModelHasError;
        private Dictionary<string, object> storedValues;

        public string BranchId
        {
            get { return this._branchID; }

            set
            {
                this._branchID = value;
                this.RaisePropertyChanged(nameof(this.BranchId));
            }
        }

        public string CustomerId
        {
            get { return this._customerID; }

            set
            {
                this._customerID = value;
                this.RaisePropertyChanged(nameof(this.CustomerId));
            }
        }

        public string? CustomerDescription
        {
            get { return this._customerDescription; }

            set
            {
                this._customerDescription = value;
                this.RaisePropertyChanged(nameof(this.CustomerDescription));
            }
        }

        public string? BranchDescription
        {
            get { return this._branchDescription; }

            set
            {
                this._branchDescription = value;
                this.RaisePropertyChanged(nameof(this.BranchDescription));
            }
        }

        public string? Address1
        {
            get { return this._address1; }

            set
            {
                this._address1 = value;
                this.RaisePropertyChanged(nameof(this.Address1));
            }
        }

        public string? Address2
        {
            get { return this._address2; }

            set
            {
                this._address2 = value;
                this.RaisePropertyChanged(nameof(this.Address2));
            }
        }

        public string? Address3
        {
            get { return this._address3; }

            set
            {
                this._address3 = value;
                this.RaisePropertyChanged(nameof(this.Address3));
            }
        }

        public string? ContactPerson
        {
            get { return this._contactPerson; }

            set
            {
                this._contactPerson = value;
                this.RaisePropertyChanged(nameof(this.ContactPerson));
            }
        }

        public string? PostalCode
        {
            get { return this._postalCode; }

            set
            {
                this._postalCode = value;
                this.RaisePropertyChanged(nameof(this.PostalCode));
            }
        }

        public string? Phone1
        {
            get { return this._phone1; }

            set
            {
                this._phone1 = value;
                this.RaisePropertyChanged(nameof(this.Phone1));
            }
        }

        public string? Phone2
        {
            get { return this._phone2; }

            set
            {
                this._phone2 = value;
                this.RaisePropertyChanged(nameof(this.Phone2));
            }
        }

        public string? Fax
        {
            get { return this._fax; }

            set
            {
                this._fax = value;
                this.RaisePropertyChanged(nameof(this.Fax));
            }
        }

        public string? Email
        {
            get { return this._email; }

            set
            {
                this._email = value;
                this.RaisePropertyChanged(nameof(this.Email));
            }
        }

        public string? Website
        {
            get { return this._website; }

            set
            {
                this._website = value;
                this.RaisePropertyChanged(nameof(this.Website));
            }
        }

        public string? GSTRegNo
        {
            get { return this._gSTRegNo; }

            set
            {
                this._gSTRegNo = value;
                this.RaisePropertyChanged(nameof(this.GSTRegNo));
            }
        }

        public string? CompanyRegNo
        {
            get { return this._companyRegNo; }

            set
            {
                this._companyRegNo = value;
                this.RaisePropertyChanged(nameof(this.CompanyRegNo));
            }
        }

        public string? VehicleNo
        {
            get { return this._vehicleNo; }

            set
            {
                this._vehicleNo = value;
                this.RaisePropertyChanged(nameof(this.VehicleNo));
            }
        }

        public string? LabelStyle
        {
            get { return this._labelStyle; }

            set
            {
                this._labelStyle = value;
                this.RaisePropertyChanged(nameof(this.LabelStyle));
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

        public Customerbranch_Model()
        {
        }
    }
}
