﻿using UnityEngine;
using System.Collections;



public class PlatformClass : MonoBehaviour
{

    // Use this for initialization
    private GameObject player;
    public int amount; //amount of trees on the platform
    public int Stage = 0; 
void OnEnable()
    {
        Stage = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("Spawn", 0.5f);
    }

    void Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
           //gameObject tree = TreePool.current.GetRandomTree();
          int t = Random.Range(0, PlatformManager.current.trees.Length);
          GameObject tree = ObjectPoolManager.Current.GetObject(PlatformManager.current.trees[t].name);
            tree.transform.parent = transform;
            Vector3 temp = new Vector3(Random.Range(-0.4f, 0.4f), 10, Random.Range(-0.4f, 0.4f));
            tree.transform.localPosition = temp;
            while (Physics.CheckSphere(tree.transform.localPosition, 0.7f))
            {
                temp = new Vector3(Random.Range(-0.4f, 0.4f), 10, Random.Range(-0.4f, 0.4f));
                tree.transform.localPosition = temp;
            }
            tree.transform.localRotation = transform.rotation;
            tree.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Stage > 4) //once 4 platforms have been set active since this platfrom, then throw it back into the pool
        {
            gameObject.SetActive(false);
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform obj = transform.GetChild(i);
                obj.parent = null;
                obj.gameObject.SetActive(false);

            }
        }
    }
    //public void IncrementStage()
    //{
    //    Stage++;
    //}

    
    //ISSUE WITH THESE TRIGGER FUNCTIONS (as in I don't know what I'm doing, so I'm just messing around)
    void OnTriggerEnter(Collider col)
    {
        PlatformManager.current.SetPlatEnd(true);
        Stage++;
    }

    void OnTriggerExit()
    {
        PlatformManager.current.SetPlatEnd(false);
    }



}