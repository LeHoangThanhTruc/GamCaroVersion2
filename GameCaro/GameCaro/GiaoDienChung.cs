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
    public partial class GiaoDienChung : Form
    {
        private string userId;
        public GiaoDienChung(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void btnPhongCho_Click(object sender, EventArgs e)
        {
            this.Hide();
            PhongCho f = new PhongCho(userId);
            f.Show();
        }
        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            this.Hide();
            CaiDat f = new CaiDat(userId);
            f.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoSoCaNhan f = new HoSoCaNhan(userId);
            f.Show();
        }


        private void GiaoDienChung_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived += ClientXuLyGiaoDienChung;
        }

        //Hàm ClientXuLySettings sẽ được TỰ ĐỘNG THỰC THI khi client NHẬN ĐƯỢC TIN NHẮN TỪ SERVER
        private void ClientXuLyGiaoDienChung(string msg) 
        {
            //Để sẵn hàm này, khi có yêu cầu xử lý giao diện chung thì sẽ bổ sung sau
        }

    }
}
