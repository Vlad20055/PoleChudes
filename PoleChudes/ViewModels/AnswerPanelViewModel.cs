using System.Collections.ObjectModel;
using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;

public class AnswerPanelViewModel
{
    public ObservableCollection<AnswerUnitViewModel> Units { get; }

    public AnswerPanelViewModel(AnswerPanel model)
    {
        Units = new ObservableCollection<AnswerUnitViewModel>(
            model.AnswerUnits.Select(u => new AnswerUnitViewModel(u))
        );
    }
}