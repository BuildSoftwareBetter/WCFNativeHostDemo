using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo
{
    class Program
    {
        public static IPAddress GetIP() //获取本地IP
        {
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            return ipHost.AddressList[0];
        }

        static void Main(string[] args)
        {
            IPAddress ip = GetIP();

            HostService service = new HostService(1209, typeof(Service), typeof(IService));

            service.Open();

            Console.ReadKey();
        }
    }
}
