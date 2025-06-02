using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class PrizesSuperGamePanel : INotifyPropertyChanged
{
    private bool _isVisible = false;
    private int _score = 1000;

    public List<PrizeSuperGameUnit> Units;
    public int Score { get => _score; set { _score = value; OnPropertyChanged(); } }
    public bool IsVisible {  get => _isVisible; set { _isVisible = value; OnPropertyChanged(); } }

    public PrizesSuperGamePanel(PrizeList prizeList)
    {
        Units = new List<PrizeSuperGameUnit>(prizeList.Prizes.Select(p => new PrizeSuperGameUnit(p)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


public class PrizeSuperGameUnit : INotifyPropertyChanged
{
    private bool _isSelected = false;

    public Prize Prize;
    public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }

    public PrizeSuperGameUnit(Prize prize)
    {
        Prize = prize;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

