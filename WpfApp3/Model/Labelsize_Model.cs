namespace WpfApp3.Model;

public partial class Labelsize_Model : ModelBase
{
    private string _code = null!;
    private string? _description;
    private int _id;
    public int? _length;
    public int? _width;

    public string Code
    {
        get { return _code; }
        set
        {
            _code = value;
            this.RaisePropertyChanged(nameof(this.Code));
        }
    }

    public string? Description
    {
        get { return _description; }
        set
        {
            _description = value;
            this.RaisePropertyChanged(nameof(this.Description));
        }
    }

    public int? Length
    {
        get { return _length; }
        set
        {
            _length = value;
            this.RaisePropertyChanged(nameof(this.Length));
        }
    }

    public int? Width
    {
        get { return _width; }
        set
        {
            _width = value;
            this.RaisePropertyChanged(nameof(this.Width));
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
    public Labelsize_Model()
    {
    }
}
