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
    public partial class LichSuTranDau : Form
    {
        private string userId;

        public LichSuTranDau(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void LichSuTranDau_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình ListView
            lvLichSu.View = View.Details;
            lvLichSu.FullRowSelect = true;
            lvLichSu.GridLines = true;

            lvLichSu.Columns.Clear();
            lvLichSu.Columns.Add("Người chơi 1", 150);
            lvLichSu.Columns.Add("Người chơi 2", 150);
            lvLichSu.Columns.Add("Kết quả", 100);

            // 2. Đăng ký lắng nghe message
            NetworkClient.OnMessageReceived += ClientXuLyLichSu;

            // 3. Gửi yêu cầu lấy lịch sử
            NetworkClient.Instance.Send($"GET_MATCH_HISTORY|{userId}");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ClientXuLyLichSu(string msg)
        {
            if (!msg.StartsWith("MATCH_HISTORY_DATA|"))
                return;

            try
            {
                string json = msg.Substring("MATCH_HISTORY_DATA|".Length);

                // BẮT BUỘC
                json = json.Replace("'", "\"");

                List<MatchHistoryItem> list =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatchHistoryItem>>(json);

                if (list == null) return;

                this.Invoke(new Action(() =>
                {
                    lvLichSu.Items.Clear();

                    foreach (var match in list)
                    {
                        ListViewItem item = new ListViewItem(match.FirstPlayerName);
                        item.SubItems.Add(match.SecondPlayerName);
                        item.SubItems.Add(match.WinnerName);

                        // 🎯 Đổi màu theo kết quả
                        if (match.WinnerName == "Thắng")
                            item.ForeColor = Color.Green;
                        else
                            item.ForeColor = Color.Red;

                        lvLichSu.Items.Add(item);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý lịch sử:\n" + ex.Message);
            }
        }
        private void LichSuTranDau_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyLichSu;
        }


    }
}
