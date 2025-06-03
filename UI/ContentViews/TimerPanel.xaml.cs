namespace UI.ContentViews;

public partial class TimerPanel : ContentView
{
    private CancellationTokenSource? _cts;
    private TaskCompletionSource? _tcs;

    public TimerPanel()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Запускает таймер на 60 секунд.
    /// Возвращает Task, который завершится, когда таймер дойдёт до нуля.
    /// При этом сам Panel станет видимым (если нужно скрыть дополнительно WordInputPanel —
    /// обрабатывайте событие TimeUp в родителе).
    /// </summary>
    public Task StartTimerAsync()
    {
        // Если уже где-то идёт таймер, отменим его
        _cts?.Cancel();

        _cts = new CancellationTokenSource();
        _tcs = new TaskCompletionSource();

        // Показываем сам TimerPanel
        IsVisible = true;

        // Начальный интервал — 60 секунд
        int totalSeconds = 60;

        // Обновляем лейбл сразу
        CountdownLabel.Text = FormatTime(totalSeconds);

        // Запускаем метод-обработчик отсчёта
        _ = RunCountdownAsync(totalSeconds, _cts.Token);

        return _tcs.Task;
    }

    private async Task RunCountdownAsync(int startSeconds, CancellationToken token)
    {
        int remaining = startSeconds;

        try
        {
            while (remaining > 0)
            {
                // Ждём одну секунду или отмену
                await Task.Delay(1000, token);

                remaining--;

                // Обновляем UI в главном потоке
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CountdownLabel.Text = FormatTime(remaining);
                });
            }

            // Когда дошли до нуля:
            OnTimeUp();
        }
        catch (OperationCanceledException)
        {
            // Если таймер отменили извне — просто уйдём
        }
    }

    private void OnTimeUp()
    {
        // Завершаем TaskCompletionSource
        _tcs?.TrySetResult();
    }

    /// <summary>
    /// Останавливает таймер досрочно (при необходимости).
    /// </summary>
    public void CancelTimer()
    {
        _cts?.Cancel();
        _cts = null;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            IsVisible = false;
        });

        _tcs?.TrySetCanceled();
    }

    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }
}