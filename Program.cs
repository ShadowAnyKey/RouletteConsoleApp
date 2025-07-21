namespace RouletteConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>()
                .AddTransient<IRouletteGame, RouletteGame>()
                .BuildServiceProvider();

            var game = services.GetRequiredService<IRouletteGame>();

            Console.WriteLine("=== Игра «Угадай число от 1 до 10» ===");
            GamerInfo Info = game.Info();
            int deposit = Info.Score;

            while (true)
            {
                Console.Write("Введите ваше число (или Q для выхода): ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Неверный");
                    continue;
                }
                Console.Write("Введите ставку: ");
                string? inputStavka = Console.ReadLine();
                if (!int.TryParse(inputStavka, out int stavka))
                {
                    Console.WriteLine("Неверная ставка");
                    continue;
                }
                GameResult result = game.Play(guess, deposit, stavka);
                Console.WriteLine(result.Message);
                Console.WriteLine();

                deposit = result.Deposit;

                if (result.Deposit <= 0)
                {
                    Console.WriteLine($"Ты всё проиграл, {Info.Name}");
                    break;
                }
            }

            Console.WriteLine("Спасибо за игру!");
        }
    }
}