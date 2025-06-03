using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class WordInputPanel : INotifyPropertyChanged
{
    private bool _isVisible = false;
    private bool _isRefuseVisible = true;

    public bool IsVisible { get => _isVisible; set { _isVisible = value; OnPropertyChanged(); } }
    public bool IsRefuseVisible { get => _isRefuseVisible; set { _isRefuseVisible = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
