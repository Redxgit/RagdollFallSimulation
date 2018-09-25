using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherAccelerometerData : MonoBehaviour {
	[SerializeField] private FromAnimationToRagdoll[] observers;
	[SerializeField] private bool recordingInfo;
	[SerializeField] private List<Vector3> deltaPosition;
	[SerializeField] private List<Vector3> deltaAngles;
	[SerializeField] private List<Vector3> gravityDir;

	private Vector3 lastPos;
	private Vector3 lastAngle;

	// Use this for initialization
	void Start() {
		deltaAngles = new List<Vector3>();
		deltaPosition = new List<Vector3>();
		gravityDir = new List<Vector3>();
		for (int i = 0; i < observers.Length; i++) {
			observers[i].GoingRagdoll += WentRagdoll;
			observers[i].ReturnToAnimation += ReturnedToAnim;
		}
	}


	public void WentRagdoll() {
		recordingInfo = true;
		deltaAngles = new List<Vector3>();
		deltaPosition = new List<Vector3>();
		gravityDir = new List<Vector3>();
		lastAngle = transform.eulerAngles;
		lastPos = transform.position;
	}

	public void ReturnedToAnim() {
		recordingInfo = false;
	}

	private void FixedUpdate() {
		if (!recordingInfo) return;
		deltaPosition.Add(transform.position - lastPos);
		deltaAngles.Add(new Vector3(Mathf.DeltaAngle(transform.eulerAngles.x, lastAngle.x),
			Mathf.DeltaAngle(transform.eulerAngles.y, lastAngle.y),
			Mathf.DeltaAngle(transform.eulerAngles.z, lastAngle.z)));
		lastAngle = transform.eulerAngles;
		lastPos = transform.position;
		gravityDir.Add(Vector3.Project(Physics.gravity, -transform.up));
	}

	// Update is called once per frame
	void Update() { }
}