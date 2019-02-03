using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {

    public enum weapon_Stance
    {
        SingleHanded = 0,
        RightHandedTwoHand = 1,
        LeftHandedTwoHand = 2
    };

    // -Use Item
    public void useItem() {
		// based upon selected item play animation and apply effect
		if(DEBUG) Debug.Log(equipped_Items[activeReadyItemSlot]);
	}
	//Set Weapon stance
	public void setWeaponStance(weapon_Stance stance) {
		if ((int)stance > 3) stance = 0;
		character_Animator.SetInteger("Stance", (int)stance);
	}
	//Get Weapon Stance 
	public weapon_Stance getWeaponStance() {
		return (weapon_Stance)character_Animator.GetInteger("Stance");
	}
	// -Interact
	public void interact(){
		// If interact button is pressed check if there is a colision with appropriately tagged object
	}
	// -Dodge
	public void dodge() {
		if(DEBUG) Debug.Log (this.name + " dodged");
		//if stamina allows 
		if (character_Stamina > 0) {
			character_Animator.SetTrigger("Dodge");
			//turn collisions off 
		}
	}	
}