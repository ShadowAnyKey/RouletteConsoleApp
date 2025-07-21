namespace RouletteConsoleApp.Services
{
    public class RouletteGame : IRouletteGame
    {
        private readonly IRandomNumberGenerator _rng;
        
        public RouletteGame(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        public GamerInfo Info()
        {
            int score;
            Console.WriteLine("Приветсвую тебя, лудик. Как тебя зовут? ");
            string? name = Console.ReadLine();
            Console.WriteLine($"Привет, {name}. Сколько хочешь депнуть сегодня? Минималка косарь");

            while (true)
            {
                try
                {
                    score = Convert.ToInt32(Console.ReadLine());
                    if (score >= 1000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Сумма деппа не может быть отрицательной или быть меньше минималки");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Неправильная сумма. Введите корректное число");
                    continue;
                }
            }

            return new GamerInfo
            {
                Name = name,
                Score = score
            };
        }
        public GameResult Play(int guess, int deposit, int stavka)
        {
            int gameResults;

            if (stavka < 100)
            {
                return new GameResult
                {
                    Deposit = deposit,
                    Message = "Ставка не может быть меньше 100"
                };
            }

            if (stavka > deposit)
            {
                return new GameResult
                {
                    Deposit = deposit,
                    Message = "Ставка не может быть больше депозита"
                };
            }

            if (guess < 1 || guess > 10)
            {
                return new GameResult
                {
                    Deposit = deposit,
                    Message = "Угадываемое число не может быть отрицательным или быть больше 10"
                };
            }

            int winning = _rng.Next(1, 10);
            bool winResult = guess == winning;
            if (winResult == true)
            {
                gameResults = stavka * 10;
                deposit = deposit + gameResults;
                return new GameResult
                {
                    IsWin = true,
                    WinningNumber = winning,
                    Deposit = deposit,
                    Message = $"Поздравляю! Загаданное число было {winning}. Ты выиграл {gameResults}! Теперь твой счёт составляет {deposit}"
                };
            }
            else
            {
                deposit = deposit - stavka;
                return new GameResult
                {
                    IsWin = false,
                    WinningNumber = winning,
                    Deposit = deposit,
                    Message = $"Не повезло(. Загаданное число было {winning}. Ты проиграл {stavka}! Теперь твой счёт составляет {deposit}"
                };
            }
        }
    }
}
