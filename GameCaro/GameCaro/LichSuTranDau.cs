using Microsoft.VisualBasic.ApplicationServices;
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
        

        public LichSuTranDau(string id)
        {
            InitializeComponent();
            userId = id;
        }

        private void LichSuTranDau_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyLichSuTranDau;
            NetworkClient.OnMessageReceived += ClientXuLyLichSuTranDau;

        }
        private void ClientXuLyLichSuTranDau(string msg)
        {
            
        }


        
    }
}
