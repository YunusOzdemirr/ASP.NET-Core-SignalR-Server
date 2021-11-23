using System;
using System.Collections;
using System.Threading.Tasks;
using SignalRApi.Interfaces;

namespace SignalRApi.Concrete
{
    public class ReceiveManager:IReceiveManager
    {
        public Task<string[]> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<ArrayList> GetAllList()
        {
            throw new NotImplementedException();
        }
    }
}

