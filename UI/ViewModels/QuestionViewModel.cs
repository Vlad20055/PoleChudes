using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;

public class QuestionViewModel : INotifyPropertyChanged
{
    private readonly GameTask _model;
    private string _question = string.Empty;

    public string Question
    {
        get => _question;
        set
        {
            if (_question != value)
            {
                _question = value;
                OnPropertyChanged();
            }
        }
    }

    public QuestionViewModel(GameTask model)
    {
        _model = model;
        _model.PropertyChanged += Model_PropertyChanged;
        Question = _model.Question;
    }

    private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GameTask.Question))
        {
            Question = _model.Question;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}
