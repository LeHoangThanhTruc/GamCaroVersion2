using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GameCaro
{
    
    public class SocketManager
    {
        #region client
        Socket client;
        public bool ConnectServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ep);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion
        #region server
        Socket server;
        public void CreateServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), PORT);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ep);
            server.Listen(10);
            Thread acceptClient = new Thread(() =>
            {
                client = server.Accept();
            });
            //Thread này là thread chạy nền. Khi chương trình chính kết thúc, thread này sẽ tự hủy
            acceptClient.IsBackground = true;
            acceptClient.Start();
        }
        #endregion
        #region both
        public string IP = "127.0.0.1";
        public int PORT = 9876;
        public const int BUFFER = 1024;
        public bool isServer = true;
        public bool Send(Object data)
        {
            byte[] sendData = SerializeData(data);
            return SendData(client, sendData);
        }
        public object Receive()
        {
            /*byte[] receiveData = new byte[BUFFER];
            bool result = ReceiveData(client, receiveData);
        
            return DeserializeData(receiveData);*/
            byte[] receiveData = new byte[BUFFER];
            int receivedBytes = client.Receive(receiveData);
            if (receivedBytes <= 0) return null;

            byte[] actualData = new byte[receivedBytes];
            Array.Copy(receiveData, actualData, receivedBytes);
            return DeserializeData(actualData);
        }
        private bool SendData(Socket target, byte[] data)
        {
            /*return target.Send(data) == 1 ? true : false;*/
            try
            {
                target.Send(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool ReceiveData(Socket target, byte[] data)
        {
            /*return target.Receive(data) == 1 ? true : false;*/
            try
            {
                int received = target.Receive(data);
                return received > 0;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Nén đối tượng thành mảng byte
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }
        /// <summary>
        /// giải nén 1 mảng byte thành 1 đối tượng object
        /// </summary>
        /// <param name="theByteArray"></param>
        /// <returns></returns>
        public object DeserializeData(byte[] theByteArray) { 
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }
        /// <summary>
        /// Lấy ra IPV4 của cạc mạng đang dùng
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
       
        // Lấy IP nội bộ (LAN, Wi-Fi, 4G USB...)
        public string GetLocalIPv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Chỉ lấy adapter đang bật, bỏ loopback và tunnel
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                            !ip.Address.ToString().StartsWith("169.254"))
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "127.0.0.1"; // fallback
        }

        // Lấy Public IP (kết nối Internet bên ngoài)
        public string GetPublicIP()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    return client.DownloadString("https://api.ipify.org");
                }
            }
            catch
            {
                return "Không lấy được Public IP";
            }
        }

        #endregion
    }
}
