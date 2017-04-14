using Assets.Scripts.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BaseActiveModels: MonoBehaviour
    {
        protected Vector3 targetPosition;
        protected enum directionType { Forward, Reverse, Left, Right };
        protected directionType direction;
        protected bool isMoving;

        //public abstract bool CanExecute();


        protected virtual bool IsCollisionWithWallOrCube(Vector3 transformPositions, Vector3 targetPositions)
        {
            RaycastHit hit;
            if (Physics.Linecast(transformPositions, targetPositions, out hit))
            {
                if ((hit.collider.gameObject.tag == "UnbreakingCube") ||
                    (hit.collider.gameObject.tag == "BreakingCube"))
                {
                    //print("No way man!");
                    return true;
                    //CollisionWithGameObject();
                }
                
            }
            return false;
            
        }
        /*protected void Start()
        {
            isMoving = false;
            rnd = new System.Random();
        }*/
        protected bool SetTargetPositionAndCheckCollisions(Vector3 direction, directionType dirType)
        {
            targetPosition = SetTargetPosition(transform.position, direction);
            if (!IsCollisionWithWallOrCube(transform.position, targetPosition))
            {
                TurnFace(dirType);
                return true;
            }
            return false;
        }
        protected Vector3 SetTargetPosition(Vector3 transformPosition, Vector3 target)
        {
            return new Vector3(
                transformPosition.x + target.x,
                transformPosition.y + target.y,
                transformPosition.z + target.z);
        }
        protected void PlaceModel(float i, float j)
        {
            CreateModel(i, j);
            isMoving = false;
        }
        protected abstract void CreateModel(float i, float j);
        protected abstract void OnDestroy();

        protected void TurnFace(directionType faceDirection)
        {
            switch (faceDirection)
            {
                case directionType.Forward:
                    if ((this.transform.eulerAngles.y < 0) || (this.transform.eulerAngles.y > 1))
                    {
                        this.transform.Rotate(this.transform.rotation.x,
                            0 - this.transform.eulerAngles.y,
                            this.transform.rotation.z
                            );
                    }
                    break;
                case directionType.Reverse:
                    if ((this.transform.eulerAngles.y < 179) || (this.transform.eulerAngles.y > 181))
                    {
                        this.transform.Rotate(this.transform.rotation.x,
                        180 - this.transform.eulerAngles.y,
                        this.transform.rotation.z
                        );
                    }
                    break;
                case directionType.Left:
                    if ((this.transform.eulerAngles.y < 269) || (this.transform.eulerAngles.y > 271))
                    {
                        this.transform.Rotate(this.transform.rotation.x,
                        270 - this.transform.eulerAngles.y,
                        this.transform.rotation.z
                        );
                    }
                    break;
                case directionType.Right:
                    if ((this.transform.eulerAngles.y < 89) || (this.transform.eulerAngles.y > 91))
                    {
                        this.transform.Rotate(this.transform.rotation.x,
                    90 - this.transform.eulerAngles.y,
                    this.transform.rotation.z
                    );
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
