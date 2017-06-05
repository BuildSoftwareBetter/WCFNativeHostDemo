using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NativeHostDemo
{
    public class ServerProxy
    {
        private System.Timers.Timer timer;
        private void CheckOnline()
        {
            timer = new System.Timers.Timer(60 * 1000);
            timer.Elapsed += timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ServerNode.HeartBeat();
            }
            catch (Exception ex)
            {
                //立即重启服务
                InitServiceConnect();
            }
            //throw new NotImplementedException();
        }

        public ServerProxy(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
            CheckOnline();
        }
        public string IP
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        private IService proxy = null;

        public IService ServerNode
        {
            get
            {
                try
                {
                    if (proxy == null)
                    {
                        InitServiceConnect();
                    }
                    //proxy.HeartBeat();   //发送心跳
                    return proxy;
                }
                catch (Exception ex)
                {
                    InitServiceConnect();
                    return proxy;
                }
            }
        }

        private void InitServiceConnect()
        {
            EndpointAddress endPointAdd = new EndpointAddress(string.Format("net.tcp://{0}:{1}/FaceRecoService/Service", IP, Port));

            NetTcpBinding bind = new NetTcpBinding(SecurityMode.None);  //绑定类型

            //绑定配置
            bind.MaxBufferPoolSize = 10 * 1024 * 1024;
            bind.MaxBufferSize = 10 * 1024 * 1024;
            bind.MaxReceivedMessageSize = 10 * 1024 * 1024;
            bind.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas();
            bind.ReaderQuotas.MaxArrayLength = 10 * 1024 * 1024;
            bind.ReaderQuotas.MaxStringContentLength = 10 * 1024 * 1024;
            bind.ReaderQuotas.MaxBytesPerRead = 10 * 1024 * 1024;
            bind.ReceiveTimeout = new TimeSpan(4, 0, 0);
            bind.ReliableSession.InactivityTimeout = new TimeSpan(4, 0, 0);
            bind.ReceiveTimeout = new TimeSpan(0, 30, 0);

            //通道
            proxy = ChannelFactory<IService>.CreateChannel(bind, endPointAdd);
        }
    }
}
