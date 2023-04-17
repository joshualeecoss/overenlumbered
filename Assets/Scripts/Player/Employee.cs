using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the employee actions
/// </summary>
public class Employee : Character
{
    // Transform of the target tree
    public Transform target;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    void Start()
    {
        // sets the speed of the swing animation
        SetSwingAnimationSpeed(GameManager.attackSpeed);
        // Checks to see which weapon to set the range trigger to
        if (!GameManager.chainsawUnlocked)
        {
            rangeTrigger.GetComponent<RangeTrigger>().SetState(RangeTrigger.WeaponType.Axe);
        }
        else if (GameManager.chainsawUnlocked)
        {
            rangeTrigger.GetComponent<RangeTrigger>().SetState(RangeTrigger.WeaponType.Chainsaw);
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    void FixedUpdate()
    {
        ProcessRotation();
        ProcessMovement();
    }

    /// <summary>
    /// Description:
    /// Rotates the character to move in the direction of the target, as long as there is a target.
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public override void ProcessRotation()
    {
        speed = 0;
        Vector3 lookRotation = Vector3.forward;
        if (target != null)
        {
            lookRotation = target.position - transform.position;
            lookRotation = new Vector3(lookRotation.x, 0, lookRotation.z);
            lookRotation = lookRotation.normalized;
        }
        Quaternion rotation = Quaternion.LookRotation(lookRotation, Vector3.up);
        if (lookRotation.magnitude > 0)
        {
            speed = moveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Update()
    {
        Animate();
        ChainsawUnlocked();
        FindTarget();
        EmployeeSpeed();
    }

    /// <summary>
    /// Description:
    /// Sets the move speed of the employee to the speed dictated by the game manager
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void EmployeeSpeed()
    {
        if (target != null)
        {
            moveSpeed = GameManager.employeeMoveSpeed;
        }
    }


    /// <summary>
    /// Description:
    /// Finds the closest tree object to the employee character and sets it as the target. If the closest tree object is currently targeted
    /// by another employee, sets the second closest tree as the target.
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void FindTarget()
    {
        if (target == null)
        {
            GameObject[] candidates = GameObject.FindGameObjectsWithTag("Tree");
            float minDistance = Mathf.Infinity;
            Transform closest;
            Transform secondClosest = null;

            if (candidates.Length == 0)
            {
                target = null;
            }
            else
            {
                closest = candidates[0].transform;
                for (int i = 1; i < candidates.Length; i++)
                {
                    float distance = Vector3.Distance(candidates[i].transform.position, transform.position);
                    if (distance < minDistance)
                    {
                        secondClosest = closest;
                        closest = candidates[i].transform;
                        minDistance = distance;
                    }
                }
                if (!closest.GetComponent<TreeBehaviour>().IsTargeted())
                {
                    target = closest;
                    target.GetComponent<TreeBehaviour>().SetTargeted(true);
                }
                else if (closest.GetComponent<TreeBehaviour>().IsTargeted() && secondClosest != null)
                {
                    target = secondClosest;
                    target.GetComponent<TreeBehaviour>().SetTargeted(true);
                }
            }
        }

    }
}

