using Domain.Entities;

namespace Application.Managers;

public class RightWrongLettersManager
{
    private RightWrongLetters _rightWrongLetters;

    public RightWrongLettersManager(RightWrongLetters rightWrongLetters)
    {
        _rightWrongLetters = rightWrongLetters;

        ConfigureRightAndWrongStrings();
    }

    public string GetRightLetters() => _rightWrongLetters.RightLetters;
    public string GetWrongLetters() => _rightWrongLetters.WrongLetters;
    public string GetAnswer() => _rightWrongLetters.Answer;

    public void ConfigureRightAndWrongStrings()
    {
        Dictionary<char, bool> letters = new Dictionary<char, bool>();
        string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        foreach (char letter in alphabet)
        {
            letters[letter] = false;
        }
        foreach (char letter in _rightWrongLetters.Answer)
        {
            letters[letter] = true;
        }
        foreach (var letter in letters)
        {
            if (letter.Value) _rightWrongLetters.RightLetters += letter.Key;
            else _rightWrongLetters.WrongLetters += letter.Key;
        }
    }
    public void RemoveRightLetter(char letter)
    {
        string temp = string.Empty;
        temp += letter;
        _rightWrongLetters.RightLetters = _rightWrongLetters.RightLetters.Replace(temp, string.Empty);
    }
    public void RemoveWrongLetter(char letter)
    {
        string temp = string.Empty;
        temp += letter;
        _rightWrongLetters.WrongLetters = _rightWrongLetters.WrongLetters.Replace(temp, string.Empty);
    }
}
