using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class Service : IService
    {
        public void Say(string msg)
        {
            Console.WriteLine(string.Format("{0} - {1}", DateTime.Now, msg));
        }
        public int HeartBeat()
        {
            Console.WriteLine(string.Format("{0} - Rec HeartBeat", DateTime.Now));
            return 1;
        }
    }
}
