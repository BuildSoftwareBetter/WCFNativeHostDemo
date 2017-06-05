using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerHandle hd = new Client.ServerHandle();
            string msg = null;
            msg = Console.ReadLine();

            while (!msg.Equals("exit"))
            {
                hd.ServerProxy.ServerNode.Say(msg);
                msg = Console.ReadLine();
            }
        }
    }
}
