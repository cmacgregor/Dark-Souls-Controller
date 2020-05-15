using UnityEngine;
using System.Collections;

namespace Characters.HumanoidCharacter.LocalHumanoidInputController
{
    public class LocalHumanoidInputController : HumanoidInputController, ILocalHumanoidInputController
    {
        //Class to derive humanoid character actions from local controller inputs 

        public float ANALOG_DEAD_ZONE = 0.1f;

        protected bool usableItemCycle;
        protected private bool skillCycle;
        protected private bool gestureMenu;
        protected private bool mainMenu;
        protected private bool toggleTargetCamera;

        public bool DownItemCycle { get => usableItemCycle; }
        public bool UpItemCycle { get => skillCycle; }
        public bool GestureMenu { get => gestureMenu; }
        public bool MainMenu { get => mainMenu; }
        public bool ToggleTargetCamera { get => toggleTargetCamera; }

        public override void ParseInputs()
        {
            //analog sticks
            axisHorizontal = Input.GetAxis("HorizontalMovement");
            axisVertical = Input.GetAxis("VerticalMovement");
            //shoulder buttons
            leftLightAction = Input.GetButtonDown("LeftLightAction");
            leftHeavyAction = Input.GetButtonDown("LeftHeavyAction");
            rightLightAction = Input.GetButtonDown("RightLightAction");
            rightHeavyAction = Input.GetButtonDown("RightHeavyAction");
            //face buttons
            toggleTwoHanded = Input.GetButtonDown("ToggleTwoHanded");
            useItem = Input.GetButtonDown("UseItem");
            dodge = Input.GetButtonDown("Dodge");
            interact = Input.GetButtonDown("Interact");
            leftSideItemCycle = Input.GetButtonDown("LeftSideItemCycle");
            rightSideItemCycle = Input.GetButtonDown("RightSideItemCycle");

            //analog sticks
            toggleTargetCamera = Input.GetButtonDown("ToggleTargetCamera");
            //face buttons
            usableItemCycle = Input.GetButtonDown("UsableItemCycle");
            skillCycle = Input.GetButtonDown("SkillCycle");
            //menus
            gestureMenu = Input.GetButtonDown("GestureMenu");
            mainMenu = Input.GetButtonDown("MainMenu");
        }
    }
}