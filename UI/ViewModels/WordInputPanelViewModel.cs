using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class WordInputPanelViewModel : INotifyPropertyChanged
{
    private WordInputPanel _model;

    public WordInputPanelViewModel(WordInputPanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(WordInputPanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
            if (e.PropertyName == nameof(WordInputPanel.IsRefuseVisible))
                OnPropertyChanged(nameof(IsRefuseVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;
    public bool IsRefuseVisible => _model.IsRefuseVisible;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
