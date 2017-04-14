using Assets.Scripts.BaseClasses;
using Assets.Scripts.Loaders;
using Assets.Scripts.PositionClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ActiveObjects
{
    class CleverEnemy : BaseEnemy
    {
        const int stepUntilRefereshCount = 2;
        const int limitDistanceForEyesContact = 4;
        bool[,] map;
        //double[,] prices;
        Position playerPosition;

        List<Position> openList; //Алгоритм поиска маршрута перенести в отдельный класс.
        List<Position> closedList;
        List<Position> wayToPlayer;
        System.Random rnd;
        int countsUntilRefreshingTheWay;
        bool isWayToEnemyFounded;
        bool isEnemyFounded;
        protected override void CreateEnemyModel(float i, float j)
        {
            GameObject gameModelsObject;
            gameModelsObject = EnemyLoader.GetEnemyPrefab();
            gameModelsObject.transform.position = new Vector3(i, 1.0f, j);
        }

        public CleverEnemy(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
            rnd = new System.Random();

        }
        private void Start()
        {
            isMoving = false;
            rnd = new System.Random();
            map = new bool[findFieldSize("x"), findFieldSize("z")];
            
            isWayToEnemyFounded = findRouteToPlayer();
            countsUntilRefreshingTheWay = stepUntilRefereshCount;
        }

        private int findFieldSize(string dimension)
        {
            if(dimension == "x") //ПЛохой код, обязательно потом переделать
                return (int)GameObject.FindGameObjectsWithTag("FieldCell").Max(pos => pos.transform.position.x)+1;
            else
                return (int)GameObject.FindGameObjectsWithTag("FieldCell").Max(pos => pos.transform.position.z)+1;
        }
        protected override bool CanExecute()
        {
            if (isMoving)
            {
                return true;
            }

            //Проверка на то, является ли первая позиция - той же самой где и стоим.
            if (countsUntilRefreshingTheWay == 0)
            {
                isWayToEnemyFounded = findRouteToPlayer();
                countsUntilRefreshingTheWay = stepUntilRefereshCount;
            }
            if (isWayToEnemyFounded)
                if(wayToPlayer.Count>0)
                {
                    direction = calculateDirectionForNewStep(wayToPlayer.PopAt(0).GettPositionAsVector3(), transform.position);
                    countsUntilRefreshingTheWay--;
                    return switchDirection(direction);
                }
            direction = (directionType)Enum.ToObject(typeof(directionType), rnd.Next(0, 3));
            return switchDirection(direction);
        }

        private directionType calculateDirectionForNewStep(Vector3 position1, Vector3 position2)
        {
            Vector3 nextPosition = position1 - position2;
            if (nextPosition == Vector3.forward)
                return directionType.Forward;
            else if (nextPosition == Vector3.back)
                return directionType.Reverse;
            else if (nextPosition == Vector3.left)
                return directionType.Left;
            else if (nextPosition == Vector3.right)
                return directionType.Right;
            return directionType.Forward; //Костыль, нуль он не хочет принимать((
        }
        bool switchDirection(directionType direction)
        {
            if (direction == directionType.Forward)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.forward, directionType.Forward);
            }
            if (direction == directionType.Left)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.left, directionType.Left);
            }
            if (direction == directionType.Right)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.right, directionType.Right);
            }
            if (direction == directionType.Reverse)
            {
                return SetTargetPositionAndCheckCollisions(Vector3.back, directionType.Reverse);
            }

            return false;
        }
        bool isPlayerOnLine(Vector3 currentPosition, Vector3 lineDirection,int lineLong)
        {
            RaycastHit hit;
            if ((Physics.Raycast(currentPosition,
                SetTargetPosition(currentPosition, lineDirection * lineLong), out hit))
                && (hit.transform.tag == "Player"))
            {
                return true;
            }
            return false;
        }
        private bool isPlayerOnEyeContact(Vector3 currentPosition)
        {
            return ((isPlayerOnLine(this.transform.position, Vector3.forward, limitDistanceForEyesContact)) ||
                (isPlayerOnLine(this.transform.position, Vector3.back, limitDistanceForEyesContact)) ||
                (isPlayerOnLine(this.transform.position, Vector3.left, limitDistanceForEyesContact)) ||
                (isPlayerOnLine(this.transform.position, Vector3.right, limitDistanceForEyesContact)));
        }
        private void Execute()
        {
            isMoving = true;
            StartCoroutine(onCoroutine());
        }
        void Update()
        {
            if (CanExecute())
            {
                Execute();
            }
        }
        IEnumerator onCoroutine()
        {

            //print("Enemy Step");
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition, 2f * Time.deltaTime);
            //print(" 2 " + transform.position.z);
            if (CollisionWithEnemy())
            {
                // Дополнить. Убить
            }
            if (transform.position == targetPosition)
            {
                isMoving = false;
                //print(" 2 ");
            }
            yield return new WaitForEndOfFrame();  //или Нуль, подумать.
                                                   //yield return null;
        }

        void AddGameObjectsArrayOnMap(GameObject[] objects)
        {
            foreach (GameObject objct in objects)
            {
                map[(int)objct.transform.position.x, (int)objct.transform.position.z] = true;
            }
        }
        void fillTheMap()
        {
            AddGameObjectsArrayOnMap(GameObject.FindGameObjectsWithTag("UnbreakingCube"));
            AddGameObjectsArrayOnMap(GameObject.FindGameObjectsWithTag("BreakingCube"));
        }
        double calculatePriceForCurrentCell(int x, int z, int x_1, int z_1)
        {
            return (Math.Sqrt(Math.Pow((x-x_1), 2) + Math.Pow((z- z_1), 2)));
        }
        void WorkingWithCurrentNeiborghoodCell(int directionToX, int directionToZ, Position curCell)
        {
            if ((!map[curCell.X+directionToX, curCell.Z+directionToZ]) && 
                (!closedList.Any(p => (p.X == curCell.X + directionToX) && (p.Z == curCell.Z+directionToZ))))
            {
                if (!openList.Any(p => (p.X == curCell.X + directionToX) && (p.Z == curCell.Z+directionToZ)))
                {
                    double price = calculatePriceForCurrentCell(curCell.X + directionToX, curCell.Z+directionToZ,
                        playerPosition.X, playerPosition.Z);
                    double PriceAndStepPrice = 10 + price;
                    Position addingPoint = new Position(curCell.X + directionToX, curCell.Z+directionToZ, PriceAndStepPrice, price, 10, curCell);
                    openList.Add(addingPoint);
                }
            }
        }
        void WorkingWithNeiborghoodCells(Position curCell)
        {
            WorkingWithCurrentNeiborghoodCell(1, 0, curCell);
            WorkingWithCurrentNeiborghoodCell(-1, 0, curCell);
            WorkingWithCurrentNeiborghoodCell(0, 1, curCell);
            WorkingWithCurrentNeiborghoodCell(0, -1, curCell);
            //Цикл по каждой соседней клетке
            /*if ((!map[curCell.X+1, curCell.Z])&&(!closedList.Any(p => (p.X == curCell.X+1)&&(p.Z == curCell.Z))))
            {
                if (!openList.Any(p => (p.X == curCell.X + 1) && (p.Z == curCell.Z)))
                {
                    double price = calculatePriceForCurrentCell(curCell.X + 1, curCell.Z,
                        playerPosition.X, playerPosition.Z);
                    double PriceAndStepPrice = 10 + price;
                    Position addingPoint = new Position(curCell.X + 1, curCell.Z, PriceAndStepPrice, price, 10, curCell);
                    openList.Add(addingPoint);
                }
            }
            if ((!map[curCell.X - 1, curCell.Z]) && (!closedList.Any(p => (p.X == curCell.X - 1) && (p.Z == curCell.Z))))
            {
                if (!openList.Any(p => (p.X == curCell.X - 1) && (p.Z == curCell.Z)))
                {
                    double price = calculatePriceForCurrentCell(curCell.X - 1, curCell.Z,
                        playerPosition.X, playerPosition.Z);
                    double PriceAndStepPrice = 10 + price;
                    Position addingPoint = new Position(curCell.X - 1, curCell.Z, PriceAndStepPrice, price, 10, curCell);
                    openList.Add(addingPoint);
                }
            }
            if ((!map[curCell.X, curCell.Z+1]) && (!closedList.Any(p => (p.X == curCell.X) && (p.Z == curCell.Z+1))))
            {
                if (!openList.Any(p => (p.X == curCell.X) && (p.Z == curCell.Z + 1)))
                {
                    double price = calculatePriceForCurrentCell(curCell.X, curCell.Z+1,
                        playerPosition.X, playerPosition.Z);
                    double PriceAndStepPrice = 10 + price;
                    Position addingPoint = new Position(curCell.X, curCell.Z+1, PriceAndStepPrice, price, 10, curCell);
                    openList.Add(addingPoint);
                }
            }
            if ((!map[curCell.X, curCell.Z-1]) && (!closedList.Any(p => (p.X == curCell.X) && (p.Z == curCell.Z-1))))
            {
                if (!openList.Any(p => (p.X == curCell.X) && (p.Z == curCell.Z - 1)))
                {
                    double price = calculatePriceForCurrentCell(curCell.X, curCell.Z-1,
                        playerPosition.X, playerPosition.Z);
                    double PriceAndStepPrice = 10 + price;
                    Position addingPoint = new Position(curCell.X, curCell.Z-1, PriceAndStepPrice, price, 10, curCell);
                    openList.Add(addingPoint);
                }
            }
            */
            //Если у соседей точки, которая выбрана текущей, не расчитана стоимость, то считаем. КРЕСТ!
            //Учет непроходимости!!! Если непроходима, то пишем МаксИнт или просто не трогаем.
        }
        bool findRouteToPlayer()
        {
            fillTheMap();
            openList = new List<Position>(); //Алгоритм поиска маршрута перенести в отдельный класс.
            closedList = new List<Position>();
            Position curCell = new Position(this.transform.position.x, this.transform.position.z,0,0,0, null);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return false;

            playerPosition = new Position(player.transform.position.x,
                player.transform.position.z, 0,0,0, null);
            openList.Add(curCell);
            while ((openList.Count>0)&&(!openList.Any(p => (p.X == playerPosition.X) && (p.Z == playerPosition.Z)))) // Пока добавим в открытый список позицию игрока или пока не кончится открытый список
            {
                curCell = findNewCurrentPoint(openList); // Поиск новой текущей точки с наименьшем F
                closedList.Add(curCell);
                openList.Remove(curCell);
                //Для четырех клеток соседних

                WorkingWithNeiborghoodCells(curCell);
            }
            if (!(openList.Count < 1))
            {
                wayToPlayer = new List<Position>();
                print("Done!!! Way founded/");
                //Position calculatedPlayerPosition = openList.Where(pos => (pos.X == playerPosition.X) && (pos.Z == playerPosition.Z)).First();
                calculateWay();
                print(wayToPlayer.Count);
                foreach (Position way in wayToPlayer)
                {
                    print(way.X + " " + way.Z + " ");
                }
                print("Done!!! Way calculated");
                return true;
                //Start Position this.transform.position
            }
            else {
                print("Failure!!! Way calculated");
                return false;
            }

        }
        void calculateWay()
        {
            Position closedListPosition = openList.Where(pos => (pos.X == playerPosition.X) && (pos.Z == playerPosition.Z)).First();//closedList.Where(pos => pos == foundedPlayerPositionInOpenList.RelativeCell).First();
            wayToPlayer.Add(closedListPosition);
            while (closedList.Count > 1)
            {
                if (closedListPosition.RelativeCell == null)
                {
                    //print("null");
                    //print(closedListPosition.X + " " + closedListPosition.Z + " ");
                    //print("null");
                    break;
                } 
                closedListPosition = closedList.Where(
                    pos => pos == closedListPosition.RelativeCell).First(); //Тут есть баг!!!!
                wayToPlayer.Add(closedListPosition);
                closedList.Remove(closedListPosition); // Мб просто цикл сделать до 1?
            }
            wayToPlayer.Reverse();
            //wayToPlayer.Remove(wayToPlayer[0]);
        }
        private Position findNewCurrentPoint(List<Position> points)
        {
            
            return points.Where(pos => pos.F == points.Min(ps => ps.F)).First();
        }

        protected override void OnDestroy()
        {
            //BaseExplosion.EnemyDestroyEvent(transform.position);
        }
    }
}
