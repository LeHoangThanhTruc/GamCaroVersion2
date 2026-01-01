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
    public partial class FormXacThucOTPDoiMK : Form
    {
        private string userId;
        private int countdown = 60;
        private System.Windows.Forms.Timer countdownTimer;
        public FormXacThucOTPDoiMK(string id)
        {
            InitializeComponent();
            userId = id;

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void FormXacThucOTPDoiMK_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
            NetworkClient.OnMessageReceived += XuLyPhanHoi;

            lblThongTin.Text = "Mã OTP đã được gửi đến email của bạn.\nVui lòng kiểm tra hộp thư (kể cả Spam).";
            Console.WriteLine($"[OTP] Form loaded for user {userId}");
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maOTP = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(maOTP))
            {
                MessageBox.Show("Vui lòng nhập mã OTP!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maOTP.Length != 6)
            {
                MessageBox.Show("Mã OTP phải có 6 chữ số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NetworkClient.Instance.Send($"VERIFY_RESET_OTP|{userId}|{maOTP}");
            btnXacNhan.Enabled = false;
            btnXacNhan.Text = "Đang xác thực...";
        }

        private void XuLyPhanHoi(string msg)
        {
            Console.WriteLine($"[OTP] Nhận được message: {msg}");

            if (msg.StartsWith("VERIFY_RESET_OK"))
            {
                Console.WriteLine($"[OTP] Xác thực thành công!");

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Xác thực thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }));
                }
                else
                {
                    MessageBox.Show("Xác thực thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                return;
            }
            else if (msg.StartsWith("VERIFY_RESET_FAIL|"))
            {
                string[] parts = msg.Split('|');
                string error = parts.Length > 1 ? parts[1] : "UNKNOWN";

                Console.WriteLine($"[OTP] Xác thực thất bại: {error}");

                //this.Invoke(new Action(() =>
                //{
                //    btnXacNhan.Enabled = true;
                //    btnXacNhan.Text = "Xác nhận";

                //    switch (error)
                //    {
                //        case "MA_SAI":
                //            MessageBox.Show("Mã OTP không đúng! Vui lòng thử lại.", "Lỗi",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            txtOTP.Clear();
                //            txtOTP.Focus();
                //            break;
                //        case "MA_HET_HAN":
                //            MessageBox.Show("Mã OTP đã hết hạn! Vui lòng gửi lại mã mới.", "Lỗi",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            break;
                //        case "MA_KHONG_TON_TAI":
                //            MessageBox.Show("Không tìm thấy mã OTP!", "Lỗi",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            this.Close();
                //            break;
                //        default:
                //            MessageBox.Show("Xác thực thất bại! Vui lòng thử lại.", "Lỗi",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            break;
                //    }
                //}));

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnXacNhan.Enabled = true;
                        btnXacNhan.Text = "Xác nhận";

                        switch (error)
                        {
                            case "MA_SAI":
                                MessageBox.Show("Mã OTP không đúng! Vui lòng thử lại.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtOTP.Clear();
                                txtOTP.Focus();
                                break;
                            case "MA_HET_HAN":
                                MessageBox.Show("Mã OTP đã hết hạn! Vui lòng gửi lại mã mới.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case "MA_KHONG_TON_TAI":
                                MessageBox.Show("Không tìm thấy mã OTP!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                                break;
                            default:
                                MessageBox.Show("Xác thực thất bại! Vui lòng thử lại.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }));
                }
            }
            else if (msg.StartsWith("RESEND_RESET_OTP_OK"))
            {
                Console.WriteLine($"[OTP] Đã gửi lại mã OTP");

                //this.Invoke(new Action(() =>
                //{
                //    MessageBox.Show("Đã gửi lại mã OTP!\nVui lòng kiểm tra email.", "Thông báo",
                //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    BatDauDemNguoc();
                //}));

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Đã gửi lại mã OTP!\nVui lòng kiểm tra email.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BatDauDemNguoc();
                    }));
                }
            }
            else if (msg.StartsWith("RESEND_RESET_OTP_FAIL"))
            {
                //this.Invoke(new Action(() =>
                //{
                //    btnGuiLai.Enabled = true;
                //    btnGuiLai.Text = "Gửi lại mã";
                //    MessageBox.Show("Không thể gửi lại mã OTP!\nVui lòng thử lại sau.", "Lỗi",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}));
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnGuiLai.Enabled = true;
                        btnGuiLai.Text = "Gửi lại mã";
                        MessageBox.Show("Không thể gửi lại mã OTP!\nVui lòng thử lại sau.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
        }

        private void btnGuiLai_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"[OTP] Gửi lại mã OTP cho {userId}");
            NetworkClient.Instance.Send($"RESEND_RESET_OTP|{userId}");

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

        private void FormXacThucOTPDoiMK_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine($"[OTP] Form closing");
            countdownTimer.Stop();
            countdownTimer.Dispose();
            NetworkClient.OnMessageReceived -= XuLyPhanHoi;
        }
    }
}
