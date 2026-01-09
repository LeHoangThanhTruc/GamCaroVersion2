using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayChu
{
    public class GameCaroRoom
    {
        public string IDFirstPlayer { get; set; }
        public string IDSecondPlayer { get; set; }
        public string Winner { get; set; }
        public string TimeBeginMatch { get; set; }   // có thể null
        public string TimeEndMatch { get; set; }     // có thể null
    }
}
