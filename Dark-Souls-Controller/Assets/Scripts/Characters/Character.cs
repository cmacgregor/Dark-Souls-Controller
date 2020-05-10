using System;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        public CharacterInputController inputController;
        public CharacterActionController actionController;
        public CharacterActions characterActions;

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

            //setup dependencies for action controller
            actionController.SetInputController(inputController);
            actionController.SetCharacterActions(characterActions);

        }

        void Update()
        {
            inputController.ParseInputs();
            actionController.HandleInputs();
        }
    }
}
