using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotPractice.Services
{
    public class TextHandler : ITextCounter
    {
        public int Count(string text)
        {
            return text.Length;
        }
    }
}
