using UnityEngine;
using System.Collections;

public class player_input_controller : MonoBehaviour {
	//debug variable
//	static bool DEBUG = true;
	public static bool DEBUG = false;
	public static float ANALOG_DEAD_ZONE = 0.1f;
    
	#region input variables 
    //movement inputs
    float input_MoveH;      //horizontal directional
    float input_MoveV;      //vertical directional
    bool input_dodge;       //dodge 
    bool input_sprint;      //sprint 
    bool input_interact;    //interact
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
	private int stance_toggle_held_time;
	private static int STANCE_TOGGLE_HOLD_TIME = 60;
	enum stance
    {
        SingleHand = 0,
        RightHand = 1,
        LeftHand = 2
    };
			
    //Character controller 
    Humanoid_CharacterController player_character;
	//Movement calculation variables 
	Vector3 movement_Vector;
	//<type>	targeted_enemy; 
    
    void Start() {
        //set character controller
		player_character = GetComponent<Humanoid_CharacterController>();
		//set initial button held time
		stance_toggle_held_time = 0;
    }

    // Update is called once per frame
    void Update() {
		#region parse this frames input
		input_MoveH = Input.GetAxis("Horizontal");
		input_MoveV = Input.GetAxis("Vertical");
		input_dodge = Input.GetButtonDown("Dodge");
		input_sprint = Input.GetButton("Sprint");
		input_interact= Input.GetButtonDown("Interact");
		input_lla = Input.GetButtonDown("Left Hand Light Action");
		input_lha = Input.GetButtonUp("Left Hand Heavy Action");
		input_rla = Input.GetButtonDown("Right Hand Light Action");
		input_rha = Input.GetButtonUp("Right Hand Heavy Action");
		input_tth = Input.GetButtonUp("Two-handed Toggle");
		input_use = Input.GetButtonDown("Use Item");
		input_left = Input.GetButtonDown("Cycle Left Weapons");
		input_right = Input.GetButtonDown("Cycle Right Weapons");
		input_down = Input.GetButtonDown("Cycle Ready Items");
		input_up = Input.GetButtonDown("Cycle Ready Spells");
		input_gestureMenu = Input.GetButtonDown("Gesture Menu");
		input_menu = Input.GetButtonDown("Menu");
		input_toggleView = Input.GetButtonDown("Toggle Targeting");
		#endregion

		#region Handle movement
		if (input_MoveH != ANALOG_DEAD_ZONE || input_MoveV != ANALOG_DEAD_ZONE)	{
			if(DEBUG) Debug.Log("input_MoveH" + input_MoveH);
			if(DEBUG) Debug.Log("input_MoveV" + input_MoveV);

			movement_Vector = new Vector3(input_MoveH, 0, input_MoveV);
			player_character.Move(movement_Vector);
			// -sprint
			if (input_sprint && !player_character.Sprint) { //and player stamina allows 
				if(DEBUG) Debug.Log("input_sprint");
				player_character.Sprint = true;
			}
			// -regular motion
			else {
				player_character.Sprint = false;
			}
		}
		#endregion
		
		#region Handle Main Menu
		if (input_menu)	{
			if(DEBUG) Debug.Log("input_menu");
		}
		#endregion
		
		#region Handle Gesture Menu
		else if (input_gestureMenu)	{
			if(DEBUG) Debug.Log("input_gestureMenu");
		}
		#endregion

		#region Handle Non-menu Button inputs 
		else {
			#region Handle weapon actions
			//Light actions
			//Kick
			if (movement_Vector.magnitude <= 0.2 ){ 
				if (DEBUG) Debug.Log ("movement + rha");
				player_character.rightSideAction(1);
			}
			//Light action right
			else if (input_rla)	{
				if(DEBUG) Debug.Log("input_rla");
				player_character.rightSideAction(0);
			}
			//Light action Left
			if (input_lla)	{
				if(DEBUG) Debug.Log("input_lla");
				player_character.leftSideAction(0);
			}
			//heavy actions
			//lunge attack 
			if (movement_Vector.magnitude <= 0.2 ){ 
				if (DEBUG) Debug.Log ("movement + rha");
				player_character.rightSideAction(4);
			}
			//Heavy Hold Right
			else if (Input.GetButton ("Right Hand Heavy Action")) {
				player_character.rightSideAction(2);
			}
			//Heavy Hold left
			if (Input.GetButton ("Left Hand Heavy Action")) {
				player_character.leftSideAction(2);
			}
			// -Heavy action Right
			if (input_rha)	{
				if(DEBUG) Debug.Log("input_rha");
				player_character.rightSideAction(3);
			}
			// -Heavy action Left 
			if (input_lha)	{
				if(DEBUG) Debug.Log("input_lha");
				player_character.leftSideAction(3);
			}		
			#region handle two handed stance changes 
			//check if weapon toggle button has been pressed
			if (Input.GetButton("Two-handed Toggle")) {
				stance_toggle_held_time++;
			}
			//if weapon toggle button has been released
			if (input_tth)	{
				if(DEBUG) Debug.Log("input_tth");
				if(DEBUG) Debug.Log("button_held_time: " + stance_toggle_held_time);
				if (player_character.getWeaponStance() == (int)stance.SingleHand) {
					//if held down breifly two hand left hand 
					if (stance_toggle_held_time > STANCE_TOGGLE_HOLD_TIME) {
						if(DEBUG) Debug.Log("stance = LeftHanded");
						player_character.setWeaponStance((int)stance.LeftHand);
					}
					//else two hand right hand wpeaon
					else {
						if(DEBUG) Debug.Log("stance = RightHanded");
						player_character.setWeaponStance((int)stance.RightHand);
					}
				}
				else {
					if(DEBUG) Debug.Log("stance = SingleHanded");
					player_character.setWeaponStance((int)stance.SingleHand);
				}
				stance_toggle_held_time = 0;
			}
			#endregion
			#endregion	
			// -Dodge
			if (input_dodge) {
				if(DEBUG) Debug.Log("input_dodge");
				player_character.dodge();	
			}
			// -Interact 
			if (input_interact)	{
				if(DEBUG) Debug.Log("input_interact");
				player_character.interact();
			}
			// -Use Item
			if (input_use) {
				if (DEBUG) Debug.Log ("input_use");
				player_character.useItem();
			}
			//Toggle Targeting or center camera 
			if (input_toggleView)	{
				if(DEBUG) Debug.Log("input_toggleView");
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
			#region Handle Directional buttons
			if (input_left) {
				player_character.cycleLeftWeapon();
			} else if (input_right) {
				player_character.cycleRightWeapon();
			} else if (input_up) {
				player_character.cycleSpells();
			} else if (input_down) {
				player_character.cycleReadyItem();
			}
			#endregion
		}
		#endregion
    }

    void FixedUpdate() {
		//called at a fixed interval independant of fps 
	}
	
	void LateUpdate() {
		//Called after all other updates
		//move_Vector = new Vector3(0,0,0);
	}
}