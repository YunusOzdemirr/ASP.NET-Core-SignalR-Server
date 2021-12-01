using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.Entities;

namespace SignalRApi.Hubs
{
    public class ServerToClientStreamingHub : Hub
    {
        public ChannelReader<Stock> GetSomeDataWithChannelReader(int count, int delay, CancellationToken cancellationToken)
        {
            var channel = Channel.CreateUnbounded<Stock>();
            _ = WriteItemsAsync(channel.Writer, count, delay, cancellationToken);
            return channel.Reader;
        }

        public async IAsyncEnumerable<Stock> GetSomeDataWithAsyncStreams(
  int count,
  int delay,
  [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (int i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(delay);
                yield return new Stock { price = i };
            }
        }
        private async Task WriteItemsAsync(ChannelWriter<Stock> writer, int count, int delay, CancellationToken cancellationToken)
        {
            try
            {
                for (var i = 0; i < count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await writer.WriteAsync(new Stock() { price = i });
                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                writer.TryComplete(ex);
            }

            writer.TryComplete();
        }
    }
}

