using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace MayChu
{
    public class Program
    {
        private static IFirebaseClient firebaseClient;
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        // Danh sách người đang chờ ghép trận
        static List<(Socket socket, string userId)> waitingList = new List<(Socket, string)>();
        object matchLock = new object();
        Dictionary<string, Socket> clientMap = new Dictionary<string, Socket>();


        static void Main(string[] args)
        {
            Console.WriteLine("Server dang ket noi...281");
            Program server = new Program();
            //Khởi tạo cấu hình Firebase
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "Urxaa4u3mw7uUn6HL9Bk2WntO7QzEIbzMDAQIWRm",
                BasePath = "https://gamecarodatabaserealtime-default-rtdb.asia-southeast1.firebasedatabase.app/"

            };
            firebaseClient = new FireSharp.FirebaseClient(config);
            Console.WriteLine("Đa ket noi Firebase! ");

            server.Connect();

            Console.ReadLine();
        }
        void Connect()
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 9998);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(IP);
            server.Listen(1000);

            Console.WriteLine("Server started on port 9998...");

            Thread listen = new Thread(() =>
            {
                while (true)
                {
                    Socket client = server.Accept();
                    Console.WriteLine("Kết nối thành công!");
                    Console.WriteLine("Client connected: " + client.RemoteEndPoint.ToString());
                    clientList.Add(client);

                    

                    Thread receive = new Thread(Receive);
                    receive.IsBackground = true;
                    receive.Start(client);
                }
            });

            listen.IsBackground = true;
            listen.Start();

            Console.ReadLine();
        }

        //Hàm này để test thử kết nối giữa server, firebase và gửi dữ liệu về client
        async void LuuDuLieuDangChuoiXuongDatabaseVaGuiDi(Socket client)
        {
            string message = "Hello from C# server! Lan 246";
            var postData = new Dictionary<string, string>
            {
                { "Question",message},
            };



            FirebaseResponse response = await firebaseClient.UpdateAsync($"test", postData);
            // Lấy toàn bộ node test
            var res = firebaseClient.Get("test");

            // Gửi về client
            SendToClient(client, res.Body);
        }
        void SendToClient(Socket client, string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            client.Send(data);
        }

        //Hàm Receive() là hàm nhận dữ liệu từ mọi client
        void Receive(object obj)
        {
            Socket client = (Socket)obj;
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024 * 5000];
                    int bytesReceived = client.Receive(data);
                    if (bytesReceived <= 0)
                        break;
                    string message = Encoding.UTF8.GetString(data, 0, bytesReceived);
                    Console.WriteLine("Received from " + client.RemoteEndPoint.ToString() + ": " + message);

                    // Server lưu message vào Firebase (không đè)
                    //Dòng này nghĩa là mọi gói tin từ client gửi đến đều được ghi vào node test
                    //firebaseClient.Push("test", new { message = message });

                    // ============================
                    // PHÂN LOẠI GÓI TIN
                    // ============================
                    // 1) Gói ĐĂNG KÝ
                    if (message.StartsWith("REGISTER|"))
                    {
                        XuLyDangKy(client, message);
                        continue;
                    }

                    // 2) Gói ĐĂNG NHẬP
                    if (message.StartsWith("LOGIN|"))
                    {
                        XuLyDangNhap(client, message);
                        continue;
                    }
                   
                     //3) FIND_MATCH
                    if (message.StartsWith("FIND_MATCH|"))
                    {
                        XuLyTimDoiThu(client, message);
                        continue;
                    }
                    // 4) CANCEL_FIND_MATCH
                    if (message.StartsWith("CANCEL_FIND_MATCH|"))
                    {
                        XuLyHuyTimDoiThu(client, message);
                        continue;
                    }

                    // Các message khác (chat, đánh cờ...) => broadcast
                    // Gửi lại tin nhắn cho tất cả các client khác
                    foreach (var c in clientList)
                    {
                        if (c != client)
                        {
                            byte[] sendData = Encoding.UTF8.GetBytes(message);
                            c.Send(sendData);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Client disconnected: " + client.RemoteEndPoint.ToString());
                    clientList.Remove(client);
                    client.Close();
                    break;
                }
            }
        }
        void XuLyHuyTimDoiThu(Socket client, string message)
        {
            try
            {
                string[] tach = message.Split('|');
                if (tach.Length < 2) return;

                string userId = tach[1];

                lock (matchLock)
                {
                    // Xóa tất cả userId trùng trong waitingList
                    waitingList.RemoveAll(x => x.userId == userId);
                }

                Console.WriteLine($"{userId} đã hủy tìm đối thủ.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR CANCEL_FIND_MATCH: " + ex.Message);
            }
        }

        void XuLyTimDoiThu(Socket client, string message)
        {
            try
            {
                string[] tach = message.Split('|');
                if (tach.Length < 2)
                {
                    Console.WriteLine("FIND_MATCH lỗi định dạng: " + message);
                    return;
                }

                string userId = tach[1];

                lock (matchLock)
                {
                    // Nếu không ai đang đợi
                    if (waitingList.Count == 0)
                    {
                        waitingList.Add((client, userId));

                        try
                        {
                            client.Send(Encoding.UTF8.GetBytes("WAITING"));
                        }
                        catch { }

                        Console.WriteLine($"{userId} đã vào hàng chờ");
                        return;
                    }

                    // Có người đợi → lấy ra và ghép
                    var doiThu = waitingList[0];
                    waitingList.RemoveAt(0);

                    // Gửi kết quả ghép cho client 1 (người vừa bấm)
                    try
                    {
                        client.Send(
                            Encoding.UTF8.GetBytes($"FOUND_MATCH|{doiThu.userId}")
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi gửi cho client1: " + ex.Message);
                    }

                    // Gửi kết quả ghép cho client 2 (người đã đợi trước)
                    try
                    {
                        doiThu.socket.Send(
                            Encoding.UTF8.GetBytes($"FOUND_MATCH|{userId}")
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi gửi cho client2: " + ex.Message);
                    }

                    Console.WriteLine($"Ghép thành công: {userId} <-> {doiThu.userId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR FIND_MATCH: " + ex.ToString());
            }
        }



        void Send(Socket client, string message)
        {
            if (client == null) return;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                client.Send(data);
            }
            catch { }
        }
        void Send(string userId, string message)
        {
            if (clientMap.ContainsKey(userId))
            {
                Send(clientMap[userId], message);
            }
        }

        void XuLyDangKy(Socket client, string message)
        {
            // Bỏ split cũ
            string json = message.Substring(9); // Bỏ "REGISTER|"

            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<GoiTinDangKy>(json);

                if (data == null)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|JSON không hợp lệ"));
                    return;
                }

                // Tạo ID user
                string idUser = TaoIDNgauNhien(5);

                // Lưu Firebase
                firebaseClient.Set($"Users/IDUser_{idUser}", new
                {
                    IDUser = idUser,
                    TenTaiKhoan = data.TenTaiKhoan,
                    MatKhau = data.MatKhau,
                    HoVaTen = data.HoVaTen,
                    Gmail = data.Gmail
                });



                Console.WriteLine($"Đã tạo user mới: ID={idUser}, TK={data.TenTaiKhoan}");

                client.Send(Encoding.UTF8.GetBytes("REGISTER_OK|" + idUser));
            }
            catch
            {
                client.Send(Encoding.UTF8.GetBytes("ERROR|Không parse được JSON"));
            }
        }

        string TaoIDNgauNhien(int length)
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";

            Random rnd = new Random();
            return new string(Enumerable.Repeat(chars, length)
                             .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        void XuLyDangNhap(Socket client, string message)
        {
            // LOGIN|username|password
            string[] tach = message.Split('|');

            if (tach.Length != 3)
            {
                client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng gói tin LOGIN"));
                return;
            }

            string username = tach[1];
            string password = tach[2];

            // 1. Lấy toàn bộ danh sách user trong Firebase
            var ketQua = firebaseClient.Get("Users");

            if (ketQua.Body == "null")
            {
                client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|KHONG_CO_USER_NAO"));
                return;
            }

            // 2. Convert Firebase thành dictionary
            var allUsers = ketQua.ResultAs<Dictionary<string, GoiTinDangKy>>();

            bool timThayTaiKhoan = false;

            foreach (var user in allUsers)
            {
                var info = user.Value;

                if (info.TenTaiKhoan == username)
                {
                    timThayTaiKhoan = true;

                    // 3. Kiểm tra mật khẩu
                    if (info.MatKhau != password)
                    {
                        client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|SAI_MAT_KHAU"));
                        return;
                    }

                    // 4. Đăng nhập thành công → trả về IDUser
                    client.Send(Encoding.UTF8.GetBytes("LOGIN_OK|" + user.Key));
                    clientMap[user.Key] = client;   // user.Key là IDUser_xxx
                    return;
                }
            }

            if (!timThayTaiKhoan)
            {
                client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|TAI_KHOAN_KHONG_TON_TAI"));
            }
        }

    }
}
