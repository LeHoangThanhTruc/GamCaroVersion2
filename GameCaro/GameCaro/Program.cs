using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Chỉ connect 1 lần
            NetworkClient.Instance.Connect();
            //Application.Run(new DangNhap());

            // Kiểm tra session đã lưu
            SessionData savedSession = SessionManager.Instance.LoadSession();

            if (savedSession != null)
            {
                // Có session -> Xác thực với server
                MessageBox.Show($"Chào mừng trở lại, {savedSession.Username}!", "Auto Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Gửi yêu cầu xác thực session lên server
                NetworkClient.Instance.Send($"VERIFY_SESSION|{savedSession.UserId}|{savedSession.SessionToken}");

                // Chờ phản hồi từ server trong form loading tạm
                Application.Run(new FormAutoLogin(savedSession));
            }
            else
            {
                // Không có session -> Hiện form đăng nhập bình thường
                Application.Run(new DangNhap());
            }
        }
    }
}
