using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class TimerPanelViewModel : INotifyPropertyChanged
{
    private TimerPanel _model;

    public TimerPanelViewModel(TimerPanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(TimerPanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
