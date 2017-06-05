using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo.Client
{
    public class ServerHandle
    {
        public ServerHandle(string ip = "localhost")
        {
            _ip = ip;
        }


        private ServerProxy _ServerProxy;
        private string _ip;
        public ServerProxy ServerProxy
        {
            get

            {
                if (_ServerProxy == null)
                {
                    _ServerProxy = new ServerProxy(_ip, 1209);
                }
                return _ServerProxy;
            }
        }
    }
}
