using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class AnswerPanelViewModel
{
    private AnswerPanel _model;

    public ObservableCollection<AnswerUnitViewModel> Units { get; }

    public AnswerPanelViewModel(AnswerPanel model)
    {
        _model = model;
        Units = new ObservableCollection<AnswerUnitViewModel>(
            model.AnswerUnits.Select(u => new AnswerUnitViewModel(u))
        );
        model.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(AnswerPanel.AnswerUnits))
                ResetModel();
        };
    }

    public void ResetModel()
    {
        // Очистим текущие ViewModel-объекты
        Units.Clear();

        // Пересоздаём из новой модели
        foreach (var unit in _model.AnswerUnits)
        {
            Units.Add(new AnswerUnitViewModel(unit));
        }
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