using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class Presenter : INotifyPropertyChanged
{
    private string _message = string.Empty;

    public string Message { get =>  _message; set { if (_message != value) { _message = value; OnPropertyChanged(); } } }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
