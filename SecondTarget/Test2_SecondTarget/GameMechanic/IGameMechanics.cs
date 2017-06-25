using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    interface IGameMechanics {
        OpenResult MarkCellWithAFlag(int i, int j, LogicalCell[,] gameMap);
        OpenResult OpenCurrentCell(int i, int j, LogicalCell[,] gameMap);
        void PlantBombs(LogicalCell[,] gameMap, int bombsCount, int startX, int startY);
    }
}
