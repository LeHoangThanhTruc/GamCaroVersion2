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
    public partial class GiaoDienChung : Form
    {
        private string userId;
        public GiaoDienChung(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void btnPhongCho_Click(object sender, EventArgs e)
        {
            this.Hide();
            PhongCho f = new PhongCho(userId);
            f.Show();
        }
        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //CaiDat f = new CaiDat(userId);
            //f.Show();

            this.Hide();
            CaiDat f = new CaiDat(userId);
            f.ShowDialog(); 

            // Kiểm tra xem form này còn tồn tại không
            if (!this.IsDisposed && !this.Disposing)
            {
                this.Show();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoSoCaNhan f = new HoSoCaNhan(userId);
            f.Show();
        }


        private void GiaoDienChung_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLyGiaoDienChung;
            // Hiển thị thông tin người dùng
            SessionData session = SessionManager.Instance.GetCurrentSession();
            if (session != null)
            {
                // Giả sử bạn có label để hiển thị tên
                // lblTenNguoiDung.Text = $"Xin chào, {session.Username}";
            }
            MusicManager.UpdateState();
        }

        //Hàm ClientXuLySettings sẽ được TỰ ĐỘNG THỰC THI khi client NHẬN ĐƯỢC TIN NHẮN TỪ SERVER
        private void ClientXuLyGiaoDienChung(string msg) 
        {
            //Để sẵn hàm này, khi có yêu cầu xử lý giao diện chung thì sẽ bổ sung sau
            if (msg.StartsWith("LOGOUT_OK"))
            {
                this.Invoke(new Action(() =>
                {
                    Console.WriteLine("✅ Server xác nhận đăng xuất");
                }));
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Gửi thông báo logout lên server
                NetworkClient.Instance.Send($"LOGOUT|{userId}");

                // Xóa session local
                SessionManager.Instance.ClearSession();

                // Quay lại form đăng nhập
                this.Hide();
                DangNhap loginForm = new DangNhap();
                loginForm.Show();

                MessageBox.Show("Đã đăng xuất thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GiaoDienChung_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đóng form mà không đăng xuất, session vẫn được giữ
            NetworkClient.OnMessageReceived -= ClientXuLyGiaoDienChung;
        }

        private void btnThongTinNPH_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://www.facebook.com/groups/1436646758046138";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trình duyệt: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
