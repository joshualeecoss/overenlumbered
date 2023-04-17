using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the doomsday timer element
/// </summary>
public class Timer : UIelement
{
    [SerializeField] private Image uiFill;

    public float time;
    public float max;

    void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        time -= Time.deltaTime * GameManager.timerRate;
        uiFill.fillAmount = time / max;

        if (time < 0)
        {
            time = 0;
        }

        GameManager.timeToGameOver = uiFill.fillAmount;
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
