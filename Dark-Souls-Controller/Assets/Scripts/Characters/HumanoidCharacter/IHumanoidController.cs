using UnityEngine;

interface IHumanoidController : ICharacterController
{
    Vector3 MovementVector { get; set; }
    float AxisHorizontal { get; set; }
    float AxisVertical { get; set; }
    bool LeftLightAction { get; set; }
    bool LeftHeavyAction { get; set; }
    bool RightLightAction { get; set; }
    bool RightHeavyAction { get; set; }
    bool ToggleTwoHanded { get; set; }
    bool UseItem { get; set; }
    bool Dodge { get; set; }
    bool Interact { get; set; }
    bool LeftSideItemCycle { get; set; }
    bool RightSideItemCycle { get; set; }
}