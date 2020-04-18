using System;
using UnityEngine;
using System.Collections;

public class HumanoidCharacter : OverworldCharacter, 
    IKillable<HumanoidCharacter>, 
    IDamageable<HumanoidCharacter>
{
    //debug variable
    public bool DEBUG = true;

    //player stats
    private int stamina;
    private int health;
    private int kickStaminaCost = 20;

    private enum ArmorSlots
    {
        Head,
        Body,
        Arms,
        Feet
    };

    #region Unity functions
    void Start()
    {
        //Setup components 
        animationTree = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForInteraction();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");

        if (other.tag == "Interactable")
        {
            Debug.Log("Can interact with " + other.name);
        }
    }
    #endregion
}