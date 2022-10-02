using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotPractice.Services
{
    public interface ITextCounter
    {
        int Count(string text);
    }
}
