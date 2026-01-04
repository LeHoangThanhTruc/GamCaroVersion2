/*
 * Trong form DangNhap mình muốn sau khi nhập xong txtTenTaiKhoan và txtMatKhau thì khi bấm nút
 * btnDangNhap thì sẽ kiểm tra xem có ô textbox nào trống không, chỉ cần 1 ô trống thôi thì sẽ 
 * xem như không hợp lệ và hiện lên thông báo cho người nhập, tiếp theo nếu cả 2 ô đều được 
 * điền đầy đủ nội dung thì sẽ sẽ lấy cả thông tin trong 2 ô txtTenTaiKhoan và txtMatKhau 
 * truyền xuống server. Còn trong server, sẽ nhận gói tin mà client gửi xuống sau đó so sánh 
 * với thông tin có trong firebase. Thật ra cách so sánh thế nào cho tối ưu thì mình không rõ 
 * nhưng mà theo cách nghĩ của mình thì ta sẽ lấy thông tin tất TenTaiKhoan va MatKhau hiện có
 * trong Users ra và so sánh với thông tin mà server nhận được, nếu cả hai tên tài khoản và mật
 * khẩu đều đầu giống TenTaiKhoan va MatKhau thì server sẽ ngừng so sánh và gửi thông báo đăng
 * nhập thành công lên form DangNhap và sau khi nhận được thông báo đăng nhập thành công thì 
 * form DangNhap sẽ bị ẩn và tự động chuyển qua form tiếp theo là form GiaoDienChung, còn nếu 
 * server gửi thông báo tên tài khoản sai hoặc mật khẩu sai lên form DangNhap của client thì 
 * client sẽ không được phép chuyển sang form tiếp theo, còn nếu server gửi thông báo tài 
 * khoản không tồn tại lên form DangNhap của client thì nghĩa là không có bất kỳ TenTaiKhoan
 * và MatKhau nào trong firebase trùng với tên tài khoản và mật khẩu mà client gửi xuống, lúc 
 * này form DangNhap của client cũng không được phép chuyển form.
 * 
 * Cập nhật lại : hiện tại để tối ưu hiệu suất khi so sánh trong firebase nên sẽ thay đổi cấu trúc 
 * firebase Users ban đầu lấy key là IDUser nhưng hiện tại hoán đổi vị trí của TenTaiKhoan và
 * IDUser, TenTaiKhoan sẽ là key của node Users, còn IDUser sẽ là một trường con trong node TenTaiKhoan
*/
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class DangNhap : Form
    {
        private string userId;
        private string username;
        public DangNhap()
        {
            InitializeComponent();
            //MessageBox.Show("DangNhap created");
        }


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTenTaiKhoan.Text.Trim();
            string mk = txtMatKhau.Text.Trim();

            // 1. Kiểm tra rỗng
            if (tk == "" || mk == "")
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được để trống!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            username = tk;

            // 2. Gửi gói tin LOGIN xuống server
            string goiTin = $"LOGIN|{tk}|{mk}";
            NetworkClient.Instance.Send(goiTin);

        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /* Test thử xem form DangNhap có gửi gói tin xuống server được hay không
             string msg = "Message thứ 2 của test";

             NetworkClient.Instance.Send(msg);

             MessageBox.Show("Đã gửi lên server: " + msg);
            */
            using (var dk = new DangKy())
            {
                this.Hide();
                dk.ShowDialog();
                this.Show();
                txtTenTaiKhoan.Focus();
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyDangNhap;
            NetworkClient.OnMessageReceived += ClientXuLyDangNhap;

            lblServerInfo.Text = $"🔗 Server: {ServerConfig.Instance.GetServerIP()}:9998";
        }
        private void ClientXuLyDangNhap(string msg)
        {
            //if (msg.StartsWith("LOGIN_OK|"))
            //{
            //    string id = msg.Substring(16);
            //    //MessageBox.Show("Debug nhận được: [" + msg + "]");
            //    this.Invoke(new Action(() =>
            //    {
            //        //MessageBox.Show(id);
            //        this.Hide();
            //        GiaoDienChung f = new GiaoDienChung(id);  
            //        f.Show();
            //        return;
            //    }));

            //    return;
            //}
            //else if (msg == "LOGIN_FAIL|SAI_MAT_KHAU")
            //{
            //    MessageBox.Show("Sai mật khẩu!");
            //    return;
            //}
            //else if (msg == "LOGIN_FAIL|TAI_KHOAN_KHONG_TON_TAI")
            //{
            //    MessageBox.Show("Tài khoản không tồn tại!");
            //    return;
            //}

            // Format mới: LOGIN_OK|IDUser|SessionToken
            if (msg.StartsWith("LOGIN_OK|"))
            {
                string[] parts = msg.Split('|');
                if (parts.Length < 2)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Lỗi phản hồi từ server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    return;
                }

                string id = parts[1];
                string sessionToken = parts.Length >= 3 ? parts[2] : ""; // Token từ server

                this.Invoke(new Action(() =>
                {
                    // ✅ LƯU SESSION KHI ĐĂNG NHẬP THÀNH CÔNG
                    bool rememberMe = ckNhoMatKhau.Checked; // Checkbox "Nhớ đăng nhập"
                    SessionManager.Instance.SaveSession(id, username, sessionToken, rememberMe);

                    // Chuyển sang giao diện chính
                    this.Hide();
                    GiaoDienChung f = new GiaoDienChung(id);
                    f.Show();
                }));

                return;
            }
            else if (msg == "LOGIN_FAIL|SAI_MAT_KHAU")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Sai mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
                return;
            }
            else if (msg == "LOGIN_FAIL|TAI_KHOAN_KHONG_TON_TAI")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
                return;
            }
            else if (msg == "LOGIN_FAIL|CHUA_XAC_THUC_EMAIL")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Tài khoản chưa xác thực email!\nVui lòng kiểm tra email và xác thực OTP.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }));
                return;
            }
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyDangNhap;
            Application.Exit(); // đảm bảo thoát
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = ckHienMatKhau.Checked ? '\0' : '●';
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var formQuenMK = new QuenMatKhau())
            {
                formQuenMK.ShowDialog();
                txtTenTaiKhoan.Focus();
            }
        }

        private void txtTenTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCauHinhServer_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form cấu hình Server
                FormServerConfig configForm = new FormServerConfig();
                DialogResult result = configForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Cập nhật hiển thị IP mới
                    lblServerInfo.Text = $"🔗 Server: {ServerConfig.Instance.GetServerIP()}:9998";

                    // Hỏi người dùng có muốn kết nối lại không
                    DialogResult reconnect = MessageBox.Show(
                        "IP Server đã thay đổi!\n\n" +
                        "Bạn có muốn kết nối lại ngay bây giờ?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (reconnect == DialogResult.Yes)
                    {
                        // Hiển thị loading
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;

                        // Ngắt kết nối cũ
                        NetworkClient.Instance.Disconnect();

                        // Đợi 1 chút
                        System.Threading.Thread.Sleep(500);

                        // Kết nối lại với IP mới
                        NetworkClient.Instance.Connect();

                        this.Cursor = Cursors.Default;
                        this.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cấu hình Server:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
        }


        //private void XuLyDangNhap(string msg)
        //{
        //    if (msg.StartsWith("LOGIN_OK|"))
        //    {
        //        string id = msg.Substring(9);
        //        idUser = id;

        //        this.Invoke(new Action(() =>
        //        {
        //            MessageBox.Show("Đăng nhập thành công!", "Thành công");
        //            // Chuyển sang form menu chính...
        //        }));
        //    }
        //    else if (msg.StartsWith("LOGIN_FAIL|"))
        //    {
        //        string loiLoi = msg.Substring(11);

        //        this.Invoke(new Action(() =>
        //        {
        //            switch (loiLoi)
        //            {
        //                case "SAI_MAT_KHAU":
        //                    MessageBox.Show("Mật khẩu không đúng!", "Lỗi");
        //                    break;
        //                case "TAI_KHOAN_KHONG_TON_TAI":
        //                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi");
        //                    break;
        //                case "CHUA_XAC_THUC_EMAIL": // ← THÊM CASE MỚI
        //                    MessageBox.Show("Tài khoản chưa xác thực email!\nVui lòng kiểm tra email và xác thực OTP.", "Lỗi");
        //                    break;
        //                default:
        //                    MessageBox.Show("Đăng nhập thất bại!", "Lỗi");
        //                    break;
        //            }
        //        }));
        //    }
        //}
    }
}

