using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMoveRBody2 : MonoBehaviour {
	[SerializeField] private Rigidbody body;

	[SerializeField] private Transform targetPosition;
	[SerializeField] private Transform positionToAddForce;

	[SerializeField] private float force;
	[SerializeField] private FromAnimationToRagdoll observer;

	[SerializeField] private bool moving;

	public bool Debugging;
	private float eTimeCorrecting;

	void Start() {
		observer.GoingRagdoll += WentRagdoll;
		observer.ReturnToAnimation += ReturnedAnim;
	}

	private void WentRagdoll() {
		moving = true;
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		body.gameObject.SetActive(false);
		body.gameObject.SetActive(true);
		eTimeCorrecting = 0f;
	}

	private void ReturnedAnim() {
		moving = false;
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		body.Sleep();
		body.gameObject.SetActive(false);
		body.gameObject.SetActive(true);
	}

	private void FixedUpdate() {
		if (!moving && !Debugging) return;
		eTimeCorrecting += Time.fixedDeltaTime;
		if (eTimeCorrecting > 1f && !Debugging) moving = false;
		body.AddForceAtPosition((targetPosition.position - positionToAddForce.position) * force,
			positionToAddForce.position);
	}
}