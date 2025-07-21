namespace RouletteConsoleApp.Interfaces
{
    public interface IRouletteGame
    {
        GamerInfo Info();
        GameResult Play(int guess, int deposit, int stavka);
    }
}