using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {
	//Weapon Actions

	public void leftSideAction(short action_id) {
		if (DEBUG) Debug.Log("side: left"); 
		character_Animator.SetBool("LeftSideAction", true);
		weaponAction(action_id);
	}
	public void rightSideAction(short action_id){
		if (DEBUG) Debug.Log("side: right"); 
		character_Animator.SetBool("LeftSideAction", false);
		weaponAction(action_id);		
	}
	private void weaponAction(short action_id) {
		//action_id:
		//	0 - light action
		//	1 - kick 
		//	2 - charged heavy
		//	3 - heavy action 
		//	4 - lunging attack 
		if (DEBUG) Debug.Log("action_id: " + action_id); 
		//If player has enough stamina 
		if (character_Stamina > 0) {
			//Perform action  
			character_Animator.SetInteger("WeaponActionID", action_id);
			//reduce stamina by the used actions cost
			if (action_id == 0) character_Stamina -= kick_stamina_cost;
		}	
	}
}