using UnityEngine;
using System.Collections;



public class PlatformClass : MonoBehaviour
{

    // Use this for initialization
    private GameObject player;
    public int amount; //amount of trees on the platform

void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Spawn();
    }

   public void Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
           //gameObject tree = TreePool.current.GetRandomTree();
          int t = Random.Range(0, PlatformManager.current.trees.Length);
          GameObject tree = ObjectPoolManager.Current.GetObject(PlatformManager.current.trees[t].name);
            tree.transform.parent = transform;
            // X location / Scale.x, Y location/ Scale.y,
            Vector3 temp = new Vector3(Random.Range(-0.4f, 0.4f), 10f/100f, Random.Range(-0.4f, 0.4f));
            tree.transform.localPosition = temp;
            //while (Physics.CheckSphere(tree.transform.localPosition, 0.7f))
            //{
            //    temp = new Vector3(Random.Range(-0.4f, 0.4f), 10, Random.Range(-0.4f, 0.4f));
            //    tree.transform.localPosition = temp;
            //}
            tree.SetActive(true);
        }
    }


    public void DeactivatePlatform()
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