using Domain.Entities;

namespace Application.Managers;

public class LettersPanelManager
{
    public LettersPanel LettersPanel { get; set; }

    public void Enable() => LettersPanel.IsVisible = true;
    public void Disable() => LettersPanel.IsVisible = false;

    public LettersPanelManager()
    {
        LettersPanel = ConstructLettersPanel();
    }

    public LettersPanel ConstructLettersPanel()
    {
        LettersPanel lettersPanel = new LettersPanel();
        const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        foreach (var letter in alphabet)
        {
            lettersPanel.LetterUnits.Add(new LetterUnit() { Letter = letter, Color = "LightGray", Enabled = false });
        }
        return lettersPanel;
    }

    public void BlockPanel()
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            el.Enabled = false;
        }
    }

    public void BlockLetter(char letter)
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            if (el.Letter == letter)
            {
                el.Enabled = false;
                return;
            }
        }
    }

    public void UnblockPanelAccordingToColors()
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            if (el.Color == "LightGray")
            {
                el.Enabled = true;
            }
        }
    }

    public void SetColor(char letter, string color)
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            if (el.Letter == letter)
            {
                el.Color = color;
                return;
            }
        }
    }

    public void SetDefaultState()
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            el.Color = "LightGray";
            el.Enabled = false;
        }
    }

    public void UnblockAllLetters()
    {
        foreach (var el in LettersPanel.LetterUnits)
        {
            el.Enabled = true;
        }
    }
}
