//Lớp GlobalConnection lưu trữ 1 socket dùng chung
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace GameCaro
{
    public static class GlobalConnection
    {
        public static Socket ClientSocket { get; set; }
    }
}
