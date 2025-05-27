using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class PlusPanel : INotifyPropertyChanged
{
    private bool _isVisible = false;
    private List<int> _availablePositions = new List<int>();

    public bool IsVisible { get => _isVisible; set { if (_isVisible != value) { _isVisible = value; OnPropertyChanged(); } } }
    public List<int> AvailablePositions { get => _availablePositions; set { if (_availablePositions != value) { _availablePositions = value; OnPropertyChanged(); } } }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
