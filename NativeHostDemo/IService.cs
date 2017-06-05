using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void Say(string msg);

        [OperationContract]
        int HeartBeat();
    }
}
