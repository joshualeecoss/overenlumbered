using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the behaviour of the trees (health etc)
/// </summary>
public class TreeBehaviour : MonoBehaviour
{
    [Header("Tree Stats")]
    [Tooltip("Number of chops it takes for the tree to be destroyed")]
    public float health = 2f;
    [Tooltip("Dollar value of each tree chopped")]
    // the total "health" that each tree has
    private float maxHealth;
    // whether the tree has been chopped down to half health or not
    private bool chopped = false;
    // id number for tree
    private int treeNum;
    // is the tree targeted by an employee?
    private bool isTargeted = false;

    /// <summary>
    /// Description:
    /// Standard Unity function called when script is loaded
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Awake()
    {
        // Set the maximum health to the health determined 
        health = GameManager.treeHealth;
        maxHealth = health;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.AddMoney(GameManager.treeValue);
            GetComponentInParent<SpawnTrees>().RemoveOccupiedPoint(treeNum);
        }
        CheckForHalfDamage();
    }

    /// <summary>
    /// Description:
    /// Checks to see if half the tree's health has been removed
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void CheckForHalfDamage()
    {
        if (health <= maxHealth / 2 && !chopped)
        {
            // Decrease the scale of the tree on the y axis to make it appear like it's been chopped
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 1.75f, transform.localScale.z);
            chopped = true;
        }
    }

    /// <summary>
    /// Description:
    /// Sets the tree ID number
    /// Input: int num
    /// Return: void (no return)
    /// </summary>
    /// <param name="num">int value</param>
    public void SetTreeNum(int num)
    {
        treeNum = num;
    }

    /// <summary>
    /// Description:
    /// returns whether or not the tree is currently being targeted by an employee
    /// Input: none
    /// Return: bool isTargeted
    /// </summary>
    /// <returns>bool</returns>
    public bool IsTargeted()
    {
        return isTargeted;
    }

    /// <summary>
    /// Description:
    /// sets the tree's isTargeted bool value
    /// Input: bool targeted
    /// return: void (no return)
    /// </summary>
    /// <param name="targeted">bool</param>
    public void SetTargeted(bool targeted)
    {
        isTargeted = targeted;
    }
}
