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

        
    }
}
