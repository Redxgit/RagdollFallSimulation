using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeObstaclePos : MonoBehaviour {
	[SerializeField] private Transform objToColocate;

	[SerializeField] private float maxDeviationFwd;
	[SerializeField] private float maxDeviationUp;

	private Vector3 basePos;

	[SerializeField] private FromAnimationToRagdoll observer;

	// Use this for initialization
	void Start() {
		observer.ReturnToAnimation += RandomizePos;
		basePos = objToColocate.position;
	}

	public void RandomizePos() {
		objToColocate.position = new Vector3(basePos.x, basePos.y + Random.Range(-maxDeviationUp, maxDeviationUp),
			basePos.z + Random.Range(-maxDeviationFwd, maxDeviationFwd));
	}

	// Update is called once per frame
	void Update() { }
}