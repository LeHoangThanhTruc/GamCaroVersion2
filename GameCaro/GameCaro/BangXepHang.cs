using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GameCaro
{
    public partial class BangXepHang : Form
    {
        private string userId;

        public BangXepHang(string id)
        {
            InitializeComponent();
            userId = id;
        }
        private StringBuilder rankingBuffer = new StringBuilder();


        public BangXepHang()
        {
            InitializeComponent();
        }
        void XuLyDuLieuBangXepHang(string json)
        {
            try
            {
                var allUsers = JsonConvert
                    .DeserializeObject<Dictionary<string, UserProfile>>(json);

                if (allUsers == null || allUsers.Count == 0)
                    return;

                // Sắp xếp theo SoTranChienThang giảm dần
                var top5 = allUsers.Values

                    .OrderByDescending(u => u.SoTranDaChienThang ?? 0)
                    .Take(5)
                    .ToList();

                this.Invoke(new Action(() =>
                {
                    HienThiTop5(top5);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý bảng xếp hạng: " + ex.Message);
            }
        }
        void HienThiTop5(List<UserProfile> top5)
        {
            TextBox[] txtTen =
    {
        txtTenTaiKhoan1, txtTenTaiKhoan2, txtTenTaiKhoan3,
        txtTenTaiKhoan4, txtTenTaiKhoan5
    };

            TextBox[] txtId =
            {
        txtid1, txtid2, txtid3, txtid4, txtid5
    };

            TextBox[] txtWin =
            {
        txtsotranchienthang1, txtsotranchienthang2, txtsotranchienthang3,
        txtsotranchienthang4, txtsotranchienthang5
    };

            for (int i = 0; i < top5.Count; i++)
            {
                txtTen[i].Text = top5[i].TenTaiKhoan;
                txtId[i].Text = top5[i].IDUser;
                txtWin[i].Text = (top5[i].SoTranDaChienThang ?? 0).ToString();
            }
        }
        private void ClientXuLyBangXepHang(string msg)
        {
            // Chỉ xử lý ranking
            if (!msg.StartsWith("RANKING_DATA|") && rankingBuffer.Length == 0)
                return;

            // Nếu là gói đầu
            if (msg.StartsWith("RANKING_DATA|"))
            {
                rankingBuffer.Clear();
                rankingBuffer.Append(msg.Substring(13)); // bỏ prefix
            }
            else
            {
                // Gói tiếp theo
                rankingBuffer.Append(msg);
            }

            // Kiểm tra JSON đã đủ chưa
            if (IsJsonComplete(rankingBuffer.ToString()))
            {
                string fullJson = rankingBuffer.ToString();
                rankingBuffer.Clear();

                Console.WriteLine("✅ Đã nhận đủ JSON BXH, length = " + fullJson.Length);

                XuLyDuLieuBangXepHang(fullJson);
            }

        }
        private bool IsJsonComplete(string json)
        {
            int open = 0;
            int close = 0;

            foreach (char c in json)
            {
                if (c == '{') open++;
                if (c == '}') close++;
            }

            return open > 0 && open == close;
        }






        private void BangXepHang_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLyBangXepHang;
            NetworkClient.Instance.Send("GET_RANKING");

        }
        private void BangXepHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyBangXepHang;
        }

        private void btnQuayLaiGiaoDienChung_Click(object sender, EventArgs e)
        {
            GiaoDienChung gdChung = new GiaoDienChung(userId);

            gdChung.Show();

            this.Close();
        }
    }
}
