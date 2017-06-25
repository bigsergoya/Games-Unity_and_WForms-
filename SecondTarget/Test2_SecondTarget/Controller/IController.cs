using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test2_SecondTarget {
    interface IController {
        OpenResult MarkCellWithAFlag(int i, int j);
        OpenResult OpenCurrentCell(int i, int j);
    }
}
