namespace WpfApp3.Model;

public partial class certification_Model : ModelBase
{
    private string _code = null!;
    private string? _description;
    private int _id;

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

    public int Id
    {
        get { return _id; }
        set
        {
            _id = value;
            this.RaisePropertyChanged(nameof(this.Id));
        }
    }

    public certification_Model()
    {
    }
}
