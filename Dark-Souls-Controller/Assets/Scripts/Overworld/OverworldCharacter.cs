﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCharacter : MonoBehaviour {

    public ICharacterController characterController;
    public IAnimator animationTree; //may need to be apart of action controller 
    public IActionController actionController;    
    //also needs rigidbody
}
