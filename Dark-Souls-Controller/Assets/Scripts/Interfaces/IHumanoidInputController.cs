using UnityEngine;

namespace Characters.HumanoidCharacter
{
    interface IHumanoidInputController : ICharacterInputController
    {
        float AxisHorizontal { get; }
        float AxisVertical { get; }
        bool LeftLightAction { get; }
        bool LeftHeavyAction { get; }
        bool RightLightAction { get; }
        bool RightHeavyAction { get; }
        bool ToggleTwoHanded { get; }
        bool UseItem { get; }
        bool Dodge { get; }
        bool Interact { get; }
        bool LeftSideItemCycle { get; }
        bool RightSideItemCycle { get; }
    }
}