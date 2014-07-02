namespace LordCommander.Shared
{
    public class GameDto
    {
        public PlayerDto Player { get; private set; }
        public PlayerDto Opponent { get; private set; }

        public GameDto(PlayerDto player, PlayerDto opponent)
        {
            Player = player;
            Opponent = opponent;
        }
    }
}
