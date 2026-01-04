////Lớp network là nơi xử lý kết nối, gửi, nhận
///BAN GOC
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace GameCaro
//{
//    public class NetworkClient
//    {
//        // Singleton Instance
//        private static NetworkClient instance;
//        public static NetworkClient Instance
//        {
//            get
//            {
//                if (instance == null)
//                    instance = new NetworkClient();
//                return instance;
//            }
//        }

//        private TcpClient client;
//        private NetworkStream stream;

//        // Event khi nhận dữ liệu
//        public static event Action<string> OnMessageReceived;

//        private NetworkClient() { }

//        public void Connect()
//        {
//            try
//            {
//                client = new TcpClient();
//                client.Connect("127.0.0.1", 9998); // thay bằng IP server thật

//                stream = client.GetStream();
//                MessageBox.Show("Client đã kết nối tới server! 280");

//                // Start thread nhận dữ liệu
//                Thread t = new Thread(Receive);
//                t.IsBackground = true;
//                t.Start();
//            }
//            catch
//            {
//                Console.WriteLine("Không thể kết nối tới server!");
//            }
//        }


//        public void Send(string message)
//        {
//            try
//            {
//                if (stream == null) return;

//                byte[] data = Encoding.UTF8.GetBytes(message);
//                stream.Write(data, 0, data.Length);
//            }
//            catch (IOException ex)
//            {
//                MessageBox.Show("Mất kết nối server! Vui lòng thử lại.\n\n" + ex.Message);
//                Disconnect();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi không xác định.\n\n" + ex.Message);
//            }
//        }

//        public void Disconnect()
//        {
//            try { stream?.Close(); } catch { }
//            try { client?.Close(); } catch { }

//            stream = null;
//            client = null;
//        }


//        private void Receive()
//        {
//            try
//            {
//                while (true)
//                {
//                    byte[] buffer = new byte[1024 * 5000];
//                    int bytes = stream.Read(buffer, 0, buffer.Length);

//                    if (bytes <= 0)
//                        continue;

//                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

//                    OnMessageReceived?.Invoke(msg);
//                }
//            }
//            catch { }
//        }
//    }
//}

//-------------------------------------------------------------
// BẢN SAU KHI SỬA SETTINGS
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace GameCaro
//{
//    public class NetworkClient
//    {
//        // Singleton Instance
//        private static NetworkClient instance;
//        public static NetworkClient Instance
//        {
//            get
//            {
//                if (instance == null)
//                    instance = new NetworkClient();
//                return instance;
//            }
//        }

//        private TcpClient client;
//        private NetworkStream stream;
//        private bool isConnected = false;

//        // Event khi nhận dữ liệu
//        public static event Action<string> OnMessageReceived;

//        private NetworkClient() { }

//        public void Connect()
//        {
//            try
//            {
//                client = new TcpClient();
//                client.Connect("127.0.0.1", 9998); // thay bằng IP server thật

//                stream = client.GetStream();
//                isConnected = true;
//                MessageBox.Show("Client đã kết nối tới server!");

//                // Start thread nhận dữ liệu
//                Thread t = new Thread(Receive);
//                t.IsBackground = true;
//                t.Start();
//            }
//            catch
//            {
//                Console.WriteLine("Không thể kết nối tới server!");
//            }
//        }

//        public void Send(string message)
//        {
//            try
//            {
//                if (stream == null) return;

//                byte[] data = Encoding.UTF8.GetBytes(message);
//                stream.Write(data, 0, data.Length);
//            }
//            catch (IOException ex)
//            {
//                MessageBox.Show("Mất kết nối server! Vui lòng thử lại.\n\n" + ex.Message);
//                Disconnect();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi không xác định.\n\n" + ex.Message);
//            }
//        }

//        public void Disconnect()
//        {
//            isConnected = false;

//            try
//            {
//                if (stream != null)
//                {
//                    stream.Close();
//                    stream = null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Lỗi đóng stream: " + ex.Message);
//            }

//            try
//            {
//                if (client != null)
//                {
//                    client.Close();
//                    client = null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Lỗi đóng client: " + ex.Message);
//            }

//            Console.WriteLine("Đã ngắt kết nối với server");
//        }

//        private void Receive()
//        {
//            try
//            {
//                while (isConnected && stream != null)
//                {
//                    byte[] buffer = new byte[1024 * 5000];
//                    int bytes = stream.Read(buffer, 0, buffer.Length);

//                    if (bytes <= 0)
//                        continue;

//                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

//                    OnMessageReceived?.Invoke(msg);
//                }
//            }
//            catch (Exception ex)
//            {
//                if (isConnected)
//                {
//                    Console.WriteLine("Lỗi nhận dữ liệu: " + ex.Message);
//                }
//            }
//            finally
//            {
//                Console.WriteLine("Thread Receive đã dừng");
//            }
//        }
//    }
//}

//-------------------------------------------------------------
// BẢN THỬ LAN
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
        private bool isConnected = false;

        // Event khi nhận dữ liệu
        public static event Action<string> OnMessageReceived;

        private NetworkClient() { }

        /// <summary>
        /// Kết nối đến Server với IP được cấu hình
        /// </summary>
        public void Connect()
        {
            try
            {
                // Lấy IP Server từ cấu hình
                string serverIP = ServerConfig.Instance.GetServerIP();

                client = new TcpClient();

                // ⚠️ QUAN TRỌNG: Thay đổi này cho phép kết nối LAN
                client.Connect(serverIP, 9998);

                stream = client.GetStream();
                isConnected = true;

                MessageBox.Show(
                    $" Đã kết nối tới Server!\nĐịa chỉ: {serverIP}:9998",
                    "Kết nối thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Start thread nhận dữ liệu
                Thread t = new Thread(Receive);
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Không thể kết nối tới Server!\n\n" +
                    $"Server IP: {ServerConfig.Instance.GetServerIP()}\n" +
                    $"Lỗi: {ex.Message}\n\n" +
                    $"Hãy kiểm tra:\n" +
                    $"1. Server đã chạy chưa?\n" +
                    $"2. IP Server có đúng không?\n" +
                    $"3. Firewall đã mở port 9998 chưa?",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Console.WriteLine($"Không thể kết nối tới server: {ex.Message}");
            }
        }

        /// <summary>
        /// Kết nối với IP tùy chỉnh (dùng khi người dùng nhập IP mới)
        /// </summary>
        public bool ConnectWithCustomIP(string serverIP)
        {
            try
            {
                // Validate IP
                if (!ServerConfig.IsValidIP(serverIP))
                {
                    MessageBox.Show(
                        "Địa chỉ IP không hợp lệ!\nVí dụ: 192.168.1.100",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
                }

                // Ngắt kết nối cũ nếu có
                if (isConnected)
                {
                    Disconnect();
                }

                // Lưu IP mới
                ServerConfig.Instance.SaveServerIP(serverIP);

                // Kết nối lại
                Connect();
                return isConnected;
            }
            catch
            {
                return false;
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
            isConnected = false;

            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi đóng stream: " + ex.Message);
            }

            try
            {
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi đóng client: " + ex.Message);
            }

            Console.WriteLine("Đã ngắt kết nối với server");
        }

        private void Receive()
        {
            try
            {
                while (isConnected && stream != null)
                {
                    byte[] buffer = new byte[1024 * 5000];
                    int bytes = stream.Read(buffer, 0, buffer.Length);

                    if (bytes <= 0)
                        continue;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                    OnMessageReceived?.Invoke(msg);
                }
            }
            catch (Exception ex)
            {
                if (isConnected)
                {
                    Console.WriteLine("Lỗi nhận dữ liệu: " + ex.Message);
                }
            }
            finally
            {
                Console.WriteLine("Thread Receive đã dừng");
            }
        }

        /// <summary>
        /// Kiểm tra kết nối hiện tại
        /// </summary>
        public bool IsConnected()
        {
            return isConnected && client != null && client.Connected;
        }
    }
}