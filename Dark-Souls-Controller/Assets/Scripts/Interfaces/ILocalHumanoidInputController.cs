
namespace Characters.HumanoidCharacter.LocalHumanoidInputController
{
    interface ILocalHumanoidInputController : IHumanoidInputController
    {
        bool DownItemCycle { get; }
        bool UpItemCycle { get; }
        bool GestureMenu { get; }
        bool MainMenu { get; }
        bool ToggleTargetCamera { get; }
    }
}
