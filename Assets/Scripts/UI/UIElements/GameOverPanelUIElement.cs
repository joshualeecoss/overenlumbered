using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the Game Over panel UI element
/// </summary>
public class GameOverPanelUIElement : UIelement
{
    public Button restartButton;
    public Button quitButton;

    public void RestartButtonClicked()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
