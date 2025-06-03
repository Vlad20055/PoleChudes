using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class MenuPanel : INotifyPropertyChanged
{
    private bool _isVisible = true;
    private bool _isRotateButtonVisible = false;
    private bool _isWordButtonVisible = false;

    public bool IsVisible { get => _isVisible; set { _isVisible = value; OnPropertyChanged(); } }
    public bool IsRotateButtonVisible { get => _isRotateButtonVisible; set { _isRotateButtonVisible = value; OnPropertyChanged(); } }
    public bool IsWordButtonVisible { get => _isWordButtonVisible; set { _isWordButtonVisible = value; OnPropertyChanged(); } }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
