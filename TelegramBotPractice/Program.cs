using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TelegramBotPractice.Configuration;
using TelegramBotPractice.Controllers;
using TelegramBotPractice.Services;
using Telegram.Bot;

namespace TelegramBotPractice
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddSingleton<INumCounter, NumbersHandler>();
                  services.AddSingleton<ITextCounter, TextHandler>();

                  Settings appSettings = Settings.BuildSettings();
                  services.AddSingleton(Settings.BuildSettings());

                  services.AddSingleton<IStorage, MemoryStorage>();

                  services.AddTransient<DefaultMessageController>();
                  services.AddTransient<TextMessageController>();
                  services.AddTransient<InlineKeyboardController>();

                  services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
                  // Регистрируем постоянно активный сервис бота
                  services.AddHostedService<Bot>();
              }
                ) // Задаем конфигурацию
              .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
              .Build(); // Собираем


            Console.WriteLine("Service Started");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Service Stopped");
        }
    }
}
