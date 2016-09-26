using UnityEngine;
using System.Collections;

public class TranslationNode : MonoBehaviour {

    GameObject rotationNode;
	// Use this for initialization
	void Start ()
    {
        rotationNode = GameObject.Find("RotationNode");
	}
	
	public void UnparentFromRotationNode()
    {
        transform.parent = null;
    }

    public void ParentToRotationNode()
    {
        transform.parent = rotationNode.transform;
    }
}
