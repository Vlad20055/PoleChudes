using System.ComponentModel;
using System.Runtime.CompilerServices;
using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;

public class PrizePanelViewModel : INotifyPropertyChanged
{
    private readonly PrizePanel _model;

    public PrizePanelViewModel(PrizePanel model)
    {
        _model = model;
        _model.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(PrizePanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
            else if (e.PropertyName == nameof(PrizePanel.AreButtonsVisible))
                OnPropertyChanged(nameof(AreButtonsVisible));
            else if (e.PropertyName == nameof(PrizePanel.SelectedPrize))
            {
                OnPropertyChanged(nameof(SelectedPrize));
                OnPropertyChanged(nameof(PrizeImage));
                OnPropertyChanged(nameof(PrizeName));
            }
        };
    }

    public bool IsVisible => _model.IsVisible;
    public bool AreButtonsVisible => _model.AreButtonsVisible;
    public Prize? SelectedPrize => _model.SelectedPrize;
    public string PrizeImage => SelectedPrize?.PrizeImage ?? string.Empty;
    public string PrizeName => SelectedPrize?.Name ?? string.Empty;


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
