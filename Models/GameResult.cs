namespace RouletteConsoleApp.Models
{
    public struct GameResult
    {
        public int Deposit { get; set; }
        public bool IsWin { get; set; }
        public int WinningNumber { get; set; }
        public string Message { get; set; }
    }
}