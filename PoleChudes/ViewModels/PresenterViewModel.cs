using System.ComponentModel;
using System.Runtime.CompilerServices;
using PoleChudes.Domain.Entities;


namespace PoleChudes.ViewModels;

public class PresenterViewModel : INotifyPropertyChanged
{
    private readonly Presenter _model;

    public PresenterViewModel(Presenter model)
    {
        _model = model;
        _model.PropertyChanged += Model_PropertyChanged;
    }

    public string Message
    {
        get => _model.Message;
        set
        {
            if (_model.Message != value)
                _model.Message = value;
        }
    }

    public ImageSource? Image
        => _model.Image;

    private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Presenter.Message))
            OnPropertyChanged(nameof(Message));
        else if (e.PropertyName == nameof(Presenter.Image))
            OnPropertyChanged(nameof(Image));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
