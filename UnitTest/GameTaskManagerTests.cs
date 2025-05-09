using PoleChudes;

namespace UnitTest;

public class GameTaskManagerTests
{
    [Fact]
    public void LoadAllTasks_ReturnsNonEmptyList()
    {
        // assuming tasks.txt в embedded ресурсах содержит хотя бы одну пару
        var all = GameTaskManager.LoadAllTasks();
        Assert.NotEmpty(all);
        Assert.All(all, t =>
        {
            Assert.False(string.IsNullOrWhiteSpace(t.Question));
            Assert.False(string.IsNullOrWhiteSpace(t.Answer));
        });
    }

    [Fact]
    public void GetRandomTask_ReturnsOneOfLoaded()
    {
        var all = GameTaskManager.LoadAllTasks();
        var rnd = GameTaskManager.GetRandomTask();
        Assert.NotNull(rnd);
        Assert.Contains(all, t => t.Question == rnd!.Question && t.Answer == rnd.Answer);
    }
}
