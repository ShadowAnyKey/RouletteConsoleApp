namespace RouletteConsoleApp.Services
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _rnd = new();

        public int GetRandomNumber(int minValue, int maxValue) => _rnd.Next(minValue, maxValue);
    }
}