using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {
	// Actions:
		// -Use Item
		public void useItem() {
			// based upon selected item play animation and apply effect
		Debug.Log(character_ReadyItemIDs[activeReadyItem]);
		}
		//Set Weapon stance
		public void setWeaponStance(int stance) {
			if (stance > 3) stance = 0;
			character_Animator.SetInteger("Stance", stance);
		}
		//Get Weapon Stance 
		public int getWeaponStance() {
			return character_Animator.GetInteger("Stance");
		}
		// -Interact
		public void interact(){
			// If interact button is pressed and there is a colision with appropriately tagged object
		}
		// -Dodge
		public void dodge() {
			if(DEBUG) Debug.Log ("CharacterController.dodge");
			//if stamina allows 
			if (character_Stamina > 0) {
				character_Animator.SetTrigger("Dodge");
				//turn collisions off 
			}
		}	
}