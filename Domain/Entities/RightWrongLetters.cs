namespace Domain.Entities;

public class RightWrongLetters
{
    public readonly string Answer;
    public string RightLetters = string.Empty;
    public string WrongLetters = string.Empty;

    public RightWrongLetters(string answer)
    {
        Answer = answer;
    }

    
}
