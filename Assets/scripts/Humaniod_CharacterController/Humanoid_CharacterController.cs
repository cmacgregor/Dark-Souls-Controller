using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {

	//debug variable
//	static bool DEBUG = true;
	static bool DEBUG = false;

	//public character movement variables 
	public float character_SprintSpeed = 8.0f;
	public float character_WeightSpeedMultiplier = 2.0f;
	//character gravity variables 
	public float Gravity = 21f;
	public float TerminalVelocity = 20f;

	private bool sprinting = false;

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
	#region property getters and setters
	public bool Sprint {
		get {
			return sprinting;
		}
		set {
			if (sprinting != value) {
				sprinting = value;
				character_Animator.SetBool ("Sprint", value);
			}
		}
	} 
	#endregion
}