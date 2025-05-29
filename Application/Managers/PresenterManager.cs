using Domain.Entities;

namespace Application.Managers;

public class PresenterManager
{
    private Presenter _presenter { get; set; } = new Presenter();

    public PresenterManager(Presenter presenter)
    {
        _presenter = presenter;
    }

    public void SetMessage(string message)
    {
        _presenter.Message = message;
    }

}
