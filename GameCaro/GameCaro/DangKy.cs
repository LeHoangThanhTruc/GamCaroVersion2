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
            //MessageBox.Show("msg: "+msg);
            if (!msg.StartsWith("REGISTER_OK|"))
                return;

            string id = msg.Substring(12);
            idUser = id;

            this.Invoke(new Action(() =>
            {
                this.Close();
            }));
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

