using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PoleChudes.Domain.Entities;

public class GameTask : INotifyPropertyChanged
{
    private string _question = string.Empty;
    public string Question { get => _question; set { _question = value; OnPropertyChanged(); } }
    public string Answer { get; set; } = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
