using UnityEngine;

namespace Characters
{
    //remove monobehaviour inheritence after spawn manager is created and can set the controller at creation time
    abstract public class CharacterActionController : MonoBehaviour, ICharacterActionController
    {
        abstract public void HandleInputs();

        abstract public void SetInputController(CharacterInputController brain);

        abstract public void SetCharacterActions(CharacterActions body);
    }
}
