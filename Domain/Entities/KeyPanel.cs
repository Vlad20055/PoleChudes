using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class KeyPanel : INotifyPropertyChanged
{
    private bool _isVisible = false;

    public bool IsVisible { get => _isVisible; set { if (_isVisible != value) { _isVisible = value; OnPropertyChanged(); } } }
    public List<KeyUnit> KeyUnits { get; set; }

    public KeyPanel(List<KeyUnit> units)
    {
        KeyUnits = units;
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class KeyUnit : INotifyPropertyChanged
{
    private string _color = "LightGrey";
    private float _scale = 1f;

    public string Color { get =>  _color; set { if (_color != value) { _color = value;  OnPropertyChanged(); } } }
    public float Scale { get => _scale; set { if (_scale != value) { _scale = value; OnPropertyChanged(); } } }
    public char Number { get; set; } = '*';
    
    public KeyUnit(char keyNumber)
    {
        Number = keyNumber;
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}