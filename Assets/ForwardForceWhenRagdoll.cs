using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardForceWhenRagdoll : MonoBehaviour {
	[SerializeField] private Rigidbody[] bodies;

	[SerializeField] private Transform[] positions;

	[SerializeField] private Transform directionToAddForce;

	[SerializeField] private float force;

	[SerializeField] private FromAnimationToRagdoll observer;


	void Start() {
		observer.GoingRagdoll += WentRagdoll;
		observer.ReturnToAnimation += ReturnedAnim;
	}

	private void WentRagdoll() {
		for (int i = 0; i < bodies.Length; i++) {
			bodies[i].AddForceAtPosition(directionToAddForce.forward * force, positions[i].position);
		}
	}

	private void ReturnedAnim() { }
}