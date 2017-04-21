using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Square
    {
        int i;
        int j;
        public enum type {Breaking, Unbreaking, Player, Enemy, Empty, Bonus}; 
        type squareType;
        public int I
        {
            get
            {
                return i;
            }

            set
            {
                i = value;
            }
        }

        public int J
        {
            get
            {
                return j;
            }

            set
            {
                j = value;
            }
        }

        internal type SquareType
        {
            get
            {
                return squareType;
            }

            set
            {
                squareType = value;
            }
        }

        public Square(int i, int j, type typ)
        {
            this.i = i;
            this.j = j;
            squareType = typ;
            /*switch (type)
            {
                case "breaking":
                    squareType = Square.type.Breaking;
                    break;
                case "unbreaking":
                    squareType = Square.type.Unbreaking;
                    break;
                case "player":
                    squareType = Square.type.Player;
                    break;
                case "enemy":
                    squareType = Square.type.Enemy;
                    break;
                default:
                    break;
            }*/
        }
    }
}
