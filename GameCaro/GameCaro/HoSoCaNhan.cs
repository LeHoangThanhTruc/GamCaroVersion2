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
        }
        //Hàm  ClientXuLyProfile sẽ được TỰ ĐỘNG THỰC THI khi client NHẬN ĐƯỢC TIN NHẮN TỪ SERVER
        private void ClientXuLyProfile(string msg)
        {
            //Để sẵn hàm này, khi có yêu cầu xử lý giao diện chung thì sẽ bổ sung sau
        }

    }
}
