using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IKillable<T>
{
    void Kill();
}

interface IDamageable<T>
{
    void Damage(int damageInflicted);
}

interface IInteractable<T>
{
    void Interact();
}
