using UnityEngine;
using System.Collections;

public class LocalHumanoidInputController : HumanoidInputController, ILocalHumanoidInputController
{
    //Class to derive humanoid character actions from local controller inputs 

    public float ANALOG_DEAD_ZONE = 0.1f;

    private bool downItemCycle;
    private bool upItemCycle;
    private bool gestureMenu;
    private bool mainMenu;
    private bool toggleTargetCamera;

    public bool DownItemCycle { get => downItemCycle; set => throw new System.NotImplementedException(); }
    public bool UpItemCycle { get => upItemCycle; set => throw new System.NotImplementedException(); }
    public bool GestureMenu { get => gestureMenu; set => throw new System.NotImplementedException(); }
    public bool MainMenu { get => mainMenu; set => throw new System.NotImplementedException(); }
    public bool ToggleTargetCamera { get => toggleTargetCamera; set => throw new System.NotImplementedException(); }

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