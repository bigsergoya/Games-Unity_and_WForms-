using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Test2_SecondTarget.CustomPanel { 

    class PanelPainter : ICustomControllPainter {

        public PanelPainter() { }

        public void Draw(Graphics graphics, CustomControllViewInfo panelViewInfo) {
            DrawBackground(graphics, panelViewInfo);
            DrawCellsBackground(graphics, panelViewInfo);
            DrawCellsForeground(graphics, panelViewInfo);

            DrawCellsBorder(graphics, panelViewInfo);
        }
        protected void DrawBackground(Graphics graphics, CustomControllViewInfo panelViewInfo) {
            graphics.FillRectangle(Brushes.Gray, panelViewInfo.Bounds);
        }
        protected virtual void DrawCellsBackground(Graphics graphics, CustomControllViewInfo panelViewInfo) {
            foreach (LayoutCell cell in panelViewInfo.Cells) {
                if (cell.IsPressed)
                    graphics.FillRectangle(Brushes.Orange, cell.Boundaries);
                else if (cell.IsMouseOver)
                    graphics.FillRectangle(Brushes.Blue, cell.Boundaries);
                else
                    graphics.FillRectangle(Brushes.LightGray, cell.Boundaries);
            }
        }
        protected virtual void DrawCellsBorder(Graphics graphics, CustomControllViewInfo panelViewInfo) {
            foreach (LayoutCell cell in panelViewInfo.Cells) {
                graphics.DrawRectangle(Pens.Black, cell.Boundaries);
            }
        }
        protected virtual void DrawCellsForeground(Graphics graphics, CustomControllViewInfo panelViewInfo) {
            foreach (LayoutCell cell in panelViewInfo.Cells) {
                switch (cell.ViewType) {
                    case LayoutCellViewType.Closed:
                        break;
                    case LayoutCellViewType.Opened:
                        graphics.FillRectangle(Brushes.DarkGray, cell.Boundaries);
                        if (cell.NeiborhoodBombs > 0) {
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            graphics.DrawString(cell.NeiborhoodBombs.ToString(), new Font("Times", 12), Brushes.Black, 
                                cell.Boundaries.Location.X + cell.Boundaries.Width / 2, cell.Boundaries.Location.Y + cell.Boundaries.Height / 2, sf);
                        }
                        break;
                    case LayoutCellViewType.BlowUp:
                        graphics.FillEllipse(Brushes.Red, cell.Boundaries);
                        break;
                    case LayoutCellViewType.FlaggedBomb:
                        graphics.FillEllipse(Brushes.Green, cell.Boundaries);
                        break;
                    case LayoutCellViewType.Flagged:
                        //graphics.FillEllipse(Brushes.Blue, cell.Boundaries);
                        graphics.FillPolygon(Brushes.Yellow, CreateTriangle(cell.Boundaries));
                        break;
                }
            }
        }
        PointF[] CreateTriangle(System.Drawing.Rectangle boundaries) {
            List<PointF> Triangle = new List<PointF>();
            Triangle.Add(new Point(boundaries.X + boundaries.Width / 2, boundaries.Y));
            Triangle.Add(new Point(boundaries.X, boundaries.Y + boundaries.Height));
            Triangle.Add(new Point(boundaries.X + boundaries.Width, boundaries.Y + boundaries.Height));
            return Triangle.ToArray();
        }
    }
}
