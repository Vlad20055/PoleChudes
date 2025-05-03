using PoleChudes.Domain.Entities;
using PoleChudes.UseCases;

namespace PoleChudes;

public class Game
{
    public required BarabanManager barabanManager;
    public required Baraban Baraban;
    public required Player Player1;
    public required Player Player2;
    public required Player Player;
    public required GameTask GameTask;
    public required AnswerPanelManager AnswerPanelManager;
    public required AnswerPanel AnswerPanel;
    public required LettersPanelManager LettersPanelManager;
    public required LettersPanel LettersPanel;
}
