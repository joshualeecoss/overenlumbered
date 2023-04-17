using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base character class for both the player and employees
/// </summary>
public class Character : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Movement speed of the player")]
    public float moveSpeed = 2f;
    [Tooltip("Rotation speed of the player")]
    public float rotationSpeed = 720f;
    public float gravity = 20f;
    [Tooltip("Axe object")]
    public Axe axe;
    [Tooltip("Chainsaw object")]
    public Chainsaw chainsaw;
    [Header("Animation")]
    [Tooltip("Character animator component")]
    public Animator animator;

    // List of tree objects in range of the range trigger
    private List<GameObject> treesInRange = new List<GameObject>();
    // BoxCollider object to tell when trees are in range
    public BoxCollider rangeTrigger;
    // Character controller for movement
    public CharacterController controller;
    // movement direction of character
    protected Vector3 moveDirection;
    // character's movement speed
    protected float speed;
    // float values for vertical and horizontal input
    protected float leftRightInput, forwardBackwardInput;

    /// <summary>
    /// Description:
    /// Gets input from the input manager
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public virtual void ProcessInput() { }

    /// <summary>
    /// Description:
    /// Gets the input from the input manager and handles the movement of the player character
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public virtual void ProcessMovement()
    {
        // apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveDirection = transform.TransformDirection(Vector3.forward).normalized;
            moveDirection *= speed * Time.deltaTime;
        }
        controller.Move(moveDirection);
    }

    /// <summary>
    /// Description:
    /// Rotates the character to move in the direction of the player input
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public virtual void ProcessRotation()
    {
        speed = 0;
        if (Mathf.Abs(leftRightInput) > 0 || Mathf.Abs(forwardBackwardInput) > 0)
        {
            Vector3 lookRotation = new Vector3(leftRightInput, 0, forwardBackwardInput);
            lookRotation = lookRotation.normalized;

            Quaternion rotation = Quaternion.LookRotation(lookRotation, Vector3.up);
            if (lookRotation.magnitude > 0)
            {
                speed = moveSpeed;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    /// <summary>
    /// Description:
    /// Handles the animation of the character
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void Animate()
    {
        Move();
        SwingAxe();
    }

    /// <summary>
    /// Description:
    /// Handles the movement animation of the character. 
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Move()
    {
        if (leftRightInput != 0 || forwardBackwardInput != 0)
        {
            animator.SetFloat("speed", 1);
        }
        else if (leftRightInput == 0 && forwardBackwardInput == 0)
        {
            animator.SetFloat("speed", 0);
        }
    }

    /// <summary>
    /// Description:
    /// Handles the axe swinging animation of the character. 
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void SwingAxe()
    {
        if (TreesAreInRange() && !GameManager.chainsawUnlocked)
        {
            SetSwingAnimationSpeed(GameManager.attackSpeed);
            animator.SetBool("axeInRange", true);
        }
        else if (!TreesAreInRange() && !GameManager.chainsawUnlocked)
        {
            animator.SetBool("axeInRange", false);
        }
    }

    /// <summary>
    /// Description:
    /// Checks to see whether the chainsaw has been unlocked and changes the character's weapon accordingly
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void ChainsawUnlocked()
    {
        if (GameManager.chainsawUnlocked)
        {
            axe.gameObject.SetActive(false);
            chainsaw.gameObject.SetActive(true);
        }
        else
        {
            axe.gameObject.SetActive(true);
            chainsaw.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Description:
    /// Returns whether or not there are trees in range of the player.
    /// Input: none.
    /// Return: bool
    /// </summary>
    /// <returns>bool: trees in range</returns>
    public bool TreesAreInRange()
    {
        return treesInRange.Count > 0;
    }

    /// <summary>
    /// Description:
    /// If there are trees in range of the player, returns treesInRange list.
    /// Input: none
    /// Return: List<GameObject> treesInRange
    /// </summary>
    /// <returns>List<GameObject> treesInRange: a list of the tree objects in range of the player</returns>
    public List<GameObject> GetTreesInRange()
    {
        if (treesInRange.Count > 0)
        {
            return treesInRange;
        }
        else { return new List<GameObject>(); }
    }

    /// <summary>
    /// Description:
    /// Changes the multiplier on the axe swing animation 
    /// Input: float speed
    /// Return: void (no return)
    /// </summary>
    /// <param name="speed">float speed: a multiplier to send to the animator in order to speed up the
    ///                     axe swing animation speed</param>
    public void SetSwingAnimationSpeed(float speed)
    {
        animator.SetFloat("swingTime", speed);
    }

    /// <summary>
    /// Description:
    /// Checks to see if the axe can hit a tree and if so, does.
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void HitOneTree()
    {
        if (treesInRange.Count != 0 && !GameManager.chainsawUnlocked)
        {
            axe.HitOneTree();
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when an object enters the trigger
    /// Input: Collider other
    /// Return: void (no return)
    /// </summary>
    /// <param name="other">The collider of the object entering the trigger</param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        // If the game object has the Tree tag, add it to the treesInRange list
        if (other.tag == "Tree")
        {
            treesInRange.Add(other.gameObject);
        }
        if (GameManager.chainsawUnlocked)
        {
            chainsaw.HitOneTree();
            StartCoroutine(ChainsawTimer());
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when an object exits the trigger
    /// Input: Collider other
    /// Return: void (no return)
    /// </summary>
    /// <param name="other">The collider of the object exiting the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        // If the game object has the Tree tag, remove it from the treesInRange list
        if (other.tag == "Tree")
        {
            other.GetComponent<TreeBehaviour>().SetTargeted(false);
            treesInRange.Remove(other.gameObject);
            StopCoroutine(ChainsawTimer());
            rangeTrigger.enabled = true;
        }
    }

    /// <summary>
    /// Description:
    /// Allows the chainsaw to take time in between hits
    /// Input: none
    /// Output: IEnumerator
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChainsawTimer()
    {
        rangeTrigger.enabled = false;
        yield return new WaitForSeconds(GameManager.attackSpeed);
        rangeTrigger.enabled = true;
    }
}
