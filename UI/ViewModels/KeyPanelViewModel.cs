using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class KeyPanelViewModel : INotifyPropertyChanged
{
    private readonly KeyPanel _model;

    public KeyPanelViewModel(KeyPanel model)
    {
        _model = model;
        Units = new ObservableCollection<KeyUnitViewModel>(
            model.KeyUnits.ConvertAll(ku =>
                new KeyUnitViewModel(ku)
            )
        );
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(KeyPanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;

    public ObservableCollection<KeyUnitViewModel> Units { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}


public class KeyUnitViewModel : INotifyPropertyChanged
{
    private readonly KeyUnit _model;

    public KeyUnitViewModel(KeyUnit model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(KeyUnit.Scale))
                OnPropertyChanged(nameof(Scale));
            else if (e.PropertyName == nameof(KeyUnit.Color))
                OnPropertyChanged(nameof(Color));
        };
    }

    public float Scale => _model.Scale;
    public string Color => _model.Color;
    public char Number => _model.Number;


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
