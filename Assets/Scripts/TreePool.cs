using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//Will soon be obsolete.
//Should be replaced with ObjectPool/ObjectPoolManger
public class TreePool : MonoBehaviour {

    public static TreePool current; 
    public GameObject[] trees;
    public int amount;
    List<GameObject> pool;

    void Awake()
    {
        current = this;
    }

    //Creates pool of tree objects
	void Start () {
        pool = new List<GameObject>();
        for(int i = 0; i < amount; i++)
        {
            GameObject tempobj = (GameObject)Instantiate(trees[UnityEngine.Random.Range(0, trees.Length)]);
            tempobj.SetActive(false);
            pool.Add(tempobj);
        }
	}

    //Get Tree function
    public GameObject GetRandomTree()
    {
        IEnumerable<int> nums = Enumerable.Range(0, amount).OrderBy(x => Guid.NewGuid()).Take(20);
        foreach(int i in nums)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        return null;
    }

    }
