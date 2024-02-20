using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catch_Wpf.Persistence
{
    public class GameTable
    {
        private int m;
        private int[,] tabla;
        private bool e1;
        private bool e2;
        private bool p;
        private int[] _e1pos;
        private int[] _e2pos;
        private int[] _ppos;

        public int M { get { return m; } private set { m = value; } }
        public int[,] Tábla { get { return tabla; } set { tabla = value; } }
        public bool E1 { get { return e1; } set { e1 = value; } }
        public bool E2 { get { return e2; } set { e2 = value; } }
        public bool P { get { return p; } set { p = value; } }
        public int[] E1Pos { get { return _e1pos; } set { _e1pos = value; } }
        public int[] E2Pos { get { return _e2pos; } set { _e2pos = value; } }
        public int[] PPos { get { return _ppos; } set { _ppos = value; } }
        public int[] FirstBomb { get; set; }

        public GameTable(int m, int[,] t)
        {
            M = m;
            Tábla = t;
        }
        
        public void setE1(bool b)
        {
            if (!b)
            {
                E1Pos = new int[2];
                E1Pos[0] = M - 1;
                E1Pos[1] = 0;
                Tábla[E1Pos[0], E1Pos[1]] = 2;
            }
        }

        public void setE2(bool b)
        {
            if (!b)
            {
                E2Pos = new int[2];
                E2Pos[0] = M - 1;
                E2Pos[1] = M - 1;
                Tábla[E2Pos[0], E2Pos[1]] = 4;
            }
        }

        public void setP(bool b)
        {
            if (!b)
            {
                PPos = new int[2];
                PPos[0] = 0;
                PPos[1] = M / 2;
                Tábla[PPos[0], PPos[1]] = 3;
            }
        }
    }
}
