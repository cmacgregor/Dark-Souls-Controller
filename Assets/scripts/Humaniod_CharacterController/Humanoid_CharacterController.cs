using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {

	//debug variable
//	static bool DEBUG = true;
	static bool DEBUG = false;

	public float character_SprintSpeed = 8.0f;
	public float character_NormalSpeed = 6.0f;
	
	private bool sprinting = false;
	
	private float character_Gravity = 20.0f;
	
	//player stats
	private int character_Stamina;
	private int character_Health;
	private int kick_stamina_cost = 20;
	
	//player equipment
	private int[] character_LeftSide_WeaponIDs = new int[3]; 
	private int[] character_RightSide_WeaponIDs = new int[3]; 
	private int[] character_ReadyItemIDs = new int[5];
	//Will need to be calculated later based upon stats 
	private int[] character_ReadySpellIDs = new int[4];
	//private int[] character_ArmorIDs = new int[4];
	//0 - Head
	//1 - Chest
	//2 - Arms
	//3 - Legs 
	private int activeLeftWeapon = 0;
	private int activeRightWeapon = 0;
	private int activeReadyItem = 0;
	private int activeSpell = 0;

	
	private Animator character_Animator; 
	private CharacterController character_Controller;

	void Start () {
		//Setup components 
		character_Animator = GetComponent<Animator>();
		character_Controller = GetComponent<CharacterController>();
		//character_Controller.center = 
	}
	
	public void cycleReadyItem() {
		activeReadyItem = cycleEquipment (character_ReadyItemIDs, activeReadyItem);
		Debug.Log(activeReadyItem);
		
	}
	public void cycleSpells() {
		activeSpell = cycleEquipment (character_ReadySpellIDs, activeSpell);
		Debug.Log(activeSpell);
	}
	public void cycleLeftWeapon() {
		activeLeftWeapon = cycleEquipment (character_LeftSide_WeaponIDs, activeLeftWeapon);
		Debug.Log(activeLeftWeapon);
	}
	public void cycleRightWeapon() {
		activeRightWeapon = cycleEquipment (character_RightSide_WeaponIDs, activeRightWeapon);
		Debug.Log(activeRightWeapon);
	}

	private int cycleEquipment(int[] equipmentArr, int  index) {
		if ((index++ <= equipmentArr.Length) & (equipmentArr [index++] != 0))
			index++;  
		else index = 0;

		return index;
	}
	
/* 	void handleAirborneMotion()
	{	
		Debug.Log ("handeling airborne  motion");
		//play falling animation
		character_Animator.SetBool("Falling", true);
		//mitigate forward speed
		// apply extra gravity from multiplier:
		Vector3 drag = (Physics.gravity * character_GravityMultiplier) - Physics.gravity;	
	}
 *//* 	public void attack(int attack_type)
	{
		//0 = Right Handed Light action
		//1 = Left Handed Light Action
		//2 = Right Handed Heavy Action
		//3 = Left Handed Heavy Action
		//4 = Kick
		if (attack_type == 0) {
			if (DEBUG) Debug.Log ("CharacterController.attack = right handed light action");
		}
		else if (attack_type == 1) {
			if (DEBUG) Debug.Log ("CharacterController.attack = left handed light action");
			character_Animator.SetLayerWeight(1, 1.0f);
			character_Animator.SetTrigger ("LightAction");
		}
		else if (attack_type == 2) {
			if (DEBUG) Debug.Log ("CharacterController.attack = right handed heavy action");
		}
		else if (attack_type == 3) {
			if (DEBUG) Debug.Log ("CharacterController.attack = left handed heavy action");
			character_Animator.SetLayerWeight(1, 1.0f);
			character_Animator.SetTrigger ("HeavyAction");
		}
		else if (attack_type == 5) {
			if (DEBUG) Debug.Log ("CharacterController.attack = kick");
		}
	} */

/* 	public void dodge()
	{
		if(DEBUG) Debug.Log ("CharacterController.dodge");
		//if stamina allows 
		if (character_Stamina > 0) {
			character_Animator.SetTrigger("Dodge");
		}
	} */

/* 	public void use_item()
	{
		if(DEBUG) Debug.Log ("CharacterController.use_item");
	} */

/* 	public void interact()
	{
		if(DEBUG) Debug.Log ("CharacterController.interact");
	} */

/* 	private void checkIfGrounded()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * character_GroundDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, character_GroundDistance))
		{
			//m_GroundNormal = hitInfo.normal;
			isGrounded = true;
			character_Animator.applyRootMotion = true;
		}
		else
		{
			isGrounded = false;
			//m_GroundNormal = Vector3.up;
			character_Animator.applyRootMotion = false;
		}
	}
	*/
	#region property getters and setters
	public bool Sprint {
		get {
			return sprinting;
		}
		set {
			sprinting = value;
		}
	} 
	#endregion
}