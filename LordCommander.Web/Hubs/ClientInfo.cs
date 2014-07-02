using LordCommander.Core;
using LordCommander.Shared;

namespace LordCommander.Web.Hubs
{
    internal class ClientInfo
    {
        #region Constructors

        public ClientInfo(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }

        #endregion Constructors

        #region Public Properties

        public string ConnectionId
        {
            get;
            private set;
        }

        public string Name { get; set; }

        public Player Player
        {
            get;
            set;
        }

        #endregion Public Properties

        public PlayerDto ToDto()
        {
            return new PlayerDto { ConnectionId = ConnectionId, Name = Name };
        }
    }
}