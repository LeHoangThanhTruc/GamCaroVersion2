using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using GameCaroShared;

namespace GameCaroClient
{
    public class SocketManager
    {
        #region Fields
        private Socket client;
        private Socket server;
        private const int BUFFER_SIZE = 4096;
        public string IP = "127.0.0.1";
        public int PORT = 9876;
        public bool isServer = true;
        #endregion

        #region Client Methods
        public bool ConnectServer()
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Parse(IP), PORT);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ep);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Server Methods
        public void CreateServer()
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Parse(IP), PORT);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(ep);
                server.Listen(10);

                Thread acceptThread = new Thread(() =>
                {
                    client = server.Accept();
                });
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server creation failed: " + ex.Message);
            }
        }
        #endregion

        #region Send & Receive
        public bool Send(object data)
        {
            if (client == null)
            {
                Console.WriteLine("Client is not connected");
                return false;
            }
            try
            {
                byte[] sendData = SerializeData(data);
                client.Send(sendData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public T Receive<T>()
        {
            try
            {
                byte[] buffer = new byte[BUFFER_SIZE];
                int received = client.Receive(buffer);
                if (received <= 0) return default;

                byte[] actualData = new byte[received];
                Array.Copy(buffer, actualData, received);
                return DeserializeData<T>(actualData);
            }
            catch
            {
                return default;
            }
        }
        #endregion

        #region Serialization
        private byte[] SerializeData(object o)
        {
            string json = JsonSerializer.Serialize(o);
            return Encoding.UTF8.GetBytes(json);
        }

        private T DeserializeData<T>(byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }
        #endregion

        #region Utilities
        public string GetLocalIPv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !ip.Address.ToString().StartsWith("169.254"))
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "127.0.0.1";
        }

        public string GetPublicIP()
        {
            try
            {
                using var webClient = new System.Net.WebClient();
                return webClient.DownloadString("https://api.ipify.org");
            }
            catch
            {
                return "Cannot get public IP";
            }
        }
        #endregion
    }
}
