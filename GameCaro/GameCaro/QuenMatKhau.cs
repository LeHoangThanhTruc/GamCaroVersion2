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
    public partial class QuenMatKhau : Form
    {
        private string currentUserId = "";
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnGuiOTP_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTaiKhoan.Text.Trim();

            if (string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi yêu cầu lấy OTP
            string goiTin = $"FORGOT_PASSWORD|{tenTaiKhoan}";
            NetworkClient.Instance.Send(goiTin);

            btnGuiOTP.Enabled = false;
            btnGuiOTP.Text = "Đang gửi...";
        }

        private void btnXacThucOTP_Click(object sender, EventArgs e)
        {
            string maOTP = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(maOTP))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi OTP để xác thực
            string goiTin = $"VERIFY_RESET_OTP|{currentUserId}|{maOTP}";
            NetworkClient.Instance.Send(goiTin);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi yêu cầu đổi mật khẩu
            string goiTin = $"RESET_PASSWORD|{currentUserId}|{matKhauMoi}";
            NetworkClient.Instance.Send(goiTin);
        }

        private void XuLyQuenMatKhau(string msg)
        {
            if (msg.StartsWith("FORGOT_PASSWORD_OK|"))
            {
                // Format: FORGOT_PASSWORD_OK|IDUser_xxx
                currentUserId = msg.Substring(19);

                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Mã OTP đã được gửi đến email của bạn!\n" +
                                  "Vui lòng kiểm tra email và nhập mã xác thực.",
                                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnGuiOTP.Enabled = true;
                    btnGuiOTP.Text = "Gửi lại OTP";
                    panelOTP.Visible = true;
                }));
            }
            else if (msg == "FORGOT_PASSWORD_FAIL|TAI_KHOAN_KHONG_TON_TAI")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGuiOTP.Enabled = true;
                    btnGuiOTP.Text = "Gửi OTP";
                }));
            }
            else if (msg == "VERIFY_RESET_OK")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Xác thực thành công!\nVui lòng nhập mật khẩu mới.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    panelOTP.Visible = false;
                    panelDoiMatKhau.Visible = true;
                }));
            }
            else if (msg.StartsWith("VERIFY_RESET_FAIL|"))
            {
                string loiLoi = msg.Substring(18);
                this.Invoke(new Action(() =>
                {
                    // Replace the switch expression with a switch statement for C# 7.3 compatibility
                    string thongBao;
                    switch (loiLoi)
                    {
                        case "MA_KHONG_TON_TAI":
                            thongBao = "Mã OTP không tồn tại!";
                            break;
                        case "MA_HET_HAN":
                            thongBao = "Mã OTP đã hết hạn!";
                            break;
                        case "MA_SAI":
                            thongBao = "Mã OTP không chính xác!";
                            break;
                        default:
                            thongBao = "Xác thực thất bại!";
                            break;
                    };

                    MessageBox.Show(thongBao, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
            else if (msg == "RESET_PASSWORD_OK")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Đổi mật khẩu thành công!\nBạn có thể đăng nhập bằng mật khẩu mới.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }));
            }
            else if (msg == "RESET_PASSWORD_FAIL")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuenMatKhau_Load(object sender, EventArgs e)
        {
            // Đăng ký handler nhận tin từ server
            NetworkClient.OnMessageReceived -= XuLyQuenMatKhau;
            NetworkClient.OnMessageReceived += XuLyQuenMatKhau;

            // Ẩn panel đổi mật khẩu ban đầu
            panelDoiMatKhau.Visible = false;
        }

        private void QuenMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyQuenMatKhau;
        }

        private void lnkGuiLaiOTP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string tenTaiKhoan = txtTenTaiKhoan.Text.Trim();

            if (string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi lại yêu cầu OTP
            string goiTin = $"FORGOT_PASSWORD|{tenTaiKhoan}";
            NetworkClient.Instance.Send(goiTin);

            MessageBox.Show("Đang gửi lại mã OTP...", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauMoi.PasswordChar = checkBox1.Checked ? '\0' : '●';
            txtXacNhanMatKhau.PasswordChar = checkBox1.Checked ? '\0' : '●';
        }

        private void panelOTP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblOTP_Click(object sender, EventArgs e)
        {

        }

        private void txtOTP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
