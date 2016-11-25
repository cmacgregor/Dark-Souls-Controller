using UnityEngine;
using System.Collections;

public class player_input_controller : MonoBehaviour
{
	//debug variable
//	static bool DEBUG = true;
	static bool DEBUG = false;
	#region input variables 
    //movement inputs
    float input_h;          //horizontal directional
    float input_v;          //vertical directional
    bool input_dodge;       //dodge 
    bool input_sprint;      //sprint 
    bool input_intearct;    //interact
	//combat inputs		
    bool input_lla;         //left handed light attack
    bool input_lha;         //left handed heavy attack
    bool input_rla;         //right handed light attack
    bool input_rha;         //right handed heavy attack
    bool input_tth;          //toggle hands on weapon
    //held item management inputs
    bool input_use;         //use selected item
    bool input_left;        //direction pad left
    bool input_right;       //direction pad right
    bool input_down;        //direction pad down
    bool input_up;          //direction pad up
                            //misc inputs
    bool input_gestureMenu; //open gestures menu
    bool input_menu;        //open main menu
    bool input_toggleView;  //reset camera
	#endregion
    //Weapon Toggle variables
    int button_held_time;
    enum stance
    {
        SingleHand = 0,
        RightHand = 1,
        LeftHand = 2
    };

    //Character controller 
    CharacterController player_character;
	//Main Camera's transform
	Transform camera_main;
    //<type>	targeted_enemy; 
    
	// Use this for initialization
    void Start()
    {
        //grab main camera
		camera_main = Camera.main.transform;
        //set character controller
		player_character = GetComponent<CharacterController>();
		//set initial button held time
        button_held_time = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        #region parse this frames input
        input_h = Input.GetAxis("Horizontal");
        input_v = Input.GetAxis("Vertical");
        input_dodge = Input.GetButtonDown("Dodge");
        input_sprint = Input.GetButton("Sprint");
        input_intearct = Input.GetButtonDown("Interact");
        input_lla = Input.GetButtonDown("Left Hand Light Attack");
        input_lha = Input.GetButtonDown("Left Hand Heavy Attack");
        input_rla = Input.GetButtonDown("Right Hand Light Attack");
        input_rha = Input.GetButtonDown("Right Hand Heavy Attack");
        input_tth = Input.GetButtonUp("Two-handed Toggle");
        input_use = Input.GetButtonUp("Use Item");
//        input_left = Input.GetButtonDown("");
//        input_right = Input.GetButtonDown("");
//        input_down = Input.GetButtonDown("");
//        input_up = Input.GetButtonDown("");
        input_gestureMenu = Input.GetButtonDown("Gesture Menu");
        input_menu = Input.GetButtonDown("Menu");
        input_toggleView = Input.GetButtonDown("Toggle Targeting");
        #endregion

        if (input_menu)
        {
			if(DEBUG) Debug.Log("input_menu");
        }
        else if (input_gestureMenu)
        {
			if(DEBUG) Debug.Log("input_gestureMenu");
        }
        else if (input_toggleView)
        {
			if(DEBUG) Debug.Log("input_toggleView");
            /*
            //enemy is in line of site
            if (player_character.isInFOV("Enemy"))
            {
                //lock on to enemy
                targeted_enemy = player_chatacter.getClosestEnemiesInFOV();
            }
            //else center camera
            else
            {

            }
            */
        }
        else
        {
            // -Heavy action right 
            if (input_rla)
            {
				if(DEBUG) Debug.Log("input_rla");
				player_character.attack ("Right Handed Light Attack");
            }
            // -Heavy action left
            if (input_rha)
            {
				if(DEBUG) Debug.Log("input_rha");
				player_character.attack ("Right Handed Heavy Attack");
            }
            // -Light action right 
            if (input_lla)
            {
				if(DEBUG) Debug.Log("input_lla");
				player_character.attack ("Light Handed Light Attack");
            }
            // -Light action left 
            if (input_lha)
            {
				if(DEBUG) Debug.Log("input_lha");
				player_character.attack ("Left Handed Heavy Attack");
            }
			// -Kicking attack
//			if (input_rla && (input_h > 0f || input_h > 0f)) 
//			{
//				if (DEBUG) Debug.Log ("input_rla + movement");
//			}
            #region handle two handed stance changes 
            //check if weapon toggle button has been pressed
            if (Input.GetButton("Two-handed Toggle"))
            {
                button_held_time++;
            }
            //if weapon toggle button has been released
            if (input_tth)
            {
				if(DEBUG) Debug.Log("input_tth");
				if(DEBUG) Debug.Log("button_held_time: " + button_held_time);
				if (player_character.Weapon_stance == (int)stance.SingleHand)
                {
                    //if held down breifly two hand left hand 
                    if (button_held_time > 60)
                    {
						if(DEBUG) Debug.Log("stance = LeftHanded");
						player_character.Weapon_stance = (int)stance.LeftHand;
                    }
                    //else two hand right hand wpeaon
                    else
                    {
						if(DEBUG) Debug.Log("stance = RightHanded");
						player_character.Weapon_stance = (int)stance.RightHand;
                    }
                }
                else
                {
					if(DEBUG) Debug.Log("stance = SingleHanded");
					player_character.Weapon_stance = (int)stance.SingleHand;
                }
                button_held_time = 0;
            }
            #endregion
            // -Dodge
            if (input_dodge)
            {
				if(DEBUG) Debug.Log("input_dodge");
				player_character.dodge();	
            }
            #region Handle movement
            if (input_h > 0 || input_v > 0)
            {
				if(DEBUG) Debug.Log("input_h");
				if(DEBUG) Debug.Log("input_v");
                //if locked on
                /*
                if (targeted_enemy != null)
                {
                    //alter movement calculations to be target-centric
                    Debug.Log("targeting enemy");
                }
                */
                // -sprint
                if (input_sprint) //and player stamina
                { //and !player_character.isSprinting()) {
					if(DEBUG) Debug.Log("input_sprint");
					player_character.Sprinting = true;
                }
				else 
				{
					player_character.Sprinting = false;
				}
                // -regular motion
            }
            #endregion
            // -Interact 
            if (input_intearct)
            {
				if(DEBUG) Debug.Log("input_interact");
            }
			// -Use Item
			if (input_use) 
			{
				if (DEBUG) Debug.Log ("input_use");
				player_character.use_item();
			}
        }
    }
}