using UnityEngine;
using System.Collections;

namespace Characters.HumanoidCharacter.LocalHumanoidInputController
{
    public class LocalHumanoidInputController : HumanoidInputController, ILocalHumanoidInputController
    {
        //Class to derive humanoid character actions from local controller inputs 

        public float ANALOG_DEAD_ZONE = 0.1f;

        private bool usableItemCycle;
        private bool skillCycle;
        private bool gestureMenu;
        private bool mainMenu;
        private bool toggleTargetCamera;

        public bool DownItemCycle { get => usableItemCycle; }
        public bool UpItemCycle { get => skillCycle; }
        public bool GestureMenu { get => gestureMenu; }
        public bool MainMenu { get => mainMenu; }
        public bool ToggleTargetCamera { get => toggleTargetCamera; }

        public override void ParseInputs()
        {
            base.ParseInputs();
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