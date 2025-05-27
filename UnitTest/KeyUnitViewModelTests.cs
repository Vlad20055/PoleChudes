using Domain.Entities;
using Application.UseCases;
using UI.ViewModels;

namespace UnitTest;

public class KeyUnitViewModelTests
{
    [Fact]
    public void SelectCommand_ChangesScaleAndColor()
    {
        KeyPanelManager keyPanelManager = new KeyPanelManager();

        var model = keyPanelManager.KeyPanel.KeyUnits[0];
        var vm = new KeyUnitViewModel(model);

        // Изначально
        Assert.Equal(1f, vm.Scale);
        Assert.Equal("LightGrey", vm.Color);

        // Вызываем команду
        keyPanelManager.SelectKey('1');

        Assert.Equal(1.5f, vm.Scale);
        Assert.Equal("Gold", vm.Color);
    }
}
