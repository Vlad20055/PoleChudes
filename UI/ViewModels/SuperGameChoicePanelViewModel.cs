using Domain.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModels;

public class SuperGameChoicePanelViewModel : INotifyPropertyChanged
{
    private SuperGameChoicePanel _model;

    public SuperGameChoicePanelViewModel(SuperGameChoicePanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(SuperGameChoicePanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;
    public string Description => _model.Description;
    public string SuperGameText => _model.SuperGameText;
    public string RefuseText => _model.RefuseText;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
