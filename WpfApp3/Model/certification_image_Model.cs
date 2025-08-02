using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace WpfApp3.Model;

public partial class certification_image_Model : ModelBase, IEditableObject
{
    private string _code = null!;
    private byte[] _image;
    private BitmapImage _imageActual;
    private int _id;
    private Dictionary<string, object> storedValues;

    public string Code
    {
        get { return _code; }
        set
        {
            _code = value;
            this.RaisePropertyChanged(nameof(this.Code));
        }
    }

    public byte[] Image
    {
        get { return _image; }
        set
        {
            _image = value;
            this.RaisePropertyChanged(nameof(this.Image));
        }
    }

    [NotMapped]
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

    public certification_image_Model()
    {
    }
}
