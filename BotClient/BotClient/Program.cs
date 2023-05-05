using Domain.Models;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://localhost:7151/api/ProductControllers");

            Console.WriteLine(result);
            var test = await result.Content.ReadAsStringAsync();
            Console.WriteLine(test);

            //Product[] products = JsonConvert.DeserializeObject <Product[]>(test);

            //foreach(var product in products)
            //{
            //    Console.WriteLine(product.NumberProduct + "" + product.Namee + " " + product.ProductPrice);
            //}


            var botClient = new TelegramBotClient("6000813956:AAE5A3kLEh6EtE_9OjcEKVJlP_FbWB-LtV8");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();





        }
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Massege updates: hhttp://core.telegram.org/bots/api#massage
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;
            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
            //Message setMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "You said: \n" + messageText,
            //    cancellationToken: cancellationToken); 

            if (message.Text == "Проверка")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "проверка: Ок",
                    cancellationToken: cancellationToken);
            }
            if (message.Text == "Привет")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Здравствуй, Солнышко",
                    cancellationToken: cancellationToken);
            }
            if (message.Text == "Картинка")
            {
                await botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: "https://wonder-day.com/wp-content/uploads/2022/05/wonder-day-cute-drawings-pigs-36.jpg",
                    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
            }
            if (message.Text == "Видео")
            {
                message = await botClient.SendVideoAsync(
                chatId: chatId,
                video: "https://rr3---sn-4g5e6nzz.googlevideo.com/videoplayback?expire=1683294851&ei=I7ZUZI6kIMnwkgbMkwg&ip=191.96.106.77&id=o-ANOOylsYpLRGkKsOEPergUVqzit29WmPYITGK65QTUSa&itag=18&source=youtube&requiressl=yes&spc=qEK7B3r4zQDsM1YfiqkiGkmG8ktDrMeY2zBKSeyq1g&vprv=1&svpuc=1&mime=video%2Fmp4&ns=ogMGlVDmHZc-Yri--OVOkgoN&gir=yes&clen=103907&ratebypass=yes&dur=2.670&lmt=1489684741712259&fexp=24007246&c=WEB&n=KXnlNaEefJXvPg&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cspc%2Cvprv%2Csvpuc%2Cmime%2Cns%2Cgir%2Cclen%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRQIgRRBtBc3koENmTXigFf7PZoW0udXKZUu0hvxrWmdfB-ECIQDt-UZKLw1fXH3yPAkpsoGKepNMTXGzCBH-TiS1kJT6Hw%3D%3D&rm=sn-a5meez76&req_id=efd3ded772c1a3ee&cm2rm=sn-jvhnu5g-c35d7d,sn-jvhnu5g-n8vy7d,sn-n8vd6ez&ipbypass=yes&redirect_counter=4&cms_redirect=yes&cmsv=e&mh=pt&mip=94.29.124.225&mm=34&mn=sn-4g5e6nzz&ms=ltu&mt=1683272928&mv=m&mvi=3&pl=18&lsparams=ipbypass,mh,mip,mm,mn,ms,mv,mvi,pl&lsig=AG3C_xAwRAIgKkq6LnwMOvydGRs-3sTyikws1xTQqYQHba6933eaUagCIHSHOZWqZyzhUNI5_wPkGnNDWZnKA4a2hPQOfo-UW817",
                thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                supportsStreaming: true,
                cancellationToken: cancellationToken);
            }
            if (message.Text == "Стикер")
            {
                message = await botClient.SendStickerAsync(
                chatId: chatId,
                sticker: "CAACAgIAAxkBAAEgq91kVLfuuRgcnXNHlyjKnsPRIvmwmQACXRwAAqvQQUix35BnpHrVzi8E",
                cancellationToken: cancellationToken);
            }
            


        }
        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegramm Api Error: \n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
              _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}