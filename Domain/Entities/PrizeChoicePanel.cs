using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class PrizeChoicePanel : INotifyPropertyChanged
{
    private bool _isVisible = false;
    public string Description { get; set; } = "Выберите опцию.\nЕсли вы выберите 'Приз', вы покинете игру";
    public string KeyText { get; set; } = "Да, Я хочу ПРИЗ!";
    public string RefuseText { get; set; } = "Отказаться!";

    public bool IsVisible { get => _isVisible; set { if (_isVisible != value) { _isVisible = value; OnPropertyChanged(); } } }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
