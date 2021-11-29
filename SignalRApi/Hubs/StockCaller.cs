using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using SignalRApi.Entities;

namespace SignalRApi.Hubs
{
    public class StockCaller
    {
        private readonly SemaphoreSlim _marketStateLock = new SemaphoreSlim(1, 1);
        public static ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();
        public StockCaller()
        {
            //Thread thread1 = new Thread(new ThreadStart(AddValue));
            //thread1.Start();
            _marketStateLock.WaitAsync();
            AddValue();
            _marketStateLock.Release();

        }

        public async void AddValue()
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
                    await AddValueAsync(stock);
                }
            }
        }

        public async Task AddValueAsync(Stock stock)
        {
            await _marketStateLock.WaitAsync();
            if (_stocks.ContainsKey(stock.symbol))
                _stocks[stock.symbol] = stock;
            else
                _stocks.TryAdd(stock.symbol, stock);
            //    _stocks.TryAdd(stock.symbol, stock);
            //_stocks.TryAdd(stock.symbol, stock);
            await Task.CompletedTask;
            _marketStateLock.Release();
        }
       
        public IEnumerable<Stock> GetValues()
        {
            return _stocks.Values;
        }
    }
}

