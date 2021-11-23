using System;
using System.Threading.Tasks;

namespace SignalRApi.Interfaces
{
    public interface IReceiveManager
    {
        public Task<string[]> GetAll();
    }
}

