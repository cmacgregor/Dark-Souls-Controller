using UnityEngine;
using System.Collections;
using HumanoidCharacterClass;

public class localInputController : CharacterActionController {
	//debug variable
	public bool DEBUG = false;
	public float ANALOG_DEAD_ZONE = 0.1f;
    
	#region input variables 
    //movement inputs
    float input_axisHorizontal;      //horizontal directional
    float input_axisVertical;      //vertical directional
    bool input_dodge;       //dodge 
    bool input_sprint;      //sprint 
    bool input_interact;    //interact
	//combat inputs		
    bool input_leftLightAttack;         //left handed light attack
    bool input_leftHeavyAttack;         //left handed heavy attack
    bool input_rightLightAttack;         //right handed light attack
    bool input_rightHeavyAttack;         //right handed heavy attack
    bool input_toggleTwoHanded;          //toggle hands on weapon
    //held item management inputs
    bool input_useItem;         //use selected item
    bool input_leftSideItemCycle;        //direction pad left
    bool input_rightSideItemCycle;       //direction pad right
    bool input_downItemCycle;        //direction pad down
    bool input_upItemCycle;          //direction pad up
	//misc inputs
    bool input_gestureMenu; //open gestures menu
    bool input_mainMenu;        //open main menu
    bool input_toggleTargetCamera;  //reset camera
    #endregion

    //Weapon Toggle variables
    private int stance_toggle_held_time = 0;
	private static int STANCE_TOGGLE_HOLD_TIME = 60;

    //Character controller 
    HumanoidCharacterClass.HumanoidCharacter playerCharacter;
	//Movement calculation variables 
	Vector3 movement_Vector;
	//<type>	targeted_enemy; 
    
    void Start() {
        //set character controller
		playerCharacter = GetComponent<HumanoidCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        parseInputs();
        handleInputs();
    }
        
    private void handleInputs()
    {
        if (input_mainMenu || input_gestureMenu)
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
        else if (input_gestureMenu)
        {
            if (DEBUG) Debug.Log("input_gestureMenu");
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
                if (DEBUG) Debug.Log("movement + rha");
                playerCharacter.rightSideAction(1);
            }
            else
            {
                if (DEBUG) Debug.Log("input_rla");
                playerCharacter.rightSideAction(0);
            }
        }

        //Light action Left
        if (input_leftLightAttack)
        {
            if (DEBUG) Debug.Log("input_lla");
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
                if (DEBUG) Debug.Log("movement + rha");
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
            if (DEBUG) Debug.Log("input_lha");
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
            if (DEBUG) Debug.Log("input_tth");
            if (DEBUG) Debug.Log("button_held_time: " + stance_toggle_held_time);
            if (playerCharacter.getWeaponStance() == HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.SingleHanded)
            {
                //if held down two hand left hand 
                if (stance_toggle_held_time > STANCE_TOGGLE_HOLD_TIME)
                {
                    if (DEBUG) Debug.Log("stance = LeftHanded");
                    playerCharacter.setWeaponStance(HumanoidCharacterClass.HumanoidCharacter.weapon_Stance.LeftHandedTwoHand);
                }
                //else two hand right hand weapon
                else
                {
                    if (DEBUG) Debug.Log("stance = RightHanded");
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

    private void parseInputs()
    {
        input_axisHorizontal = Input.GetAxis("Horizontal");
        input_axisVertical = Input.GetAxis("Vertical");
        input_sprint = Input.GetButton("Sprint");
        input_toggleTargetCamera = Input.GetButtonDown("Toggle Targeting");
        input_leftLightAttack = Input.GetButtonDown("Left Hand Light Action");
        input_leftHeavyAttack = Input.GetButtonUp("Left Hand Heavy Action");
        input_rightLightAttack = Input.GetButtonDown("Right Hand Light Action");
        input_rightHeavyAttack = Input.GetButtonUp("Right Hand Heavy Action");
        input_toggleTwoHanded = Input.GetButtonUp("Two-handed Toggle");
        input_useItem = Input.GetButtonDown("Use Item");
        input_dodge = Input.GetButtonDown("Dodge");
        input_interact = Input.GetButtonDown("Interact");
        input_leftSideItemCycle = Input.GetButtonDown("Cycle Left Weapons");
        input_rightSideItemCycle = Input.GetButtonDown("Cycle Right Weapons");
        input_downItemCycle = Input.GetButtonDown("Cycle Ready Items");
        input_upItemCycle = Input.GetButtonDown("Cycle Ready Spells");
        input_gestureMenu = Input.GetButtonDown("Gesture Menu");
        input_mainMenu = Input.GetButtonDown("Menu");
    }
}