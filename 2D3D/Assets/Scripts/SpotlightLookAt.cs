using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SpotlightLookAt : LookAt {

	Light spotlight;

	// Use this for initialization
	void Start () {
		spotlight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		float distance = Vector3.Distance(transform.position, target.position);

		Debug.Log(distance);
		spotlight.intensity = (distance / 5);
	}
}
