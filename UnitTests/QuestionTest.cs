using PoleChudes.ViewModels;
using PoleChudes.Domain.Entities;

namespace UnitTests;

public class QuestionTest
{
    [Fact]
    public void When_ModelQuestionChanged_Then_ViewModelNotifiesAndUpdates()
    {
        // Arrange: модель + VM
        var model = new GameTask { Question = "Start" };
        var vm = new QuestionViewModel(model);

        string? changedProp = null;
        vm.PropertyChanged += (_, e) => changedProp = e.PropertyName;

        // Act: меняем вопрос в модели
        model.Question = "New question";

        // Assert: VM сгенерировала событие и обновила своё свойство
        Assert.Equal("Question", changedProp);
        Assert.Equal("New question", vm.Question);
    }
}