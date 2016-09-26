using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
 
//NEW CLASS
//I want to utiltize this class for the tree pooling as well
//But for now they are dealt with separately 
public class ObjectPool{

    private GameObject pooledObj;
    private int amount;
    private List<GameObject> pool;

    // Use this for initialization
    public ObjectPool(GameObject obj, int max) {
        pool = new List<GameObject>();
        for (int i = 0; i < max; i++)
        {
            GameObject Nobj = GameObject.Instantiate(obj) as GameObject;
            obj.SetActive(false);
            //obj.transform.parent = transform;
            pool.Add(Nobj);
        }
        this.amount = max;
        this.pooledObj = obj;

    }
	
	public GameObject GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }
}
