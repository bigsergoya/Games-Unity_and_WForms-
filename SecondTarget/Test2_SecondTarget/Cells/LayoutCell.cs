using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    enum LayoutCellViewType { Closed, Opened, BlowUp, Flagged, FlaggedBomb };
    class LayoutCell {
        bool isMouseOver;
        bool isPressed;
        readonly Rectangle boundaries;
        int neiborhoodBombs;

        public LayoutCell(Rectangle boundaries) {
            isMouseOver = false;
            isPressed = false;
            this.boundaries = boundaries;
            neiborhoodBombs = 0;
        }

        public bool IsMouseOver { get { return isMouseOver; } set { isMouseOver = value; } }
        public bool IsPressed { get { return isPressed; } set { isPressed = value; } }
        public Rectangle Boundaries { get { return boundaries; } }
        public LayoutCellViewType ViewType { get; set; } = LayoutCellViewType.Closed;
        public int NeiborhoodBombs { get { return neiborhoodBombs; } set { neiborhoodBombs = value; } }

    }
}
