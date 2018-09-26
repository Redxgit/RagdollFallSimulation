using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FromAnimationToRagdoll : MonoBehaviour {
	[SerializeField] private Animator anim;

	[SerializeField] private Rigidbody[] rbodies;

	[SerializeField] private bool kinematicOnStart;

	private Vector3 initPos;

	private Quaternion initRot;

	[SerializeField] private float forceToAdd;
	private bool isOnRagdoll;

	public Action GoingRagdoll = delegate { };

	public Action ReturnToAnimation = delegate { };

	// Use this for initialization
	void Start() {
		for (int i = 0; i < rbodies.Length; i++) {
			rbodies[i].isKinematic = kinematicOnStart;
		}

		initPos = transform.position;
		initRot = transform.rotation;
	}

	public void ResetThings() {
		isOnRagdoll = false;
		transform.position = initPos;
		transform.rotation = initRot;

		for (int i = 0; i < rbodies.Length; i++) {
			rbodies[i].isKinematic = kinematicOnStart;
		}

		anim.enabled = true;
		ReturnToAnimation.Invoke();
	}

	public void GoRagdoll() {
		if (!enabled) return;
		if (isOnRagdoll) return;
		isOnRagdoll = true;
		for (int i = 0; i < rbodies.Length; i++) {
			rbodies[i].isKinematic = false;
			//	rbodies[i].AddForce(transform.forward * forceToAdd);
		}

		anim.enabled = false;
		GoingRagdoll.Invoke();
	}

	public void StartWalking() {
		anim.SetTrigger("Walk");
	}

	public void StartIdle() {
		anim.SetTrigger("Idle");
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			GoRagdoll();
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			ResetThings();
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			StartWalking();
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			StartIdle();
		}
	}

	private void Reset() {
		anim = GetComponent<Animator>();
		rbodies = GetComponentsInChildren<Rigidbody>();
	}

	[ContextMenu("ForceRagdoll")]
	private void ForceRagdoll() {
		anim.enabled = false;
		for (int i = 0; i < rbodies.Length; i++) {
			rbodies[i].isKinematic = false;
		}
	}
}