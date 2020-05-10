using UnityEngine;

namespace Characters
{
    //remove monobehaviour inheritence after spawn manager is created and can set input controllers at creation time
    abstract public class CharacterInputController : MonoBehaviour, ICharacterInputController
    {
        abstract public void ParseInputs();
    }
}
