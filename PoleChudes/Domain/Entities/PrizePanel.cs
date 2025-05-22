using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PoleChudes.Domain.Entities;

public class PrizePanel : INotifyPropertyChanged
{
    private bool _isVisible = false;
    private bool _areButtonsEnabled = false;
    private Prize? _selectedPrize;

    public bool IsVisible { get =>  _isVisible; set { if (_isVisible != value) { _isVisible = value; OnPropertyChanged(); } } }
    public bool AreButtonsVisible { get => _areButtonsEnabled; set { if (_areButtonsEnabled != value) { _areButtonsEnabled = value; OnPropertyChanged(); } } }
    public Prize? SelectedPrize { get => _selectedPrize; set { if (_selectedPrize != value) { _selectedPrize = value; OnPropertyChanged(); } } }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
