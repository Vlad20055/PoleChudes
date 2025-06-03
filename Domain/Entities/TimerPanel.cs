using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class TimerPanel : INotifyPropertyChanged
{
    private bool _isVisible = false;

    public bool IsVisible {  get => _isVisible; set { _isVisible = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
