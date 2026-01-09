using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class HoSoCaNhan : Form
    {
        private string uid;
        public HoSoCaNhan(string id)
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


        private void HoSoCaNhan_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLyProfile;

            // GỬI REQUEST LẤY PROFILE
            NetworkClient.Instance.Send($"GET_PROFILE|{uid}");
        }
        //Hàm  ClientXuLyProfile sẽ được TỰ ĐỘNG THỰC THI khi client NHẬN ĐƯỢC TIN NHẮN TỪ SERVER
        private void ClientXuLyProfile(string msg)
        {
            // 1. Chỉ xử lý profile
            // ====== PROFILE DATA ======
            if (msg.StartsWith("PROFILE_DATA|"))
            {
                try
                {
                    string json = msg.Substring("PROFILE_DATA|".Length);
                    json = json.Replace("'", "\"");

                    UserProfile profile =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(json);

                    if (profile == null) return;

                    this.Invoke(new Action(() =>
                    {
                        HienThiAvatar(profile.Avatar);
                        HienThiThongTinDangKy(profile);
                        HienThiThongKe(profile);
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xử lý PROFILE_DATA:\n" + ex.Message);
                }

                return;
            }

            // ====== UPDATE AVATAR OK ======
            if (msg.StartsWith("UPDATE_AVATAR_OK|"))
            {
                string newAvatar = msg.Split('|')[1];

                this.Invoke(new Action(() =>
                {
                    HienThiAvatar(newAvatar);
                    MessageBox.Show("Đổi avatar thành công!");
                }));

                return;
            }

            // ====== UPDATE AVATAR FAIL ======
            if (msg.StartsWith("UPDATE_AVATAR_FAIL"))
            {
                MessageBox.Show("Đổi avatar thất bại!");
            }
        }
        private void HienThiAvatar(string avatarName)
        {
            try
            {
                avatarName = Path.GetFileName(avatarName);

                if (string.IsNullOrWhiteSpace(avatarName))
                    avatarName = "default.jpg";

                string avatarPath = Path.Combine(
                    Application.StartupPath,
                    "Avatars",
                    avatarName
                );

                // Nếu không tồn tại → fallback
                if (!File.Exists(avatarPath))
                {
                    avatarPath = Path.Combine(
                        Application.StartupPath,
                        "Avatars",
                        "default.jpg"
                    );
                }

                // ĐẢM BẢO FILE TỒN TẠI
                if (!File.Exists(avatarPath))
                {
                    MessageBox.Show("Không tìm thấy avatar:\n" + avatarPath);
                    return;
                }

                // Load ảnh an toàn KHÔNG LOCK FILE
                using (FileStream fs = new FileStream(avatarPath, FileMode.Open, FileAccess.Read))
                {
                    Image img = Image.FromStream(fs);
                    picAvatar.Image?.Dispose();
                    picAvatar.Image = new Bitmap(img);
                }

                picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị avatar:\n" + ex.Message);
            }

        }
        private void HienThiThongTinDangKy(UserProfile profile)
        {
            if (profile == null) return;

            // Vì đang ở thread khác → luôn Invoke
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HienThiThongTinDangKy(profile)));
                return;
            }

            txttentaikhoan.Text = profile.TenTaiKhoan ?? "";
            txtid.Text = profile.IDUser ?? "";
            txtgmail.Text = profile.Gmail ?? "";
        }
        private void HienThiThongKe(UserProfile profile)
        {
            if (profile == null) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HienThiThongKe(profile)));
                return;
            }

            int soTranThang = profile.SoTranDaChienThang ?? 0;
            int soTranThamGia = profile.SoTranDaThamGia ?? 0;
            int soTranThua = profile.SoTranDaThua ?? 0;

            txtchienthang.Text = soTranThang.ToString();
            txtthamgia.Text = soTranThamGia.ToString();
            txtdathua.Text = soTranThua.ToString();

            // TÍNH TỶ LỆ THẮNG
            double tyLe = 0;
            if (soTranThamGia > 0)
            {
                tyLe = (double)soTranThang / soTranThamGia * 100;
            }

            txttyle.Text = tyLe.ToString("0.##") + " %";
        }

        private void btnChooseAvartar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.InitialDirectory = Path.Combine(Application.StartupPath, "Avatars");
                    ofd.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png";
                    ofd.Title = "Chọn avatar";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string avatarName = Path.GetFileName(ofd.FileName);

                        // GỬI YÊU CẦU XUỐNG SERVER
                        NetworkClient.Instance.Send(
                            $"UPDATE_AVATAR|{uid}|{avatarName}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn avatar:\n" + ex.Message);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtdathua_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
