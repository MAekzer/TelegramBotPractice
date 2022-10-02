using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBotPractice.Services;

namespace TelegramBotPractice.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).ActiveFeature = callbackQuery.Data;

            // Генерим информационное сообщение
            string instruction = String.Empty;
            string option = String.Empty;

            switch (callbackQuery.Data)
            {
                case "text":
                    option = " Подсчет символов в сообщении";
                    instruction = " Отправьте любое сообщение, бот посчитает количество символов в нем (учитывая пробелы).";
                    break;
                case "num":
                    option = " Подсчет суммы чисел";
                    instruction = "В следующем сообщении отправьте только числа через пробел, бот посчитает их сумму.";
                    break;
            }

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбрана функция - {option}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.{Environment.NewLine}" +
                $"{Environment.NewLine}{instruction}", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
