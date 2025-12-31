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
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
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

            // 1) Gói ĐĂNG KÝ
            if (message.StartsWith("REGISTER|"))
            {
                XuLyDangKy(client, message);
                continue;
            }

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

                    // 5) VERIFY_OTP
                    if (message.StartsWith("VERIFY_OTP|"))
                    {
                        XuLyXacThucOTP(client, message);
                        continue;
                    }

                    // 6) FORGOT_PASSWORD
                    if (message.StartsWith("FORGOT_PASSWORD|"))
                    {
                        XuLyQuenMatKhau(client, message);
                        continue;
                    }

                    // 7) VERIFY_RESET_OTP
                    if (message.StartsWith("VERIFY_RESET_OTP|"))
                    {
                        XuLyXacThucResetOTP(client, message);
                        continue;
                    }

                    // 8) RESET_PASSWORD
                    if (message.StartsWith("RESET_PASSWORD|"))
                    {
                        XuLyDatLaiMatKhau(client, message);
                        continue;
                    }

                    // 9) RESEND OTP
                    if (message.StartsWith("RESEND_OTP|"))
                    {
                        XuLyGuiLaiOTP(client, message);
                        continue;
                    }

                    // 10) VERIFY_SESSION
                    if (message.StartsWith("VERIFY_SESSION|"))
                    {
                        XuLyXacThucSession(client, message);
                        continue;
                    }

                    // 11) LOGOUT
                    if (message.StartsWith("LOGOUT|"))
                    {
                        XuLyDangXuat(client, message);
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

        // CAC HAM XU LY QUEN MK
        void XuLyQuenMatKhau(Socket client, string message)
        {
            try
            {
                // Format: FORGOT_PASSWORD|TenTaiKhoan
                string[] parts = message.Split('|');
                if (parts.Length != 2)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                    return;
                }

                string tenTaiKhoan = parts[1];

                // Tìm user trong Firebase
                var ketQua = firebaseClient.Get("Users");

                if (ketQua.Body == "null")
                {
                    client.Send(Encoding.UTF8.GetBytes("FORGOT_PASSWORD_FAIL|TAI_KHOAN_KHONG_TON_TAI"));
                    return;
                }

                var allUsers = ketQua.ResultAs<Dictionary<string, GoiTinDangKy>>();

                foreach (var user in allUsers)
                {
                    var info = user.Value;

                    if (info.TenTaiKhoan.ToLower() == tenTaiKhoan.ToLower())
                    {
                        string idUser = user.Key;
                        string email = info.Gmail;

                        // Tạo mã OTP reset password
                        string maOTP = new Random().Next(100000, 999999).ToString();

                        // Lưu mã OTP vào Firebase
                        firebaseClient.Set($"PasswordResetCodes/{idUser}", new
                        {
                            Code = maOTP,
                            Email = email,
                            TenTaiKhoan = tenTaiKhoan,
                            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            ExpiryMinutes = 10
                        });

                        // Gửi email chứa mã OTP
                        GuiEmailResetPassword(email, maOTP, tenTaiKhoan);

                        // Phản hồi client
                        client.Send(Encoding.UTF8.GetBytes($"FORGOT_PASSWORD_OK|{idUser}"));
                        Console.WriteLine($"📧 Đã gửi mã reset password đến {email} cho tài khoản {tenTaiKhoan}");
                        return;
                    }
                }

                // Không tìm thấy tài khoản
                client.Send(Encoding.UTF8.GetBytes("FORGOT_PASSWORD_FAIL|TAI_KHOAN_KHONG_TON_TAI"));
                Console.WriteLine($"❌ Không tìm thấy tài khoản: {tenTaiKhoan}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR FORGOT_PASSWORD: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("ERROR|Lỗi xử lý"));
            }
        }

        void XuLyXacThucResetOTP(Socket client, string message)
        {
            try
            {
                // Format: VERIFY_RESET_OTP|IDUser|123456
                string[] parts = message.Split('|');
                if (parts.Length != 3)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                    return;
                }

                string idUser = parts[1];
                string maOTP = parts[2];

                // Lấy mã OTP từ Firebase
                var result = firebaseClient.Get($"PasswordResetCodes/{idUser}");

                if (result.Body == "null")
                {
                    client.Send(Encoding.UTF8.GetBytes("VERIFY_RESET_FAIL|MA_KHONG_TON_TAI"));
                    Console.WriteLine($"❌ Không tìm thấy mã reset cho {idUser}");
                    return;
                }

                var resetData = result.ResultAs<Dictionary<string, string>>();
                string maDung = resetData["Code"];
                DateTime createdAt = DateTime.Parse(resetData["CreatedAt"]);
                int expiryMinutes = int.Parse(resetData["ExpiryMinutes"]);

                // Kiểm tra hết hạn
                if (DateTime.Now > createdAt.AddMinutes(expiryMinutes))
                {
                    client.Send(Encoding.UTF8.GetBytes("VERIFY_RESET_FAIL|MA_HET_HAN"));
                    firebaseClient.Delete($"PasswordResetCodes/{idUser}");
                    Console.WriteLine($"❌ Mã reset của {idUser} đã hết hạn.");
                    return;
                }

                // Kiểm tra mã OTP
                if (maOTP != maDung)
                {
                    client.Send(Encoding.UTF8.GetBytes("VERIFY_RESET_FAIL|MA_SAI"));
                    Console.WriteLine($"❌ Mã OTP sai cho {idUser}");
                    return;
                }

                // Xác thực thành công
                client.Send(Encoding.UTF8.GetBytes("VERIFY_RESET_OK"));
                Console.WriteLine($"✅ Xác thực mã reset thành công cho {idUser}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR VERIFY_RESET_OTP: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("ERROR|Lỗi xử lý"));
            }
        }

        void XuLyDatLaiMatKhau(Socket client, string message)
        {
            try
            {
                // Format: RESET_PASSWORD|IDUser|MatKhauMoi
                string[] parts = message.Split('|');
                if (parts.Length != 3)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                    return;
                }

                string idUser = parts[1];
                string matKhauMoi = parts[2];

                // Kiểm tra mã reset còn hợp lệ không
                var result = firebaseClient.Get($"PasswordResetCodes/{idUser}");

                if (result.Body == "null")
                {
                    client.Send(Encoding.UTF8.GetBytes("RESET_PASSWORD_FAIL"));
                    return;
                }

                // Cập nhật mật khẩu mới trong Firebase
                //firebaseClient.Update($"Users/{idUser}", new { MatKhau = matKhauMoi });
                // MÃ HÓA MẬT KHẨU TRƯỚC KHI LƯU
                Console.WriteLine($"[RESET] Đang mã hóa mật khẩu mới cho {idUser}...");
                string matKhauDaMaHoa = PasswordHasher.HashPassword(matKhauMoi);
                Console.WriteLine($"[RESET] Đã mã hóa: {matKhauDaMaHoa.Substring(0, 20)}...");

                firebaseClient.Update($"Users/{idUser}", new { MatKhau = matKhauDaMaHoa });

                // Xóa mã reset đã sử dụng
                firebaseClient.Delete($"PasswordResetCodes/{idUser}");

                client.Send(Encoding.UTF8.GetBytes("RESET_PASSWORD_OK"));
                Console.WriteLine($"✅ Đã reset mật khẩu thành công cho {idUser}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR RESET_PASSWORD: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("RESET_PASSWORD_FAIL"));
            }
        }

        private void GuiEmailResetPassword(string emailNguoiNhan, string maOTP, string tenTaiKhoan)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Game Caro", "trinhvht8@gmail.com"));
                message.To.Add(new MailboxAddress("", emailNguoiNhan));
                message.Subject = "Đặt lại mật khẩu - Game Caro";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f5f5f5;'>
                    <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px;'>
                        <h2 style='color: #2196F3;'>🔐 Đặt lại mật khẩu</h2>
                        <p>Xin chào <strong>{tenTaiKhoan}</strong>,</p>
                        <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản Game Caro.</p>
                        <div style='background-color: #e3f2fd; padding: 20px; border-radius: 5px; text-align: center; margin: 20px 0;'>
                            <p style='margin: 0; font-size: 14px; color: #666;'>Mã xác thực của bạn là:</p>
                            <h1 style='margin: 10px 0; color: #2196F3; font-size: 36px; letter-spacing: 5px;'>{maOTP}</h1>
                        </div>
                        <p style='color: #666; font-size: 14px;'>
                            ⏰ Mã có hiệu lực trong <strong>10 phút</strong>.<br>
                            ⚠️ Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.
                        </p>
                        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='color: #999; font-size: 12px; text-align: center;'>
                            © 2024 Game Caro. All rights reserved.
                        </p>
                    </div>
                </div>
            "
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("trinhvht8@gmail.com", "vqms tlae xgep ksgx");
                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine($"📧 Đã gửi email reset password đến {emailNguoiNhan}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi gửi email reset: " + ex.Message);
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
            //// Bỏ split cũ
            //string json = message.Substring(9); // Bỏ "REGISTER|"

            //try
            //{
            //    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<GoiTinDangKy>(json);

            //    if (data == null)
            //    {
            //        client.Send(Encoding.UTF8.GetBytes("ERROR|JSON không hợp lệ"));
            //        return;
            //    }

            //    // Tạo ID user
            //    string idUser = TaoIDNgauNhien(5);

            //    // Lưu Firebase
            //    firebaseClient.Set($"Users/IDUser_{idUser}", new
            //    {
            //        IDUser = idUser,
            //        TenTaiKhoan = data.TenTaiKhoan,
            //        MatKhau = data.MatKhau,
            //        HoVaTen = data.HoVaTen,
            //        Gmail = data.Gmail
            //    });



            //    Console.WriteLine($"Đã tạo user mới: ID={idUser}, TK={data.TenTaiKhoan}");

            //    client.Send(Encoding.UTF8.GetBytes("REGISTER_OK|" + idUser));
            //}
            //catch
            //{
            //    client.Send(Encoding.UTF8.GetBytes("ERROR|Không parse được JSON"));
            //}

            // Cắt bỏ phần "REGISTER|" để lấy JSON data
            string json = message.Substring(9); // "REGISTER|" dài 9 ký tự

            // Deserialize gói tin
            GoiTinDangKy data;
            try
            {
                // Sử dụng Newtonsoft.Json vì FireSharp dùng nó, đảm bảo tương thích
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<GoiTinDangKy>(json);
            }
            catch
            {
                Console.WriteLine("Lỗi deserialize gói tin đăng ký.");
                return;
            }

            // --- Bắt đầu Logic Kiểm tra Tồn tại (Yêu cầu 4) ---
            // 1. Lấy toàn bộ danh sách user từ Firebase
            var ketQua = firebaseClient.Get("Users");

            if (ketQua.Body != "null")
            {
                var allUsers = ketQua.ResultAs<Dictionary<string, GoiTinDangKy>>();

                // 2. Kiểm tra xem TenTaiKhoan đã tồn tại chưa
                foreach (var user in allUsers)
                {
                    var userInfo = user.Value;
                    if (userInfo.TenTaiKhoan.ToLower() == data.TenTaiKhoan.ToLower())
                    {
                        // Tên tài khoản đã tồn tại
                        client.Send(Encoding.UTF8.GetBytes("REGISTER_FAIL|TAI_KHOAN_DA_TON_TAI"));
                        Console.WriteLine($"Đăng ký thất bại: Tài khoản '{data.TenTaiKhoan}' đã tồn tại.");
                        return;
                    }
                }
            }
            // --- Kết thúc Logic Kiểm tra Tồn tại ---

            // 3. Nếu chưa tồn tại, thực hiện đăng ký

            // Tạo IDUser ngẫu nhiên
            string newIDUser = TaoIDNgauNhien(); // Giả sử bạn có hàm này, nếu chưa có thì cần thêm vào

            // Bổ sung IDUser vào gói tin (nếu cần thiết cho hàm Put)
            //--------------------------------data.IDUser = newIDUser;
            data.IDUser = newIDUser;
            data.isVerified=false; // Chưa xác thực email

            // MA HOA MAT KHAU TRUOC KHI LUU
            Console.WriteLine($"[HASH] Mật khẩu gốc: {data.MatKhau}");
            data.MatKhau = PasswordHasher.HashPassword(data.MatKhau);
            Console.WriteLine($"[HASH] Đã mã hóa thành: {data.MatKhau.Substring(0, 20)}...");

            firebaseClient.Set("Users/" + newIDUser, data);

            // Lưu xuống Firebase (Put - thêm mới hoặc ghi đè)
            // Node: Users -> newIDUser -> {HoVaTen, TenTaiKhoan, Gmail, MatKhau}
            firebaseClient.Set("Users/" + newIDUser, data);

            // Gửi email xác thực
            GuiEmailXacThuc(data.Gmail, newIDUser);

            // Gửi phản hồi về client: Đăng ký thành công, chờ xác thực OTP
            client.Send(Encoding.UTF8.GetBytes($"REGISTER_PENDING|{newIDUser}"));
            Console.WriteLine($"📧 Đã tạo tài khoản {data.TenTaiKhoan}, đang chờ xác thực OTP.");

            //// 4. Gửi thông báo đăng ký thành công và IDUser về client
            //client.Send(Encoding.UTF8.GetBytes($"REGISTER_OK|{newIDUser}"));
            //Console.WriteLine($"Đăng ký thành công: IDUser = {newIDUser}, Tài khoản = {data.TenTaiKhoan}");
        }

        // Hàm giả định để tạo ID ngẫu nhiên, bạn cần thêm hàm này vào class Program
        private string TaoIDNgauNhien()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return "IDUser_" + result;
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

            string[] tach = message.Split('|');

            if (tach.Length != 3)
            {
                client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng gói tin LOGIN"));
                return;
            }

            string username = tach[1];
            string password = tach[2];

            var ketQua = firebaseClient.Get("Users");

            if (ketQua.Body == "null")
            {
                client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|KHONG_CO_USER_NAO"));
                return;
            }

            var allUsers = ketQua.ResultAs<Dictionary<string, GoiTinDangKy>>();

            bool timThayTaiKhoan = false;

            foreach (var user in allUsers)
            {
                var info = user.Value;

                if (info.TenTaiKhoan == username)
                {
                    timThayTaiKhoan = true;

                    // ✅ THÊM: Kiểm tra tài khoản đã xác thực chưa
                    if (!info.isVerified)
                    {
                        client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|CHUA_XAC_THUC_EMAIL"));
                        Console.WriteLine($"❌ Tài khoản {username} chưa xác thực email.");
                        return;
                    }

                    // Kiểm tra mật khẩu
                    //if (info.MatKhau != password)
                    //{
                    //    client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|SAI_MAT_KHAU"));
                    //    return;
                    //}
                    Console.WriteLine($"[LOGIN] Đang xác thực mật khẩu cho {username}...");

                    if (!PasswordHasher.VerifyPassword(password, info.MatKhau))
                    {
                        client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|SAI_MAT_KHAU"));
                        Console.WriteLine($"❌ Sai mật khẩu cho {username}");
                        return;
                    }

                    // Đăng nhập thành công - tạo session token
                    //client.Send(Encoding.UTF8.GetBytes("LOGIN_OK|" + user.Key));
                    //clientMap[user.Key] = client;
                    //Console.WriteLine($"✅ {username} đăng nhập thành công.");
                    //return;

                    string sessionToken = GenerateSessionToken();

                    // Lưu session vào Firebase
                    firebaseClient.Set($"Sessions/{user.Key}", new
                    {
                        Token = sessionToken,
                        LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Username = username
                    });

                    // Lưu vào active sessions
                    activeSessions[user.Key] = new SessionInfo
                    {
                        UserId = user.Key,
                        SessionToken = sessionToken,
                        LoginTime = DateTime.Now,
                        ClientSocket = client
                    };

                    // Gui kem session token ve client
                    client.Send(Encoding.UTF8.GetBytes($"LOGIN_OK|{user.Key}|{sessionToken}"));
                    clientMap[user.Key] = client;
                    Console.WriteLine($"✅ {username} đăng nhập thành công. Token: {sessionToken.Substring(0, 10)}...");
                    return;
                }
            }

            if (!timThayTaiKhoan)
            {
                client.Send(Encoding.UTF8.GetBytes("LOGIN_FAIL|TAI_KHOAN_KHONG_TON_TAI"));
            }
        }

        // Ham gửi email sử dụng MailKit
        private void GuiEmailXacThuc(string emailNguoiNhan, string idUser)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Game Caro", "trinhvht8@gmail.com"));
                message.To.Add(new MailboxAddress("", emailNguoiNhan));
                message.Subject = "Xác thực tài khoản Game Caro";

                // Tạo mã xác thực 6 số
                string maXacThuc = new Random().Next(100000, 999999).ToString();

                // Lưu mã xác thực vào Firebase tạm thời
                firebaseClient.Set($"VerificationCodes/{idUser}", new
                {
                    Code = maXacThuc,
                    Email = emailNguoiNhan,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ExpiryMinutes = 10
                });

                message.Body = new TextPart("html")
                {
                    Text = $@"
                <h2>Chào mừng đến Game Caro!</h2>
                <p>Mã xác thực của bạn là: <strong>{maXacThuc}</strong></p>
                <p>Mã có hiệu lực trong 10 phút.</p>
            "
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Dùng App Password của Gmail (không phải mật khẩu thường)
                    client.Authenticate("trinhvht8@gmail.com", "vqms tlae xgep ksgx");

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine($"Đã gửi email xác thực đến {emailNguoiNhan}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
            }
        }

        // Ham Xac thuc OTP (fixed field name to match Users data)
        void XuLyXacThucOTP(Socket client, string message)
        {
            // Format: VERIFY_OTP|IDUser|123456
            string[] parts = message.Split('|');
            if (parts.Length != 3)
            {
                client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                return;
            }

            string idUser = parts[1];
            string maOTP = parts[2];

            // Lấy mã OTP từ Firebase
            var result = firebaseClient.Get($"VerificationCodes/{idUser}");

            if (result.Body == "null")
            {
                client.Send(Encoding.UTF8.GetBytes("VERIFY_FAIL|MA_KHONG_TON_TAI"));
                Console.WriteLine($"❌ Không tìm thấy mã OTP cho {idUser}");
                return;
            }

            var verifyData = result.ResultAs<Dictionary<string, string>>();
            string maDung = verifyData["Code"];
            DateTime createdAt = DateTime.Parse(verifyData["CreatedAt"]);
            int expiryMinutes = int.Parse(verifyData["ExpiryMinutes"]);

            // Kiểm tra thời gian hết hạn
            if (DateTime.Now > createdAt.AddMinutes(expiryMinutes))
            {
                client.Send(Encoding.UTF8.GetBytes("VERIFY_FAIL|MA_HET_HAN"));
                firebaseClient.Delete($"VerificationCodes/{idUser}"); // Xóa mã hết hạn
                Console.WriteLine($"❌ Mã OTP của {idUser} đã hết hạn.");
                return;
            }

            // Kiểm tra mã OTP
            if (maOTP != maDung)
            {
                client.Send(Encoding.UTF8.GetBytes("VERIFY_FAIL|MA_SAI"));
                Console.WriteLine($"❌ Mã OTP sai cho {idUser}");
                return;
            }

            // ✅ Xác thực thành công
            firebaseClient.Update($"Users/{idUser}", new { isVerified = true }); // <- fixed property name
            firebaseClient.Delete($"VerificationCodes/{idUser}"); // Xóa mã đã dùng

            client.Send(Encoding.UTF8.GetBytes("VERIFY_OK"));
            Console.WriteLine($"✅ Tài khoản {idUser} đã được xác thực thành công!");
        }

        void XuLyGuiLaiOTP(Socket client, string message)
        {
            try
            {
                // Format: RESEND_OTP|IDUser
                string[] parts = message.Split('|');
                if (parts.Length != 2)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                    return;
                }

                string idUser = parts[1];

                // Kiểm tra user có tồn tại không
                var userResult = firebaseClient.Get($"Users/{idUser}");

                if (userResult.Body == "null")
                {
                    client.Send(Encoding.UTF8.GetBytes("RESEND_OTP_FAIL|USER_KHONG_TON_TAI"));
                    Console.WriteLine($"❌ Không tìm thấy user {idUser}");
                    return;
                }

                var userData = userResult.ResultAs<GoiTinDangKy>();
                string email = userData.Gmail;

                // Tạo mã OTP mới
                string maOTPMoi = new Random().Next(100000, 999999).ToString();

                // Cập nhật hoặc tạo mới mã xác thực trong Firebase
                firebaseClient.Set($"VerificationCodes/{idUser}", new
                {
                    Code = maOTPMoi,
                    Email = email,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ExpiryMinutes = 10
                });

                // Gửi email mới
                GuiEmailXacThuc(email, idUser, maOTPMoi);

                // Phản hồi client
                client.Send(Encoding.UTF8.GetBytes("RESEND_OTP_OK"));
                Console.WriteLine($"📧 Đã gửi lại mã OTP đến {email} cho user {idUser}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR RESEND_OTP: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("RESEND_OTP_FAIL|SERVER_ERROR"));
            }
        }

        // Cập nhật hàm GuiEmailXacThuc để nhận mã OTP từ tham số
        private void GuiEmailXacThuc(string emailNguoiNhan, string idUser, string maXacThuc = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Game Caro", "trinhvht8@gmail.com"));
                message.To.Add(new MailboxAddress("", emailNguoiNhan));
                message.Subject = "Xác thực tài khoản Game Caro";

                // Nếu không truyền mã, tạo mã mới
                if (string.IsNullOrEmpty(maXacThuc))
                {
                    maXacThuc = new Random().Next(100000, 999999).ToString();
                }

                // Lưu mã xác thực vào Firebase (chỉ khi tạo mã mới)
                if (string.IsNullOrEmpty(maXacThuc))
                {
                    firebaseClient.Set($"VerificationCodes/{idUser}", new
                    {
                        Code = maXacThuc,
                        Email = emailNguoiNhan,
                        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ExpiryMinutes = 10
                    });
                }

                message.Body = new TextPart("html")
                {
                    Text = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f5f5f5;'>
                    <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px;'>
                        <h2 style='color: #4CAF50;'>🎮 Chào mừng đến Game Caro!</h2>
                        <p>Mã xác thực của bạn là:</p>
                        <div style='background-color: #e8f5e9; padding: 20px; border-radius: 5px; text-align: center; margin: 20px 0;'>
                            <h1 style='margin: 0; color: #4CAF50; font-size: 36px; letter-spacing: 5px;'>{maXacThuc}</h1>
                        </div>
                        <p style='color: #666; font-size: 14px;'>
                            ⏰ Mã có hiệu lực trong <strong>10 phút</strong>.<br>
                            ⚠️ Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email.
                        </p>
                        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='color: #999; font-size: 12px; text-align: center;'>
                            © 2024 Game Caro. All rights reserved.
                        </p>
                    </div>
                </div>
            "
                };

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("trinhvht8@gmail.com", "vqms tlae xgep ksgx");
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                }

                Console.WriteLine($"📧 Đã gửi email xác thực đến {emailNguoiNhan}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi gửi email: " + ex.Message);
            }
        }

        // QUAN LY SESSION DANG HOAT DONG CUA CAC USER TREN SERVER
        Dictionary<string, SessionInfo> activeSessions = new Dictionary<string, SessionInfo>();

        class SessionInfo
        {
            public string UserId { get; set; }
            public string SessionToken { get; set; }
            public DateTime LoginTime { get; set; }
            public Socket ClientSocket { get; set; }
        }

        void XuLyXacThucSession(Socket client, string message)
        {
            try
            {
                // Format: VERIFY_SESSION|IDUser|SessionToken
                string[] parts = message.Split('|');
                if (parts.Length != 3)
                {
                    client.Send(Encoding.UTF8.GetBytes("SESSION_INVALID|FORMAT_ERROR"));
                    return;
                }

                string userId = parts[1];
                string sessionToken = parts[2];

                // Kiểm tra user có tồn tại không
                var userResult = firebaseClient.Get($"Users/{userId}");
                if (userResult.Body == "null")
                {
                    client.Send(Encoding.UTF8.GetBytes("SESSION_INVALID|USER_NOT_FOUND"));
                    Console.WriteLine($"❌ User {userId} không tồn tại");
                    return;
                }

                // Kiểm tra session trong Firebase (tùy chọn - nếu muốn lưu persistent)
                var sessionResult = firebaseClient.Get($"Sessions/{userId}");

                if (sessionResult.Body != "null")
                {
                    var sessionData = sessionResult.ResultAs<Dictionary<string, string>>();
                    string savedToken = sessionData.ContainsKey("Token") ? sessionData["Token"] : "";
                    DateTime loginTime = sessionData.ContainsKey("LoginTime")
                        ? DateTime.Parse(sessionData["LoginTime"])
                        : DateTime.MinValue;

                    // Kiểm tra session còn hiệu lực (7 ngày)
                    TimeSpan duration = DateTime.Now - loginTime;
                    if (duration.TotalDays > 7)
                    {
                        // Session hết hạn
                        firebaseClient.Delete($"Sessions/{userId}");
                        client.Send(Encoding.UTF8.GetBytes("SESSION_INVALID|EXPIRED"));
                        Console.WriteLine($"❌ Session của {userId} đã hết hạn");
                        return;
                    }

                    // Kiểm tra token có khớp không
                    if (savedToken == sessionToken)
                    {
                        // ✅ Session hợp lệ
                        client.Send(Encoding.UTF8.GetBytes($"SESSION_VALID|{userId}"));
                        clientMap[userId] = client;

                        // Lưu vào active sessions
                        activeSessions[userId] = new SessionInfo
                        {
                            UserId = userId,
                            SessionToken = sessionToken,
                            LoginTime = loginTime,
                            ClientSocket = client
                        };

                        Console.WriteLine($"✅ Session hợp lệ cho {userId}");
                        return;
                    }
                }

                // Session không hợp lệ
                client.Send(Encoding.UTF8.GetBytes("SESSION_INVALID|TOKEN_MISMATCH"));
                Console.WriteLine($"❌ Session không hợp lệ cho {userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR VERIFY_SESSION: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("SESSION_INVALID|SERVER_ERROR"));
            }
        }

        void XuLyDangXuat(Socket client, string message)
        {
            try
            {
                // Format: LOGOUT|IDUser
                string[] parts = message.Split('|');
                if (parts.Length != 2)
                {
                    client.Send(Encoding.UTF8.GetBytes("ERROR|Sai định dạng"));
                    return;
                }

                string userId = parts[1];

                // Xóa session trong Firebase
                firebaseClient.Delete($"Sessions/{userId}");

                // Xóa khỏi active sessions
                if (activeSessions.ContainsKey(userId))
                {
                    activeSessions.Remove(userId);
                }

                // Xóa khỏi clientMap
                if (clientMap.ContainsKey(userId))
                {
                    clientMap.Remove(userId);
                }

                // Xóa khỏi waiting list nếu đang tìm đối thủ
                lock (matchLock)
                {
                    waitingList.RemoveAll(x => x.userId == userId);
                }

                client.Send(Encoding.UTF8.GetBytes("LOGOUT_OK"));
                Console.WriteLine($"✅ {userId} đã đăng xuất");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR LOGOUT: " + ex.Message);
                client.Send(Encoding.UTF8.GetBytes("ERROR|Lỗi đăng xuất"));
            }
        }

        private string GenerateSessionToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
