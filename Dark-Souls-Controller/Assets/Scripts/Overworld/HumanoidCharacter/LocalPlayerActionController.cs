using UnityEngine;
using System.Collections.Generic;

public class LocalPlayerActionController : IActionController, IKillable, IDamageable
 {

    private bool sprinting = false;

    private float interactionRadius = 1.0f;

    //Character movement variables 
    public float sprintSpeed = 4.0f;
    public float weightSpeedMultiplier = 2.0f;

    //character gravity variables 
    public float Gravity = 21f;
    public float terminalVelocity = 20f;
    
    public enum weapon_Stance
    {
        SingleHanded = 0,
        RightHandedTwoHand = 1,
        LeftHandedTwoHand = 2
    };

    public enum actionID
    {
        Light,
        Kick,
        Heavy,
        Charged,
        Lunge
    };

    //active equipment definitions 
    public const int ITEM_SLOTS = 5;
    public const int WEAPON_SLOTS = 3;
    public const int SKILL_SLOTS = 3;
    public const int ARMOR_SLOTS = 4;

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

    private void handleInputs( List<bool> currentInputs)
    {
        if (input_mainMenu || input_secondaryMenu)
        {
            handleMenus();
        }
        else
        {
            handleActions();
        }
        handleLocomotion();
    }

    private void handleMenus()
    {
        if (input_mainMenu)
        {
            if (DEBUG) Debug.Log("input_menu");
        }
        else if (input_secondaryMenu)
        {
            if (DEBUG) Debug.Log("input_secondaryMenu");
        }
    }

    private void handleActions()
    {
        #region Handle Equipement Cycling
        if (input_leftSideItemCycle)
        {
            playerCharacter.cycleLeftWeapon();
        }
        else if (input_rightSideItemCycle)
        {
            playerCharacter.cycleRightWeapon();
        }
        else if (input_upItemCycle)
        {
            playerCharacter.cycleSkills();
        }
        else if (input_downItemCycle)
        {
            playerCharacter.cycleReadyItem();
        }
        #endregion

        #region Handle Toggle View
        if (input_toggleTargetCamera)
        {
            if (DEBUG) Debug.Log("input_toggleView");
            //            //enemy is in line of site
            //			if (player_character.isInFOV("Enemy")) {
            //				//lock on to enemy
            //				targeted_enemy = player_chatacter.getClosestEnemiesInFOV();
            //			}
            //			//else center camera
            //			else {
            //
            //			}
        }
        #endregion

        #region Handle Non-menu actions

        #region Handle weapon actions

        #region Light actions
        //Light action right
        if (input_rightLightAttack)
        {
            //Kick
            if (movement_Vector.magnitude < ANALOG_DEAD_ZONE)
            {
                playerCharacter.rightSideAction(1);
            }
            else
            {
                playerCharacter.rightSideAction(0);
            }
        }

        //Light action Left
        if (input_leftLightAttack)
        {
            playerCharacter.leftSideAction(0);
        }
        #endregion

        #region Heavy Actions
        //Heavy Hold Right
        //if (Input.GetButton ("Right Hand Heavy Action")) {
        //	player_character.rightSideAction(2);
        //}
        //Heavy Hold left
        //if (Input.GetButton ("Left Hand Heavy Action")) {
        //	player_character.leftSideAction(2);
        //}
        // -Heavy action Right
        if (input_rightHeavyAttack)
        {
            //lunge attack 
            if (Input.GetButton("Right Hand Heavy Action") && movement_Vector.magnitude < ANALOG_DEAD_ZONE)
            {
                playerCharacter.rightSideAction(4);
            }
            else
            {
                if (DEBUG) Debug.Log("input_rha");
                playerCharacter.rightSideAction(3);
            }
        }
        // -Heavy action Left 
        if (input_leftHeavyAttack)
        {
             playerCharacter.leftSideAction(3);
        }
        #endregion

        #endregion

        #region handle two handed stance changes 
        //check if weapon toggle button has been pressed
        if (Input.GetButton("Two-handed Toggle"))
        {
            stance_toggle_held_time++;
        }
        //if weapon toggle button has been released
        if (input_toggleTwoHanded)
        {
            if (playerCharacter.getWeaponStance() == HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.SingleHanded)
            {
                //if held down two hand left hand 
                if (stance_toggle_held_time > STANCE_TOGGLE_HOLD_TIME)
                {
                    playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.LeftHandedTwoHand);
                }
                //else two hand right hand weapon
                else
                {
                    playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.RightHandedTwoHand);
                }
            }
            else
            {
                if (DEBUG) Debug.Log("stance = SingleHanded");
                playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.SingleHanded);
            }
            stance_toggle_held_time = 0;
        }
        #endregion

        // -Dodge
        if (input_dodge)
        {
            if (DEBUG) Debug.Log("input_dodge");
            playerCharacter.dodge();
        }

        // -Interact 
        if (input_interact)
        {
            if (DEBUG) Debug.Log("input_interact");
            playerCharacter.interact();
        }

        // -Use Item
        if (input_useItem)
        {
            if (DEBUG) Debug.Log("input_use");
            playerCharacter.useItem();
        }
        #endregion
    }

    #region Interface Functions
    public void Damage(int damageInflicted)
    {
        //calculated damge taken 

        //play corresponding animation

    }

    public void Kill()
    {
        //play animation

        //display game over message
    }
    #endregion
    
    private void handleLocomotion()
    {
        if (input_axisHorizontal != ANALOG_DEAD_ZONE || input_axisVertical != ANALOG_DEAD_ZONE)
        {
            //if (DEBUG) Debug.Log("input_MoveH" + input_axisHorizontal);
            //if (DEBUG) Debug.Log("input_MoveV" + input_axisVertical);

            // -sprint
            if (input_sprint)
            { //and player stamina allows 
                playerCharacter.Sprint = true;
            }
            // -regular motion
            else
            {
                playerCharacter.Sprint = false;
            }
            movement_Vector = new Vector3(input_axisHorizontal, 0, input_axisVertical);
            playerCharacter.Move(movement_Vector);
        }
    }

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
    public void checkForInteraction()
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
    #endregion

       #region Movement Functions
    public void Move(Vector3 moveVector)
    {
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

    #region Weapon Action Functions
    public void leftSideAction(short action)
    {
        animationTree.SetBool("LeftSideAction", true);
        weaponAction((actionID)action);
    }

    public void rightSideAction(short action)
    {
        animationTree.SetBool("LeftSideAction", false);
        weaponAction((actionID)action);
    }

    private void weaponAction(actionID action)
    {
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
    
    #region Equipment Item Management Functions
    public void cycleReadyItem()
    {
        activeReadyItemSlot = cycleEquipment(ref equippedItems, activeReadyItemSlot);
    }

    public void cycleSkills()
    {
        activeSkillSlot = cycleEquipment(ref equippedSkills, activeSkillSlot);
    }

    public void cycleLeftWeapon()
    {
        activeLeftWeaponSlot = cycleWeapon(ref equippedLeftSideWeapons, ref activeLeftWeaponSlot);
        //play weapon switch animation
    }

    public void cycleRightWeapon()
    {
        activeRightWeaponSlot = cycleWeapon(ref equippedRightSideWeapons, ref activeRightWeaponSlot);
        //play weapon switch animation
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

    public void handleInputs(List<bool> currentInputs)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}