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
    public partial class CaiDat : Form
    {
        private string uid;
        public CaiDat(string id)
        {
            InitializeComponent();
            uid = id;
        }

        private void btnQuayLaiGiaoDienChung_Click(object sender, EventArgs e)
        {
            this.Hide();
            GiaoDienChung f = new GiaoDienChung(uid);
            f.Show();
        }



        private void CaiDat_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLySettings;
        }

        //Hàm ClientXuLySettings sẽ được TỰ ĐỘNG THỰC THI khi client NHẬN ĐƯỢC TIN NHẮN TỪ SERVER
        private void ClientXuLySettings(string msg)
        {
            //Để sẵn hàm này, khi có yêu cầu xử lý giao diện chung thì sẽ bổ sung sau
        }

        private void linkLabelChangeHoVaTen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormThayDoiThongTin f = new FormThayDoiThongTin(uid, "HoVaTen");
            f.ShowDialog();
            this.Show();
        }

        private void linkLabelChangeTenTaiKhoan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormThayDoiThongTin f = new FormThayDoiThongTin(uid, "TenTaiKhoan");
            f.ShowDialog();
            this.Show();
        }

        private void linkLabelChangeMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormDoiMatKhau f = new FormDoiMatKhau(uid);
            DialogResult result = f.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Đổi mật khẩu thành công - đóng tất cả form và quay về đăng nhập
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (!(form is DangNhap))
                    {
                        form.Close();
                    }
                }

                // Mở form đăng nhập
                DangNhap loginForm = new DangNhap();
                loginForm.Show();
            }
            else
            {
                // Hủy hoặc có lỗi - hiện lại form CaiDat
                this.Show();
            }
        }

        private void CaiDat_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLySettings;
        }

        private void linkLabelChangeGmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormThayDoiThongTin f = new FormThayDoiThongTin(uid, "TenTaiKhoan");
            f.ShowDialog();
            this.Show();
        }
    }
}
