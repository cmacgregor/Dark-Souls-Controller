using UnityEngine;
using System.Collections;

public partial class Humanoid_CharacterController : MonoBehaviour {

    //debug variable
    public bool DEBUG = true;

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

    private ushort activeLeftWeaponSlot = 0;
	private ushort activeRightWeaponSlot = 0;
	private ushort activeReadyItemSlot = 0;
	private ushort activeSpellSlot = 0;
	
	private Animator character_Animator;
   
	private CharacterController character_Controller;

	void Start () {
		//Setup components 
		character_Animator = GetComponent<Animator>();
		character_Controller = GetComponent<CharacterController>();

        TEST_fillAllEquipmentSlots();
	}
	
	public void cycleReadyItem() {
        activeReadyItemSlot = cycleEquipment (ref equipped_Items, activeReadyItemSlot);

        if (DEBUG) Debug.Log("New Ready Item Slot: " + activeReadyItemSlot);
    }
    public void cycleSpells() {
		activeSpellSlot = cycleEquipment (ref equipped_Spells, activeSpellSlot);

        if (DEBUG) Debug.Log("New Spell Slot: " + activeSpellSlot);

    }

    public void cycleLeftWeapon()
    {
        activeLeftWeaponSlot = cycleWeapon(ref equipped_LeftSideWeapons, ref activeLeftWeaponSlot);
        //character_Animator.Play("equipLeftWeapon");

        if (DEBUG) Debug.Log("New Left Side Weapon Slot: " + activeLeftWeaponSlot);

    }

    public void cycleRightWeapon()
    {
        activeRightWeaponSlot = cycleWeapon(ref equipped_RightSideWeapons, ref activeRightWeaponSlot);
        //character_Animator.Play("equipRightWeapon");

        if (DEBUG) Debug.Log("New Right Side Weapon Slot: " + activeRightWeaponSlot);

    }

    private ushort cycleWeapon(ref int[] equippedWeapons, ref ushort currentWeaponSlot) {
        ushort newWeaponSlot = cycleEquipment (ref equippedWeapons, currentWeaponSlot);

        return newWeaponSlot;
	}

    private ushort cycleEquipment(ref int[] equipmentArr, ushort index) {
        if (index+1 < equipmentArr.Length)
        {
            index++;
        }
        else
        {
            index = 0;
        }

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

    #region Test helper functions
    private void TEST_fillAllEquipmentSlots()
    {
        TEST_fillEquipementType(ref equipped_Armor);
        TEST_fillEquipementType(ref equipped_Items);
        TEST_fillEquipementType(ref equipped_LeftSideWeapons);
        TEST_fillEquipementType(ref equipped_RightSideWeapons);
        TEST_fillEquipementType(ref equipped_Spells);
    }

    private void TEST_fillEquipementType(ref int[] equipArr)
    {
        for(int i = 1; i > equipArr.Length; i++)
        {
            equipArr[i] = i + 1;
        }
    }
    #endregion
}