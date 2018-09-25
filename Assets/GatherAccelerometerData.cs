using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class GatherAccelerometerData : MonoBehaviour {
	[SerializeField] private FromAnimationToRagdoll[] observers;
	[SerializeField] private bool recordingInfo;
	[SerializeField] private List<Vector3> deltaPosition;
	[SerializeField] private List<Vector3> deltaAngles;
	[SerializeField] private List<Vector3> gravityDir;
	[SerializeField] private List<float> deltaT;
	[SerializeField] private Transform objToTrack;

	public float lastTime;

	private Vector3 lastPos;
	private Vector3 lastAngle;

	private float startingTime;
	private bool firstIt;

	[SerializeField] private float distanceThreshold;

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
		deltaT = new List<float>();
		lastAngle = objToTrack.eulerAngles;
		lastPos = objToTrack.position;
		lastTime = Time.time;
		startingTime = lastTime;
		firstIt = true;
	}

	public void ReturnedToAnim() {
		recordingInfo = false;
		deltaAngles = new List<Vector3>();
		deltaPosition = new List<Vector3>();
		gravityDir = new List<Vector3>();
		deltaT = new List<float>();
	}

	private void FixedUpdate() {
		if (!recordingInfo) return;
		if (firstIt) {
			firstIt = false;
			return;
		}

		float dpos = Vector3.Distance(objToTrack.position, lastPos);
		if (dpos < distanceThreshold) {
			recordingInfo = false;
			return;
		}
		
		deltaT.Add(Time.time - lastTime);
		lastTime = Time.time;
		deltaPosition.Add(objToTrack.position - lastPos);
		deltaAngles.Add(new Vector3(Mathf.DeltaAngle(objToTrack.eulerAngles.x, lastAngle.x),
			Mathf.DeltaAngle(objToTrack.eulerAngles.y, lastAngle.y),
			Mathf.DeltaAngle(objToTrack.eulerAngles.z, lastAngle.z)));
		lastAngle = objToTrack.eulerAngles;
		lastPos = objToTrack.position;
		gravityDir.Add(Vector3.Project(Physics.gravity, -objToTrack.up));
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.C)) {
			deltaAngles = new List<Vector3>();
			deltaPosition = new List<Vector3>();
			gravityDir = new List<Vector3>();
			deltaT = new List<float>();
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			Debug.Log("Valid fall, processing and saving ...");
		}
	}
}