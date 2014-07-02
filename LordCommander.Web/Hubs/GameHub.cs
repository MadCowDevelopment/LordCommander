using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LordCommander.Shared;
using Microsoft.AspNet.SignalR;

namespace LordCommander.Web.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        #region Fields

        private static readonly Dictionary<string, ClientInfo> ConnectedPlayers = new Dictionary<string, ClientInfo>();
        private static readonly List<ClientInfo> QueuedPlayers = new List<ClientInfo>();
        private static readonly List<GameInfo> RunningGames = new List<GameInfo>();

        #endregion Fields

        #region Private Properties

        private string ConnectionId
        {
            get { return Context.ConnectionId; }
        }

        private GameInfo RunningGame
        {
            get
            {
                return
                    RunningGames.Single(
                        p => p.Player1.ConnectionId == ConnectionId || p.Player2.ConnectionId == ConnectionId);
            }

        }

        private ClientInfo Player
        {
            get { return ConnectedPlayers[ConnectionId]; }
        }

        #endregion Private Properties

        #region Public Methods

        public void LeaveQueue()
        {
            lock (QueuedPlayers)
            {
                QueuedPlayers.Remove(Player);
                Clients.Clients(QueuedPlayers.Select(p => p.ConnectionId).ToList()).PlayerLeftQueue(ConnectionId);
            }
        }

        public void SignIn(string name)
        {
            if (!ConnectedPlayers.ContainsKey(ConnectionId))
            {
                Clients.Others.PlayerConnected(ConnectionId);
                ConnectedPlayers.Add(ConnectionId, new ClientInfo(ConnectionId, name));
            }
        }

        public void Execute(PlayerActionDto action)
        {
            if (RunningGame == null) return;
            RunningGame.Execute(action);
        }

        public override Task OnDisconnected()
        {
            if (ConnectedPlayers.ContainsKey(ConnectionId))
            {
                ConnectedPlayers.Remove(ConnectionId);
            }

            return base.OnDisconnected();
        }

        public void Queue()
        {
            lock (QueuedPlayers)
            {
                if (QueuedPlayers.Any()) StartNewGame(QueuedPlayers.First());
                else QueuePlayer();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void QueuePlayer()
        {
            QueuedPlayers.Add(Player);
            Clients.Others.PlayerJoinedQueue(ConnectionId);
        }

        private void StartNewGame(ClientInfo opponent)
        {
            var gameInfo = new GameInfo(this, Player, opponent);
            RunningGames.Add(gameInfo);
            RunningGame.Start();
        }

        #endregion Private Methods
    }

    
}