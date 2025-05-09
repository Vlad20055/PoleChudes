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

    public int OpenLetter(char letter) // returns the number of opened letters
    {
        int numberOfOpenedLetters = 0;
        foreach (var el in AnswerPanel.AnswerUnits)
        {
            if (el.Letter == letter)
            {
                el.IsOpened = true;
                numberOfOpenedLetters++;
            }
        }
        return numberOfOpenedLetters;
    }

}
