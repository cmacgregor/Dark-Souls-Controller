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

    //private definitions 
    private const int ITEM_SLOTS = 5;

    private const int WEAPON_SLOTS = 3;
    private const int MAGIC_SLOTS = 3;

    private const int ARMOR_SLOTS = 4;
    private enum ArmorSlots
    {
        Head,
        Body,
        Arms,
        Feet
    };

    //player stats
    private int character_Stamina;
	private int character_Health;
	private int kick_stamina_cost = 20;

    //player equipment
    private int[] equipped_Armor = new int[ARMOR_SLOTS];
    private int[] equipped_Items = new int[ITEM_SLOTS];
    private int[] equipped_Spells = new int[MAGIC_SLOTS];
    private int[] equipped_LeftSideWeapons = new int[WEAPON_SLOTS]; 
	private int[] equipped_RightSideWeapons = new int[WEAPON_SLOTS];

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
		activeReadyItem = cycleEquipment (equipped_Items, activeReadyItem);
		Debug.Log(activeReadyItem);
		
	}
	public void cycleSpells() {
		activeSpell = cycleEquipment (equipped_Spells, activeSpell);
		Debug.Log(activeSpell);
	}
	public void cycleLeftWeapon() {
		activeLeftWeapon = cycleEquipment (equipped_LeftSideWeapons, activeLeftWeapon);
		Debug.Log(activeLeftWeapon);
	}
	public void cycleRightWeapon() {
		activeRightWeapon = cycleEquipment (equipped_RightSideWeapons, activeRightWeapon);
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