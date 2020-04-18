using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter 
{
    private CharacterController characterController;
    private ActionController actionController;
    private CharacterActions characterActions;

    public CharacterController CharacterController { get => characterController; set => characterController = value; }
    public ActionController ActionController { get => actionController; set => actionController = value; }
    public CharacterActions Actions { get => characterActions; set => characterActions = value; }
}
