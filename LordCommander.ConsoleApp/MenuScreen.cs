using System;

namespace LordCommander.ConsoleApp
{
    public class MenuScreen : Screen
    {
        public MenuScreen(GameClient gameClient)
            : base(gameClient)
        {
        }

        public override void Render()
        {
            Console.WriteLine("Q - Queue");
            Console.WriteLine("X - Exit");
        }

        public override void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Q:
                    GameClient.Queue();
                    break;
                case ConsoleKey.X:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}