using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.rotation = Camera.main.transform.rotation;

//		Vector3 oppositeCamera = transform.position - Camera.main.transform.position;
//		Quaternion faceCamera = Quaternion.LookRotation(oppositeCamera);
//		Vector3 euler = faceCamera.eulerAngles;
//		euler.y = 0f;
//		faceCamera.eulerAngles = euler;
//		transform.rotation = faceCamera;
	}


}
