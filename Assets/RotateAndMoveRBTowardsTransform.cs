using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class RotateAndMoveRBTowardsTransform : MonoBehaviour {
	[SerializeField] private string desc;

	[SerializeField] private Transform posToAdd;
	[SerializeField] private Rigidbody rbody;
	[SerializeField] private Transform target;

	[SerializeField] private FromAnimationToRagdoll observer;

	[SerializeField] private bool correcting;

	[SerializeField] private float force;
	
	[SerializeField] private float speed;
	[SerializeField] private Vector3 targetLocalPosition;
	[SerializeField] private Vector3 targetLocalEulerAngles;

	private float eTimeCorrecting;


	void Start() {
		observer.GoingRagdoll += WentRagdoll;
		observer.ReturnToAnimation += ReturnedAnim;
	}

	private void WentRagdoll() {
		correcting = true;
		rbody.velocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
		rbody.gameObject.SetActive(false);
		rbody.gameObject.SetActive(true);
		eTimeCorrecting = 0f;
	}

	private void ReturnedAnim() {
		correcting = false;
		rbody.velocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
		rbody.Sleep();
		rbody.gameObject.SetActive(false);
		rbody.gameObject.SetActive(true);
	}

	private void FixedUpdate() {
		if (!correcting) return;
		eTimeCorrecting += Time.fixedDeltaTime;
		if (eTimeCorrecting > 1f) correcting = false;
		//rbody.AddForceAtPosition(target.position - posToAdd.position * force, posToAdd.position);
		//rbody.MovePosition(transform.position + Vector3.Lerp(transform.localPosition, targetLocalPosition, speed));
		
		
		
		
		rbody.MovePosition(Vector3.Lerp(transform.position, target.position, speed));
		rbody.MoveRotation(Quaternion.Euler(new Vector3(
			Mathf.LerpAngle(transform.eulerAngles.x, target.eulerAngles.x,
				speed),
			Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y,
				speed),
			Mathf.LerpAngle(transform.eulerAngles.z, target.eulerAngles.z,
				speed))));
		
		
		rbody.AddTorque((target.position - transform.position )* force);
	}
}