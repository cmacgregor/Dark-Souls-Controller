using UnityEngine;

namespace Characters.HumanoidCharacter
{
    public class HumanoidInputController : CharacterInputController, IHumanoidInputController
    {
        Vector3 movementVector;
        float axisHorizontal;
        float axisVertical;
        bool leftLightAction;
        bool leftHeavyAction;
        bool rightLightAction;
        bool rightHeavyAction;
        bool toggleTwoHanded;
        bool useItem;
        bool dodge;
        bool interact;
        bool leftSideItemCycle;
        bool rightSideItemCycle;

        public Vector3 MovementVector { get => movementVector; }
        public float AxisHorizontal { get => axisHorizontal; }
        public float AxisVertical { get => axisVertical; }
        public bool LeftLightAction { get => leftLightAction; }
        public bool LeftHeavyAction { get => leftHeavyAction; }
        public bool RightLightAction { get => rightLightAction; }
        public bool RightHeavyAction { get => rightHeavyAction; }
        public bool ToggleTwoHanded { get => toggleTwoHanded; }
        public bool UseItem { get => useItem; }
        public bool Dodge { get => dodge; }
        public bool Interact { get => interact; }
        public bool LeftSideItemCycle { get => leftSideItemCycle; }
        public bool RightSideItemCycle { get => rightSideItemCycle; }

        public virtual void ParseInputs()
        {
            //analog sticks
            axisHorizontal = Input.GetAxis("LeftStickX");
            axisVertical = Input.GetAxis("LeftStickY");
            //shoulder buttons
            leftLightAction = Input.GetButtonDown("L1");
            leftHeavyAction = Input.GetButtonUp("L2");
            rightLightAction = Input.GetButtonDown("R1");
            rightHeavyAction = Input.GetButtonUp("R2");
            //face buttons
            toggleTwoHanded = Input.GetButtonUp("TopButton");
            useItem = Input.GetButtonDown("RightButton");
            dodge = Input.GetButtonDown("LeftButton");
            interact = Input.GetButtonDown("ButtomButton");
            leftSideItemCycle = Input.GetButtonDown("DPadLeft");
            rightSideItemCycle = Input.GetButtonDown("DPadRight");
        }
    }
}
