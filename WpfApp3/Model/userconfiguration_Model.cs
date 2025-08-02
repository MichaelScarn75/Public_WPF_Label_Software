using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;

namespace WpfApp3.Model;

public partial class userconfiguration_Model : ModelBase, IEditableObject
{
    private int _Com_Port_Number;
    private int _DataBit;
    private Parity _Parity;
    private int _Baudrate;
    private StopBits _StopBits;
    private string _BigLabelPrinter = string.Empty;
    private string _MediumLabelPrinter = string.Empty;
    private string _SmallLabelPrinter = string.Empty;
    private string _BaseCurrency = string.Empty;
    private Dictionary<string, object> storedValues;

    public int Com_Port_Number
    {
        get { return this._Com_Port_Number; }
        set
        {
            this._Com_Port_Number = value;
            this.RaisePropertyChanged(nameof(this.Com_Port_Number));
        }
    }

    public int DataBit
    {
        get { return this._DataBit; }
        set
        {
            this._DataBit = value;
            this.RaisePropertyChanged(nameof(this.DataBit));
        }
    }

    public Parity Parity
    {
        get { return this._Parity; }
        set
        {
            this._Parity = value;
            this.RaisePropertyChanged(nameof(this.Parity));
        }
    }

    public int Baudrate
    {
        get { return this._Baudrate; }
        set
        {
            this._Baudrate = value;
            this.RaisePropertyChanged(nameof(this.Baudrate));
        }
    }

    public StopBits StopBits
    {
        get { return this._StopBits; }
        set
        {
            this._StopBits = value;
            this.RaisePropertyChanged(nameof(this.StopBits));
        }
    }

    public string BigLabelPrinter
    {
        get { return this._BigLabelPrinter; }
        set
        {
            this._BigLabelPrinter = value;
            this.RaisePropertyChanged(nameof(this.BigLabelPrinter));
        }
    }

    public string MediumLabelPrinter
    {
        get { return this._MediumLabelPrinter; }
        set
        {
            this._MediumLabelPrinter = value;
            this.RaisePropertyChanged(nameof(this.MediumLabelPrinter));
        }
    }

    public string SmallLabelPrinter
    {
        get { return this._SmallLabelPrinter; }
        set
        {
            this._SmallLabelPrinter = value;
            this.RaisePropertyChanged(nameof(this.SmallLabelPrinter));
        }
    }

    public string BaseCurrency
    {
        get { return this._BaseCurrency; }
        set
        {
            this._BaseCurrency = value;
            this.RaisePropertyChanged(nameof(this.BaseCurrency));
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

    public userconfiguration_Model()
    {
    }
}
