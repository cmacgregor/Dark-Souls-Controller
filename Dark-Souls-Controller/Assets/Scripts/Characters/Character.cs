using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter 
{
    private CharacterInputController inputController;
    private ActionController actionController;
    private CharacterActions characterActions;

    public CharacterInputController InputController { get => inputController; set => inputController = value; }
    public ActionController ActionController { get => actionController; set => actionController = value; }
    public CharacterActions Actions { get => characterActions; set => characterActions = value; }

    void Start()
    {
        inputController = GetComponent<CharacterInputController>();
        ActionController = GetComponent<ActionController>();
        characterActions = GetComponent<CharacterActions>();
    }

    void Update()
    {

    }
}
