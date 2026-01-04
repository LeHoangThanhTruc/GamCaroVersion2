using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayChu
{
    public partial class FormServerInfo : Form
    {
        private string serverIP;
        public FormServerInfo()
        {
            InitializeComponent();
            serverIP = GetLocalIP();
        }

        private void FormServerInfo_Load(object sender, EventArgs e)
        {
            DisplayServerInfo();
        }

        private void DisplayServerInfo()
        {
            lblServerIP.Text = serverIP;
            lblPortInfo.Text = "9998";

            txtFullAddress.Text = $"{serverIP}:9998";

            // Hiển thị hướng dẫn
            lblInstructions.Text =
                " HƯỚNG DẪN KẾT NỐI:\n\n" +
                "1️ Máy Client mở Game Caro\n" +
                "2️ Click nút 'Cấu hình Server'\n" +
                $"3️ Nhập IP: {serverIP}\n" +
                "4️ Click 'Lưu' và khởi động lại\n" +
                "5️ Đăng ký/Đăng nhập bình thường\n\n" +
                " Server đang chạy và sẵn sàng!";
        }

        private void btnCopyIP_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(serverIP);
                MessageBox.Show(
                    " Đã copy IP vào clipboard!\n\n" +
                    "Gửi IP này cho người chơi khác để họ kết nối.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi copy: " + ex.Message);
            }
        }

        private void btnCopyFull_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtFullAddress.Text);
                MessageBox.Show(
                    " Đã copy địa chỉ đầy đủ vào clipboard!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi copy: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            serverIP = GetLocalIP();
            DisplayServerInfo();
            MessageBox.Show(
                " Đã làm mới thông tin mạng!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnShowAllIPs_Click(object sender, EventArgs e)
        {
            string info = GetAllNetworkInfo();
            MessageBox.Show(
                info,
                "Tất cả địa chỉ IP",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private string GetLocalIP()
        {
            try
            {
                var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);

                foreach (var ni in interfaces)
                {
                    var properties = ni.GetIPProperties();
                    var ipv4 = properties.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        .Where(ua => !System.Net.IPAddress.IsLoopback(ua.Address))
                        .Select(ua => ua.Address)
                        .FirstOrDefault();

                    if (ipv4 != null)
                    {
                        return ipv4.ToString();
                    }
                }

                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        private string GetAllNetworkInfo()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("📡 TẤT CẢ CÁC ĐỊA CHỈ IP:\n");

            try
            {
                var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);

                foreach (var ni in interfaces)
                {
                    sb.AppendLine($"🔌 {ni.Name} ({ni.NetworkInterfaceType})");

                    var properties = ni.GetIPProperties();
                    var ips = properties.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        .Select(ua => ua.Address.ToString());

                    foreach (var ip in ips)
                    {
                        sb.AppendLine($"   • {ip}");
                    }
                    sb.AppendLine();
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"❌ Lỗi: {ex.Message}");
            }

            return sb.ToString();
        }
    }
}
