using UnityEngine;
using System.Collections;

namespace Characters.HumanoidCharacter.LocalHumanoidInputController
{
    public class LocalHumanoidInputController : HumanoidInputController, ILocalHumanoidInputController
    {
        //Class to derive humanoid character actions from local controller inputs 

        public float ANALOG_DEAD_ZONE = 0.1f;

        private bool downItemCycle;
        private bool upItemCycle;
        private bool gestureMenu;
        private bool mainMenu;
        private bool toggleTargetCamera;

        public bool DownItemCycle { get => downItemCycle; }
        public bool UpItemCycle { get => upItemCycle; }
        public bool GestureMenu { get => gestureMenu; }
        public bool MainMenu { get => mainMenu; }
        public bool ToggleTargetCamera { get => toggleTargetCamera; }

        public override void ParseInputs()
        {
            base.ParseInputs();
            //analog sticks
            toggleTargetCamera = Input.GetButtonDown("R3");
            //face buttons
            downItemCycle = Input.GetButtonDown("DPadUp");
            upItemCycle = Input.GetButtonDown("DPadDown");
            //menus
            gestureMenu = Input.GetButtonDown("Select");
            mainMenu = Input.GetButtonDown("Start");
        }
    }
}