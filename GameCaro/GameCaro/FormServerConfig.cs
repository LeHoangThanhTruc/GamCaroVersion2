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
    public partial class FormServerConfig : Form
    {
        public FormServerConfig()
        {
            InitializeComponent();
        }

        private void FormServerConfig_Load(object sender, EventArgs e)
        {
            // Hiển thị IP hiện tại
            txtServerIP.Text = ServerConfig.Instance.GetServerIP();

            // Hiển thị hướng dẫn
            lblHuongDan.Text =
                " Hướng dẫn:\n\n" +
                "• Localhost (127.0.0.1): Chơi trên cùng 1 máy\n" +
                "• LAN: Nhập IP máy chủ trong mạng\n" +
                "  Ví dụ: 192.168.1.100\n\n" +
                " Cách tìm IP Server:\n" +
                "• Mở CMD trên máy Server\n" +
                "• Gõ lệnh: ipconfig\n" +
                "• Tìm IPv4 Address";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string ip = txtServerIP.Text.Trim();

            // Validate IP
            if (!ServerConfig.IsValidIP(ip))
            {
                MessageBox.Show(
                    "Địa chỉ IP không hợp lệ!\n\n" +
                    "Định dạng đúng: X.X.X.X\n" +
                    "Ví dụ: 192.168.1.100",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Lưu IP
            ServerConfig.Instance.SaveServerIP(ip);

            MessageBox.Show(
                $"✅ Đã lưu Server IP: {ip}\n\n" +
                "Ứng dụng sẽ kết nối đến Server này khi khởi động.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLocalHost_Click(object sender, EventArgs e)
        {
            txtServerIP.Text = "127.0.0.1";
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            string ip = txtServerIP.Text.Trim();

            if (!ServerConfig.IsValidIP(ip))
            {
                MessageBox.Show(
                    "IP không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Thử kết nối
            Cursor = Cursors.WaitCursor;
            bool success = TestConnection(ip);
            Cursor = Cursors.Default;

            if (success)
            {
                MessageBox.Show(
                    " Kết nối thành công!\n\n" +
                    $"Server {ip}:9998 đang hoạt động.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    " Không thể kết nối!\n\n" +
                    "Vui lòng kiểm tra:\n" +
                    "• Server đã chạy chưa?\n" +
                    "• IP có đúng không?\n" +
                    "• Firewall đã mở port 9998?",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool TestConnection(string ip)
        {
            try
            {
                using (var testClient = new System.Net.Sockets.TcpClient())
                {
                    testClient.Connect(ip, 9998);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
