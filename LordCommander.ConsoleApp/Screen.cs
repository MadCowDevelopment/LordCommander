using System;

namespace LordCommander.ConsoleApp
{
    public abstract class Screen
    {
        protected GameClient GameClient { get; private set; }

        protected Screen(GameClient gameClient)
        {
            GameClient = gameClient;
        }

        public abstract void Render();
        public abstract void HandleInput(ConsoleKey key);
    }
}