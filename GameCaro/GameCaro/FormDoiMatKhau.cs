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
    public partial class FormDoiMatKhau : Form
    {
        private string userId;
        //private bool useOTPMethod = false;
        //private bool otpVerified = false;
        public FormDoiMatKhau(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
            NetworkClient.OnMessageReceived += XuLyPhanHoi;

            //UpdateUIMode();
        }

        //private void UpdateUIMode()
        //{
        //    if (useOTPMethod)
        //    {
        //        // Chế độ OTP - không cần mật khẩu hiện tại
        //        lblMatKhauHienTai.Visible = false;
        //        txtMatKhauHienTai.Visible = false;
        //        lnkQuenMatKhau.Visible = false;

        //        if (otpVerified)
        //        {
        //            lblTitle.Text = "Đặt Mật Khẩu Mới";
        //        }
        //        else
        //        {
        //            lblTitle.Text = "Đang Xác Thực OTP...";
        //        }
        //    }
        //    else
        //    {
        //        // Chế độ bình thường - cần mật khẩu hiện tại
        //        lblMatKhauHienTai.Visible = true;
        //        txtMatKhauHienTai.Visible = true;
        //        lnkQuenMatKhau.Visible = true;
        //        lblTitle.Text = "Đổi Mật Khẩu";
        //    }
        //}

        private void XuLyPhanHoi(string msg)
        {
            if (msg.StartsWith("CHANGE_PASSWORD_OK"))
            {
                this.Invoke(new Action(() =>
                {
                    // Hiển thị thông báo yêu cầu khởi động lại
                    MessageBox.Show(
                        "Bạn đã đổi mật khẩu thành công!\nVui lòng khởi động lại ứng dụng.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Xóa session local
                    SessionManager.Instance.ClearSession();

                    // Ngắt kết nối với server
                    NetworkClient.Instance.Disconnect();

                    // Đóng toàn bộ ứng dụng
                    Application.Exit();
                }));
            }
            else if (msg.StartsWith("CHANGE_PASSWORD_FAIL|"))
            {
                string[] parts = msg.Split('|');
                string error = parts.Length > 1 ? parts[1] : "UNKNOWN";

                this.Invoke(new Action(() =>
                {
                    btnDoiMatKhau.Enabled = true;
                    btnDoiMatKhau.Text = "Đổi mật khẩu";

                    switch (error)
                    {
                        case "SAI_MAT_KHAU_HIEN_TAI":
                            MessageBox.Show("Mật khẩu hiện tại không đúng!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtMatKhauHienTai.Clear();
                            txtMatKhauHienTai.Focus();
                            break;
                        default:
                            MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }));
            }
            //else if (msg.StartsWith("FORGOT_PASSWORD_OK|"))
            //{
            //    string tempUserId = msg.Split('|')[1];

            //    this.Invoke(new Action(() =>
            //    {
            //        MessageBox.Show("Đã gửi mã OTP đến email của bạn!", "Thông báo",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        // Mở form xác thực OTP
            //        FormXacThucOTPDoiMK formOTP = new FormXacThucOTPDoiMK(tempUserId);
            //        DialogResult result = formOTP.ShowDialog();

            //        if (result == DialogResult.OK)
            //        {
            //            MessageBox.Show("Xác thực OTP thành công!\nBạn có thể đặt mật khẩu mới.", "Thành công",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            // OTP xác thực thành công
            //            useOTPMethod = true;
            //            otpVerified = true;
            //            UpdateUIMode();
            //            txtMatKhauMoi.Focus();
            //        }
            //    }));
            //}

            //// ✅ THÊM XỬ LÝ RESET_PASSWORD_OK (khi đổi mật khẩu qua OTP)
            //else if (msg.StartsWith("RESET_PASSWORD_OK"))
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        // Đăng xuất và quay về form đăng nhập
            //        NetworkClient.Instance.Send($"LOGOUT|{userId}");
            //        SessionManager.Instance.ClearSession();

            //        MessageBox.Show("Đổi mật khẩu thành công!\nVui lòng đăng nhập lại.", "Thành công",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        // Set DialogResult để form CaiDat biết phải đóng
            //        this.DialogResult = DialogResult.OK;
            //        this.Close();
            //    }));
            //}
            //else if (msg.StartsWith("RESET_PASSWORD_FAIL"))
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        btnDoiMatKhau.Enabled = true;
            //        btnDoiMatKhau.Text = "Đổi mật khẩu";
            //        MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }));
            //}

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(matKhauMoi) || string.IsNullOrWhiteSpace(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (matKhauMoi != xacNhanMatKhau)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return;
            }

            // Kiểm tra độ dài mật khẩu
            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //if (useOTPMethod && otpVerified)
            //{
            //    // Đổi mật khẩu qua OTP (không cần mật khẩu cũ)
            //    NetworkClient.Instance.Send($"RESET_PASSWORD|{userId}|{matKhauMoi}");
            //}
            else
            {
                // Đổi mật khẩu bình thường (cần mật khẩu cũ)
                string matKhauHienTai = txtMatKhauHienTai.Text.Trim();

                if (string.IsNullOrWhiteSpace(matKhauHienTai))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauHienTai.Focus();
                    return;
                }

                NetworkClient.Instance.Send($"CHANGE_PASSWORD|{userId}|{matKhauHienTai}|{matKhauMoi}");
            }

            btnDoiMatKhau.Enabled = false;
            btnDoiMatKhau.Text = "Đang xử lý...";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    DialogResult result = MessageBox.Show(
        //        "Bạn sẽ nhận mã OTP qua email để đổi mật khẩu.\nTiếp tục?",
        //        "Xác nhận",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question
        //    );

        //    if (result == DialogResult.Yes)
        //    {
        //        // Gửi yêu cầu OTP qua hệ thống quên mật khẩu
        //        NetworkClient.Instance.Send($"FORGOT_PASSWORD_SETTING|{userId}");
        //    }
        //}

        private void ckHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            char displayChar = ckHienMatKhau.Checked ? '\0' : '●';
            txtMatKhauHienTai.PasswordChar = displayChar;
            txtMatKhauMoi.PasswordChar = displayChar;
            txtXacNhanMatKhau.PasswordChar = displayChar;
        }

        private void FormDoiMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
        }
    }
}
