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
    public partial class FormXacThucOTP : Form
    {
        private string idUser;
        public FormXacThucOTP(string idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        private void FormXacThucOTP_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanhoi;
            NetworkClient.OnMessageReceived += XuLyPhanhoi;

            lblThongTin.Text = $"Mã OTP đã được gửi đến email của bạn.\nVui lòng kiểm tra hộp thư (kể cả Spam).";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maOTP = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(maOTP))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maOTP.Length != 6)
            {
                MessageBox.Show("Mã OTP phải có 6 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi yêu cầu xác thực lên server
            NetworkClient.Instance.Send($"VERIFY_OTP|{idUser}|{maOTP}");
            btnXacNhan.Enabled = false;
            btnXacNhan.Text = "Đang xác thực...";
        }

        private void XuLyPhanhoi(string msg)
        {
            if (msg.Contains("VERIFY_OK"))
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Xác thực thành công! Bạn có thể đăng nhập ngay.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }));
            }
            else if (msg.StartsWith("VERIFY_FAIL|"))
            {
                string[] parts = msg.Split('|');
                string loiLoi = parts.Length > 1 ? parts[1] : "UNKNOWN";

                this.Invoke(new Action(() =>
                {
                    btnXacNhan.Enabled = true;
                    btnXacNhan.Text = "Xác nhận";

                    switch (loiLoi)
                    {
                        case "MA_SAI":
                            MessageBox.Show("Mã OTP không đúng! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtOTP.Clear();
                            txtOTP.Focus();
                            break;
                        case "MA_HET_HAN":
                            MessageBox.Show("Mã OTP đã hết hạn! Vui lòng đăng ký lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            break;
                        case "MA_KHONG_TON_TAI":
                            MessageBox.Show("Không tìm thấy mã OTP! Vui lòng đăng ký lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Xác thực thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }));
            }
        }

        private void FormXacThucOTP_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanhoi;
        }
    }
}
