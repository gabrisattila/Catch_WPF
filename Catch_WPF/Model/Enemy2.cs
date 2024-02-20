using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catch_Wpf.Persistence;

namespace Catch_Wpf.Model
{
    public class Enemy2
    {
        /*
         * VALUE: 4
         */

        private int[] pos;
        private Player player;
        private bool _isAlive;

        public Player Wanted { get { return player; } set { player = value; } }
        public int[] Pos { get { return pos; } set { pos = value; } }
        public bool Alive { get { return _isAlive; } set { _isAlive = value; } }

        public Enemy2(int x, int y, Player p)
        {
            Pos = new int[2];
            Pos[0] = x;
            Pos[1] = y;
            Alive = true;
            Wanted = p;
        }

        public Pair<int, char> merreKéne(int[] ep, int[] pp)
        {
            int sáv;
            char irány;

            int táv;
            if (Math.Abs(pp[0] - ep[0]) >
                Math.Abs(pp[1] - ep[1]))
            {
                sáv = 0;
                táv = pp[0] - ep[0];
                if (táv > 0)
                {
                    irány = '+';
                }
                else
                {
                    irány = '-';
                }

            }
            else
            {
                sáv = 1;
                táv = pp[1] - ep[1];
                if (táv > 0)
                {
                    irány = '+';
                }
                else
                {
                    irány = '-';
                }
            }

            Pair<int, char> r = new Pair<int, char>(sáv, irány);
            return r;
        }

        public int[] next(Pair<int, char> sav_irany)
        {
            int[] vs = new int[2];
            vs[0] = Pos[0];
            vs[1] = Pos[1];
            if (sav_irany.First == 0)
            {
                if (sav_irany.Second == '+')
                {
                    vs[0]++;
                    return vs;
                }
                else
                {
                    vs[0]--;
                    return vs;
                }
            }
            else
            {
                if (sav_irany.Second == '+')
                {
                    vs[1]++;
                    return vs;
                }
                else
                {
                    vs[1]--;
                    return vs;
                }
            }
        }

        public void Move(GameTable t, int[] next)
        {
            if (Alive)
            {
                if (next[0] < 0 || next[0] > t.M - 1 || next[1] < 0 || next[1] > t.M - 1)
                {
                    next = Pos;
                }
                // Akna
                if (t.Tábla[next[0], next[1]] == 1 ||
                    t.Tábla[next[0], next[1]] == 6)
                {
                    t.Tábla[Pos[0], Pos[1]] = 0;
                    Pos = next;
                    t.Tábla[Pos[0], Pos[1]] = 7;
                    Alive = false;
                }
                // elkapás
                else if (t.Tábla[next[0], next[1]] == 3)
                {
                    t.Tábla[Pos[0], Pos[1]] = 0;
                    Pos = next;
                    t.Tábla[Pos[0], Pos[1]] = 4;
                    Wanted.Get_Caught = true;
                    return;
                }
                // field
                else
                {
                    t.Tábla[Pos[0], Pos[1]] = 0;
                    Pos = next;
                    t.Tábla[Pos[0], Pos[1]] = 4;
                }
            }
            else
            {
                return;
            }
        }
    }
}
