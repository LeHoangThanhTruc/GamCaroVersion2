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
        
        public LichSuTranDau()
        {
            InitializeComponent();
            


        }

        private void LichSuTranDau_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyLichSuTranDau;
            NetworkClient.OnMessageReceived += ClientXuLyLichSuTranDau;
        }
        private void ClientXuLyLichSuTranDau(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ClientXuLyLichSuTranDau(msg)));
                return;
            }

            
        }
        private void LichSuTranDau_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyLichSuTranDau;
        }
    }
}
