using UnityEngine;
using System.Collections;

public class Grapher1 : MonoBehaviour {

	public int resolution = 10;

	private ParticleSystem.Particle[] points;

	// Use this for initialization
	void Start () 
	{		
		if (resolution < 10 || resolution > 100) {
			Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
			resolution = 10;
		}
		points = new ParticleSystem.Particle[resolution];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
