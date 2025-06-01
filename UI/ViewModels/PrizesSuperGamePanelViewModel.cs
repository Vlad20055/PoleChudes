using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class PrizesSuperGamePanelViewModel : INotifyPropertyChanged
{
    private PrizesSuperGamePanel _model;

    public PrizesSuperGamePanelViewModel(PrizesSuperGamePanel model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(PrizesSuperGamePanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
            if (e.PropertyName == nameof(PrizesSuperGamePanel.Score))
                OnPropertyChanged(nameof(Score));
        };
        Prizes = new(model.Units.Select(u => new PrizeSuperGameUnitViewModel(u)));
    }

    public List<PrizeSuperGameUnitViewModel> Prizes { get; }
    public bool IsVisible => _model.IsVisible;
    public int Score => _model.Score;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class PrizeSuperGameUnitViewModel : INotifyPropertyChanged
{
    private PrizeSuperGameUnit _model;

    public string Name => _model.Prize.Name;
    public int Cost => _model.Prize.Cost;
    public bool IsSelected => _model.IsSelected;

    public PrizeSuperGameUnitViewModel(PrizeSuperGameUnit model)
    {
        _model = model;
        _model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(PrizeSuperGameUnit.IsSelected))
                OnPropertyChanged(nameof(IsSelected));
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


