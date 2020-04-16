using UnityEngine;
using System;

public class HumanoidCharacterActionController : IActionController
 {
    //class to derive character actions from input controller
    IHumanoidInputController inputController; 
    HumanoidCharacterActions characterActions;
    //Player stats class
    //Player equippedItemsManager
    //<type>	targeted_enemy; 

    #region Needs to be moved to EquippedItemsManager class
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
    public void SetEquippedItem(ushort item_Num, int item_Slot)
    {
        try
        {
            equippedItems[item_Slot] = item_Num;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
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
            Debug.LogException(e);
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
            Debug.LogException(e);
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
            Debug.LogException(e);
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
            Debug.LogException(e);
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

    #endregion

    public void handleInputs()
    {
        deriveActions();
        //handleLocomotion();
    }

    //Needs to be moved to inheritied class for local player
    //private void handleMenus()
    //{
    //    if (input_mainMenu)
    //    {
    //        Debug.Log("input_menu");
    //    }
    //    else if (input_secondaryMenu)
    //    {
    //        Debug.Log("input_secondaryMenu");
    //    }
    //}

    private void deriveActions()
    {
        #region Handle Equipement Cycling
        if (inputController.LeftSideItemCycle)
        {
            //playerCharacter.cycleLeftWeapon();
        }
        else if (inputController.RightSideItemCycle)
        {
            //playerCharacter.cycleRightWeapon();
        }
        #endregion

        #region Handle Non-menu actions

        #region Handle weapon actions

        #region Light actions
        //Light action right
        if (inputController.RightLightAction)
        {
            //Kick
            //if (movement_Vector.magnitude < ANALOG_DEAD_ZONE)
            //{
            //    playerCharacter.rightSideAction(1);
            //}
            //else
            //{
            //    playerCharacter.rightSideAction(0);
            //}
        }

        //Light action Left
        if (inputController.LeftLightAction)
        {
            //playerCharacter.leftSideAction(0);
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
        if (inputController.RightHeavyAction)
        {
            //lunge attack 
            //if (Input.GetButton("Right Hand Heavy Action") && movement_Vector.magnitude < ANALOG_DEAD_ZONE)
            //{
            //    playerCharacter.rightSideAction(4);
            //}
            //else
            //{
            //    playerCharacter.rightSideAction(3);
            //}
        }
        // -Heavy action Left 
        if (inputController.LeftHeavyAction)
        {
            //playerCharacter.leftSideAction(3);
        }
        #endregion

        #endregion

        #region handle two handed stance changes 
        //check if weapon toggle button has been pressed
        //if (Input.GetButton("Two-handed Toggle"))
        //{
        //    stance_toggle_held_time++;
        //}
        ////if weapon toggle button has been released
        //if (input_toggleTwoHanded)
        //{
        //    if (playerCharacter.getWeaponStance() == HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.SingleHanded)
        //    {
        //        //if held down two hand left hand 
        //        if (stance_toggle_held_time > STANCE_TOGGLE_HOLD_TIME)
        //        {
        //            playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.LeftHandedTwoHand);
        //        }
        //        //else two hand right hand weapon
        //        else
        //        {
        //            playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.RightHandedTwoHand);
        //        }
        //    }
        //    else
        //    {
        //        playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.SingleHanded);
        //    }
        //    stance_toggle_held_time = 0;
        //}
        #endregion

        // -Dodge
        if (inputController.Dodge)
        {
            //If Stamina allows
            characterActions.Dodge();
        }

        // -Interact 
        if (inputController.Interact)
        {
            //playerCharacter.interact();
        }

        // -Use Item
        if (inputController.UseItem)
        {
            characterActions.UseItem();
            //playerCharacter.useItem();
        }
        #endregion
    }
}