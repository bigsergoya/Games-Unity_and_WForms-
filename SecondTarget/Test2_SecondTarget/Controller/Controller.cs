using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test2_SecondTarget {
    class Controller : IController {
        CustomControll view;
        GameField gameField; 
        public Controller(CustomControll view, int countOfBombs, int sizeOfCellsLine, int startClickX, int startClickY) {
            this.view = view;
            gameField = new GameField(sizeOfCellsLine, countOfBombs, startClickX, startClickY);
        }

        public OpenResult MarkCellWithAFlag(int i, int j) {
            return gameField.MarkCurrentCell(i, j);
        }

        public OpenResult OpenCurrentCell(int i, int j) {
            return gameField.OpenCurrentCell(i, j);
        }
    }
}
