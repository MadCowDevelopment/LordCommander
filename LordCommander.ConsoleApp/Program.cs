using LordCommander.ConsoleApp;

namespace SiegeLord.TestApp
{
    public static class Program
    {
        static void Main()
        {
            new GameClient().Start();
            for (; ; ) { }
        }
    }
}
