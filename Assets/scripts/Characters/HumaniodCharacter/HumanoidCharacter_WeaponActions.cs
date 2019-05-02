using UnityEngine;
using System.Collections;

namespace HumanoidCharacterClass
{
    public partial class HumanoidCharacter : OverworldCharacter
    {
        //Weapon Actions
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
    }
}