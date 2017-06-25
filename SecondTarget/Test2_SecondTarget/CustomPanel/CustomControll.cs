using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test2_SecondTarget.CustomPanel;

namespace Test2_SecondTarget {
    class CustomControll : Panel { 

        ICustomControllPainter viewPainter;
        CustomControllViewInfo viewInfo;
        Controller controller;

        public CustomControll() : base() {             
            viewPainter = DefinePainter();
        }

        protected virtual int BombsCount { get { return 5; } }

        protected virtual int SizeOfField { get { return 5; } }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);

            viewInfo = new CustomControllViewInfo(this, SizeOfField, this.Bounds, BombsCount);
        }

        protected virtual ICustomControllPainter DefinePainter() {
            return new PanelPainter();
        }

        override protected void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            viewPainter.Draw(e.Graphics, viewInfo);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left) {
                ChangePressed(e.Location);
                return;
            } else if ((e.Button == MouseButtons.Right)&&(!viewInfo.IsItFirstClick())) {
                Position clickPos = FindSelectedCellIndex(e.Location);
                if((viewInfo.IsFlagCanBeAdded(clickPos))&&(IsControllerInitialized())){
                    viewInfo.SetMarked(clickPos);
                    OpenResult result = controller.MarkCellWithAFlag(clickPos.X, clickPos.Y);
                    switch (result.situation) {
                        case Situations.Win: 
                            viewInfo.IsWin = true;
                            OpenBombCells(result);
                            ShowMessageAndAskUserForNewActions("Победа. Вы правильно пометили все бомбы. Начать заново?", false);
                        break;
                    case Situations.Defeat:
                            viewInfo.IsDefeated = true;
                            OpenBombCells(result);
                            ShowMessageAndAskUserForNewActions("Поражение. Вы неправильно пометили все бомбы. Начать заново?", true);
                            break;
                        case Situations.Empty:
                            Invalidate(viewInfo.GetCellBoundaries(clickPos));
                            break;
                        default:
                            break;

                    }
                }
            }
            else {
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left) return;

            ClearPressed();
        }
        void ChangePressed(Point location) {
            Position index = FindSelectedCellIndex(location);
            if ((index.IsValid)&&(!viewInfo.IsTaggedByFlag(index.X, index.Y))) {
                if (viewInfo.IsItFirstClick())
                    controller = new Controller(this, BombsCount, SizeOfField, index.X, index.Y);
                viewInfo.SetPressed(index);
                OpenResult result = controller.OpenCurrentCell(index.X, index.Y);

                switch (result.situation) {
                    case Situations.Defeat:
                        viewInfo.IsDefeated = true; 
                        OpenBombCells(result);
                        ShowMessageAndAskUserForNewActions("Поражение. Вы кликнули по бомбе. Начать заново?", true);
                        break;

                    case Situations.Win:
                        viewInfo.IsWin = true;
                        OpenArea(result);
                        OpenBombCells(result);
                        ShowMessageAndAskUserForNewActions("Победа. Вы открыли все пустые клетки. Начать заново?", false);
                        break;

                    case Situations.Empty:
                        viewInfo.SetOpened(index);
                        OpenArea(result);
                        break;
                        
                    default:
                            break;      
                }
            }
        }
        void OpenArea(OpenResult result) {
            if (result.OpenedCellsIndex.Count > 0) {
                viewInfo.CurrentOpenedCells.AddRange(result.OpenedCellsIndex);
                foreach (Position pos in viewInfo.CurrentOpenedCells.Where(p => !p.IsABomb())) {
                    viewInfo.SetOpened(pos);
                    if (pos.CountOfBombs > 0)
                        viewInfo.SetCountOfNeiborhoodBombs(pos, pos.CountOfBombs);
                    Invalidate(viewInfo.GetCellBoundaries(pos));
                }
            }
        }
        void ClearPressed() {
            foreach (LayoutCell cell in viewInfo.Cells)
                cell.IsPressed = false;
            if (viewInfo.isLastPressedCellValid()) { 
                Invalidate(viewInfo.GetLastPressedCellBoundaries());
                viewInfo.LastPressedCellsIndex = Position.Empty;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            ChangeMouseOver(e.Location);
        }

        void ChangeMouseOver(Point location) {
            Position index = FindSelectedCellIndex(location);
            if (!index.IsValid) {
            } else {
                if (viewInfo.Cells[index.X, index.Y].ViewType != LayoutCellViewType.Opened)
                    if (!viewInfo.LastSelectedCellsIndex.Equals(index)) {
                    viewInfo.Cells[index.X, index.Y].IsMouseOver = true;
                    if (viewInfo.LastSelectedCellsIndex.IsValid) {
                        viewInfo.UnsetMouseOver();
                        Invalidate(viewInfo.GetLastSelectedCellBoundaries());
                    }
                    Invalidate(viewInfo.GetCellBoundaries(index));
                    viewInfo.LastSelectedCellsIndex = index;
                }
            }
        }

        Position FindSelectedCellIndex(Point location) {
            for (int i = 0; i < viewInfo.Cells.GetLength(0); i++) {
                for (int j = 0; j < viewInfo.Cells.GetLength(0); j++) {
                    if (CheckBounds(viewInfo.Cells[i, j].Boundaries, location))
                        return new Position(i, j);
                }
            }
            return Position.Empty;
        }

        bool CheckBounds(Rectangle bounds, Point location) {
            return bounds.Contains(location);
        }
        public bool IsControllerInitialized() {
            return controller != null;
        }
        void OpenBombCells(OpenResult result) {
            foreach (Position position in result.OpenedCellsIndex.Where(p => p.IsABomb())) {                
                if(viewInfo.Cells[position.X,position.Y].ViewType == LayoutCellViewType.Flagged) {
                    viewInfo.SetBombFlagged(viewInfo.Cells[position.X, position.Y]);
                } else {
                    viewInfo.SetBlowUp(position);
                }
                Invalidate(viewInfo.GetCellBoundaries(position));
            }
        }
        void RestartGame() {
            viewInfo = new CustomControllViewInfo(this, SizeOfField, this.Bounds, BombsCount);
            Invalidate();
        }
        void RestartOrCloseApp(bool IsRestart) {
            if(IsRestart)
                RestartGame();
            else
                Application.Exit();
        }
        void ShowMessageAndAskUserForNewActions(String messageText, bool IsErrorIcon) {
            DialogResult dialogResult;
            if(IsErrorIcon)
            dialogResult = MessageBox.Show(messageText, "MMiner", MessageBoxButtons.YesNo, MessageBoxIcon.Error
                , MessageBoxDefaultButton.Button1);
            else
                dialogResult = MessageBox.Show(messageText, "MMiner", MessageBoxButtons.YesNo, MessageBoxIcon.Information
                , MessageBoxDefaultButton.Button1);
            RestartOrCloseApp(dialogResult == DialogResult.Yes);
        }
    }
}
