using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameCaroShared;
using System.Text.Json;

namespace GameCaroServer
{
    
    public class ServerManager
    {
        private Socket serverSocket;
        private List<Socket> clients;
        private const int BUFFER_SIZE = 4096;
        public int PORT = 9876;
        public ServerManager()
        {
            clients = new List<Socket>();
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(2);

            Console.WriteLine($"Server listening on port {PORT}...");

            Thread acceptThread = new Thread(AcceptClients);
            acceptThread.IsBackground = true;
            acceptThread.Start();
        }
        public void Send(Socket client, SocketData data)
        {
            try
            {
                byte[] buff = SerializeData(data);
                client.Send(buff);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send error: " + ex.Message);
            }
        }
        private void AcceptClients()
        {
            while (true)
            {
                Socket client = serverSocket.Accept();
                lock (clients)
                {
                    if (clients.Count >= 2)
                    {
                        // chỉ nhận tối đa 2 client
                        Send(client, new SocketData((int)SocketCommand.NOTIFY, "Server full", new System.Drawing.Point()));
                        client.Close();
                        continue;
                    }

                    clients.Add(client);
                    Console.WriteLine($"New client connected: {client.RemoteEndPoint}");

                    // Gán player
                    int playerNumber = clients.Count; // 1 hoặc 2
                    Send(client, new SocketData((int)SocketCommand.ASSIGN_PLAYER, playerNumber.ToString(), new System.Drawing.Point()));
                }

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }

        }

        private void HandleClient(Socket client)
        {
            /*try
            {
                while (true)
                {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int received = client.Receive(buffer);
                    if (received <= 0) break;

                    string json = Encoding.UTF8.GetString(buffer, 0, received);

                    SocketData data = JsonSerializer.Deserialize<SocketData>(json);

                    Console.WriteLine($"Received Command={data.Command} from {client.RemoteEndPoint}");

                    // broadcast
                    Broadcast(data, client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client disconnected: {ex.Message}");
            }
            finally
            {
                lock (clientSockets)
                {
                    clientSockets.Remove(client);
                }
                client.Close();
            }*/
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int received = client.Receive(buffer);
                    if (received <= 0) break;

                    byte[] actualData = new byte[received];
                    Array.Copy(buffer, actualData, received);

                    SocketData data = DeserializeData<SocketData>(actualData);
                    Console.WriteLine($"Received command: {data.Command} from {client.RemoteEndPoint}");

                    // Broadcast dữ liệu cho client còn lại
                    Broadcast(data, client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client disconnected: {ex.Message}");
            }
            finally
            {
                lock (clients)
                {
                    clients.Remove(client);
                }
                client.Close();
            }
        }

        private void Broadcast(SocketData data, Socket excludeClient)
        {
            byte[] sendData = SerializeData(data);
            lock (clients)
            {
                foreach (var c in clients)
                {
                    if (c != excludeClient)
                    {
                        try { c.Send(sendData); } catch { }
                    }
                }
            }
        }

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
    }
}
