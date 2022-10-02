using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotPractice.Models;

namespace TelegramBotPractice.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
