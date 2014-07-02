using System;
using System.Reactive.Linq;
using System.Threading;
using LordCommander.Client;

namespace LordCommander.ConsoleApp
{
    public class GameClient
    {
        private Screen _currentScreen;

        public GameProxy Proxy { get; set; }

        private readonly AuthenticationHelper _authenticationHelper =
            new AuthenticationHelper(ServerConstants.AuthBaseAddress);

        private Screen CurrentScreen
        {
            get { return _currentScreen; }
            set
            {
                _currentScreen = value;
                Console.Clear();
                _currentScreen.Render();
            }
        }

        public void Start()
        {
            InitializeProxy();
            StartInputThread();
            CurrentScreen = new LoginScreen(this, _authenticationHelper);
        }

        private void StartInputThread()
        {
            var gameLoop = new Thread(GameLoop);
            gameLoop.Start();
        }

        private void InitializeProxy()
        {
            
            Proxy = new GameProxy();
            Proxy.StateChanged.Subscribe(HandleStateChanged);
        }

        private void HandleStateChanged(ConnectionStateInfo state)
        {
            if (state == ConnectionStateInfo.Connected)
            {
                Proxy.SignIn();
                CurrentScreen = new MenuScreen(this);
            }
        }

        private void GameLoop()
        {
            for (; ; )
            {
                var key = Console.ReadKey(true);
                CurrentScreen.HandleInput(key.Key);
            }
        }

        public void Queue()
        {
            Proxy.GameStarted.Take(1).Subscribe(p =>
            {
                CurrentScreen = new GameScreen(this);
            });

            Proxy.Queue();
            CurrentScreen = new QueueScreen(this);
        }

        public void LeaveQueue()
        {
            Proxy.LeaveQueue();
            CurrentScreen = new MenuScreen(this);
        }
    }

    public class LoginScreen : Screen
    {
        public LoginScreen(GameClient gameClient, AuthenticationHelper authenticationHelper) : base(gameClient)
        {
        }

        public override void Render()
        {
            Console.WriteLine("");
        }

        public override void HandleInput(ConsoleKey key)
        {
        }
    }

    public class GameScreen : Screen
    {
        public GameScreen(GameClient gameClient) : base(gameClient)
        {
            GameClient.Proxy.CurrentPlayerChanged.Subscribe(
                p => Console.WriteLine("CurrentPlayer: {0} ({1})", p.Name, p.ConnectionId));
        }

        public override void Render()
        {
            Console.WriteLine("Game has started.");
        }

        public override void HandleInput(ConsoleKey key)
        {
            
        }
    }
}