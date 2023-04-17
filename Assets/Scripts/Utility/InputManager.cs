using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton class that reads and stores player inputs
/// </summary>
public class InputManager : MonoBehaviour
{
    // Global instance for scripts to reference
    public static InputManager instance;

    /// <summary>
    /// Description:
    /// Standard Unity function called when script is loaded
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Awake()
    {
        // Set up the instance of the Input Manager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Player Movement Input")]
    [Tooltip("The horizontal movement input of the player")]
    public float horizontalMoveAxis;
    [Tooltip("The vertical movement input of the player")]
    public float verticalMoveAxis;

    /// <summary>
    /// Description:
    /// Reads and stores movement input
    /// Input: CalbackContext context
    /// Return: void (no return)
    /// </summary>
    /// <param name="context">the context of the movement input</param>
    public void ReadMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalMoveAxis = inputVector.x;
        verticalMoveAxis = inputVector.y;
    }

    [Header("Pause Input")]
    [Tooltip("Whether or not the pause button was pressed this frame")]
    public bool pausePressed;

    /// <summary>
    /// Description:
    /// Reads and stores the pause input
    /// Input: CallbackContext callbackContext
    /// Return: void (no return)
    /// </summary>
    /// <param name="context">The context of the pause input</param>

    public void ReadPauseInput(InputAction.CallbackContext context)
    {
        pausePressed = !context.canceled;
        StartCoroutine(ResetPausePressed());
    }

    /// <summary>
    /// Description:
    /// Coroutine that resets the pausedPressed variable after one frame
    /// Input: none
    /// Return: IEnumerator
    /// </summary>
    /// <returns>IEnumerator: Allows this to function as a coroutine</returns>
    private IEnumerator ResetPausePressed()
    {
        yield return new WaitForEndOfFrame();
        pausePressed = false;
    }
}
