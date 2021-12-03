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
                await Task.Delay(1000);
            }
        }
        //stream etmen gereken method bu PriceLogStream diğer method zaten private.
        public ChannelReader<int> PriceLogStream(int delay)
        {
            var channel = Channel.CreateUnbounded<int>();
            _ = WriteItems2(channel.Writer, delay);

            return channel.Reader;
        }
        private async Task WriteItems2(ChannelWriter<int> writer, int delay)
        {
            i = 0;
            while (true)
            {
                await writer.WriteAsync(i);
                i++;
                await Task.Delay(delay);
            }

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

