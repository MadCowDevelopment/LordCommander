using System;

namespace LordCommander.ConsoleApp
{
    public class SplashScreen : Screen
    {
        public SplashScreen(GameClient gameClient)
            : base(gameClient)
        {
        }

        public override void Render()
        {
            Console.WriteLine("Connecting to game server...");
        }

        public override void HandleInput(ConsoleKey key)
        {
        }
    }
}