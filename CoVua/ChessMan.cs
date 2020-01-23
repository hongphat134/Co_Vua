using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoVua
{
    enum Statue {Hit,NotHit};
    class ChessMan
    {
        public string Name;
        public TurnPlay SoHuu;
        public Statue TrangThai;
        public Button btn;
        public Color clrText;
        public ChessMan() { }
        public ChessMan(string Name,TurnPlay SoHuu,Statue TrangThai,Color clrText) {
            this.Name = Name;
            this.SoHuu = SoHuu;
            this.TrangThai = TrangThai;
            this.clrText = clrText;
        }
    }
}
