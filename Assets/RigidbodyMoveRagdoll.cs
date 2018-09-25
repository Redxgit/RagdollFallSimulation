using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMoveRagdoll : MonoBehaviour {

	new protected Rigidbody rigidbody;
	public bool keepUpright = true;
	public float uprightForce = 10;
	public float uprightOffset = 1.45f;
	public float additionalUpwardForce = 10;
	public float dampenAngularForce = 0;

	public Transform startObject;
	public Transform endObject;

	public Vector3 forceToApply;
	//
	//
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.maxAngularVelocity = 40; // **** CANNOT APPLY HIGH ANGULAR FORCES UNLESS THE MAXANGULAR VELOCITY IS INCREASED ****
	}
	//
	void FixedUpdate()
	{
		if (keepUpright)
		{
			// ***** USE TWO FORCES PULLING UP AND DOWN AT THE TOP AND BOTTOM OF THE OBJECT RESPECTIVELY TO PULL IT UPRIGHT ***
			//
			//  *** THIS TECHNIQUE CAN BE USED FOR PULLING AN OBJECT TO FACE ANY VECTOR ***
			//
			rigidbody.AddForceAtPosition(new Vector3(forceToApply.x+ additionalUpwardForce, (forceToApply.y + additionalUpwardForce), forceToApply.z+ additionalUpwardForce),
				startObject.position);
			//
			rigidbody.AddForceAtPosition(new Vector3(-forceToApply.x, -forceToApply.y, -forceToApply.z),
				endObject.position);
			//  

		}
		if (dampenAngularForce > 0)
		{
			rigidbody.angularVelocity *= (1 - Time.deltaTime * dampenAngularForce);
		}
	}
}
