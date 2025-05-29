using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class PrizeChoicePanelViewModel : INotifyPropertyChanged
{
    private readonly PrizeChoicePanel _model;
    public PrizeChoicePanelViewModel(PrizeChoicePanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(PrizeChoicePanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
        };
    }

    public bool IsVisible => _model.IsVisible;
    public string Description => _model.Description;
    public string KeyText => _model.KeyText;
    public string RefuseText => _model.RefuseText;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
