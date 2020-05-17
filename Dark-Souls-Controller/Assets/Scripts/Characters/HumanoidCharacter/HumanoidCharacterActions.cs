using UnityEngine;

namespace Characters.HumanoidCharacter
{
    public class HumanoidCharacterActions : CharacterActions, ICharacterActions
    {
        //Class to control all visible actions a character can perform
        Animator characterAnimator;
        CharacterController characterController;

        //public Vector3 movement;            //movement vector
        //public bool interact;                //interact
        //public bool dodge;                   //dodge 
        //public bool useItem;                 //use selected item
        //public bool toggleTwoHanded;         //toggle hands on weapon

        //public bool downItemCycle;           //direction pad down
        //public bool upItemCycle;             //direction pad up

        //public bool leftLightAction;         //left handed light attack
        //public bool leftHeavyAction;         //left handed heavy attack
        //public bool rightLightAction;        //right handed light attack
        //public bool rightHeavyAction;        //right handed heavy attack
        //public bool leftSideItemCycle;       //direction pad left
        //public bool rightSideItemCycle;      //direction pad right

        //local player only actions inputs
        //public bool gestureMenu;             //open gestures menu
        //public bool mainMenu;                //open main menu
        //public float axisHorizontal;         //horizontal directional
        //public float axisVertical;           //vertical directional
        //public bool toggleTargetCamera;      //reset camera

        //Character movement variables 
        public bool SetMove = false;
        public Vector3 MovementDirection;

        public bool SetDodge = false;

        private bool sprinting = false;
        public float sprintSpeed = 4.0f;
        public float sprintSpeedMultiplier = 2.0f;

        //character gravity variables 
        bool m_IsGrounded;
        Vector3 m_GroundNormal;
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

        //Weapon Toggle variables
        private int stance_toggle_held_time = 0;
        private static int STANCE_TOGGLE_HOLD_TIME = 60;

        public CharacterController CharacterController { get => characterController; set => characterController = value; }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            characterAnimator.SetBool("Moving", SetMove);
            if(SetMove)
            { 
                Move(MovementDirection);
            }

        }

        //THIS SHOULD BE IN LOcAL PLATER ONLY CLASS
        public void SetCharacterRotationBasedOffCamera()
        {
            //rotate character based upon the current camera position
            CharacterController.transform.rotation =
                Quaternion.Euler(CharacterController.transform.eulerAngles.x,
                                    Camera.main.transform.eulerAngles.y,
                                    CharacterController.transform.eulerAngles.z);
        }

        public void Move(Vector3 moveVector)
        {
            SetCharacterRotationBasedOffCamera(); 
            if (characterController.isGrounded)
            {
                //rotate character based upon the current camera position
                CharacterController.transform.rotation = Quaternion.Euler(CharacterController.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, CharacterController.transform.eulerAngles.z);

                characterAnimator.SetBool("Falling", false);
                handleGroundedMovement(moveVector);
            }
            else if(!characterController.isGrounded)
            {
                characterAnimator.SetBool("Falling", true);
                handleAirborneMovement();
            }
        }

        public void cycleRightSideItem()
        {
            characterAnimator.SetTrigger("CycleRightSideItem");
        }

        public void cycleLeftSideItem()
        {
            characterAnimator.SetTrigger("CycleLeftSideItem");
        }

        public void UseItem()
        {
            // based upon selected item play animation and apply effect
            //Debug.Log(equippedItems[activeReadyItemSlot]);
        }

        public void Dodge()
        {
            characterAnimator.SetTrigger("Dodge");
            //turn collisions off 
        }

        public void setWeaponStance(weapon_Stance stance)
        {
            if ((int)stance > 3) stance = 0;
            characterAnimator.SetInteger("Stance", (int)stance);
        }

        public weapon_Stance getWeaponStance()
        {
            return (weapon_Stance)characterAnimator.GetInteger("Stance");
        }


        private void handleGroundedMovement(Vector3 moveVector)
        {
            moveVector = this.transform.TransformDirection(moveVector);

            characterAnimator.SetFloat("Locomotion", moveVector.magnitude, 0f, Time.deltaTime);
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(moveVector), 500f);
            }
            if (sprinting)
            {
                moveVector *= sprintSpeed;
            }
            //Apply Character Weight Modifier to movement Vector 
            moveVector *= sprintSpeedMultiplier;
            moveVector *= Time.deltaTime;
            //characterController.Move(moveVector);
        }

        private void OLD_handleGroundedMovement(Vector3 moveVector)
        {
            if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                //rotate character based upon the current camera position
                transform.rotation =
                    Quaternion.Euler(characterController.transform.eulerAngles.x,
                                     Camera.main.transform.eulerAngles.y,
                                     characterController.transform.eulerAngles.z);
            }

            moveVector = this.transform.TransformDirection(moveVector);

            characterAnimator.SetFloat("Locomotion", moveVector.magnitude, 0f, Time.deltaTime);
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(moveVector), 500f);
            }
            if (sprinting)
            {
                moveVector *= sprintSpeed;
            }
            //Apply Character Weight Modifier to movement Vector 
            moveVector *= sprintSpeedMultiplier;
            moveVector *= Time.deltaTime;
            characterController.Move(moveVector);
        }

        private void handleAirborneMovement()
        {
            characterController.Move(new Vector3(0, -Gravity, 0) * Time.deltaTime);
        }
    }
}
