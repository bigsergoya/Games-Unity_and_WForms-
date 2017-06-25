using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {  
    class StandartMechanics : IGameMechanics {
        public OpenResult MarkCellWithAFlag(int i, int j, LogicalCell[,] gameMap) {
            gameMap[i, j].IsMarkedWithFlag = !gameMap[i, j].IsMarkedWithFlag;
            if (gameMap[i, j].IsMarkedWithFlag)
                return IsAllFlagsPlacedWrong(gameMap);
            else
                return (new OpenResult(Situations.Empty));
        }

        private OpenResult IsAllFlagsPlacedWrong(LogicalCell[,] gameMap) {
            List<Position> bombsPosition = new List<Position>();
            int flag = 0;
            int correctFlag = 0;
            for (int i = 0; i < gameMap.GetLength(0); i++)
                for (int j = 0; j < gameMap.GetLength(0); j++) {
                    if (gameMap[i, j].IsMarkedWithFlag) {
                        flag++;
                        if (gameMap[i, j].IsABomb) {
                            correctFlag++;
                        }
                    }
                    if (gameMap[i, j].IsABomb)
                        bombsPosition.Add(new Position(i, j,-1));
                }
            if ((flag == bombsPosition.Count)) {
                if (flag == correctFlag)
                    return new OpenResult(Situations.Win, bombsPosition);
                else
                    return new OpenResult(Situations.Defeat, bombsPosition);
            } else
                return new OpenResult(Situations.Empty);
        }

        private bool IsAllBombsMarked(LogicalCell[,] gameMap) {
            foreach(LogicalCell cell in gameMap) {
                if ((cell.IsABomb) && (!cell.IsMarkedWithFlag))
                    return false;
            }
            return true;
        }
        private bool IsAllCellsExceptBombsAreMarked(LogicalCell[,] gameMap) {
            foreach (LogicalCell cell in gameMap) {
                if ((!cell.IsABomb) && (!cell.IsOpened))
                    return false;
            }
            return true;
        }

        List<Position> GetPositionsOfdBombs(LogicalCell[,] gameMap) {
            List<Position> positions = new List<Position>();
            for(int i = 0; i < gameMap.GetLength(0); i++) 
                for(int j = 0; j < gameMap.GetLength(0); j++){ 
                    if (gameMap[i,j].IsABomb)
                        positions.Add(new Position(i, j,-1));
            }
            return positions;
        }

        public OpenResult OpenCurrentCell(int i, int j, LogicalCell[,] gameMap) {
            if (!IsCurrentCellIsOpened(i, j, gameMap)) {
                MarkCellAsOpened(i, j, gameMap);
                if (gameMap[i, j].IsABomb)
                    return new OpenResult(Situations.Defeat, GetPositionsOfdBombs(gameMap));
                else {
                    List<Position> positions = new List<Position>();
                    OpenMapSector(i, j, gameMap, positions);
                    Situations curSituation;
                    if (IsAllCellsExceptBombsAreMarked(gameMap)) {
                        positions.AddRange(GetPositionsOfdBombs(gameMap));
                        curSituation = Situations.Win;
                    } else
                        curSituation = Situations.Empty;
                    return new OpenResult(curSituation, positions);
                }
            } else
                return new OpenResult();
        }

    public void PlantBombs(LogicalCell[,] gameMap, int bombsCount, int startX, int startY) {
        Random random = new Random();
        int plantedBombs = 0;
        while (plantedBombs < bombsCount) {
         int x = random.Next(0, gameMap.GetLength(0) - 1);
         int y = random.Next(0, gameMap.GetLength(0) - 1);
         LogicalCell cell = gameMap[x, y];
         if ((cell.IsABomb) || ((x == startX) && (y == startY)))
             continue;
         else
             cell.IsABomb = true;
         plantedBombs++;
        }
        #region
        /*gameMap[0, 0].IsABomb = true;
        gameMap[0, 1].IsABomb = true;
        gameMap[1, 0].IsABomb = true;
        gameMap[1, 1].IsABomb = true;
        gameMap[2, 0].IsABomb = true;*/
        #endregion
        }

        bool IsCurrentCellIsOpened(int i, int j, LogicalCell[,] gameMap) {
            return gameMap[i, j].IsOpened;
        }
        bool IsCurrentCellIsOnMap(int i, int j, int mapSize) {
            return ((i >= 0) && (i < mapSize)
                && (j >= 0) && (j < mapSize));
        }
        bool CheckTheCellForBomb(int i, int j, LogicalCell[,] gameMap) {
            if (IsCurrentCellIsOnMap(i, j, gameMap.GetLength(0)))
                return (gameMap[i, j].IsABomb);
            return false;
        }

        int NeighborBombsCount(int i, int j, LogicalCell[,] gameMap) {
            List<bool> result = new List<bool>() {
                CheckTheCellForBomb(i + 1, j + 1, gameMap),
                CheckTheCellForBomb(i + 1, j - 1, gameMap),
                CheckTheCellForBomb(i - 1, j + 1, gameMap),
                CheckTheCellForBomb(i - 1, j - 1, gameMap),
                CheckTheCellForBomb(i - 1, j, gameMap),
                CheckTheCellForBomb(i, j - 1, gameMap),
                CheckTheCellForBomb(i + 1, j, gameMap),
                CheckTheCellForBomb(i, j + 1, gameMap)
            };
            return result.Count(c => c);
        }
        void OpenMapSector(int i, int j, LogicalCell[,] gameMap, List<Position> positions) {            
            int countOfBombsInCurrentCell = NeighborBombsCount(i, j, gameMap);
            MarkCellAsOpened(i, j, gameMap);
            if (countOfBombsInCurrentCell > 0) {
                positions.Add(new Position(i, j, countOfBombsInCurrentCell));
                return;
            } else {
                positions.Add(new Position(i, j));
            }
            
            //Обследование окрестности фон Неймана первого порядка
            if (IsCurrentCellIsOnMap(i + 1, j, gameMap.GetLength(0)))
                if (!IsCurrentCellIsOpened(i + 1, j, gameMap))
                    OpenMapSector(i + 1, j, gameMap, positions);

            if (IsCurrentCellIsOnMap(i - 1, j, gameMap.GetLength(0)))
                if (!IsCurrentCellIsOpened(i - 1, j, gameMap))
                    OpenMapSector(i - 1, j, gameMap, positions);

            if (IsCurrentCellIsOnMap(i, j + 1, gameMap.GetLength(0)))
                if (!IsCurrentCellIsOpened(i, j + 1, gameMap))
                    OpenMapSector(i, j + 1, gameMap, positions);

            if (IsCurrentCellIsOnMap(i, j - 1, gameMap.GetLength(0)))
                if (!IsCurrentCellIsOpened(i, j - 1, gameMap))
                    OpenMapSector(i, j - 1, gameMap, positions);
        }
        void MarkCellAsOpened(int i, int j, LogicalCell[,] gameMap) {
            gameMap[i, j].IsOpened = true;
        }
    }
}
