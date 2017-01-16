using UnityEngine;
using System.Collections;



public class PlatformClass : MonoBehaviour
{

    // Use this for initialization
    private GameObject player;
    public int amount; //amount of trees on the platform
	private int childCount;
	private int childnum;

void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		childCount = transform.childCount;

    }

    public void Spawn()
    {
		int[] nodes = new int[childCount];
        for (int i = 0; i < amount; i++)
        {
			
          int t = Random.Range(0, PlatformManager.current.trees.Length);
          GameObject tree = ObjectPoolManager.Current.GetObject(PlatformManager.current.trees[t].name);
            tree.transform.parent = transform;
			if (childCount > 0) {
				do {
					childnum = Random.Range (0, childCount);
				} while(nodes [childnum] == 1);
				tree.transform.localPosition = transform.GetChild (childnum).transform.localPosition;
				nodes [childnum] = 1;
				
			} else {
				// X location / Scale.x, Y location/ Scale.y,
				do {
					Vector3 temp = new Vector3 (Random.Range (-0.9f, 0.9f), 1f, Random.Range (-0.9f, 0.9f));
					tree.transform.localPosition = temp;
				} while (Physics.CheckSphere (tree.transform.localPosition, 0.3f));
			}
//            //while (Physics.CheckSphere(tree.transform.localPosition, 0.7f))
//            //{
//            //    temp = new Vector3(Random.Range(-0.4f, 0.4f), 10, Random.Range(-0.4f, 0.4f));
//            //    tree.transform.localPosition = temp;
//            //}
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