using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the damaging and destruction of the tree objects
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("Axe Settings")]
    [Tooltip("Time to wait in between attacks")]
    public float initialAttackTime;
    [Tooltip("Amount of damage axe does")]
    public float initialAttackDamage;


    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    void Start()
    {
        InitializeGameManagerStats();
    }

    /// <summary>
    /// Description:
    /// Sets the game manager stats to the objects current stats
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    /// 
    private void InitializeGameManagerStats()
    {
        initialAttackDamage = GameManager.attackPower;
        initialAttackTime = GameManager.attackSpeed;
    }


    /// <summary>
    /// Description:
    /// Gets and returns the closest tree object to the character from within range
    /// Input: Vector3 position, List<GameObject> treesInRange
    /// Return: Gameobject
    /// </summary>
    /// <param name="position">The character's transform position</param>
    /// <param name="treesInRange">The list of trees within the character's range</param>
    /// <returns>GameObject (tree) closest to the character</returns>
    public GameObject GetClosestTree(Vector3 position, List<GameObject> treesInRange)
    {
        if (treesInRange.Count > 0 && treesInRange != null)
        {
            float closestDistance = Mathf.Infinity;
            int index = 0;

            for (int i = 0; i < treesInRange.Count - 1; i++)
            {
                if (treesInRange[i] != null)
                {
                    float testDistance = Vector3.Distance(position, treesInRange[i].transform.position);
                    if (testDistance < closestDistance)
                    {
                        index = i;
                        closestDistance = testDistance;
                    }
                }
            }
            return treesInRange[index];
        }
        return null;
    }

    /// <summary>
    /// Description:
    /// Damages the tree closest to the character when in range. If the tree has no health left,
    /// it is removed from the list of trees in range, and the game manager counts up one tree destroyed.
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void HitOneTree()
    {
        List<GameObject> treesInRange = GetComponentInParent<Character>().GetTreesInRange();
        GameObject closestTree = GetClosestTree(transform.position, treesInRange);
        // Do damage to the tree
        if (closestTree != null)
        {
            closestTree.GetComponent<TreeBehaviour>().health -= GameManager.attackPower;
            // If the tree has no health left, remove tree from treesInRange
            // list
            if (closestTree.GetComponent<TreeBehaviour>().health <= 0)
            {
                treesInRange.Remove(closestTree);
                GameManager.AddTreeDestroyed();
            }
        }
    }

    /// <summary>
    /// Description:
    /// Gets the initial attack power of the weapon
    /// Input: none
    /// Output: float
    /// </summary>
    /// <returns>flaot initialAttackDamage</returns>
    public float GetAttackPower()
    {
        return initialAttackDamage;
    }

    /// <summary>
    /// Description:
    /// Gets the initial attack speed of the weapon
    /// Input: none
    /// Output: float
    /// </summary>
    /// <returns>flaot initialAttackSpeed</returns>
    public float GetAttackSpeed()
    {
        return initialAttackTime;
    }
}
