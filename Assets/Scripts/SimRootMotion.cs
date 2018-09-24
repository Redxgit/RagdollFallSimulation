using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimRootMotion : MonoBehaviour {

	[SerializeField] private Animator anim;

	private void OnAnimatorMove() {
		if (anim) {
			Vector3 newPos = transform.position;
			newPos.z += anim.GetFloat("WalkSpeed") * Time.deltaTime;
			transform.position = newPos;
		}
	}
}
