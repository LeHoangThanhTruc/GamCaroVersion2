using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class FormThayDoiThongTin : Form
    {
        private string userId;
        private string fieldType; // "HoVaTen", "TenTaiKhoan", "Gmail"
        private string currentValue;
        public FormThayDoiThongTin(string id, string type)
        {
            InitializeComponent();
            userId = id;
            fieldType = type;
        }

        private void FormThayDoiThongTin_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
            NetworkClient.OnMessageReceived += XuLyPhanHoi;

            // Cập nhật tiêu đề form
            switch (fieldType)
            {
                case "HoVaTen":
                    lblTitle.Text = "Thay Đổi Họ Và Tên";
                    lblField.Text = "Họ và tên:";
                    break;
                case "TenTaiKhoan":
                    lblTitle.Text = "Thay Đổi Tên Tài Khoản";
                    lblField.Text = "Tên tài khoản:";
                    break;
                case "Gmail":
                    lblTitle.Text = "Thay Đổi Gmail";
                    lblField.Text = "Gmail:";
                    break;
            }

            // Yêu cầu lấy thông tin hiện tại từ server
            NetworkClient.Instance.Send($"GET_USER_INFO|{userId}|{fieldType}");
            txtField.Enabled = false;
            btnSave.Enabled = false;
        }

        private void XuLyPhanHoi(string msg)
        {
            if (msg.StartsWith("USER_INFO|"))
            {
                string[] parts = msg.Split('|');
                if (parts.Length >= 3 && parts[1] == fieldType)
                {
                    currentValue = parts[2];
                    this.Invoke(new Action(() =>
                    {
                        txtField.Text = currentValue;
                        txtField.Enabled = true;
                        btnSave.Enabled = true;
                        txtField.Focus();
                    }));
                }
            }
            else if (msg.StartsWith("UPDATE_INFO_OK"))
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }));
            }
            else if (msg.StartsWith("UPDATE_INFO_FAIL|"))
            {
                string[] parts = msg.Split('|');
                string error = parts.Length > 1 ? parts[1] : "UNKNOWN";

                this.Invoke(new Action(() =>
                {
                    switch (error)
                    {
                        case "TAI_KHOAN_DA_TON_TAI":
                            MessageBox.Show("Tên tài khoản này đã có người sử dụng!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "EMAIL_DA_TON_TAI":
                            MessageBox.Show("Email này đã được sử dụng bởi tài khoản khác!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                    txtField.Focus();
                }));
            }
        }

        private bool KiemTraDinhDangEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newValue = txtField.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(newValue))
            {
                MessageBox.Show("Thông tin không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtField.Focus();
                return;
            }

            // Kiểm tra nếu không thay đổi gì
            if (newValue == currentValue)
            {
                MessageBox.Show("Bạn chưa thay đổi thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra định dạng email nếu đang đổi Gmail
            if (fieldType == "Gmail" && !KiemTraDinhDangEmail(newValue))
            {
                MessageBox.Show("Email không đúng định dạng!\nVí dụ: example@gmail.com", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtField.Focus();
                return;
            }

            // Gửi yêu cầu cập nhật lên server
            NetworkClient.Instance.Send($"UPDATE_USER_INFO|{userId}|{fieldType}|{newValue}");
            btnSave.Enabled = false;
            btnSave.Text = "Đang lưu...";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormThayDoiThongTin_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
        }
    }
}
