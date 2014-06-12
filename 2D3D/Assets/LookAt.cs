using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		transform.LookAt(target);
	}
}
