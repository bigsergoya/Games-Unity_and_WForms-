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
        const int stepUntilRefereshCount = 3;
        const int limitDistanceForEyesContact = 4;

        bool[,] map;
        Position playerPosition;
        List<Position> openList; //Алгоритм поиска маршрута перенести в отдельный класс.
        List<Position> closedList;
        List<Position> wayToPlayer;
        System.Random rnd;
        int countsUntilRefreshingTheWay;
        bool isWayToEnemyFounded;
        protected override void CreateEnemyModel(float i, float j)
        {
            GameObject gameModelsObject;
            gameModelsObject = GameObjectLoader.GetObjectsPrefabByName("KindSkeleton");
            gameModelsObject.transform.position = new Vector3(i, 0.5f, j);
        }

        public CleverEnemy(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
            rnd = new System.Random();
            banMoving = false;
        }
        private void Start()
        {
            isMoving = false;
            rnd = new System.Random();
            map = new bool[FindFieldSize("x"), FindFieldSize("z")];
            
            isWayToEnemyFounded = FindRouteToPlayer();
            countsUntilRefreshingTheWay = stepUntilRefereshCount;
            animationController = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
            //Die();
        }

        private int FindFieldSize(string dimension)
        {
            if(dimension == "x") //Стоит переделать
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
            if (banMoving)
            {
                return false;
            }
            //*************************************************
            if (countsUntilRefreshingTheWay == 0)
            {
                isWayToEnemyFounded = FindRouteToPlayer();
                countsUntilRefreshingTheWay = stepUntilRefereshCount;
            }
            //isWayToEnemyFounded = false;
            //*************************************************
            countsUntilRefreshingTheWay--;
            if (isWayToEnemyFounded)
                if(wayToPlayer.Count>0)
                {
                    direction = CalculateDirectionForNewStep(wayToPlayer.PopAt(0).GettPositionAsVector3(), transform.position);
                    //countsUntilRefreshingTheWay--;
                    return SwitchDirection(direction);
                }
            direction = (directionType)Enum.ToObject(typeof(directionType), rnd.Next(0, 3));
            return SwitchDirection(direction);
        }

        private directionType CalculateDirectionForNewStep(Vector3 position1, Vector3 position2)
        {
            //Vector3 nextPosition = position1 - position2;
            Vector3 nextPosition = new Vector3(position1.x - position2.x,
                0, position1.z - position2.z);


            if (nextPosition == Vector3.forward)
                return directionType.Forward;
            else if (nextPosition == Vector3.back)
                return directionType.Reverse;
            else if (nextPosition == Vector3.left)
                return directionType.Left;
            else if (nextPosition == Vector3.right)
                return directionType.Right;
            return directionType.Forward; 
        }
        bool SwitchDirection(directionType direction)
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
        bool IsPlayerOnEyeLine(Vector3 currentPosition, Vector3 lineDirection,int lineLong)
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
        private bool IsPlayerOnEyeContact(Vector3 currentPosition)
        {
            return ((IsPlayerOnEyeLine(this.transform.position, Vector3.forward, limitDistanceForEyesContact)) ||
                (IsPlayerOnEyeLine(this.transform.position, Vector3.back, limitDistanceForEyesContact)) ||
                (IsPlayerOnEyeLine(this.transform.position, Vector3.left, limitDistanceForEyesContact)) ||
                (IsPlayerOnEyeLine(this.transform.position, Vector3.right, limitDistanceForEyesContact)));
        }
        protected override void Execute()
        {
            isMoving = true;
            StartCoroutine(OnCoroutine());
        }
        IEnumerator OnCoroutine()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition, 2f * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
                animationController.SetFloat("Speed", 0);
                //print(" 2 ");
            }
            yield return new WaitForEndOfFrame();  
        }

        void AddGameObjectsArrayOnMap(GameObject[] objects)
        {
            foreach (GameObject objct in objects)
            {
                map[(int)objct.transform.position.x, (int)objct.transform.position.z] = true;
            }
        }
        void FillTheMap()
        {
            AddGameObjectsArrayOnMap(GameObject.FindGameObjectsWithTag("UnbreakingCube"));
            AddGameObjectsArrayOnMap(GameObject.FindGameObjectsWithTag("BreakingCube"));
            AddGameObjectsArrayOnMap(GameObject.FindGameObjectsWithTag("ExitCube"));
        }
        double CalculatePriceForCurrentCell(int x, int z, int x_1, int z_1)
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
                    double price = CalculatePriceForCurrentCell(curCell.X + directionToX, curCell.Z+directionToZ,
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
            
            //Если у соседей точки, которая выбрана текущей, не расчитана стоимость, то считаем. КРЕСТ!
            //Учет непроходимости!!! Если непроходима, то пишем МаксИнт или просто не трогаем.
        }
        bool FindRouteToPlayer()
        {
            FillTheMap();
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
                curCell = FindNewCurrentPoint(openList); // Поиск новой текущей точки с наименьшем F
                closedList.Add(curCell);
                openList.Remove(curCell);
                //Расчет пути не всегда корректен. Мб проблема с бонусами или в самом алгоритме.

                WorkingWithNeiborghoodCells(curCell);
            }
            if (!(openList.Count < 1))
            {
                wayToPlayer = new List<Position>();
                print("Done!!! Way founded/");
                //Position calculatedPlayerPosition = openList.Where(pos => (pos.X == playerPosition.X) && (pos.Z == playerPosition.Z)).First();
                CalculateWay();
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
                print("Failure!!! Way is not calculated");
                return false;
            }

        }
        void CalculateWay()
        {
            Position closedListPosition = openList.Where(pos => (pos.X == playerPosition.X) && (pos.Z == playerPosition.Z)).First();//closedList.Where(pos => pos == foundedPlayerPositionInOpenList.RelativeCell).First();
            wayToPlayer.Add(closedListPosition);
            while (closedList.Count > 0)
            {
                if (closedListPosition.RelativeCell == null)
                {
                    //print("It is not a bag, it's a feature");
                    //print(closedListPosition.X + " " + closedListPosition.Z + " ");
                    //print("It is not a bag, it's a feature);
                    break;
                } 
                closedListPosition = closedList.Where(
                    pos => pos == closedListPosition.RelativeCell).First(); //Тут есть баг!!!!
                wayToPlayer.Add(closedListPosition);
                closedList.Remove(closedListPosition); // Мб просто цикл сделать до 1?
            }
            wayToPlayer.Remove(wayToPlayer.Find(point => ((point.X ==transform.position.x) 
                && (point.Z ==transform.position.z))));
            wayToPlayer.Reverse();
            //wayToPlayer.Remove(wayToPlayer[0]);
        }
        private Position FindNewCurrentPoint(List<Position> points)
        {
            
            return points.Where(pos => pos.F == points.Min(ps => ps.F)).First();
        }

        protected override void OnDestroy()
        {
            //BaseExplosion.EnemyDestroyEvent(transform.position);
        }

        protected override void Die()
        {
            //if (banMoving)
            //{
            //    return;
            //}
            animationController.SetTrigger("Die");
            source.PlayOneShot(dieSound, soundVolume);
            banMoving = true;
            var colChildren = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colChildren)
            {
                collider.enabled = false;
            }
        }
    }
}
