using UnityEngine;

namespace Characters.HumanoidCharacter
{
    public class HumanoidInputController : CharacterInputController, IHumanoidInputController
    {
        protected float axisHorizontal;
        protected float axisVertical;
        protected bool leftLightAction;
        protected bool leftHeavyAction;
        protected bool rightLightAction;
        protected bool rightHeavyAction;
        protected bool toggleTwoHanded;
        protected bool useItem;
        protected bool sprint;
        protected bool dodge;
        protected bool interact;
        protected bool leftSideItemCycle;
        protected bool rightSideItemCycle;

        public float AxisHorizontal { get => axisHorizontal; }
        public float AxisVertical { get => axisVertical; }
        public bool LeftLightAction { get => leftLightAction; }
        public bool LeftHeavyAction { get => leftHeavyAction; }
        public bool RightLightAction { get => rightLightAction; }
        public bool RightHeavyAction { get => rightHeavyAction; }
        public bool ToggleTwoHanded { get => toggleTwoHanded; }
        public bool UseItem { get => useItem; }
        public bool Sprint { get => sprint; }
        public bool Dodge { get => dodge; }
        public bool Interact { get => interact; }
        public bool LeftSideItemCycle { get => leftSideItemCycle; }
        public bool RightSideItemCycle { get => rightSideItemCycle; }
        
        public override void ParseInputs()
        {

        }
    }
}
