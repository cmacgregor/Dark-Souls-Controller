using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        private CharacterInputController inputController;
        private CharacterActionController actionController;
        private CharacterActions characterActions;

        //constructor
        public Character(CharacterInputController brain, CharacterActionController nervousSystem, CharacterActions body)
        {
            inputController = brain;
            actionController = nervousSystem;
            characterActions = body;
        }


        public CharacterInputController InputController { get => inputController; set => inputController = value; }
        public CharacterActionController ActionController { get => actionController; set => actionController = value; }
        public CharacterActions Actions { get => characterActions; set => characterActions = value; }

        void Start()
        {
            inputController = GetComponent<CharacterInputController>();
            actionController = GetComponent<CharacterActionController>();
            characterActions = GetComponent<CharacterActions>();
        }

        void Update()
        {

        }
    }
}
