using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBotPractice.Services;
using TelegramBotPractice.Configuration;

namespace TelegramBotPractice.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _storage;
        private readonly ITextCounter _textCounter;
        private readonly INumCounter _numCounter;
        private readonly Settings _settings;

        public TextMessageController(ITelegramBotClient telegramBotClient,
            IStorage storage,
            ITextCounter textCounter,
            INumCounter numCounter,
            Settings settings)
        {
            _telegramClient = telegramBotClient;
            _storage = storage;
            _textCounter = textCounter;
            _numCounter = numCounter;
            _settings = settings;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string[] commands = new string[] { "/start" };

            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчет символов" , $"text"),
                        InlineKeyboardButton.WithCallbackData($" Подсчет суммы чисел" , $"num")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Этот бот может посчитать количество символов или сумму чисел в сообщении.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Выберите, что из этого вы хотите использовать." +
                        $"{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
            }

            var feature = _storage.GetSession(message.Chat.Id).ActiveFeature;

            if (!_settings.Commands.Contains(message.Text))
            {
                switch (feature)
                {
                    case "text":
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Количество символов в сообщении: {_textCounter.Count(message.Text)}", cancellationToken: ct);
                        break;

                    case "num":

                        try
                        {
                            int[] numbers = message.Text.Split(' ').Select(int.Parse).ToArray();
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {_numCounter.Count(numbers)}", cancellationToken: ct);
                        }
                        catch (FormatException e)
                        {
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Неправильный формат ввода", cancellationToken: ct);
                        }

                        break;
                }
            }
        }
    }
}
