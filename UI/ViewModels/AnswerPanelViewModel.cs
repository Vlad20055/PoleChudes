using System.Collections.ObjectModel;
using Domain.Entities;

namespace UI.ViewModels;

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