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
        private int countdown = 60;
        private System.Windows.Forms.Timer countdownTimer;
        public FormXacThucOTP(string idUser)
        {
            InitializeComponent();
            this.idUser = idUser;

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; // 1 giây
            countdownTimer.Tick += CountdownTimer_Tick;
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
            else if (msg.StartsWith("RESEND_OTP_OK"))
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Đã gửi lại mã OTP!\nVui lòng kiểm tra email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bắt đầu đếm ngược lại
                    BatDauDemNguoc();
                }));
            }
            else if (msg.StartsWith("RESEND_OTP_FAIL"))
            {
                this.Invoke(new Action(() =>
                {
                    btnGuiLai.Enabled = true;
                    btnGuiLai.Text = "Gửi lại mã";
                    MessageBox.Show("Không thể gửi lại mã OTP!\nVui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private void FormXacThucOTP_FormClosing(object sender, FormClosingEventArgs e)
        {
            countdownTimer.Stop();
            countdownTimer.Dispose();
            NetworkClient.OnMessageReceived -= XuLyPhanhoi;
        }

        private void btnGuiLai_Click(object sender, EventArgs e)
        {
            NetworkClient.Instance.Send($"RESEND_OTP|{idUser}");

            btnGuiLai.Enabled = false;
            btnGuiLai.Text = "Đang gửi...";

            txtOTP.Clear();
            txtOTP.Focus();
        }

        private void BatDauDemNguoc()
        {
            countdown = 60;
            btnGuiLai.Enabled = false;
            btnGuiLai.Text = $"Gửi lại ({countdown}s)";
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdown--;

            if (countdown > 0)
            {
                btnGuiLai.Text = $"Gửi lại ({countdown}s)";
            }
            else
            {
                countdownTimer.Stop();
                btnGuiLai.Enabled = true;
                btnGuiLai.Text = "Gửi lại mã";
            }
        }

    }
}
