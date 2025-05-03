using System.ComponentModel;
using System.Runtime.CompilerServices;
using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;

public class LetterUnitViewModel : INotifyPropertyChanged
{
    private readonly LetterUnit _model;

    public LetterUnitViewModel(LetterUnit model)
    {
        _model = model;
        _model.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(LetterUnit.Color))
                OnPropertyChanged(nameof(Color));
            else if (e.PropertyName == nameof(LetterUnit.Enabled))
                OnPropertyChanged(nameof(Enabled));
        };
    }

    public string Letter => _model.Letter.ToString();
    public string Color => _model.Color;
    public bool Enabled => _model.Enabled;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}