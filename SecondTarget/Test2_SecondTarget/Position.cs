using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    public enum Situations { Defeat, Win, Empty };
    class Position {
        static Position empty = new Position(-1, -1);
        public static Position Empty { get { return empty; } }
        public Position(int x, int y) {
            X = x;
            Y = y;
        }
        public Position(int x, int y, int countOfBombs) {
            X = x;
            Y = y;
            CountOfBombs = countOfBombs;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int CountOfBombs { get; set; } = 0;
        
        public bool IsValid {
            get {
                return X != -1 && Y != -1;
            }
        }
        public bool IsABomb() {
            return CountOfBombs == -1;
        }
        public override bool Equals(object obj) {
            Position other = obj as Position;
            if (other == null) return false;

            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode() {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
