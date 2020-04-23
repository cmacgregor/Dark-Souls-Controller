
namespace Characters.HumanoidCharacter.LocalHumanoidInputController
{
    interface ILocalHumanoidInputController : IHumanoidInputController
    {
        bool DownItemCycle { get; set; }
        bool UpItemCycle { get; set; }
        bool GestureMenu { get; set; }
        bool MainMenu { get; set; }
        bool ToggleTargetCamera { get; set; }
    }
}
