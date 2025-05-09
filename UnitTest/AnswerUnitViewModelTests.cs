using PoleChudes.Domain.Entities;
using PoleChudes.ViewModels;

namespace UnitTest;

public class AnswerUnitViewModelTests
{
    [Fact]
    public void DisplayLetter_IsEmpty_WhenNotOpened()
    {
        var model = new AnswerUnit { Letter = 'X', IsOpened = false };
        var vm = new AnswerUnitViewModel(model);

        Assert.Equal(string.Empty, vm.DisplayLetter);
    }

    [Fact]
    public void DisplayLetter_ShowsLetter_WhenOpened()
    {
        var model = new AnswerUnit { Letter = 'Y', IsOpened = false };
        var vm = new AnswerUnitViewModel(model);

        bool notified = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(vm.DisplayLetter)) notified = true; };

        model.IsOpened = true;

        Assert.True(notified);
        Assert.Equal("Y", vm.DisplayLetter);
    }
}
