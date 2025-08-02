using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace WpfApp3.Model;

public partial class certification_actual_Model : ModelBase, IEditableObject
{
    private string _code = null!;
    private string? _description;
    private int _id;
    private byte[] _image;
    private BitmapImage _imageActual;
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

    public byte[] Image
    {
        get { return this._image; }
        set
        {
            this._image = value;
            this.RaisePropertyChanged(nameof(this._image));
        }
    }

    public BitmapImage ImageActual
    {
        get { return this._imageActual; }
        set
        {
            this._imageActual = value;
            this.RaisePropertyChanged(nameof(this.ImageActual));
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

    public certification_actual_Model()
    {
    }
}
