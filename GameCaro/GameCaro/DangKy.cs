/* Mô tả
* Trong form DangKy mình muốn sau khi nhập txtHoVaTen, txtTenTaiKhoan, txtGmail, 
* txtMatKhau, txtNhapLaiMatKhau thì khi mình bấm nút btnXacNhanDangKy thì có thể gửi tất 
* cả các thông tin trong các textbox phía trên gồm họ tên, tên tài khoản, gmail, mật khẩu 
* xuống server, đồng thời sau khi server nhận được gói tin sẽ cấp cho người đăng ký đó 
* một id ngẫu nhiên gồm 5 ký tự (các ký tự đó là gì cũng được, không giới hạn chữ, số 
* hay ký tự đặc biệt) và tách gói tin đó thành các trường HoVaTen, TenTaiKhoan, Gmail, 
* MatKhau và IDUser mới được cấp sau đó lưu xuống firebase vào node 
* User-->IDUser-->(HoVaTen, TenTaiKhoan, Gmail, MatKhau) . Các thông tin trong textbox 
* không được để trống bất kỳ ô nào, nếu ô nào bị trống thì hiện lên thông báo không được 
* phép gửi gói tin xuống server nếu bất kỳ ô textbox nào trống
* */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace GameCaro
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
            //Form DangNhap đã kết nối rồi thì những form sau đó không cần kết nối lại nhiều lần
            //MessageBox.Show("DangKy created");
        }

        private bool KiemTraDinhDangEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Regex pattern chuẩn cho email
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        private void btnXacNhanDangKy_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text) ||
                string.IsNullOrWhiteSpace(txtTenTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtGmail.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtNhapLaiMatKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống ô nào!", "Lỗi");
                return;
            }

            // Kiểm tra định dạng email
            if (!KiemTraDinhDangEmail(txtGmail.Text))
            {
                MessageBox.Show("Email không đúng định dạng!\nVí dụ: example@gmail.com", "Lỗi");
                return;
            }

            // 2. Kiểm tra confirm mật khẩu
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi");
                return;
            }

            // 3. Tạo gói tin
            var data = new GoiTinDangKy
            {
                HoVaTen = txtHoVaTen.Text,
                TenTaiKhoan = txtTenTaiKhoan.Text,
                Gmail = txtGmail.Text,
                MatKhau = txtMatKhau.Text
            };

            string json = System.Text.Json.JsonSerializer.Serialize(data);

            // 4. Gửi xuống server

            NetworkClient.Instance.Send("REGISTER|" + json);

            //MessageBox.Show("Đã gửi thông tin đăng ký xuống server!");
            //REGISTER|{"HoVaTen":"ABC","TenTaiKhoan":"xyz",...}
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyDangKy; // tránh double
            NetworkClient.OnMessageReceived += ClientXuLyDangKy;
        }
        string idUser = "";

        private void ClientXuLyDangKy(string msg)
        {
            ////MessageBox.Show("msg: "+msg);
            //if (!msg.StartsWith("REGISTER_OK|"))
            //    return;

            //string id = msg.Substring(12);
            //idUser = id;

            //this.Invoke(new Action(() =>
            //{
            //    this.Close();
            //}));
            //---------------------------------------------------------------------
            //MessageBox.Show("msg: "+msg);

            //--------------------------------------------------------------------
            if (msg.StartsWith("REGISTER_OK|"))
            {
                string id = msg.Substring(12);
                idUser = id;

                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Đăng ký thành công! ID của bạn: " + idUser, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }));
            }
            else if (msg.StartsWith("REGISTER_PENDING|")) // ← XỬ LÝ MỚI
            {
                string id = msg.Substring(17); // "REGISTER_PENDING|" có 17 ký tự
                idUser = id;

                this.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Đã gửi mã OTP đến email của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form xác thực OTP
                    NetworkClient.OnMessageReceived -= ClientXuLyDangKy; // tránh double
                    FormXacThucOTP formOTP = new FormXacThucOTP(idUser);
                    DialogResult result = formOTP.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        // Xác thực thành công
                        MessageBox.Show(
                        " Đăng ký hoàn tất!\n\nBạn có thể đăng nhập ngay bây giờ.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                        this.Close();

                        // Mon form DangNhap sau khi dong form DangKy
                        // Tim form dang nhap neu co
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form is DangNhap)
                            {
                                form.Show();
                                form.BringToFront();
                                return;
                            }
                        }
                        // neu khong tim thay thi tao moi
                        DangNhap dangNhap = new DangNhap();
                        dangNhap.Show();
                    }
                    else
                    {
                        // ❌ User hủy hoặc xác thực thất bại
                        MessageBox.Show(
                            "Xác thực chưa hoàn tất.\n\nBạn có thể đăng ký lại hoặc liên hệ hỗ trợ.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }    
                }));
            }
            else if (msg.StartsWith("REGISTER_FAIL|"))
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Tên tài khoản này đã có người sử dụng. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private void DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyDangKy;
        }

        private void ckHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = ckHienMatKhau.Checked ? '\0' : '●';
            txtNhapLaiMatKhau.PasswordChar = ckHienMatKhau.Checked ? '\0' : '●';
        }
    }
}