using System.Windows.Media.Imaging;

namespace WpfApp3.Model;

public partial class item_image_actual_Model : ModelBase
{
    private string _code;
    private byte[] _image;
    private BitmapImage _imageActual;
    private int _id;

    public string Code
    {
        get { return this._code; }
        set
        {
            this._code = value;
            this.RaisePropertyChanged(nameof(this._code));
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

    public item_image_actual_Model()
    {
    }
}
