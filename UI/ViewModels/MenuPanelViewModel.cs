using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class MenuPanelViewModel : INotifyPropertyChanged
{
    private MenuPanel _model;

    public MenuPanelViewModel(MenuPanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(MenuPanel.IsRotateButtonVisible))
                OnPropertyChanged(nameof(IsRotateButtonVisible));
            if (e.PropertyName == nameof(MenuPanel.IsWordButtonVisible))
                OnPropertyChanged(nameof(IsWordButtonVisible));
            if (e.PropertyName == nameof(MenuPanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;
    public bool IsRotateButtonVisible => _model.IsRotateButtonVisible;
    public bool IsWordButtonVisible => _model.IsWordButtonVisible;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
