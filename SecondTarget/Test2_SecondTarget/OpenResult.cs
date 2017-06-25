using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    class OpenResult {
        public OpenResult() { }
        public OpenResult(Situations situation) {
            this.situation = situation;
        }
        public OpenResult(int neiborhoodBombs) {
            CountOfNeiborhoodBombs = neiborhoodBombs;
        }
        public OpenResult(Situations situation,List<Position> openedCellsIndex) {
            this.situation = situation;
            OpenedCellsIndex = openedCellsIndex;
        }

        public Situations situation { get; set; } = Situations.Empty;
        public int CountOfNeiborhoodBombs { get; set; } = 0;
        public List<Position> OpenedCellsIndex { get; set; } = new List<Position>();
        public bool IsExplosion() {
            return situation == Situations.Defeat;
        }
        public bool IsWin() {
            return situation == Situations.Win;
        }
    }
}
