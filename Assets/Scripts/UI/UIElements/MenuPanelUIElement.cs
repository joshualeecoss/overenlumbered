using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIelement class to handle the action of the main menu buttons
/// </summary>
public class MenuPanelUIElement : UIelement
{
    // the menu's play button
    public Button playButton;
    // the menu's quit button
    public Button quitButton;

    /// <summary>
    /// When the play button is clicked
    /// </summary>
    public void PlayButtonClicked()
    {
        GameManager.uiManager.GoToGame();
    }

    public void QuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
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
    }


}
