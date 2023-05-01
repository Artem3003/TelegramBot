using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static string token { get; set; } = "5870800439:AAEK8LuWV9d3JT8hMsO_CFHZXgbHO0MxU5g";
        private static TelegramBotClient client = new TelegramBotClient(token);
        static void Main(string[] args)
        {
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            GreetingMessage(update);
        }

        private static async void GreetingMessage(Update update)
        {
            var me = await client.GetMeAsync();
            var chatId = update.Message.Chat.Id;
            var message = update.Message;
            string messageGreeting = $"Привіт👋, {message.Chat.FirstName}. Я новий член вашого корпоративу😇. Мене звати {me.FirstName}. Я дуже радий, що познайомився з вами усіма😊. Тут ти зможеш найти детальну інформацію про корпоратив та в майбутньому про кожного члена цього корпоративу. Приємного користування😀!!!";
            if (message.Text == "/start")
            {
                await using var photo = System.IO.File.OpenRead(@"./resources/greetingSticker.webp");
                await client.SendTextMessageAsync(chatId, messageGreeting, replyMarkup: GetButtons());   
                await client.SendStickerAsync(chatId, photo);
                return;
            }

            if (message.Text == "Відео годік🥳")
            {                
                await client.SendTextMessageAsync(chatId, "https://youtu.be/aBw7OvPcsB0");

                await client.SendTextMessageAsync(chatId, "Гарного перегляду)))😆");  
                return;
            } 

            if (message.Text != null)
            {
                await client.SendTextMessageAsync(chatId, "Нормально общайся!!!", replyToMessageId: message.MessageId);   
                return;
            }
        }

        private static IReplyMarkup? GetButtons()
        {
            return new ReplyKeyboardMarkup
            (
                new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { "Відео годік🥳" }
                }
            )
            {
                ResizeKeyboard = true
            };
        }
    }
}