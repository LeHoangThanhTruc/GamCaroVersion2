using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class FormAutoLogin : Form
    {
        private SessionData session;
        private System.Windows.Forms.Timer timeoutTimer;
        public FormAutoLogin(SessionData sessionData)
        {
            InitializeComponent();
            this.session = sessionData;

            timeoutTimer = new System.Windows.Forms.Timer();
            timeoutTimer.Interval = 10000;
            timeoutTimer.Tick += TimeoutTimer_Tick;
            timeoutTimer.Start();
        }

        private void FormAutoLogin_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
            NetworkClient.OnMessageReceived += XuLyPhanHoi;
        }

        private void XuLyPhanHoi(string msg)
        {
            if (msg.StartsWith("SESSION_VALID|"))
            {
                // Session hợp lệ -> Vào thẳng giao diện chính
                string userId = msg.Split('|')[1];

                this.Invoke(new Action(() =>
                {
                    timeoutTimer.Stop();
                    NetworkClient.OnMessageReceived -= XuLyPhanHoi;

                    GiaoDienChung mainForm = new GiaoDienChung(userId);
                    mainForm.Show();
                    this.Hide();
                }));
            }
            else if (msg.StartsWith("SESSION_INVALID"))
            {
                // Session không hợp lệ -> Xóa và quay lại đăng nhập
                this.Invoke(new Action(() =>
                {
                    timeoutTimer.Stop();
                    NetworkClient.OnMessageReceived -= XuLyPhanHoi;

                    SessionManager.Instance.ClearSession();
                    MessageBox.Show("Phiên đăng nhập đã hết hạn.\nVui lòng đăng nhập lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DangNhap loginForm = new DangNhap();
                    loginForm.Show();
                    this.Close();
                }));
            }
        }

        private void TimeoutTimer_Tick(object sender, EventArgs e)
        {
            timeoutTimer.Stop();
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;

            MessageBox.Show("Không thể kết nối đến server.\nVui lòng thử lại.",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DangNhap loginForm = new DangNhap();
            loginForm.Show();
            this.Close();
        }

        private void FormAutoLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            timeoutTimer?.Stop();
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
        }
    }
}
