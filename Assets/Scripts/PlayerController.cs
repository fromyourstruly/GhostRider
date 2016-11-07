using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    GameObject translationNode;
    GameObject rotationNode;
    private bool treehit = false;
    Quaternion rotation;
    private bool treehit = false;
	// Use this for initialization
	void Start ()
    {
        rotationNode = GameObject.Find("RotationNode");
        translationNode = rotationNode.transform.FindChild("TranslationNode").gameObject;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        rotation = rotationNode.transform.rotation;
        if (Input.GetKey(KeyCode.A))
        {
            //For Left turn
            rotation *= Quaternion.AngleAxis(1.0f, new Vector3(0,1,0));
            rotationNode.transform.rotation = rotation;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //For Right turn
            rotation *= Quaternion.AngleAxis(-1.0f, new Vector3(0, 1, 0));
            rotationNode.transform.rotation = rotation;
        }

        Vector3 position = translationNode.transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            //For acceleration
            position.z -= 0.2f;
            translationNode.transform.position = position;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //For deceleration
            position.z += 0.1f;
            translationNode.transform.position = position; 
        }
	}

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
            treehit = false;
    }
}
