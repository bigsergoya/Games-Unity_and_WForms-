using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    class GameField {
        LogicalCell[,] gameMap;
        int mapSize; 
        int maxBombsCount;
        IGameMechanics strategy;

        public GameField(int mapSize, int bombsCount, int startX, int startY) {
            Initialize(mapSize, bombsCount, startX, startY);
        }

        protected virtual void Initialize(int mapSize, int bombsCount, int startX, int startY) {
            if (bombsCount >= mapSize * mapSize)
                throw new ArgumentException("Too many bombsCount");
            if (bombsCount <= 0)
                throw new ArgumentException("BombsCount less or equal zero");
            if (mapSize <= 1)
                throw new ArgumentException("MapSize less or equal 1");

            this.mapSize = mapSize;
            this.maxBombsCount = bombsCount;
            CreateEmptyField();
            strategy = DefineStrategy();
            PlantBombs(startX, startY);
        }

        void CreateEmptyField() {
            gameMap = new LogicalCell[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
                for (int j = 0; j < mapSize; j++)
                    gameMap[i, j] = new LogicalCell();
        }
        void PlantBombs(int startX, int startY) {
            strategy.PlantBombs(gameMap, maxBombsCount, startX, startY);
        }
        protected virtual IGameMechanics DefineStrategy() {
            return new StandartMechanics();
        }
        public OpenResult OpenCurrentCell(int i, int j) {
            return strategy.OpenCurrentCell(i, j, gameMap);
        }
        public OpenResult MarkCurrentCell(int i, int j) {
            return strategy.MarkCellWithAFlag(i, j, gameMap);
        }
    }
}
