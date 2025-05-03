using PoleChudes.Domain.Entities;

namespace PoleChudes.UseCases;

public class AnswerPanelManager
{
    public AnswerPanel AnswerPanel { get; set; }

    public AnswerPanelManager(GameTask gameTask)
    {
        AnswerPanel = ConstructAnswerPanel(gameTask);
    }

    public AnswerPanel ConstructAnswerPanel(GameTask gameTask)
    {
        AnswerPanel answerPanel = new AnswerPanel();
        foreach (var letter in gameTask.Answer)
        {
            answerPanel.AnswerUnits.Add(new AnswerUnit() { Letter = letter, IsOpened = false });
        }
        return answerPanel;
    }

}
