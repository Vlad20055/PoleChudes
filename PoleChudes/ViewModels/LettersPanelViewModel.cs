using System.Collections.ObjectModel;
using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;

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
