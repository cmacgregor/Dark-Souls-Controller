using UnityEngine;

namespace Characters.HumanoidCharacter
{
    public class HumanoidInputController : CharacterInputController, IHumanoidInputController
    {
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
        }
    }
}
