using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameCaroShared;
namespace GameCaroServer
{
    public class GameState
    {
        public List<PlayInfor> Moves { get; private set; }
        public int CurrentPlayer { get; set; }

        public GameState()
        {
            Moves = new List<PlayInfor>();
            CurrentPlayer = 0;
        }

        public void AddMove(PlayInfor move)
        {
            Moves.Add(move);
            CurrentPlayer = (CurrentPlayer == 0) ? 1 : 0;
        }

        public void Reset()
        {
            Moves.Clear();
            CurrentPlayer = 0;
        }
    }
}
