using UnityEngine;
using System.Collections;

public class DrawFacing : MonoBehaviour {

	public Color color = Color.white;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}
}
