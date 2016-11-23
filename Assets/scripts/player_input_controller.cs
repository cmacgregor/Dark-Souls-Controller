using UnityEngine;
using System.Collections;

public class player_input_controller : MonoBehaviour
{
    //input variables 
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

    //Weapon Toggle variables
    int button_held_time;
    enum stance : int
    {
        SingleHanded,
        RightHanded,
        LeftHanded
    };

    //Character controller 
    //CharacterController player_character;
    //<type>	targeted_enemy; 
    // Use this for initialization
    void Start()
    {
        //grab main camera

        //set character controller

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
        input_sprint = Input.GetButtonDown("Sprint");
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

        }
        else if (input_gestureMenu)
        {

        }
        else if (input_toggleView)
        {
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
                Debug.Log("input_rla");
            }
            // -Heavy action left
            if (input_rha)
            {
                Debug.Log("input_rha");
            }
            // -Light action right 
            if (input_lla)
            {
                Debug.Log("input_lla");
            }
            // -Light action left 
            if (input_lha)
            {
                Debug.Log("input_lha");
            }
            #region handle two handed stance changes 
            //check if weapon toggle button has been pressed
            if (Input.GetButton("Two-handed Toggle"))
            {
                button_held_time++;
            }
            //if weapon toggle button has been released
            if (input_tth)
            {
                Debug.Log("input_tth");
                Debug.Log("button_held_time: " + button_held_time);
                if (0 == 0)
                {
                    //if held down breifly two hand left hand 
                    if (button_held_time > 60)
                    {
                        Debug.Log("stance = LeftHanded");
                    }
                    //else two hand right hand wpeaon
                    else
                    {
                        Debug.Log("stance = RightHanded");
                    }
                }
                else
                {
                    Debug.Log("stance = SingleHanded");
                }
                button_held_time = 0;
            }
            #endregion
            // -Dodge
            if (input_dodge)
            {
                Debug.Log("input_dodge");
                // -sprint dodge with ending roll
                // -regular roll 	
            }
            #region Handle movement
            if (input_h > 0 || input_v > 0)
            {
                Debug.Log("input_h");
                Debug.Log("input_v");
                //if locked on
                /*
                if (targeted_enemy != null)
                {
                    //alter movement calculations to be target-centric
                    Debug.Log("targeting enemy");
                }
                */
                // -sprint
                if (input_sprint)
                { //and !player_character.isSprinting()) {
                    Debug.Log("input_sprint");
                    //player_character.toggleSprint();
                }
                // -regular motion
            }
            #endregion
            // -Interact 
            if (input_intearct)
            {
                Debug.Log("input_interact");
            }
        }
    }
}