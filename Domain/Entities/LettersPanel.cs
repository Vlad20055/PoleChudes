using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class LettersPanel
{
    public List<LetterUnit> LetterUnits { get; set; } = new List<LetterUnit>();
}

public class LetterUnit : INotifyPropertyChanged
{
    private char _letter = '*';
    private string _color = "White";
    private bool _enabled = false;

    public char Letter { get => _letter; set { _letter = value; } }
    public string Color { get => _color; set { if (_color != value) { _color = value; OnPropertyChanged(); } } }
    public bool Enabled { get => _enabled; set { if (_enabled != value) { _enabled = value; OnPropertyChanged(); } } }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
