using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This subclass of the Weapon class sets the initial damage and speed
/// </summary>
public class Chainsaw : Weapon
{
    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Start()
    {
        initialAttackDamage = 5f;
        initialAttackTime = 0.35f;
    }
}
