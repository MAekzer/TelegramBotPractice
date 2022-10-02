using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBotPractice.Services
{
    public class NumbersHandler : INumCounter
    {
        public int Count(int[] numbers)
        {
            return numbers.Sum();
        }
    }
}
