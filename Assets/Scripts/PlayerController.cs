using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    GameObject translationNode;
    GameObject rotationNode;

    Quaternion rotation;

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
            rotation *= Quaternion.AngleAxis(0.2f, new Vector3(0,1,0));
            rotationNode.transform.rotation = rotation;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //For Right turn
            rotation *= Quaternion.AngleAxis(-0.2f, new Vector3(0, 1, 0));
            rotationNode.transform.rotation = rotation;
        }

        Vector3 position = translationNode.transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            //For acceleration
            position.z -= 1.0f;
            translationNode.transform.position = position;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //For deceleration
            position.z += 0.5f;
            translationNode.transform.position = position; 
        }
	}
}
