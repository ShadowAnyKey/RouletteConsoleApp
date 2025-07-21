namespace RouletteConsoleApp.Models
{
    public struct GameResult
    {
        public bool IsWin {  get; set; }
        public int WinningNumber { get; set; }
        public string Message {  get; set; }
        public int Stavka { get; set; }
        public int Deposit { get; set; }
    }
}
