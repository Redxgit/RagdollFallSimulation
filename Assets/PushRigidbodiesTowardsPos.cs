using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidbodiesTowardsPos : MonoBehaviour {

	[SerializeField] private string desc;

	[SerializeField] private Rigidbody[] bodies;

	[SerializeField] private Transform[] targets;

	[SerializeField] private float force;

	[SerializeField] private FromAnimationToRagdoll observer;

	private bool pushing;
	// Use this for initialization
	void Start () {
		observer.GoingRagdoll += WentRagdoll;
		observer.ReturnToAnimation += ReturnedAnim;
	}

	private void WentRagdoll() {
		//pushing = true;
		for (int i = 0; i < bodies.Length; i++) {
			if (bodies[i].isKinematic) bodies[i].isKinematic = false;
		}
		bool multipleTargets = targets.Length > 1;
		if (multipleTargets) {
			for (int i = 0; i < bodies.Length; i++) {
				bodies[i].AddForce((targets[i].position - bodies[i].position) * force);
			} 
		}
		else {
			for (int i = 0; i < bodies.Length; i++) {
				bodies[i].AddForce((targets[0].position - bodies[i].position) * force);
			} 
		}
	}

	private void ReturnedAnim() {
		pushing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate() {
		if (!pushing) return;
		bool multipleTargets = targets.Length > 1;
		if (multipleTargets) {
			for (int i = 0; i < bodies.Length; i++) {
				bodies[i].AddForce(targets[i].position - bodies[i].position * force);
			} 
		}
		else {
			for (int i = 0; i < bodies.Length; i++) {
				bodies[i].AddForce(targets[0].position - bodies[i].position * force);
			} 
		}
	}
}
