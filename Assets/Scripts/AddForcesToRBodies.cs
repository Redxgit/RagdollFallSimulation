using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForcesToRBodies : MonoBehaviour {

	[SerializeField] private Rigidbody[] bodies;
	[SerializeField] private float forceModifier;

	private Vector3[] lastPos;

	// Use this for initialization
	void Start () {
		lastPos = new Vector3[bodies.Length];
		for (int i = 0; i < bodies.Length; i++) {
			lastPos[i] = bodies[i].position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bodies.Length; i++) {
			bodies[i].AddForce((lastPos[i]- bodies[i].position)* forceModifier);
			lastPos[i] = bodies[i].position;
		}
	}

	private void Reset() {
		bodies = GetComponentsInChildren<Rigidbody>();
	}
}
