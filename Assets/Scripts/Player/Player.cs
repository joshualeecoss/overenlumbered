using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the player's actions with the given input from the input manager
/// </summary>
public class Player : Character
{
    private InputManager inputManager;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    void Start()
    {
        SetupInputManager();
        SetSwingAnimationSpeed(GameManager.attackSpeed);
        // set the range trigger to the axe
        rangeTrigger.GetComponent<RangeTrigger>().SetState(RangeTrigger.WeaponType.Axe);
    }

    /// <summary>
    /// Description:
    /// Sets up the Input Manager instance
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void SetupInputManager()
    {
        inputManager = InputManager.instance;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    void FixedUpdate()
    {
        ProcessInput();
        ProcessRotation();
        ProcessMovement();
    }

    /// <summary>
    /// Description:
    /// Gets input from the input manager
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public override void ProcessInput()
    {
        // Get input from the input manager
        leftRightInput = inputManager.horizontalMoveAxis;
        forwardBackwardInput = inputManager.verticalMoveAxis;
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
    }
}

