using Assets.Scripts;
using Assets.Scripts.Bonuses;
using Assets.Scripts.PassiveObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Field : ResourcesLoader
    {
        int length;
        int countOfBreakingWalls;
        private GameObject currentGameObject;
        private List<Square> squares;
        private Player player;
        private Square playerPos;
        public int Length
        {
            get
            {
                return length;
            }
        }

        public int CountOfBreakedWalls
        {
            get
            {
                return countOfBreakingWalls;
            }
        }

        internal Player Player
        {
            get
            {
                return player;
            }
        }

        internal Square PlayerPos
        {
            get
            {
                return playerPos;
            }

            set
            {
                playerPos = value;
            }
        }

        public Field(int length, int countOfBreakedWalls)
        {
            this.length = length;
            this.countOfBreakingWalls = countOfBreakedWalls;
            squares = new List<Square>();
        }
        bool IsCurrentPointInBorderLine (int i, int j, int length)
        {
            return (i == 0 || j == 0) || (i == length - 1 || j == length - 1);
        }
        bool IsCurrentPointColumn(int i, int j, int length)
        {
            return ((((j & 1) == 0)) && (((i & 1) == 0)) && ((i > 1) && (i < (length - 2)))) ;
        }
        void CreateFloor(int i, int j)
        {
            currentGameObject = GetFieldPrefab();
            currentGameObject.transform.position = new Vector3(i, 0.0f, j);
        }
        void CreateUnbreakingWall(int i, int j)
        {
            currentGameObject = GetUnbreakingWallPrefab();
            currentGameObject.transform.position = new Vector3(i, 0.5f, j);
        }
        void CreateBreakingWall(int i, int j)
        {
            currentGameObject = GetBreakingWallPrefab();
            currentGameObject.transform.position = new Vector3(i, 0.5f, j);
            countOfBreakingWalls--;
        }
        void PlaceBreakingWalls()
        { //Нужно перемешать только пустые клетки!!!!
            //List<Square> emptySquares = new List<Square>();
            List<Square> emptySquares = new List<Square>();
            emptySquares.AddRange(squares.FindAll(sq => sq.SquareType.Equals(Square.type.Empty)));
            ListExtensions.Shuffle<Square>(emptySquares);
            int i = 0;
            while (countOfBreakingWalls > 0)
            {
                CreateBreakingWall(emptySquares[i].I, emptySquares[i].J);
                countOfBreakingWalls--;
                squares.Find(sq => sq.Equals(emptySquares[i])).SquareType = Square.type.Breaking;
                i++;
            }
            ListExtensions.Shuffle<Square>(squares);
        }
        void PlacePlayer()
        {
            playerPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(playerPos)).SquareType = Square.type.Player;
            player = new Player(playerPos.I, playerPos.J);
        }
        void PlaceEnemy()
        {
            Square enemyPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(enemyPos)).SquareType = Square.type.Enemy;
            SimpleEnemy enemy = new SimpleEnemy(enemyPos.I, enemyPos.J);
        }
        void PlaceBonusRadius()
        {
            Square BonusPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(BonusPos)).SquareType = Square.type.Bonus;
            BonusBombRadius bonus = new BonusBombRadius(BonusPos.I, BonusPos.J);
        }
        void PlaceNoClipBonus()
        {
            Square BonusPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(BonusPos)).SquareType = Square.type.Bonus;
            BonusNoClip bonus = new BonusNoClip(BonusPos.I, BonusPos.J);
        }
        void PlaceBombCountBonus()
        {
            Square BonusPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(BonusPos)).SquareType = Square.type.Bonus;
            BonusBombCount bonus = new BonusBombCount(BonusPos.I, BonusPos.J);
        }
        void PlaceSpeedBonus()
        {
            Square BonusPos = squares.Find(sq => sq.SquareType.Equals(Square.type.Empty));
            squares.Find(sq => sq.Equals(BonusPos)).SquareType = Square.type.Bonus;
            BonusSpeed bonus = new BonusSpeed(BonusPos.I, BonusPos.J);
        }
        public void Create()
        {
            //var playField = getBreakingWallPrefab();
            
            //GameObject GameObjects;
            
            
            PlaceUnbreakingWallsAndFloor();
            PlaceBreakingWalls();
            PlacePlayer();
            PlaceEnemy();
            PlaceEnemy();
            //#333333
            PlaceBonusRadius();
            PlaceNoClipBonus();
            PlaceBombCountBonus();
            PlaceSpeedBonus();
        }
        private void PlaceUnbreakingWallsAndFloor()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    CreateFloor(i, j);
                    if (IsCurrentPointInBorderLine(i, j, length))
                    {
                        squares.Add(new Square(i, j, Square.type.Unbreaking));
                        CreateUnbreakingWall(i, j);
                    }
                    else
                    {
                        if (IsCurrentPointColumn(i, j, length))
                        {
                            squares.Add(new Square(i, j, Square.type.Unbreaking));
                            CreateUnbreakingWall(i, j);
                        }
                        else
                        {
                            squares.Add(new Square(i, j, Square.type.Empty));
                        }

                    }
                }
            }
        }
        //...
    }
}
