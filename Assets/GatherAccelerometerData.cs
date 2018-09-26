using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	[SerializeField] private int[] windowSizes;

	[SerializeField] private char commentaryChar = '%';

	public bool isFall;

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
		//Vector3 gravityProj = Vector3.Project(Physics.gravity, -objToTrack.up);
		gravityDir.Add( objToTrack.TransformDirection(Physics.gravity));
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

	public void ProcessData() {
		List<Vector3> accelerations = new List<Vector3>();
		
		//Aqui seria calcular las aceleraciones desde los datos recogidos
		
		for (int i = 0; i < deltaPosition.Count; i++) {
			accelerations.Add(deltaPosition[i]);
		}

		//List<Vector3[]> windowsData = new List<Vector3[]>();
		List<windowData> windows = new List<windowData>();
		List<string> windowInfo = new List<string>();
		for (int i = 0; i < windowSizes.Length; i++) {
			if (accelerations.Count > windowSizes[i]) {
				var windowData = windows[i];
				float[] accelsX = new float[windowSizes[i]];
				float[] accelsY = new float[windowSizes[i]];
				float[] accelsZ = new float[windowSizes[i]];
				windowData.accelerationX = new float[accelerations.Count - windowSizes[i]];
				for (int j = 0; j < accelerations.Count - windowSizes[i]; j++) {
					for (int k = 0; k < windowSizes[i]; k++) {
						windowData.accelerationX[k] = accelerations[j + k].x;
						accelsX[k] = accelerations[j + k].x;
						windowData.accelerationY[k] = accelerations[j + k].y;
						accelsY[k] = accelerations[j + k].y;
						windowData.accelerationZ[k] = accelerations[j + k].z;
						accelsZ[k] = accelerations[j + k].z;
					}

					windowData.meanX = MathUtils.MyMean(accelsX);
					windowData.meanY = MathUtils.MyMean(accelsY);
					windowData.meanZ = MathUtils.MyMean(accelsZ);

					windowData.stdDevX = MathUtils.MyStdDev(accelsX, windowData.meanX);
					windowData.stdDevY = MathUtils.MyStdDev(accelsY, windowData.meanY);
					windowData.stdDevZ = MathUtils.MyStdDev(accelsZ, windowData.meanZ);

					windowData.isFall = isFall;
				}
			}
			else {
				Debug.LogError("WindowSizeTooBig");
				return;
			}
		}

		string fullData = commentaryChar + "fall accelerations + \n\n";
	}

	[System.Serializable]
	public struct windowData {
		public int numWindow;
		public float[] accelerationX;
		public float[] accelerationY;
		public float[] accelerationZ;
		public float meanX;
		public float meanY;
		public float meanZ;
		public float stdDevX;
		public float stdDevY;
		public float stdDevZ;
		public bool isFall;
	}
}