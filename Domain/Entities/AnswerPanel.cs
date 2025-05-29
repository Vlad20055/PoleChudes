using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class AnswerPanel
{
    public List<AnswerUnit> AnswerUnits { get; set; } = new List<AnswerUnit>();
}

public class AnswerUnit : INotifyPropertyChanged
{
    private char _letter = '*';
    private bool _isOpened = false;

    public char Letter { get => _letter; set { _letter = value; } }
    public bool IsOpened { get => _isOpened; set { if (_isOpened != value) { _isOpened = value; OnPropertyChanged(); } } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

