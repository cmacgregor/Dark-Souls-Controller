using UnityEngine;
using System.Collections;
using HumanoidCharacterClass;

namespace HumanoidCharacterClass
{
    public partial class HumanoidCharacter : OverworldCharacter
    {

        public enum weapon_Stance
        {
            SingleHanded = 0,
            RightHandedTwoHand = 1,
            LeftHandedTwoHand = 2
        };

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
    }
}