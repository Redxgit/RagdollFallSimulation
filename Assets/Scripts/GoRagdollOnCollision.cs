using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRagdollOnCollision : MonoBehaviour {
	[SerializeField] private FromAnimationToRagdoll controller;

	// Use this for initialization
	void Start() { }

	// Update is called once per frame
	void Update() { }

	private void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("Obstacle") || other.transform.CompareTag("CollidersPlayer")) {
			Debug.Log("TriggerEnter");
			controller.GoRagdoll();			
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (!other.transform.CompareTag("Obstacle")&& !other.transform.CompareTag("CollidersPlayer")) return;
		Debug.Log("CollisionEnter");
		controller.GoRagdoll();
	}

	private void Reset() {
		controller = GetComponent<FromAnimationToRagdoll>();
	}
}