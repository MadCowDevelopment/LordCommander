using System;
using LordCommander.Core;
using LordCommander.Shared;

namespace LordCommander.Web.Hubs
{
    internal class GameInfo
    {
        #region Fields

        private readonly GameHub _gameHub;

        #endregion Fields

        #region Constructors

        public GameInfo(GameHub gameHub, ClientInfo player1, ClientInfo player2)
        {
            GroupName = Guid.NewGuid().ToString();
            _gameHub = gameHub;
            Player1 = player1;
            Player2 = player2;
            Game = new GameLogic();
            Player1.Player = Game.Player1;
            Player2.Player = Game.Player2;

            _gameHub.Groups.Add(Player1.ConnectionId, Player2.ConnectionId);
        }

        #endregion Constructors

        #region Public Properties

        public GameLogic Game
        {
            get;
            private set;
        }

        public ClientInfo Player1
        {
            get;
            private set;
        }

        public ClientInfo Player2
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Private Properties

        private ClientInfo CurrentClient
        {
            get { return Game.CurrentPlayer == Player1.Player ? Player1 : Player2; }
        }

        private string GroupName
        {
            get;
            set;
        }

        private dynamic SendAll
        {
            get { return _gameHub.Clients.Group(GroupName); }
        }

        private dynamic SendPlayer1
        {
            get { return _gameHub.Clients.Client(Player1.ConnectionId); }
        }

        private dynamic SendPlayer2
        {
            get { return _gameHub.Clients.Client(Player2.ConnectionId); }
        }

        #endregion Private Properties

        #region Public Methods

        public void Start()
        {
            SendPlayer1.GameStarted(new GameDto(Player1.ToDto(), Player2.ToDto()));
            SendPlayer2.GameStarted(new GameDto(Player2.ToDto(), Player1.ToDto()));
            SendAll.CurrentPlayerChanged(CurrentClient.ToDto());
        }

        #endregion Public Methods

        public void Execute(PlayerActionDto action)
        {

        }
    }
}