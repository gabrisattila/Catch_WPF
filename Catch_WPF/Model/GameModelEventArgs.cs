using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_Wpf.Model
{
    public class GameModelEventArgs
    {
        private Boolean _gameover;
        private int win_lose; // win = 1 lose_because_catch = 0 lose_because_akna = 2 step = 3

        public Boolean Gameover { get { return _gameover; } }
        public int WinLose { get { return win_lose; } set { win_lose = value; } }

        public GameModelEventArgs(Boolean g, int i)
        {
            _gameover = g;
            win_lose = i;
        }
    }
}
