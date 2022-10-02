using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotPractice.Configuration
{
    public class Settings
    {
        public string BotToken { get; set; }
        public string[] Commands { get; set; }

        public static Settings BuildSettings()
        {
            return new Settings
            {
                BotToken = "Bot_Token",
                Commands = new string[] { "/start" }
            };
        }
    }
}
