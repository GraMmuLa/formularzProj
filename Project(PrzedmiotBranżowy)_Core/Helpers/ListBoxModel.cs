namespace Project_PrzedmiotBranzowy__Core.Helpers;

public class ListBoxModel<T> : BindableBase, IListBoxModel
{
    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetProperty(ref _isSelected, value); }
    }

    private T _value;
    public T Value
    {
        get { return _value; }
        set { SetProperty(ref _value, value); }
    }

    public ListBoxModel()
    {
        IsSelected = false;
    }

    public ListBoxModel(T value)
    {
        _value = value;
        IsSelected = false;
    }
}
