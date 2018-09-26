using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_CharacterJointParams", menuName = "CharacterJointParams")]
public class CharacterJointParamsSO : ScriptableObject {

	[Header("Head, LegUp, Leg, Spine1, Arm, Forearm")]
	public CharacterJointParams[] Parameters = new CharacterJointParams[6]; 
}
