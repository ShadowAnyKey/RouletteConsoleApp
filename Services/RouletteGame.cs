namespace RouletteConsoleApp.Services
{
    public class RouletteGame : IRouletteGame
    {
        private readonly IRandomNumberGenerator _rng;
        private string _rules;

        public RouletteGame(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        public GamerInfo Info()
        {
            int initialDeposit;
            Console.WriteLine("Приветствую вас, Игрок. Как вас зовут?");
            string? name = Console.ReadLine();
            Console.WriteLine($"Здравствуйте, {name}. Сколько вы хотите внести на депозит сегодня?");
            Console.WriteLine("Минимальная сумма для взноса - 1000 монет");

            while (true)
            {
                try
                {
                    initialDeposit = Convert.ToInt32(Console.ReadLine());
                    if (initialDeposit >= 1000) break;
                    else
                    {
                        Console.WriteLine("Сумма депозита не может быть меньше минимальной");

                    }
                }
                catch
                {
                    Console.WriteLine("Введите корректное число");
                    continue;
                }
            }

            _rules = "Правила следующие.\n" +
                "Вы должны угадать число от 1 до 10.\n" +
                "Если вы угадываете число, ваша ставка увеличится в 10 раз\n" +
                "Если проиграете, ваша ставка сгорит";

            Console.WriteLine(_rules);

            return new GamerInfo
            {
                Name = name,
                InitialDeposit = initialDeposit,
            };
        }

        public GameResult Play(int guess, int deposit, int stavka)
        {
            int gameResult;

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

            int winning = _rng.GetRandomNumber(1, 10);
            bool winResult = guess == winning;

            if (winResult)
            {
                gameResult = stavka * 10;
                deposit = deposit + gameResult;

                return new GameResult
                {
                    IsWin = true,
                    WinningNumber = winning,
                    Deposit = deposit,
                    Message = $"Поздравляю! Загаданное число было {winning}. Ты выиграл {gameResult}! Теперь твой счёт составляет {deposit}"
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