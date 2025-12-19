using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    internal class ChessBoardManager
    {
        // Button và pnlChessBoard thuộc Form1 nên mình phải truyền nó xuống rồi lưu lại lấy ra sài
        //Phải tạo 1 hàm dựng cho nó rồi tạo properties cho nó
        #region Properties Ctrl R E
        private Panel chessBoard;
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }

        private List<Player> Player;
        public List<Player> Player1 { get => Player; set => Player = value; }

        private int currentPlayer; //Chỉ số của mảng Player
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        private TextBox playerName;
        public TextBox PlayerName { get => playerName; set => playerName = value; }

        private PictureBox playerMark;
        public PictureBox PlayerMark { get => playerMark; set => playerMark = value; }

        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        

        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
        {
            add { playerMarked += value; }
            remove { playerMarked -= value; }
        }

        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add { endedGame += value; }
            remove { endedGame -= value; }
        }
        private Stack<PlayInfor> playTimeLine;
        public Stack<PlayInfor> PlayTimeLine { get => playTimeLine; set => playTimeLine = value; }
        private string yourID;
        private string opponentID;
        #endregion

        #region Initialize Ctrl + .
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark, string yourID, string opponentID)
        {
            this.yourID = yourID;
            this.opponentID = opponentID;
            this.ChessBoard = chessBoard;
            this.playerName = playerName;
            this.playerMark = mark;
            this.Player = new List<Player>() 
            {
                new Player(yourID,Image.FromFile(Application.StartupPath + "\\Resources\\DauXXoaNen.png")),
                new Player(opponentID,Image.FromFile(Application.StartupPath + "\\Resources\\DauOxoaNen.png")),
            };
 
        }
        

        #endregion

        #region Methods
        public void DrawChessBoard()
        {
   
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();
            PlayTimeLine = new Stack<PlayInfor>();
            currentPlayer = 0;
            ChangePlayer();
            Matrix = new List<List<Button>>();
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    System.Diagnostics.Debug.WriteLine("Checking end game...");

                    btn.Click += Btn_Click;

                    ChessBoard.Controls.Add(btn);

                    Matrix[i].Add(btn);

                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;

            }

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null) return;
            //nên đưa file hình ảnh Resources vào chung thư mục Debug chứa file thực thi exe
            //btn.BackgroundImage = Image.FromFile("\\Resource\\DauXXoaNen.png");
            Mark(btn);
            PlayTimeLine.Push(new PlayInfor(GetChessPoint(btn), CurrentPlayer));
            CurrentPlayer = (CurrentPlayer == 1 ? 0 : 1);
            ChangePlayer();
            if (playerMarked != null)
            {
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));
            }
            if (isEndGame(btn))
            {

                EndGame();

            }
            
        }
        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];

            if (btn.BackgroundImage != null) return;

            

            //nên đưa file hình ảnh Resources vào chung thư mục Debug chứa file thực thi exe
            //btn.BackgroundImage = Image.FromFile("\\Resource\\DauXXoaNen.png");
            Mark(btn);
            PlayTimeLine.Push(new PlayInfor(GetChessPoint(btn), CurrentPlayer));
            CurrentPlayer = (CurrentPlayer == 1 ? 0 : 1);
            ChangePlayer();
            
            if (isEndGame(btn))
            {

                EndGame();

            }

        }
        private void EndGame()
        {
            if(endedGame != null)
            {
                endedGame(this, new EventArgs());
            }
        }
        public bool Undo()
        {
            if (PlayTimeLine.Count <= 0) return false;
            bool isUndo1 = UndoAStep();
            bool isUndo2 = UndoAStep();
            PlayInfor oldPoint = PlayTimeLine.Peek();
            CurrentPlayer = oldPoint.CurrentPlayer == 1 ? 0 : 1;
            return isUndo1 && isUndo2;
        }
        private bool UndoAStep()
        {
            if (PlayTimeLine.Count <= 0) return false;
            PlayInfor oldPoint = PlayTimeLine.Pop();
            Button btn = Matrix[oldPoint.Point.Y][oldPoint.Point.X];

            btn.BackgroundImage = null;

            if (PlayTimeLine.Count <= 0)
            {
                CurrentPlayer = 0;
            }
            else
            {
                oldPoint = PlayTimeLine.Peek();
                
            }

            ChangePlayer();
            return true;
        }
        private bool isEndGame(Button btn)
        {
            return (isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn));
        }
        private Point GetChessPoint(Button btn)
        {
            
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);

            return point;
        }
        private bool isEndHorizontal(Button btn) //kiểm tra thắng ngang
        {
            Point point = GetChessPoint(btn);
            int countLeft = 0;
            for(int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                {
                    break;
                }
            }
            int countRight = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }

            return countLeft+countRight == 5;
        }
        private bool isEndVertical(Button btn) //kiểm tra thắng dọc
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }
            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countTop + countBottom == 5;
        }
        private bool isEndPrimary(Button btn) //kiểm tra thắng chéo chính
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X - i < 0) break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }
            int countBottom = 0;
            for (int i = 0; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if(point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH) break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return (countTop + countBottom - 1) >= 5;

        }
        private bool isEndSub(Button btn) //kiểm tra thắng chéo phụ
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            // kiểm tra đi lên phải
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH && i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                int x = point.X + i;
                int y = point.Y - i;
                if (x >= Cons.CHESS_BOARD_WIDTH || y < 0) break;
                if (Matrix[y][x].BackgroundImage == btn.BackgroundImage)
                    countTop++;
                else
                    break;
            }

            int countBottom = 0;
            // kiểm tra đi xuống trái
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH && i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                int x = point.X - i;
                int y = point.Y + i;
                if (x < 0 || y >= Cons.CHESS_BOARD_HEIGHT) break;
                if (Matrix[y][x].BackgroundImage == btn.BackgroundImage)
                    countBottom++;
                else
                    break;
            }

            // cộng thêm 1 vì ô hiện tại bị đếm 2 lần
            return (countTop + countBottom - 1) >= 5;

        }
        
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            
        }
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion

    }
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint { get => clickedPoint; set => clickedPoint = value; }
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
