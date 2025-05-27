using System.Collections.ObjectModel;
using Domain.Entities;

namespace UI.ViewModels;

public class LettersPanelViewModel
{
    public ObservableCollection<LetterUnitViewModel> Units { get; }

    public LettersPanelViewModel(LettersPanel model)
    {
        Units = new ObservableCollection<LetterUnitViewModel>(
            model.LetterUnits.Select(mu => new LetterUnitViewModel(mu))
        );
    }
}
