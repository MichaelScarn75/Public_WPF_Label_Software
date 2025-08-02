// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;

    public class Units_Of_Measure_ViewModel : ViewModelBase
    {
        private ObservableCollection<Unitsofmeasure_Model> _unitofMeasurecode_IEnum;
        private Visibility _errortextborder1_Visibility;
        private string _errortextbox1_Text;

        public ObservableCollection<Unitsofmeasure_Model> UnitofMeasurecode_IEnum
        {
            get { return this._unitofMeasurecode_IEnum; }

            set
            {
                this._unitofMeasurecode_IEnum = value;
                this.RaisePropertyChanged(nameof(UnitofMeasurecode_IEnum));
                OCPropertyChanged(this.UnitofMeasurecode_IEnum);
            }
        }

        public Visibility ErrorTextBorder1_Visibility
        {
            get { return this._errortextborder1_Visibility; }

            set
            {
                this._errortextborder1_Visibility = value;
                this.RaisePropertyChanged(nameof(ErrorTextBorder1_Visibility));
            }
        }

        public string ErrorTextBox1_Text
        {
            get { return this._errortextbox1_Text; }

            set
            {
                this._errortextbox1_Text = value;
                this.RaisePropertyChanged(nameof(ErrorTextBox1_Text));
            }
        }

        public Units_Of_Measure_ViewModel()
        {
            this._unitofMeasurecode_IEnum = new ObservableCollection<Unitsofmeasure_Model>();
            this.Populate__unitsofmeasure();
        }

        public void Populate__unitsofmeasure()
        {
            using (var context = new ProductListingContext())
            {
                this._unitofMeasurecode_IEnum = context.UnitsOfMeasures.ToObservableCollection<Unitsofmeasure_Model>();
                this.RaisePropertyChanged(nameof(UnitofMeasurecode_IEnum));
                OCPropertyChanged(this.UnitofMeasurecode_IEnum);
            }
        }
    }
}
