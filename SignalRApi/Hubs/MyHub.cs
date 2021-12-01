using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.Entities;

namespace SignalRApi.Hubs
{
    public class MyHub : Hub
    {
        public static int i = 0;

        public readonly StockCaller _stock;

        public MyHub(StockCaller stock)
        {
            _stock = stock;
        }

        public async Task<bool> SendRandomCharacter()
        {
            while (true)
            {
                var result = _stock.GetValues();
                await Clients.All.SendAsync("receiveMessage", result);
                Thread.Sleep(200);
                //foreach (var item in result)
                //{
                //    await Clients.All.SendAsync("receiveMessage", item);
                //    Thread.Sleep(200);
                //}
            }
        }
        public ChannelReader<Stock> DelayCounter(int delay)
        {
            var channel = Channel.CreateUnbounded<Stock>();

            _ = WriteItems(channel.Writer, delay);

            return channel.Reader;
        }

        private async Task WriteItems(ChannelWriter<Stock> writer, int delay)
        {

            string[] symbols = new string[6] { "USD", "EUR", "ATLAS", "GARAN", "ISBNK", "AKBNK" };
            while (true)
            {
                Random random = new Random();
                foreach (var item in symbols)
                {
                    Stock stock = new Stock()
                    {
                        symbol = item,
                        price = random.Next(100, 500),
                    };
                    await writer.WriteAsync(stock);
                    await Task.Delay(delay);
                }
            }
        }
    }
}

