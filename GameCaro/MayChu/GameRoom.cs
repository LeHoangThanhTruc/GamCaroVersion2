using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayChu
{
    class GameRoom
    {
        public string RoomId;
        public string FirstPlayer;
        public string SecondPlayer;
        public string CurrentTurn; // userId đang tới lượt
        public int[,] Board; // 0 = trống, 1 = X, 2 = O
    }
}
