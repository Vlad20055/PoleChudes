namespace UI.Animation;

public static class AnimationExtensions
{
    public static Task AnimateAsync(this VisualElement element, string name, Action<double> callback, uint rate = 16, uint length = 250, Easing? easing = null)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        element.Animate(name, callback, rate, length, easing ?? Easing.Linear, (v, c) => tcs.SetResult(c));

        return tcs.Task;
    }
}