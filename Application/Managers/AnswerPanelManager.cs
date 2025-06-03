using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Managers;

public class AnswerPanelManager
{
    public AnswerPanel AnswerPanel { get; set; }

    public AnswerPanelManager(GameTask gameTask)
    {
        AnswerPanel = ConstructAnswerPanel(gameTask);
    }

    public void SetTask(GameTask gameTask)
    {
        List<AnswerUnit> answerUnits = new List<AnswerUnit>();
        foreach (var letter in gameTask.Answer)
        {
            answerUnits.Add(new AnswerUnit() { Letter = letter, IsOpened = false });
        }
        AnswerPanel.AnswerUnits = answerUnits;
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

    public void OpenAllAnswer()
    {
        foreach (var el in AnswerPanel.AnswerUnits)
        {
            el.IsOpened = true;
        }
    }

    public char GetLetter(int position)
    {
        char letter = AnswerPanel.AnswerUnits[position - 1].Letter;
        return letter;
    }

    public void OpenLetter(int position)
    {
        char letter = AnswerPanel.AnswerUnits[position-1].Letter;
        OpenLetter(letter);
    }

    public List<int> GetClosedLetters()
    {
        List<int> closedLetters = new List<int>();
        
        for (int i = 0; i <  AnswerPanel.AnswerUnits.Count; i++)
        {
            if (!AnswerPanel.AnswerUnits[i].IsOpened) closedLetters.Add(i+1);
        }

        return closedLetters;
    }

}
