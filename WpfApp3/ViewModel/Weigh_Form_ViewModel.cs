// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Syncfusion.Data.Extensions;
    using WpfApp3.Model;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    public class Weigh_Form_ViewModel : ViewModelBase
    {
        private PrintingHistory_Model _printinghistory_Model;
        private ObservableCollection<PrintingHistory_Model> _printinghistory_IEnum;

        public PrintingHistory_Model Printinghistory_Model
        {
            get { return this._printinghistory_Model; }

            set
            {
                this._printinghistory_Model = value;
                this.RaisePropertyChanged(nameof(Printinghistory_Model));
                OCPropertyChanged(this.Printinghistory_Model);
            }
        }

        public ObservableCollection<PrintingHistory_Model> PrintingHistory_IEnum
        {
            get { return this._printinghistory_IEnum; }

            set
            {
                this._printinghistory_IEnum = value;
                this.RaisePropertyChanged(nameof(PrintingHistory_IEnum));
                OCPropertyChanged(this.PrintingHistory_IEnum);
            }
        }

        public Weigh_Form_ViewModel()
        {
            this.populate__printinghistory();
        }

        public void populate__printinghistory()
        {
            using (ProductListingContext context = new())
            {
                PrintingHistory_IEnum = context.PrintingHistories
                    .Where(a => a.SP_WeightItem.Equals(true) && a.SP_WeightScale.Equals(true))
                    .Where(a => a.SD_SalesCode.Equals(MW_Lib.Printinghistory_Model_Proxy.SD_SalesCode))
                    .Where(a => a.SD_ItemNo.Equals(MW_Lib.Printinghistory_Model_Proxy.SD_ItemNo))
                    .Where(a => a.SD_UnitOfMeasureCode.Equals(MW_Lib.Printinghistory_Model_Proxy.SD_UnitOfMeasureCode))
                    .ToObservableCollection<PrintingHistory_Model>();
            }
        }
    }
}
