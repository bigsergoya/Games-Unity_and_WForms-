using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2_SecondTarget.CustomPanel;

namespace Test2_SecondTarget {
    interface ICustomControllPainter {
        void Draw(Graphics graphics, CustomControllViewInfo panelViewInfo);
    }
}
