using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the displaying the number of trees destroyed during the game
/// </summary>
public class TreesDestroyedUIElement : UIelement
{
    [Header("Settings")]
    [Tooltip("The text object used to display number of trees destroyed")]
    public Text displayText = null;

    /// <summary>
    /// Description:
    /// Displays the score onto the display text
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void DisplayTreesDestroyed()
    {
        if (displayText != null)
        {
            displayText.text = "Trees Destroyed: " + GameManager.treesDestroyed;
        }
    }

    /// <summary>
    /// Description:
    /// Updates this UI based on this class
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayTreesDestroyed();
    }
}
