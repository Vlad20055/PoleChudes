using Domain.Entities;

namespace Application.Managers;

public class TimerPanelManager
{
    private TimerPanel _timerPanel;
    public event Func<Task>? Start;
    public event Action? Cancel;

    public TimerPanelManager(TimerPanel timerPanel)
    {
        _timerPanel = timerPanel;
    }

    public void Enable() => _timerPanel.IsVisible = true;
    public void Disable() => _timerPanel.IsVisible = false;
    public Task? StartTimer() => Start?.Invoke();
    public void CancelTimer() => Cancel?.Invoke();
}
