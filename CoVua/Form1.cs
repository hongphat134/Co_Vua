using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoVua
{
    enum TurnPlay { Player1,Player2};
    public partial class frmChessKing : Form
    {
        #region Properties
        Button[,] lstChessBoard;

        List<ChessMan> lstCBPlayer1;

        List<ChessMan> lstCBPlayer2;

        List<Point> MovesOfChessMan;

        ChessMan OldChessMan;

        TurnPlay CurrentPlayer;
        #endregion

        private int IndexOf(int nrow,Button btn)
        {
            for(int i=0;i<Cons.ChessBoardSize;i++)
            {
                if (lstChessBoard[nrow, i] == btn) return i;
            }
            return -1;
        }
        private Point GetLocationFromChessMan(ChessMan QuanCo)
        {
            int nrow = Int32.Parse(QuanCo.btn.Tag.ToString());
            int ncol = IndexOf(nrow, QuanCo.btn);
            return new Point(nrow, ncol);
        }
        private ChessMan GetChessManByButton(Button btn,TurnPlay Curr)
        {
            if(Curr==TurnPlay.Player1)
            {
                foreach (ChessMan cm in lstCBPlayer1)
                {
                    if (cm.btn == btn) return cm;
                }
            }
            else
            {
                foreach (ChessMan cm in lstCBPlayer2)
                {
                    if (cm.btn == btn) return cm;
                }
            }
            return null;
        }
        private void CreateEventForChessMan(TurnPlay Curr)
        {
            if (Curr == TurnPlay.Player1) for (int i = 0; i < lstCBPlayer1.Count; i++) lstCBPlayer1[i].btn.Click += ChessMan_Click;
            else for (int i = 0; i < lstCBPlayer2.Count; i++) lstCBPlayer2[i].btn.Click += ChessMan_Click;                       
        }

        private void DeleteEventFromChessMan(TurnPlay Curr)
        {
            if (Curr == TurnPlay.Player1) for (int i = 0; i < lstCBPlayer1.Count; i++) lstCBPlayer1[i].btn.Click -= ChessMan_Click;
            else for (int i = 0; i < lstCBPlayer2.Count; i++) lstCBPlayer2[i].btn.Click -= ChessMan_Click; 
        }

        private void HandleTheEnemy(int HoanhDoKeDich, int TungDoKeDich)
        {
            ChessMan NeedToFind = GetChessManByButton(lstChessBoard[HoanhDoKeDich,TungDoKeDich], CurrentPlayer == TurnPlay.Player1 ? TurnPlay.Player2 : TurnPlay.Player1);
            if (NeedToFind != null) MovesOfChessMan.Add(new Point(HoanhDoKeDich, TungDoKeDich));
        }
        private List<Point> GetTypeOfChessMan(ChessMan QuanCo)
        {
            MovesOfChessMan.Clear();
            switch(QuanCo.Name)
            {
                case "Xe":
                    return Xe(QuanCo);
                case "Ngựa":
                    return Ngua(QuanCo);
                case "Tượng":
                    return Tuong(QuanCo);
                case "Hậu":
                    return Hau(QuanCo);
                case "Vua":
                    return Vua(QuanCo);
                //Default là Chốt
                default:
                    return Chot(QuanCo);
            }
        }
        private void HandleTheWayOfChessMan(ChessMan QuanCo,Color clr)
        {
            List<Point> CacDiemCoTheDi = GetTypeOfChessMan(QuanCo);
            if (clr == Color.Aquamarine)
            {
                foreach (Point point in CacDiemCoTheDi)
                {
                    lstChessBoard[point.X, point.Y].BackColor = clr;
                    lstChessBoard[point.X, point.Y].Click += Move_Click;
                }
            }
            else
            {
                foreach (Point point in CacDiemCoTheDi)
                {
                    lstChessBoard[point.X, point.Y].BackColor = clr;
                    lstChessBoard[point.X, point.Y].Click -= Move_Click;
                }
            }
        }

        private void DeleteChessMan(ChessMan QuanCo)
        {
            int Height;
            int Width;
            if (CurrentPlayer == TurnPlay.Player1)
            {
                if (lstCBPlayer2.Count % 2 == 0)
                {
                    Height = (((Cons.ChessBoardSize * 2) - lstCBPlayer2.Count) * (Cons.ChessBoardHeight - 20))/2;
                    Width = 0;                    
                }
                else
                {
                    Height = ((((Cons.ChessBoardSize * 2) - lstCBPlayer2.Count) * (Cons.ChessBoardHeight - 20))/2)-20;
                    Width = 60;
                }
                Button ShowBtnRemove = new Button()
                {
                    Location = new Point(Width, Height),
                    Size = new Size(Cons.ChessBoardWidth - 20, Cons.ChessBoardHeight - 20),
                    Text = QuanCo.btn.Text,
                    BackColor = Color.White
                };            
                pnllstCB2Remove.Controls.Add(ShowBtnRemove);
                lstCBPlayer2.Remove(QuanCo);
            }
            else
            {
                if (lstCBPlayer1.Count % 2 == 0)
                {
                    Height = (((Cons.ChessBoardSize * 2) - lstCBPlayer1.Count) * (Cons.ChessBoardHeight - 20)) / 2;
                    Width = 0;
                }
                else
                {
                    Height = ((((Cons.ChessBoardSize * 2) - lstCBPlayer1.Count) * (Cons.ChessBoardHeight - 20)) / 2) - 20;
                    Width = 60;
                }
                Button ShowBtnRemove = new Button()
                {
                    Location = new Point(Width, Height),
                    Size = new Size(Cons.ChessBoardWidth - 20, Cons.ChessBoardHeight - 20),
                    Text = QuanCo.btn.Text,
                    BackColor = Color.White
                };

                pnllstCB1Remove.Controls.Add(ShowBtnRemove);
               
                lstCBPlayer1.Remove(QuanCo);
            }
        }
        void Move_Click(object sender, EventArgs e)
        {
            Button Move = sender as Button;            
            HandleTheWayOfChessMan(OldChessMan, Color.White);
            if (Move.Text != "")
            {
                ChessMan Remove = GetChessManByButton((Button)sender, CurrentPlayer == TurnPlay.Player1 ? TurnPlay.Player2 : TurnPlay.Player1);
                DeleteChessMan(Remove);
                if (IsWin())
                {
                    MessageBox.Show(CurrentPlayer == TurnPlay.Player1 ? "Người chơi 1 thắng!" : "Người chơi 2 thắng");
                    pnlChessBoard.Enabled = false;
                    btnEndGame.Visible = true;
                }
            }
          
            Move.Text = OldChessMan.Name;
            if (OldChessMan.btn.Text == "Chốt")
            {
                if (IsBecomeQueen(Int32.Parse(Move.Tag.ToString())))
                {
                    OldChessMan.Name = "Hậu";
                    Move.Text = "Hậu";
                }
            }
            Move.ForeColor = OldChessMan.clrText;
            OldChessMan.btn.Text = "";
            OldChessMan.btn.Click -= ChessMan_Click;
            OldChessMan.TrangThai = Statue.NotHit;
            OldChessMan.btn = Move;
            OldChessMan.btn.Click += ChessMan_Click;
            DeleteEventFromChessMan(CurrentPlayer);
            CurrentPlayer = CurrentPlayer == TurnPlay.Player1 ? TurnPlay.Player2 : TurnPlay.Player1;
            CreateEventForChessMan(CurrentPlayer);
            
            
        }

        void ChessMan_Click(object sender, EventArgs e)
        {
            ChessMan QuanCo=GetChessManByButton((Button)sender, CurrentPlayer);

            if (QuanCo.TrangThai == Statue.Hit)
            {
                HandleTheWayOfChessMan(QuanCo, Color.White);
                QuanCo.TrangThai = Statue.NotHit;
                OldChessMan = null;
            }
            else
            {
                QuanCo.TrangThai = Statue.Hit;
                if (OldChessMan != null)
                {
                    HandleTheWayOfChessMan(OldChessMan, Color.White);
                    OldChessMan.TrangThai = Statue.NotHit;
                }
                OldChessMan = QuanCo;
                HandleTheWayOfChessMan(QuanCo, Color.Aquamarine);
            }
        }
        private void PutChessManOnChessBoard()
        {
            int i=0;
            //Đặt các quân cờ hàng đầu
            for (; i < Cons.ChessBoardSize; i++)
            {
                lstCBPlayer1[i].btn = lstChessBoard[0, i];
                lstCBPlayer1[i].btn.Click += ChessMan_Click;
                lstChessBoard[0, i].Text = lstCBPlayer1[i].Name;
                lstChessBoard[0, i].ForeColor = lstCBPlayer1[i].clrText;
            }
            //Đặt các quân cờ chốt
            for (; i < Cons.ChessBoardSize * 2; i++)
            {
                lstCBPlayer1[i].btn = lstChessBoard[1, i - Cons.ChessBoardSize];
                lstCBPlayer1[i].btn.Click += ChessMan_Click;
                lstChessBoard[1, i - Cons.ChessBoardSize].Text = lstCBPlayer1[i].Name;
                lstChessBoard[1, i - Cons.ChessBoardSize].ForeColor = lstCBPlayer1[i].clrText;
            }

            //Đặt các quân cờ hàng đầu
            for (i=0; i < Cons.ChessBoardSize; i++)
            {
                lstCBPlayer2[i].btn = lstChessBoard[Cons.ChessBoardSize-1, i];             
                lstChessBoard[Cons.ChessBoardSize - 1, i].Text = lstCBPlayer2[i].Name;
                lstChessBoard[Cons.ChessBoardSize - 1, i].ForeColor = lstCBPlayer2[i].clrText;
            }
            //Đặt các quân cờ chốt
            for (; i < Cons.ChessBoardSize * 2; i++)
            {
                lstCBPlayer2[i].btn = lstChessBoard[Cons.ChessBoardSize-2, i - Cons.ChessBoardSize];              
                lstChessBoard[Cons.ChessBoardSize - 2, i - Cons.ChessBoardSize].Text = lstCBPlayer2[i].Name;
                lstChessBoard[Cons.ChessBoardSize - 2, i - Cons.ChessBoardSize].ForeColor = lstCBPlayer2[i].clrText;
            }           
        }

        private void LoadChessMan()
        {
            lstCBPlayer1 = new List<ChessMan>() { 
                new ChessMan("Xe",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Ngựa",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Tượng",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Hậu",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Vua",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Tượng",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Ngựa",TurnPlay.Player1,Statue.NotHit,Color.Red),
                new ChessMan("Xe",TurnPlay.Player1,Statue.NotHit,Color.Red)
            };
            //8 chốt
            for (int i = 0; i < 8; i++) lstCBPlayer1.Add(new ChessMan("Chốt", TurnPlay.Player1, Statue.NotHit,Color.Red));

            lstCBPlayer2 = new List<ChessMan>() { 
                new ChessMan("Xe",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Ngựa",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Tượng",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Hậu",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Vua",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Tượng",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Ngựa",TurnPlay.Player2,Statue.NotHit,Color.Blue),
                new ChessMan("Xe",TurnPlay.Player2,Statue.NotHit,Color.Blue)
            };
            //8 chốt
            for (int i = 0; i < 8; i++) lstCBPlayer2.Add(new ChessMan("Chốt", TurnPlay.Player2, Statue.NotHit, Color.Blue));
        }
       
        private void CreateChessBoard()
        {
            lstChessBoard = new Button[Cons.ChessBoardSize,Cons.ChessBoardSize];
            CurrentPlayer = TurnPlay.Player1;
            for(int i=0;i<Cons.ChessBoardSize;i++)
            {              
                for(int j=0;j<Cons.ChessBoardSize;j++)
                {
                    lstChessBoard[i, j] = new Button()
                    {
                        Location=new Point(j*Cons.ChessBoardWidth,Cons.ChessBoardHeight*i),
                        Size=new System.Drawing.Size(Cons.ChessBoardWidth,Cons.ChessBoardHeight),
                        Tag=i.ToString(),
                        BackColor=Color.White
                    };
                    pnlChessBoard.Controls.Add(lstChessBoard[i, j]);
                }
            }
        }

        public frmChessKing()
        {
            InitializeComponent();
           
        }

        #region Đường đi của các loại quân cờ
        private List<Point> Xe(ChessMan QuanCo)
        {
            Point GetLocation = GetLocationFromChessMan(QuanCo);

            //Duyệt bên trái
            if (GetLocation.Y != 0)
            {
                for (int i = GetLocation.Y - 1; i >= 0; i--)
                {
                    if (lstChessBoard[GetLocation.X, i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X, i));
                    else
                    {
                        HandleTheEnemy(GetLocation.X, i);                       
                        break;
                    }
                }
            }

            //Duyệt bên phải
            if (GetLocation.Y != Cons.ChessBoardSize-1)
            {
                for (int i = GetLocation.Y + 1; i < Cons.ChessBoardSize; i++)
                {
                    if (lstChessBoard[GetLocation.X, i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X, i));
                    else
                    {
                        HandleTheEnemy(GetLocation.X, i);
                        break;
                    }
                }
            }

            //Duyệt bên trên
            if (GetLocation.X != 0)
            {
                for (int i = GetLocation.X - 1; i >= 0; i--)
                {
                    if (lstChessBoard[i, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(i, GetLocation.Y));
                    else
                    {
                        HandleTheEnemy(i, GetLocation.Y);
                        break;
                    }
                }
            }

            //Duyệt bên dưới
            if (GetLocation.X != Cons.ChessBoardSize-1)
            {
                for (int i = GetLocation.X + 1; i < Cons.ChessBoardSize; i++)
                {
                    if (lstChessBoard[i, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(i, GetLocation.Y));
                    else
                    {
                        HandleTheEnemy(i, GetLocation.Y);
                        break;
                    }
                }
            }
            return MovesOfChessMan;
        }

        private List<Point> Ngua(ChessMan QuanCo)
        {
            Point GetLocation = GetLocationFromChessMan(QuanCo);

            if (GetLocation.X + 1 < Cons.ChessBoardSize && GetLocation.Y + 2 < Cons.ChessBoardSize)
            {
                if (lstChessBoard[GetLocation.X + 1, GetLocation.Y + 2].Text=="")   MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y + 2));
                else   HandleTheEnemy(GetLocation.X + 1, GetLocation.Y + 2);
            }
            if (GetLocation.X + 1 < Cons.ChessBoardSize && GetLocation.Y - 2 >= 0)
            {
                if (lstChessBoard[GetLocation.X + 1, GetLocation.Y - 2].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y - 2));
                else HandleTheEnemy(GetLocation.X + 1, GetLocation.Y - 2);
            }
            if (GetLocation.X - 1 >= 0 && GetLocation.Y + 2 < Cons.ChessBoardSize)
            {
                if (lstChessBoard[GetLocation.X - 1, GetLocation.Y + 2].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y + 2));
                else HandleTheEnemy(GetLocation.X - 1, GetLocation.Y + 2);
            }
            if (GetLocation.X - 1 >= 0 && GetLocation.Y - 2 >= 0)
            {
                if (lstChessBoard[GetLocation.X - 1, GetLocation.Y - 2].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y - 2));
                else HandleTheEnemy(GetLocation.X - 1, GetLocation.Y - 2);
            }
            if (GetLocation.X + 2 < Cons.ChessBoardSize && GetLocation.Y + 1 < Cons.ChessBoardSize)
            {
                if (lstChessBoard[GetLocation.X + 2, GetLocation.Y + 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 2, GetLocation.Y + 1));
                else HandleTheEnemy(GetLocation.X + 2, GetLocation.Y + 1);
            }
            if (GetLocation.X + 2 < Cons.ChessBoardSize && GetLocation.Y - 1 >= 0)
            {
                if (lstChessBoard[GetLocation.X + 2, GetLocation.Y - 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 2, GetLocation.Y - 1));
                else HandleTheEnemy(GetLocation.X + 2, GetLocation.Y - 1);
            }
            if (GetLocation.X - 2 >= 0 && GetLocation.Y + 1 < Cons.ChessBoardSize)
            {
                if (lstChessBoard[GetLocation.X - 2, GetLocation.Y + 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 2, GetLocation.Y + 1));
                else HandleTheEnemy(GetLocation.X - 2, GetLocation.Y + 1);
            }
            if (GetLocation.X - 2 >= 0 && GetLocation.Y - 1 >= 0)
            {
                if (lstChessBoard[GetLocation.X - 2, GetLocation.Y - 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 2, GetLocation.Y - 1));
                else HandleTheEnemy(GetLocation.X - 2, GetLocation.Y - 1);
            }

            return MovesOfChessMan;
        }

        private List<Point> Tuong(ChessMan QuanCo)
        {
            Point GetLocation = GetLocationFromChessMan(QuanCo);

            //Duyệt chéo chính trên
            if(GetLocation.X!=0&&GetLocation.Y!=0)
            {
                for(int i=1;GetLocation.X-i>=0 && GetLocation.Y-i>=0;i++)
                {
                    if (lstChessBoard[GetLocation.X - i, GetLocation.Y - i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - i, GetLocation.Y - i));
                    else {
                        HandleTheEnemy(GetLocation.X - i, GetLocation.Y - i);
                        break;
                    }
                }
            }

            //Duyệt chéo chính dưới
            if (GetLocation.X < Cons.ChessBoardSize - 1 && GetLocation.Y < Cons.ChessBoardSize - 1)
            {
                for (int i = 1; GetLocation.Y+i<Cons.ChessBoardSize &&GetLocation.X + i<Cons.ChessBoardSize; i++)
                {
                    if (lstChessBoard[GetLocation.X + i, GetLocation.Y + i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + i, GetLocation.Y + i));
                    else
                    {
                        HandleTheEnemy(GetLocation.X + i, GetLocation.Y + i);
                        break;
                    }
                }
            }

            //Duyệt chéo phụ trên
            if(GetLocation.X!=0&&GetLocation.Y<Cons.ChessBoardSize-1)
            {
                for (int i = 1 ;GetLocation.X-i>=0&&GetLocation.Y+i<Cons.ChessBoardSize; i++)
                {
                    if (lstChessBoard[GetLocation.X - i, GetLocation.Y + i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - i, GetLocation.Y + i));
                    else
                    {
                        HandleTheEnemy(GetLocation.X - i, GetLocation.Y + i);
                        break;
                    }
                }
            }
            //Duyệt chéo phụ dưới
            if (GetLocation.Y != 0 && GetLocation.X < Cons.ChessBoardSize - 1)
            {
                for (int i = 1; GetLocation.X + i < Cons.ChessBoardSize && GetLocation.Y - i >=0; i++)
                {
                    if (lstChessBoard[GetLocation.X + i, GetLocation.Y - i].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + i, GetLocation.Y - i));
                    else
                    {
                        HandleTheEnemy(GetLocation.X + i, GetLocation.Y - i);
                        break;
                    }
                }
            }
            return MovesOfChessMan;
        }

        private List<Point> Hau(ChessMan QuanCo)
        {
            MovesOfChessMan = Xe(QuanCo);
            MovesOfChessMan = Tuong(QuanCo);
            return MovesOfChessMan;
        }

        private List<Point> Vua(ChessMan QuanCo)
        {
            Point GetLocation = GetLocationFromChessMan(QuanCo);
            if(GetLocation.X - 1 >= 0)
            {
                if(GetLocation.Y - 1 >= 0)
                {
                    if (lstChessBoard[GetLocation.X - 1, GetLocation.Y - 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y - 1));
                    else   HandleTheEnemy(GetLocation.X - 1, GetLocation.Y - 1);
                }

                if(GetLocation.Y + 1 < Cons.ChessBoardSize)
                {
                    if (lstChessBoard[GetLocation.X - 1, GetLocation.Y + 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y + 1));
                    else HandleTheEnemy(GetLocation.X - 1, GetLocation.Y + 1);
                }

                if (lstChessBoard[GetLocation.X - 1, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y));
                else HandleTheEnemy(GetLocation.X - 1, GetLocation.Y);
            }

            if(GetLocation.X + 1 < Cons.ChessBoardSize)
            {
                if (GetLocation.Y - 1 >= 0)
                {
                    if (lstChessBoard[GetLocation.X + 1, GetLocation.Y - 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y - 1));
                    else HandleTheEnemy(GetLocation.X + 1, GetLocation.Y - 1);
                }

                if (GetLocation.Y + 1 < Cons.ChessBoardSize)
                {
                    if (lstChessBoard[GetLocation.X + 1, GetLocation.Y + 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y + 1));
                    else HandleTheEnemy(GetLocation.X + 1, GetLocation.Y + 1);
                }

                if (lstChessBoard[GetLocation.X + 1, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y));
                else HandleTheEnemy(GetLocation.X + 1, GetLocation.Y);
            }


            if (GetLocation.Y - 1 >= 0)
            {
                if (lstChessBoard[GetLocation.X, GetLocation.Y - 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X, GetLocation.Y - 1));
                else HandleTheEnemy(GetLocation.X, GetLocation.Y - 1);
            }

            if (GetLocation.Y + 1 < Cons.ChessBoardSize)
            {
                if (lstChessBoard[GetLocation.X, GetLocation.Y + 1].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X, GetLocation.Y + 1));
                else HandleTheEnemy(GetLocation.X, GetLocation.Y + 1);
            }
            return MovesOfChessMan;
        }

        private List<Point> Chot(ChessMan QuanCo)
        {
            Point GetLocation = GetLocationFromChessMan(QuanCo);
            if (CurrentPlayer==TurnPlay.Player1)
            {
                if (lstChessBoard[GetLocation.X + 1, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 1, GetLocation.Y));
                if (GetLocation.X == 1)
                {
                    if (lstChessBoard[GetLocation.X + 2, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X + 2, GetLocation.Y));
                }
                if(GetLocation.Y-1>=0)  HandleTheEnemy(GetLocation.X + 1, GetLocation.Y - 1);
                if(GetLocation.Y+1<Cons.ChessBoardSize) HandleTheEnemy(GetLocation.X + 1, GetLocation.Y + 1);
            }
            else
            {
                if (lstChessBoard[GetLocation.X - 1, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 1, GetLocation.Y));
                if (GetLocation.X == Cons.ChessBoardSize - 2)
                {
                    if (lstChessBoard[GetLocation.X - 2, GetLocation.Y].Text == "") MovesOfChessMan.Add(new Point(GetLocation.X - 2, GetLocation.Y));
                }

                if (GetLocation.Y - 1 >= 0) HandleTheEnemy(GetLocation.X - 1, GetLocation.Y - 1);
                if (GetLocation.Y + 1 < Cons.ChessBoardSize) HandleTheEnemy(GetLocation.X - 1, GetLocation.Y + 1);
            }
            return MovesOfChessMan;
        }
        #endregion

        #region Kiểm tra chiến thắng

        private bool IsWin()
        {
            if(CurrentPlayer==TurnPlay.Player1)
            {
                foreach (ChessMan cm in lstCBPlayer2)
                {
                    if (cm.Name == "Vua") return false;
                }
            }
            else
            {
                foreach (ChessMan cm in lstCBPlayer1)
                {
                    if (cm.Name == "Vua") return false;
                }
            }
            return true;
        }


        private bool IsBecomeQueen(int ViTri)
        {
            if(CurrentPlayer==TurnPlay.Player1)
            {
                if (ViTri == Cons.ChessBoardSize - 1) return true;
            }
            else
            {
                if (ViTri == 0) return true;
            }
            return false;
        }

        #endregion

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            lstCBPlayer1.Clear();
            lstCBPlayer2.Clear();
            pnlChessBoard.Controls.Clear();
            pnllstCB1Remove.Controls.Clear();
            pnllstCB2Remove.Controls.Clear();
            MovesOfChessMan.Clear();
            OldChessMan = null;
            pnlBackGround.Visible = false;
            btnStartGame.Visible = true;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.pnlBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBackGround.Visible = true;
            pnlChessBoard.Enabled = true;
            btnStartGame.Visible = false;
            CreateChessBoard();
            LoadChessMan();
            PutChessManOnChessBoard();
            MovesOfChessMan = new List<Point>();
        }

    }
}
