using Domain.Entities;
using UI.ViewModels;

namespace UnitTest;

public class LetterUnitViewModelTests
{
    [Fact]
    public void When_ModelColorChanges_Then_ViewModelNotifiesAndUpdates()
    {
        // Arrange
        var model = new LetterUnit { Letter = 'A', Color = "White", Enabled = true };
        var vm = new LetterUnitViewModel(model);

        string? changedProp = null;
        vm.PropertyChanged += (_, e) => changedProp = e.PropertyName;

        // Act
        model.Color = "Red";

        // Assert
        Assert.Equal("Color", changedProp);
        Assert.Equal("Red", vm.Color);
    }

    [Fact]
    public void When_ModelEnabledChanges_Then_ViewModelNotifiesAndUpdates()
    {
        // Arrange
        var model = new LetterUnit { Letter = 'B', Enabled = false };
        var vm = new LetterUnitViewModel(model);

        string? changedProp = null;
        vm.PropertyChanged += (_, e) => changedProp = e.PropertyName;

        // Act
        model.Enabled = true;

        // Assert
        Assert.Equal("Enabled", changedProp);
        Assert.True(vm.Enabled);
    }

    [Fact]
    public void ViewModel_LetterProperty_IsInitializedCorrectly()
    {
        // Arrange
        var model = new LetterUnit { Letter = 'Z' };
        var vm = new LetterUnitViewModel(model);

        // Assert
        Assert.Equal("Z", vm.Letter);
    }
}
