namespace LordCommander.Core
{
    public class GameLogic
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Player CurrentPlayer { get; private set; }

        public GameLogic()
        {
            Player1 = new Player();
            Player2 = new Player();

            CurrentPlayer = RNG.Chance(50) ? Player1 : Player2;
        }
    }

    public class Player
    {

    }
}