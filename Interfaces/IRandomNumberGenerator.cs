using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteConsoleApp.Interfaces
{
    public interface IRandomNumberGenerator
    {
        int Next(int minValue, int maxValue);
    }
}
