using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the displaying the money the player has
/// </summary>
public class MoneyUIElement : UIelement
{
    [Header("Settings")]
    [Tooltip("The text object used to display money the player has")]
    public Text displayText = null;

    /// <summary>
    /// Description:
    /// Displays the score onto the display text
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void DisplayCashOnHand()
    {
        if (displayText != null)
        {
            displayText.text = "Cash on hand: \n" + GameManager.cashOnHand.ToString("C2", GameManager.ci) +
                "\nPrice per tree sold: " + GameManager.treeValue.ToString("C2", GameManager.ci);
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
        DisplayCashOnHand();
    }
}
