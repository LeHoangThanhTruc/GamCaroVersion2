//Lớp network là nơi xử lý kết nối, gửi, nhận
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public class NetworkClient
    {
        // Singleton Instance
        private static NetworkClient instance;
        public static NetworkClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new NetworkClient();
                return instance;
            }
        }

        private TcpClient client;
        private NetworkStream stream;

        // Event khi nhận dữ liệu
        public static event Action<string> OnMessageReceived;

        private NetworkClient() { }

        public void Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", 9998); // thay bằng IP server thật

                stream = client.GetStream();
                MessageBox.Show("Client đã kết nối tới server! 280");

                // Start thread nhận dữ liệu
                Thread t = new Thread(Receive);
                t.IsBackground = true;
                t.Start();
            }
            catch
            {
                Console.WriteLine("Không thể kết nối tới server!");
            }
        }

      
        public void Send(string message)
        {
            try
            {
                if (stream == null) return;

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Mất kết nối server! Vui lòng thử lại.\n\n" + ex.Message);
                Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định.\n\n" + ex.Message);
            }
        }

        public void Disconnect()
        {
            try { stream?.Close(); } catch { }
            try { client?.Close(); } catch { }

            stream = null;
            client = null;
        }


        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 5000];
                    int bytes = stream.Read(buffer, 0, buffer.Length);

                    if (bytes <= 0)
                        continue;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                    OnMessageReceived?.Invoke(msg);
                }
            }
            catch { }
        }
    }
}
