using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget.CustomPanel {
    class CustomControllViewInfo {
        CustomControll view;
        LayoutCell[,] cells;
        Rectangle bounds; 
        Position lastSelectedCellsIndex;
        Position lastPressedCellsIndex;
        bool isDefeated;
        bool isWin;
        int countOfFlags;
        int countOfBombs;
        List<Position> currentOpenedCells;

        public CustomControllViewInfo(CustomControll view, int cellCount, Rectangle bounds, int countOfBombs) {
            currentOpenedCells = new List<Position>();
            isDefeated = false;
            lastSelectedCellsIndex = new Position(-1, -1);
            this.view = view;
            cells = new LayoutCell[cellCount, cellCount];
            int cellWidth = bounds.Width / cellCount;
            int cellHeight = bounds.Height / cellCount;
            for (int i = 0; i < cellCount; i++) {
                for (int j = 0; j < cellCount; j++) {
                    cells[i, j] = (new LayoutCell(new Rectangle(bounds.X + cellWidth * j, bounds.Y + cellHeight * i, cellWidth, cellHeight)));
                }
            }
            this.bounds = bounds;
            this.countOfBombs = countOfBombs;
        }

        public bool IsItFirstClick() {
            return (lastPressedCellsIndex == null);
        }
        public LayoutCell[,] Cells {
            get { return cells; }
        }
        public Rectangle Bounds {
            get { return bounds; }
        }
        public List<Position> CurrentOpenedCells { get { return currentOpenedCells; } }
        public bool IsDefeated { get { return isDefeated; } set { isDefeated = value; } }
        public bool IsWin { get { return isWin; } set { isWin = value; } }

        public Position LastSelectedCellsIndex {
            get { return lastSelectedCellsIndex; }
            set { lastSelectedCellsIndex = value; }
        }
        public Position LastPressedCellsIndex {
            get { return lastPressedCellsIndex; }
            set { lastPressedCellsIndex = value; }
        }
        public void SetPressed(Position position) {
            Cells[position.X, position.Y].IsPressed = true;
            LastPressedCellsIndex = position;
        }
        public void UnsetMouseOver() {
            Cells[LastSelectedCellsIndex.X, LastSelectedCellsIndex.Y].IsMouseOver = false;
        }
        public Rectangle GetCellBoundaries(Position position) {
            return Cells[position.X, position.Y].Boundaries;
        }
        public Rectangle GetLastPressedCellBoundaries() {
            return GetCellBoundaries(lastPressedCellsIndex);
        }
        public Rectangle GetLastSelectedCellBoundaries() {
            return GetCellBoundaries(lastSelectedCellsIndex);
        }
        public void SetBombFlagged(Position position) {
            Cells[position.X, position.Y].ViewType = LayoutCellViewType.FlaggedBomb;
        }
        public void SetBombFlagged(LayoutCell cell) {
            cell.ViewType = LayoutCellViewType.FlaggedBomb;
        }
        public void SetBlowUp(Position position) {
            Cells[position.X, position.Y].ViewType = (Cells[position.X, position.Y].ViewType == LayoutCellViewType.Flagged)
                ? LayoutCellViewType.FlaggedBomb : LayoutCellViewType.BlowUp;
        }
        public void SetCountOfNeiborhoodBombs(Position position, int count) {
            Cells[position.X, position.Y].NeiborhoodBombs = count;
        }
        public void SetOpened(Position position) {
            if (Cells[position.X, position.Y].ViewType != LayoutCellViewType.Closed) return;

            Cells[position.X, position.Y].ViewType = LayoutCellViewType.Opened;
        }
        public bool IsTaggedByFlag(int x, int y) {
            return cells[x, y].ViewType == LayoutCellViewType.Flagged;
        }
        public bool isLastPressedCellValid() {
            return ((lastPressedCellsIndex != null)&&(lastPressedCellsIndex.IsValid));
        }
        public bool IsFlagCanBeAdded(Position position) {
            LayoutCellViewType currentCellViewType = Cells[position.X, position.Y].ViewType;
            if (currentCellViewType != LayoutCellViewType.Closed && currentCellViewType != LayoutCellViewType.Flagged) return false;
            return true;
        }
        public void SetMarked(Position position) {
            LayoutCellViewType currentCellViewType = Cells[position.X, position.Y].ViewType;
            if (currentCellViewType == LayoutCellViewType.Flagged) {
                Cells[position.X, position.Y].ViewType = LayoutCellViewType.Closed;
                countOfFlags--;
            } else {
                if (countOfFlags < countOfBombs) { 
                    Cells[position.X, position.Y].ViewType = LayoutCellViewType.Flagged;
                    countOfFlags++;
                } 
            }
        }
    }
}
