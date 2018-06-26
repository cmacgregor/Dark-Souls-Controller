using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {
    //Weapon Actions
    public enum actionID
    {
        Light, 
        Kick, 
        Heavy, 
        Charged, 
        Lunge
    };

    public void leftSideAction(short action) {
		if (DEBUG) Debug.Log("side: left"); 
		character_Animator.SetBool("LeftSideAction", true);
		weaponAction((actionID)action);
	}

	public void rightSideAction(short action){
		if (DEBUG) Debug.Log("side: right"); 
		character_Animator.SetBool("LeftSideAction", false);
		weaponAction((actionID)action);		
	}

	private void weaponAction(actionID action) {
		if (DEBUG) Debug.Log("action_id: " + action); 
		//If player has enough stamina 
		if (character_Stamina > 0) {
			//Perform action  
			character_Animator.SetInteger("WeaponActionID", (int)action);
			//reduce stamina by the used actions cost
			if (action == 0) character_Stamina -= kick_stamina_cost;
		}	
	}
}