using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GameCaro
{
    public partial class LichSuTranDau : Form
    {

        private string userId;
        private StringBuilder historyBuffer = new StringBuilder();


        public LichSuTranDau(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void LichSuTranDau_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLyLichSuTranDau;
            NetworkClient.Instance.Send("GET_MATCH_HISTORY|" + userId);

        }
        private void ClientXuLyLichSuTranDau(string msg)
        {
            // Chỉ xử lý lịch sử trận đấu
            if (!msg.StartsWith("MATCH_HISTORY_DATA|") && historyBuffer.Length == 0)
                return;

            // Gói đầu
            if (msg.StartsWith("MATCH_HISTORY_DATA|"))
            {
                historyBuffer.Clear();
                historyBuffer.Append(msg.Substring("MATCH_HISTORY_DATA|".Length));
            }
            else
            {
                // Gói tiếp theo
                historyBuffer.Append(msg);
            }

            // Nếu JSON đã đủ
            if (IsJsonComplete(historyBuffer.ToString()))
            {
                string fullJson = historyBuffer.ToString();
                historyBuffer.Clear();

                Console.WriteLine("✅ Đã nhận đủ JSON lịch sử trận đấu, length = " + fullJson.Length);

                XuLyDuLieuLichSuTranDau(fullJson);
            }
        }
        private bool IsJsonComplete(string json)
        {
            int open = 0, close = 0;

            foreach (char c in json)
            {
                if (c == '{') open++;
                if (c == '}') close++;
            }

            return open > 0 && open == close;
        }
        private void XuLyDuLieuLichSuTranDau(string json)
        {
            try
            {
                var allRooms = JsonConvert
                    .DeserializeObject<Dictionary<string, MatchRoom>>(json);

                if (allRooms == null || allRooms.Count == 0)
                    return;

                this.Invoke(new Action(() =>
                {
                    listView1.Items.Clear();

                    foreach (var room in allRooms.Values)
                    {
                        // Chỉ lấy trận có liên quan tới user
                        if (room.IDFirstPlayer != userId &&
                            room.IDSecondPlayer != userId)
                            continue;

                        ListViewItem item = new ListViewItem(room.IDFirstPlayer ?? "");
                        item.SubItems.Add(room.IDSecondPlayer ?? "");
                        item.SubItems.Add(room.Winner ?? "");
                        item.SubItems.Add(room.TimeBeginMatch ?? "null");
                        item.SubItems.Add(room.TimeEndMatch ?? "null");

                        listView1.Items.Add(item);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý lịch sử trận đấu: " + ex.Message);
            }
        }
        private void LichSuTranDau_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyLichSuTranDau;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnQuayLaiGiaoDienChung_Click(object sender, EventArgs e)
        {
            GiaoDienChung gdChung = new GiaoDienChung(userId);

            gdChung.Show();

            this.Close();
        }
    }
}
