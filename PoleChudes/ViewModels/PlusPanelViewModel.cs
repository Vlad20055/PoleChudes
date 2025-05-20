using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;

public class PlusPanelViewModel : INotifyPropertyChanged
{
    private readonly PlusPanel _model;

    public PlusPanelViewModel(PlusPanel model)
    {
        _model = model;
        _model.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(PlusPanel.IsVisible))
                OnPropertyChanged(nameof(IsVisible));
            else if (e.PropertyName == nameof(PlusPanel.AvailablePositions))
                RefreshAvailablePositions();
        };
    }

    public bool IsVisible => _model.IsVisible;
    public ObservableCollection<int> AvailablePositions { get; } = new();
    private void RefreshAvailablePositions()
    {
        AvailablePositions.Clear();
        foreach (var pos in _model.AvailablePositions)
            AvailablePositions.Add(pos);
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
