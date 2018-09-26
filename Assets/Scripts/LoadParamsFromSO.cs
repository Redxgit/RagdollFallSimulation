using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadParamsFromSO : MonoBehaviour {

	[Header("Head, LegUp, Leg, Spine1, Arm, Forearm")]
	[SerializeField] private CharJoints[] allJoints;

	[SerializeField] private CharacterJointParamsSO data;

	[ContextMenu("LoadData")]
	public void LoadData() {
		for (int i = 0; i < allJoints.Length; i++) {
			for (int j = 0; j < allJoints[i].joints.Length; j++) {
				var softJointLimit = allJoints[i].joints[j].lowTwistLimit;
				softJointLimit.limit = data.Parameters[i].LowTwistLimit;
				allJoints[i].joints[j].lowTwistLimit = softJointLimit;

				var highTwistLimit = allJoints[i].joints[j].highTwistLimit;
				highTwistLimit.limit = data.Parameters[i].HighTwistLimit;
				allJoints[i].joints[j].highTwistLimit = highTwistLimit;

				var swing1Limit = allJoints[i].joints[j].swing1Limit;
				swing1Limit.limit = data.Parameters[i].SwingTwistLimit;
				allJoints[i].joints[j].swing1Limit = swing1Limit;
			}
		}
	}
	

	[System.Serializable]
	struct CharJoints {
		public CharacterJoint[] joints;
	}
}
