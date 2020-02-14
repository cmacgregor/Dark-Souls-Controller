using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InanimateObject, IInteractable<Door> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact()
    {
        Debug.Log("WE OUT HERE");
    }
}
