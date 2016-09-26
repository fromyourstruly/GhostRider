using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {

    private bool treehit = false;
    void Start () {
	
	}

    // Update is called once per frame
    void Update() { }
    

    //THESE TRIGGER FUNCTIONS HAVE ISSUE WITH THE COLLIDERS ON THE PLATFORMS 
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tree")
        {
            treehit = true;
            Vector3 diff = transform.InverseTransformPoint(col.gameObject.transform.position);
            if (diff.z <= 1.0f && diff.z >= 0.3f)
                PlatformManager.current.SetPlatformStopped(true, Direction.up);
            if (diff.x <= 1.0f && diff.x > 0f)
                PlatformManager.current.SetPlatformStopped(true, Direction.right);
            if (diff.x >= -1.0f && diff.x <= 0f)
                PlatformManager.current.SetPlatformStopped(true, Direction.left);
        }
        else
        {
            PlatformManager.current.triggerPlatform();
        }
    }

    void OnTriggerExit()
    {
        if (treehit)
        {
            PlatformManager.current.SetPlatformStopped(false, Direction.up);
            PlatformManager.current.SetPlatformStopped(false, Direction.right);
            PlatformManager.current.SetPlatformStopped(false, Direction.left);
            treehit = false;
        }
    }
}
