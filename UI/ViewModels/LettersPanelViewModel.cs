using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class LettersPanelViewModel
{
    public ObservableCollection<LetterUnitViewModel> Units { get; }

    public LettersPanelViewModel(LettersPanel model)
    {
        Units = new ObservableCollection<LetterUnitViewModel>(
            model.LetterUnits.Select(mu => new LetterUnitViewModel(mu))
        );
    }
}

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
