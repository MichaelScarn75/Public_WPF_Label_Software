using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WpfApp3.Model;

public partial class item_Model : ModelBase, IEditableObject
{
    private string _itemNo = null!;
    private string? _description;
    private string? _inventoryPostingGroup;
    private string? _country;
    private int _id;
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

    public string Description
    {
        get { return this._description; }

        set
        {
            this._description = value;
            this.RaisePropertyChanged(nameof(this.Description));
        }
    }

    public string InventoryPostingGroup
    {
        get { return this._inventoryPostingGroup; }

        set
        {
            this._inventoryPostingGroup = value;
            this.RaisePropertyChanged(nameof(this.InventoryPostingGroup));
        }
    }
    public string Country
    {
        get { return this._country; }

        set
        {
            this._country = value;
            this.RaisePropertyChanged(nameof(this.Country));
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

    public item_Model()
    {
    }
}
