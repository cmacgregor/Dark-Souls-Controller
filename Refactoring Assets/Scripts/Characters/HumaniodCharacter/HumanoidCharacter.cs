using System;
using UnityEngine;
using System.Collections;

public class HumanoidCharacter : OverworldCharacter, 
    IKillable<HumanoidCharacter>, 
    IDamageable<HumanoidCharacter>
{
    //debug variable
    public bool DEBUG = true;

    private float interactionRadius = 1.0f;

    //Character movement variables 
    public float sprintSpeed = 4.0f;
    public float weightSpeedMultiplier = 2.0f;
    //character gravity variables 
    public float Gravity = 21f;
    public float terminalVelocity = 20f;

    private bool sprinting = false;
    
    public enum weapon_Stance
    {
        SingleHanded = 0,
        RightHandedTwoHand = 1,
        LeftHandedTwoHand = 2
    };
    
    //player stats
    private int stamina;
    private int health;
    private int kickStaminaCost = 20;

    //active equipment definitions 
    public const int ITEM_SLOTS = 5;
    public const int WEAPON_SLOTS = 3;
    public const int SKILL_SLOTS = 3;
    public const int ARMOR_SLOTS = 4;

    private enum ArmorSlots
    {
        Head,
        Body,
        Arms,
        Feet
    };

    //player equipment
    private int[] equippedArmor = new int[ARMOR_SLOTS];
    private int[] equippedItems = new int[ITEM_SLOTS];
    private int[] equippedSkills = new int[SKILL_SLOTS];
    private int[] equippedLeftSideWeapons = new int[WEAPON_SLOTS];
    private int[] equippedRightSideWeapons = new int[WEAPON_SLOTS];

    private ushort activeLeftWeaponSlot = 0;
    private ushort activeRightWeaponSlot = 0;
    private ushort activeReadyItemSlot = 0;
    private ushort activeSkillSlot = 0;

    #region Unity functions
    void Start()
    {
        //Setup components 
        animationTree = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitObject;
        //Check for interactable objects 
        //Physics.SphereCast();

        //check for targetable objects
        if (Physics.SphereCast(this.transform.position, interactionRadius, this.transform.forward,
            out hitObject, interactionRadius, LayerMask.GetMask("Interactable"), QueryTriggerInteraction.Collide))
        {
            //display prompt
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");

        if (other.tag == "Interactable")
        {
            Debug.Log("Can interact with " + other.name);
        }
    }
    #endregion

    #region Interface Functions
    public void Damage(int damageInflicted)
    {
        //calculated damge taken 

        //play corresponding animation

    }

    public void Kill()
    {
        //play animation

        //spwan loot
    }
    #endregion

    #region Movement Functions
    public void Move(Vector3 moveVector)
    {
        //if (DEBUG) Debug.Log ("CharacterController.move vector3: " + moveVector);

        if (characterController.isGrounded)
        {
            moveVector = handleGroundedLocomotion(moveVector);
        }
        else
        {
            handleAirborneLocomotion();
        }

    }

    private Vector3 handleGroundedLocomotion(Vector3 moveVector)
    {
        animationTree.SetBool("Falling", false);
        if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                                    Camera.main.transform.eulerAngles.y,
                                                    transform.eulerAngles.z);
        }

        moveVector = transform.TransformDirection(moveVector);
        animationTree.SetFloat("Forward", moveVector.magnitude, 0f, Time.deltaTime);
        if (moveVector.x != 0 || moveVector.z != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 500f);
        if (sprinting) moveVector *= sprintSpeed;

        //Apply Character Weight Modifier to movement Vector 
        moveVector *= weightSpeedMultiplier;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);
        return moveVector;
    }

    private void handleAirborneLocomotion()
    {
        animationTree.SetBool("Falling", true);
        characterController.Move(new Vector3(0, -Gravity, 0) * Time.deltaTime);
    }

    // -Dodge
    public void dodge()
    {
        //if stamina allows 
        if (stamina > 0)
        {
            animationTree.SetTrigger("Dodge");
            //turn collisions off 
        }
    }
    #endregion

    #region Action Functions
    // -Use Item
    public void useItem()
    {
        // based upon selected item play animation and apply effect
        if (DEBUG) Debug.Log(equippedItems[activeReadyItemSlot]);
    }
    //Set Weapon stance
    public void setWeaponStance(weapon_Stance stance)
    {
        if ((int)stance > 3) stance = 0;
        animationTree.SetInteger("Stance", (int)stance);
    }
    //Get Weapon Stance 
    public weapon_Stance getWeaponStance()
    {
        return (weapon_Stance)animationTree.GetInteger("Stance");
    }
    // -Interact
    public void interact()
    {
        // If interact button is pressed check if there is a colision with appropriately tagged object
    
    }
    #endregion

    #region Weapon Action Functions
    public enum actionID
    {
        Light,
        Kick,
        Heavy,
        Charged,
        Lunge
    };

    public void leftSideAction(short action)
    {
        if (DEBUG) Debug.Log("side: left");
        animationTree.SetBool("LeftSideAction", true);
        weaponAction((actionID)action);
    }

    public void rightSideAction(short action)
    {
        if (DEBUG) Debug.Log("side: right");
        animationTree.SetBool("LeftSideAction", false);
        weaponAction((actionID)action);
    }

    private void weaponAction(actionID action)
    {
        if (DEBUG) Debug.Log("action_id: " + action);
        //If player has enough stamina 
        if (stamina > 0)
        {
            //Perform action  
            animationTree.SetInteger("WeaponActionID", (int)action);
            //reduce stamina by the used actions cost
            if (action == 0) stamina -= kickStaminaCost;
        }
    }
    #endregion
    
    #region Equipment Item Management Functions
    public void cycleReadyItem()
    {
        activeReadyItemSlot = cycleEquipment(ref equippedItems, activeReadyItemSlot);

        if (DEBUG) Debug.Log("New Ready Item Slot: " + activeReadyItemSlot);
    }

    public void cycleSkills()
    {
        activeSkillSlot = cycleEquipment(ref equippedSkills, activeSkillSlot);

        if (DEBUG) Debug.Log("New Skill Slot: " + activeSkillSlot);

    }

    public void cycleLeftWeapon()
    {
        activeLeftWeaponSlot = cycleWeapon(ref equippedLeftSideWeapons, ref activeLeftWeaponSlot);
        //play weapon switch animation

        if (DEBUG) Debug.Log("New Left Side Weapon Slot: " + activeLeftWeaponSlot);

    }

    public void cycleRightWeapon()
    {
        activeRightWeaponSlot = cycleWeapon(ref equippedRightSideWeapons, ref activeRightWeaponSlot);
        //play weapon switch animation

        if (DEBUG) Debug.Log("New Right Side Weapon Slot: " + activeRightWeaponSlot);

    }

    private ushort cycleWeapon(ref int[] equippedWeapons, ref ushort currentWeaponSlot)
    {
        ushort newWeaponSlot = cycleEquipment(ref equippedWeapons, currentWeaponSlot);

        return newWeaponSlot;
    }

    private ushort cycleEquipment(ref int[] equipmentArr, ushort index)
    {
        if (index + 1 < equipmentArr.Length)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        return index;
    }
    #endregion

    #region property getters and setters
    public bool Sprint
    {
        get
        {
            return sprinting;
        }
        set
        {
            if (sprinting != value)
            {
                sprinting = value;
                animationTree.SetBool("Sprint", value);
            }
        }
    }

    public void SetEquippedItem(ushort item_Num, int item_Slot)
    {
        try
        {
            equippedItems[item_Slot] = item_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void SetEquippedSkill(ushort equipment_Num, int equipment_Slot)
    {
        try
        {
            equippedSkills[equipment_Slot] = equipment_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void SetEquippedLeftWeapon(ushort skill_Num, int skill_Slot)
    {
        try
        {
            equippedSkills[skill_Slot] = skill_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void SetEquippedRightWeapon(ushort skill_Num, int skill_Slot)
    {
        try
        {
            equippedSkills[skill_Slot] = skill_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void SetEquippedArmor(ushort armor_Num, int armor_Slot)
    {
        try
        {
            equippedSkills[armor_Slot] = armor_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public int getItemAtSlot(int slot_Num)
    {
        return equippedItems[slot_Num];
    }

    public int getArmorAtSlot(int slot_Num)
    {
        return equippedArmor[slot_Num];
    }

    public int getSkillAtSlot(int slot_Num)
    {
        return equippedSkills[slot_Num];
    }

    public int getLeftWeaponAtSlot(int slot_Num)
    {
        return equippedLeftSideWeapons[slot_Num];
    }

    public int getRightWeaponAtSlot(int slot_Num)
    {
        return equippedRightSideWeapons[slot_Num];
    }

    public int getMaxItems()
    {
        return ITEM_SLOTS;
    }
    #endregion
}