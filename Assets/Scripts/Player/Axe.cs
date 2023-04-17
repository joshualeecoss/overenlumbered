using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This subclass of Weapon sets the initial damage and speed of the axe 
/// </summary>
public class Axe : Weapon
{
    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Start()
    {
        initialAttackDamage = 2f;
        initialAttackTime = 2f;
    }
}
