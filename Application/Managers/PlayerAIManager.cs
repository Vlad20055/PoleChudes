namespace Application.Managers;

public class PlayerAIManager : PlayerManager
{
    Random random = new Random();

    public char SelectKey()
    {
        char[] keysNumbers = { '1', '2', '3' };
        return keysNumbers[random.Next(3)];
    }

    public char SelectLetter(string rightLetters, string wrongLetters)
    {
        if (rightLetters.Length != 0 && wrongLetters.Length != 0)
        {
            int choice = random.Next(0, 100);
            if (choice < 50) return wrongLetters[choice % wrongLetters.Length];
            else return rightLetters[choice % rightLetters.Length];
        }
        else if (rightLetters.Length != 0)
        {
            int choice = random.Next(rightLetters.Length);
            return rightLetters[choice];
        }
        else if (wrongLetters.Length != 0)
        {
            int choice = random.Next(wrongLetters.Length);
            return wrongLetters[choice];
        }
        else throw new Exception("No letters remain");
    }

    public string SelectPrizeOrMoney()
    {
        int choice = random.Next(0, 100);
        if (choice >= 70) return "money";
        else return "prize";
    }

    public bool WantTrySectorKey()
    {
        int choice = random.Next(100);
        if (choice < 50) return false;
        else return true;
    }

    public bool WantTrySectorPrize()
    {
        int choice = random.Next(100);
        if (choice < 50) return false;
        else return true;
    }

    public int SelectPosition(List<int> availablePositions)
    {
        int choice = availablePositions[random.Next(availablePositions.Count)];
        return choice;
    }
}
