using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo
{
    public class HostService
    {
        private ServiceHost _Host;
        public ServiceHost Host
        {
            get
            {
                if (_Host == null)
                {
                    Init();
                }
                return _Host;
            }
        }

        public string IP = "localhost";

        public int Port
        {
            get;
            private set;
        }

        public Type ServiceType
        {
            get;
            private set;
        }

        public Type InterfaceType
        {
            get;
            private set;
        }

        private void Init()
        {
            _Host = new ServiceHost(this.ServiceType);

            NetTcpBinding bind = new NetTcpBinding(SecurityMode.None);

            bind.MaxBufferPoolSize = 10 * 1024 * 1024;
            bind.MaxBufferSize = 10 * 1024 * 1024;
            bind.MaxReceivedMessageSize = 10 * 1024 * 1024;
            bind.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas();
            bind.ReaderQuotas.MaxArrayLength = 10 * 1024 * 1024;
            bind.ReaderQuotas.MaxStringContentLength = 10 * 1024 * 1024;
            bind.ReaderQuotas.MaxBytesPerRead = 10 * 1024 * 1024;

            Host.AddServiceEndpoint(this.InterfaceType, bind,
                new Uri(string.Format("net.tcp://{0}:{1}/wns/service", this.IP, this.Port)));

            _Host.OpenTimeout = new TimeSpan(1000 * 60 * 60 * 8);

            //host.AddServiceEndpoint(typeof(IWcfService), new WSHttpBinding(), @"net.tcp://localhost:10000");
            //System.ServiceModel.Description.ServiceMetadataBehavior meta = new System.ServiceModel.Description.ServiceMetadataBehavior();
            // meta.HttpGetEnabled = true;
            // meta.HttpGetUrl = new Uri(string.Format("http://{0}:{1}", this.IP, this.Port + 10000));//    @"http://192.168.10.227:10001");

            //meta.HttpGetUrl = new Uri(@"http://192.168.10.227:8800");
            //_Host.Description.Behaviors.Add(meta);

            if (_Host.State != CommunicationState.Opening)
            {
                _Host.Open();
            }
        }
        public HostService(int port, Type serviceType, Type interfaceType = null, IPAddress ip = null)
        {
            //this.IP = ipAddress;
            this.Port = port;
            this.ServiceType = serviceType;
            //this.InterfaceType = interfaceType;

            this.InterfaceType = ServiceType.GetInterfaces()[0];

            if (ip != null)
            {
                this.IP = ip.ToString();
            }

            Init();
        }

        public void Open()
        {
            if (_Host == null)
            {
                Init();
            }
            if (_Host.State != CommunicationState.Opening && _Host.State != CommunicationState.Opened)
            {
                _Host.Open();
            }
        }
        public void Close()
        {
            this._Host.Close();
            this._Host = null;
        }
    }
}
