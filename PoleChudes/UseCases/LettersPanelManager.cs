using PoleChudes.Domain.Entities;

namespace PoleChudes.UseCases;

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
}
