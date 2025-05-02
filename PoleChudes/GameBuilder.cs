using PoleChudes.Domain.Entities;
using PoleChudes.Domain.ObjectsSD;

namespace PoleChudes;

public class GameBuilder
{
    public Game Build()
    {
        // create baraban for game
        BarabanSD barabanSD = new BarabanSD();
        Baraban baraban = new Baraban();
        baraban.BarabanSD = barabanSD;

        // create players for game
        Player player1 = new Player();
        Player player2 = new Player();
        Player player = new Player();

        // create game with all their components
        Game game = new Game()
        {
            Baraban = baraban,
            Player1 = player1,
            Player2 = player2,
            Player = player,
        };

        return game;
    }
}
