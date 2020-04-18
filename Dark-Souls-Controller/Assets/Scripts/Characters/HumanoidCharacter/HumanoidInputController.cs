using UnityEngine;

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

    public Vector3 MovementVector { get => movementVector; set => movementVector = value; }
    public float AxisHorizontal { get => axisHorizontal; set => throw new System.NotImplementedException(); }
    public float AxisVertical { get => axisVertical; set => throw new System.NotImplementedException(); }
    public bool LeftLightAction { get => leftLightAction; set => throw new System.NotImplementedException(); }
    public bool LeftHeavyAction { get => leftHeavyAction; set => throw new System.NotImplementedException(); }
    public bool RightLightAction { get => rightLightAction; set => throw new System.NotImplementedException(); }
    public bool RightHeavyAction { get => rightHeavyAction; set => throw new System.NotImplementedException(); }
    public bool ToggleTwoHanded { get => toggleTwoHanded; set => throw new System.NotImplementedException(); }
    public bool UseItem { get => useItem; set => throw new System.NotImplementedException(); }
    public bool Dodge { get => dodge; set => throw new System.NotImplementedException(); }
    public bool Interact { get => interact; set => throw new System.NotImplementedException(); }
    public bool LeftSideItemCycle { get => leftSideItemCycle; set => throw new System.NotImplementedException(); }
    public bool RightSideItemCycle { get => rightSideItemCycle; set => throw new System.NotImplementedException(); }

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
