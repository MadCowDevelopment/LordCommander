using System;

namespace LordCommander.ConsoleApp
{
    public class QueueScreen : Screen
    {
        public QueueScreen(GameClient gameClient)
            : base(gameClient)
        {
        }

        public override void Render()
        {
            Console.WriteLine("L - Leave queue");
        }

        public override void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.L:
                    GameClient.LeaveQueue();
                    break;
                case ConsoleKey.X:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}