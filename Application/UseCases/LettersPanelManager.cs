using Domain.Entities;

namespace Application.UseCases;

public class LettersPanelManager
{
    public LettersPanel LettersPanel { get; set; }

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
            }
        }
    }
}
