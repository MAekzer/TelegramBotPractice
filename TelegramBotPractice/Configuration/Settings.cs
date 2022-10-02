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
                BotToken = "5610717304:AAH7p4WnC-UmqOP7p6ZCu_9otXCKOqSZF5Q",
                Commands = new string[] { "/start" }
            };
        }
    }
}
