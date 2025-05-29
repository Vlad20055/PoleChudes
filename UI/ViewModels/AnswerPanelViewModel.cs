using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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


public class AnswerUnitViewModel : INotifyPropertyChanged
{
    private readonly AnswerUnit _model;

    public AnswerUnitViewModel(AnswerUnit model)
    {
        _model = model;
        _model.PropertyChanged += Model_PropertyChanged;
    }

    public string DisplayLetter
        => _model.IsOpened ? _model.Letter.ToString() : string.Empty;
    private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AnswerUnit.IsOpened))
        {
            OnPropertyChanged(nameof(DisplayLetter));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}