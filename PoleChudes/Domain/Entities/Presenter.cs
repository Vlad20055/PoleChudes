using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PoleChudes.Domain.Entities;

public class Presenter : INotifyPropertyChanged
{
    private string _message = string.Empty;
    private ImageSource? _image = null;

    public string Message { get =>  _message; set { if (_message != value) { _message = value; OnPropertyChanged(); } } }
    public ImageSource? Image { get => _image; set { if (_image != value) { _image = value; OnPropertyChanged(); } } }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
