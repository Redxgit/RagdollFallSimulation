using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardForceWhenRagdoll : MonoBehaviour {
	[SerializeField] private Rigidbody[] bodies;

	[SerializeField] private Transform[] positions;

	[SerializeField] private Transform directionToAddForce;

	[SerializeField] private float force;
	[SerializeField] private float variability = 100f;

	[SerializeField] private FromAnimationToRagdoll observer;

	[SerializeField] private float randomInsideUnitSphereMultiplierPosition = 0.3f;
	[SerializeField] private float randomInsideUnitSphereMultiplierDirection = 0.1f;


	void Start() {
		observer.GoingRagdoll += WentRagdoll;
		observer.ReturnToAnimation += ReturnedAnim;
	}

	private void WentRagdoll() {
		for (int i = 0; i < bodies.Length; i++) {
			bodies[i].AddForceAtPosition(
				(directionToAddForce.forward + Random.insideUnitSphere * randomInsideUnitSphereMultiplierDirection) *
				(force + Random.Range(- variability, variability)),
				positions[i].position + Random.insideUnitSphere * randomInsideUnitSphereMultiplierPosition);
		}
	}

	private void ReturnedAnim() { }
}