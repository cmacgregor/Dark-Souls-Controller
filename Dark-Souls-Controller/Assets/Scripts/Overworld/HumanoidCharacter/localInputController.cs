using UnityEngine;
using System.Collections;

public class LocalInputController : ICharacterController
 {
    #region input variables 
    public class inputStruct
    {
        //movement inputs
        public float axisHorizontal;         //horizontal directional
        public float axisVertical;           //vertical directional
        public bool toggleTargetCamera;      //reset camera
        public bool dodge;                   //dodge 
        public bool interact;                //interact
	    //combat inputs		
        public bool leftLightAction;         //left handed light attack
        public bool leftHeavyAction;         //left handed heavy attack
        public bool rightLightAction;        //right handed light attack
        public bool rightHeavyAction;        //right handed heavy attack
        public bool toggleTwoHanded;         //toggle hands on weapon
        //held item management inputs
        public bool useItem;                 //use selected item
        public bool leftSideItemCycle;       //direction pad left
        public bool rightSideItemCycle;      //direction pad right
        public bool downItemCycle;           //direction pad down
        public bool upItemCycle;             //direction pad up
        //misc inputs
        public bool gestureMenu;             //open gestures menu
        public bool mainMenu;                //open main menu
    }
    #endregion

    public void parseInputs(ref inputStucrt currentInputs)
    {
        //analog sticks
        currentInputs.axisHorizontal = Input.GetAxis("Horizontal");
        currentInputs.axisVertical = Input.GetAxis("Vertical");
        currentInputs.toggleTargetCamera = Input.GetButtonDown("Toggle Targeting");
        //shoulder buttons
        currentInputs.leftLightAction = Input.GetButtonDown("Left Hand Light Action");
        currentInputs.leftHeavyAction = Input.GetButtonUp("Left Hand Heavy Action");
        currentInputs.rightLightAction = Input.GetButtonDown("Right Hand Light Action");
        currentInputs.rightHeavyAction = Input.GetButtonUp("Right Hand Heavy Action");
        //face buttons
        currentInputs.toggleTwoHanded = Input.GetButtonUp("Two-handed Toggle");
        currentInputs.useItem = Input.GetButtonDown("Use Item");
        currentInputs.dodge = Input.GetButtonDown("Dodge");
        currentInputs.interact = Input.GetButtonDown("Interact");
        currentInputs.leftSideItemCycle = Input.GetButtonDown("Cycle Left Weapons");
        currentInputs.rightSideItemCycle = Input.GetButtonDown("Cycle Right Weapons");
        currentInputs.downItemCycle = Input.GetButtonDown("Cycle Ready Items");
        currentInputs.upItemCycle = Input.GetButtonDown("Cycle Ready Spells");
        //menus
        currentInputs.gestureMenu = Input.GetButtonDown("Gesture Menu");
        currentInputs.mainMenu = Input.GetButtonDown("Menu");
    }
}