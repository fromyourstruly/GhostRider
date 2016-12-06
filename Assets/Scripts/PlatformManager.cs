using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {up, left, right, down };

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager current;
    private GameObject platform;
    private GameObject inactivePlat;
    public GameObject platUP;
    public GameObject platLEFT;
    public GameObject platRIGHT;

    public GameObject[] trees;

    public int turnvariable;
    public int maxPlatNum;
    private int platCount;
    private int var = 0;

    public float speed;
    private bool[] stopdir = new bool[] { false, false, false };
    private bool platEnd = false;
    private int[] DirArray = new int [] {1, 1, 1};
    private int[] DirProbability = new int[20];

    private Direction[] OrientPlan;
    private Direction[] DirectPlan;
    private Queue platformQueue;
    private int activePlatformCount = 8;
    private int treeCount = 3;

    private float straightz;
    private float straightx;
    private float turnx;
    private float turnz;
    private Direction Orientation;
    private Direction LastDirection;

    private Vector3 post = new Vector3(0, 0, 0); //basically the dummy position vector of for the current platform
    private float roat = 0; //how much to rotate the platform by such that the collider will always be turned to the end of the platform, connecting to the next one

    void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start()
    {
        straightz = platUP.GetComponent<Renderer>().bounds.size.z;
        straightx = platUP.GetComponent<Renderer>().bounds.size.x;
        turnx = platLEFT.GetComponent<Renderer>().bounds.size.x;
        turnz = platLEFT.GetComponent<Renderer>().bounds.size.z - straightz;

       // post.y -= 1;

        for(int i = 0; i < trees.Length; i++)
        {
            ObjectPoolManager.Current.CreatePool(trees[i], (activePlatformCount + 2) * treeCount);
        }

        LastDirection = Direction.up;
        Orientation = Direction.up;

        platCount = UnityEngine.Random.Range((maxPlatNum-(maxPlatNum/5)), maxPlatNum);
        SetPool();
        OrientPlan = new Direction[platCount];
        DirectPlan = new Direction[platCount];
        OrientPlan[0] = Orientation;
        DirectPlan[0] = LastDirection;

        for (int i = 1; i < platCount; i++)
        {
            OrientPlan[i] = Orientation;
            GetNextDirection();
            DirectPlan[i] = LastDirection;
 //         Debug.Log(DirectPlan[i]);
        }

        platformQueue = new Queue();

        //initial platform
     //   platform = ObjectPoolManager.Current.GetObject("up");
    //    platform.transform.parent = transform;
     //   platform.transform.localPosition = post;
     //   platform.GetComponent<PlatformClass>().Spawn();
     //   platformQueue.Enqueue(platform);

        //initial 4 pieces
        while (var < activePlatformCount/2)
        {
            post = PlatformLocation(post, var);
            platform = ObjectPoolManager.Current.GetObject(DirectPlan[var].ToString());
            platform.transform.parent = transform;
            platform.transform.localPosition = post;
            platform.transform.Rotate(0, roat, 0);
            platform.GetComponent<PlatformClass>().Spawn();
            platformQueue.Enqueue(platform);
            var++;
        }

    }

    public void triggerPlatform()
    {
        if (var < platCount)
        {
            post = PlatformLocation(post, var);
            platform = ObjectPoolManager.Current.GetObject(DirectPlan[var].ToString());
            platform.transform.parent = transform;
            platform.transform.localPosition = post;
            platform.transform.localRotation = Quaternion.Euler(0, 0, 0);
            platform.transform.Rotate(0, roat, 0);
            platform.GetComponent<PlatformClass>().Spawn();
            platformQueue.Enqueue(platform);
            var++;
        }
        if (platformQueue.Count >= activePlatformCount)
        {
            inactivePlat = (GameObject)platformQueue.Dequeue();
            inactivePlat.GetComponent<PlatformClass>().DeactivatePlatform();

        }
    }

    private void SetPool()
    {
        if (turnvariable < 0 || turnvariable > 5)
            Debug.LogError("turn variable must be between 0 and 5, inclusive");
        if(turnvariable == 0) 
            ObjectPoolManager.Current.CreatePool(platUP, activePlatformCount + 2);
        else
        {
            ObjectPoolManager.Current.CreatePool(platUP, (activePlatformCount * (1-(turnvariable)/10)) + 2);
            ObjectPoolManager.Current.CreatePool(platLEFT, (activePlatformCount * (turnvariable / 20)) + 2);
            ObjectPoolManager.Current.CreatePool(platRIGHT, (activePlatformCount * (turnvariable / 20)) + 2);
            for (int i = 0; i < (turnvariable * 2); i++)
            {
                if (i < turnvariable)
                    DirProbability[i] = 1;
                else
                    DirProbability[i] = 2;
            }

            
        }
    }


    private void GetNextDirection()
    {
        int dir;
        do
        {
            dir = DirProbability[Random.Range(0, 20)];
        } while (DirArray[dir] != 1);
        if ((int)LastDirection != dir && LastDirection != Direction.up)
            DirArray[(int)LastDirection] += 1;
        if (dir != (int)Direction.up)
            DirArray[dir] = 0;

            LastDirection = (Direction)dir;
        if (LastDirection != Direction.up)
        {
            if (Orientation == Direction.up)
                Orientation = LastDirection;
            else if (Orientation == Direction.down)
                Orientation = Opposite(LastDirection);
            else
            {
                if (LastDirection == Direction.left)
                {
                    if (Orientation == Direction.left)
                        Orientation = Direction.down;
                    else
                        Orientation = Direction.up;
                }
                else
                {
                    if (Orientation == Direction.left)
                        Orientation = Direction.up;
                    else
                        Orientation = Direction.down;
                }

            }
        }
    }



    private Vector3 PlatformLocation(Vector3 pos, int orient)
    {
        if (OrientPlan[orient] == Direction.up)
        {
            roat = 0;
            if (orient > 1 && OrientPlan[orient] != OrientPlan[orient - 1])
            {
                pos.z += turnx;
                if (OrientPlan[orient - 1] == Direction.left)
                    pos.x -= turnz;
                else
                    pos.x += turnz;
            }
            else
                pos.z += straightz;
        }
        else if (OrientPlan[orient] == Direction.down)
        {
            roat = 180;
            if (orient > 1 && OrientPlan[orient] != OrientPlan[orient - 1])
            {
                pos.z -= turnx;
                if (OrientPlan[orient - 1] == Direction.left)
                    pos.x -= turnz;
                else
                    pos.x += turnz;
            }
            else
                pos.z -= straightz;
        }
        else if (OrientPlan[orient] == Direction.right)
        {
            
            roat = 90;
            if (orient > 1 && OrientPlan[orient] != OrientPlan[orient - 1])
            {
                pos.x += turnx;
                if (OrientPlan[orient - 1] == Direction.up)
                    pos.z += turnz;
                else
                    pos.z -= turnz;
            }
            else
                pos.x += straightx;
        }
        else if (OrientPlan[orient] == Direction.left)
        {
            roat = -90;
            if (orient > 1 && OrientPlan[orient] != OrientPlan[orient - 1])
            {
                pos.x -= turnx;
                if (OrientPlan[orient - 1] == Direction.up)
                    pos.z += turnz;
                else
                    pos.z -= turnz;
            }
            else
                pos.x -= straightx;
        }
        return pos;
    }


    //ISSUE: part of the platform collider issue. 
    public void SetPlatEnd(bool hit)
    {
        platEnd = hit;
    }

    public void SetPlatformStopped(bool stop, Direction dir)
    {
        stopdir[(int)dir] = stop;
    }

    public Direction Opposite(Direction dir)
    {
        if (dir == Direction.up)
            return (Direction.down);
        else if (dir == Direction.down)
            return (Direction.up);
        else if (dir == Direction.left)
            return (Direction.right);
        else
            return (Direction.left);
    }

}