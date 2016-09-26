using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//NEW CLASS 
//Will eventually use this for tree pooling 
public class ObjectPoolManager{

    private static volatile ObjectPoolManager current;
    private Dictionary<string, ObjectPool> pools;
  

    public static ObjectPoolManager Current
    {
        get
        {
            if (current == null)
            {
                current = new ObjectPoolManager();
                current.pools = new Dictionary<string, ObjectPool>();

            }
            return current;
        }
    }

    public bool CreatePool(GameObject obj, int size)
    {
        if (Current.pools.ContainsKey(obj.name))
        {
            return false;
        }
        else
        {
            ObjectPool newPool = new ObjectPool(obj, size);
            Current.pools.Add(obj.name, newPool);
            return true;
        }
    }

    public GameObject GetObject(string objname)
    {
        return Current.pools[objname].GetObject();

    }



}
