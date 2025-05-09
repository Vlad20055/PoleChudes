using Microsoft.Maui.Graphics.Platform;
using PoleChudes.Domain.Entities;
using System.Reflection;

namespace PoleChudes.UseCases;

public class PresenterManager
{
    public Presenter Presenter { get; set; } = new Presenter();
    private ImageSource? _image = null;

    public PresenterManager()
    {
        LoadPresenterImage();
        Presenter = ConstructPresenter();
    }

    private void LoadPresenterImage()
    {
        ImageSource? image = ImageSource.FromFile("presenter.jpg");
        _image = image;
    }

    private Presenter ConstructPresenter()
    {
        Presenter presenter = new Presenter();
        presenter.Image = _image;
        return presenter;
    }

    public void SetMessage(string message)
    {
        Presenter.Message = message;
    }

}
