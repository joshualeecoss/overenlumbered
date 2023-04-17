using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that handles the action of the in game ui buttons
/// </summary>
public class InGameButtonsUIElement : UIelement
{
    [Header("Buttons")]
    [Tooltip("The button that upgrades the attack power")]
    public Button upgradeAxePowerButton;
    [Tooltip("The button that upgrades the attack speed")]
    public Button upgradeAxeSpeedButton;
    [Tooltip("The button that upgrades the tree value")]
    public Button upgradeTreeValueButton;
    [Tooltip("The button that upgrades to chainsaw")]
    public Button upgradeChainsawButton;
    [Tooltip("The button that hires an employee")]
    public Button hireEmployeeButton;
    [Tooltip("The button that upgrades saplings (tree spawn rate)")]
    public Button upgradeSaplingButton;
    [Tooltip("The button that upgrades employee movement speed")]
    public Button upgradeEmployeeMoveSpeedButton;

    /// <summary>
    /// Description:
    /// Activates the buttons when certain conditions are met
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    private void ActivateButtons()
    {
        AxePowerButtonActivation();
        AxeSpeedButtonActivation();
        TreeValueButtonActivation();
        ChainsawButtonActivation();
        HireEmployeeButtonActivation();
        SaplingButtonActivation();
        UpgradeEmployeeMoveSpeedButtonActivation();
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeAxePowerButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void AxePowerButtonActivation()
    {
        // If player hits the amount of cash to get their first upgrade, activate the button
        // and set it to be interactable
        if (GameManager.cashOnHand >= GameManager.attackPowerPrice && !GameManager.chainsawUnlocked)
        {
            upgradeAxePowerButton.gameObject.SetActive(true);
            upgradeAxePowerButton.interactable = true;
        }
        // If player drops below the amount of cash needed to buy the upgrade, set the button to
        // not be interactable
        else if (GameManager.cashOnHand < GameManager.attackPowerPrice && !GameManager.chainsawUnlocked)
        {
            upgradeAxePowerButton.interactable = false;
        }
        // If the chainsaw is unlocked, remove the button from the play area
        else if (GameManager.chainsawUnlocked)
        {
            upgradeAxePowerButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeAxeSpeedButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void AxeSpeedButtonActivation()
    {
        // If player hits the amount of cash to get their first upgrade, activate the button
        // and set it to be interactable
        if (GameManager.cashOnHand >= GameManager.attackSpeedPrice && !GameManager.chainsawUnlocked)
        {
            upgradeAxeSpeedButton.gameObject.SetActive(true);
            upgradeAxeSpeedButton.interactable = true;
        }
        // If player drops below the amount of cash needed to buy the upgrade, set the button to
        // not be interactable
        else if (GameManager.cashOnHand < GameManager.attackSpeedPrice && !GameManager.chainsawUnlocked)
        {
            upgradeAxeSpeedButton.interactable = false;
        }
        // Disables the button if the chainsaw is unlocked
        else if (GameManager.chainsawUnlocked)
        {
            upgradeAxeSpeedButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeTreeValueButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void TreeValueButtonActivation()
    {
        // If player hits the amount of cash to get their first upgrade, activate the button
        // and set it to be interactable
        if (GameManager.cashOnHand >= GameManager.treeValuePrice)
        {
            upgradeTreeValueButton.gameObject.SetActive(true);
            upgradeTreeValueButton.interactable = true;
        }
        // If player drops below the amount of cash needed to buy the upgrade, set the button to
        // not be interactable
        else if (GameManager.cashOnHand < GameManager.treeValuePrice)
        {
            upgradeTreeValueButton.interactable = false;
        }
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeChainsawButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void ChainsawButtonActivation()
    {
        // If player hits the amount of cash to get their first upgrade, activate the button
        // and set it to be interactable
        if (GameManager.cashOnHand >= GameManager.chainsawPrice && !GameManager.chainsawUnlocked)
        {
            upgradeChainsawButton.gameObject.SetActive(true);
            upgradeChainsawButton.interactable = true;
        }
        // Disables the button when the chainsaw is unlocked
        if (GameManager.chainsawUnlocked)
        {
            upgradeChainsawButton.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the hireEmployeeButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void HireEmployeeButtonActivation()
    {
        // Enables the button when the player reaches 100 cash on hand
        if (GameManager.cashOnHand >= 100)
        {
            // Hey someone should hire me too
            hireEmployeeButton.gameObject.SetActive(true);
            hireEmployeeButton.interactable = true;
        }
        // If player's cash on hand is lower than the total cost of all employees, then the
        // hireEmployeeButton is disables
        else if (GameManager.cashOnHand < GameManager.employeePrice * GameManager.totalEmployees)
        {
            hireEmployeeButton.interactable = false;
        }
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeSaplingButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void SaplingButtonActivation()
    {
        // Enables the button if player can afford the first upgrade, only when the chainsaw has
        // been unlocked
        if (GameManager.cashOnHand >= GameManager.saplingPrice && GameManager.chainsawUnlocked)
        {
            upgradeSaplingButton.gameObject.SetActive(true);
            upgradeSaplingButton.interactable = true;
        }
        // If player drops below the amount of cash needed to buy the upgrade, set the button to
        // not be interactable
        else if (GameManager.cashOnHand < GameManager.saplingPrice)
        {
            upgradeSaplingButton.interactable = false;
        }
    }

    /// <summary>
    /// Description:
    /// Activates and deactivates the upgradeEmployeeMoveSpeedButton based on different criteria
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void UpgradeEmployeeMoveSpeedButtonActivation()
    {
        // Activates the button and sets it to interactable if the player has enough cashonhand, and will only activate
        // when the chainsaw is unlocked and the player has employees
        if (GameManager.cashOnHand >= GameManager.employeeMoveSpeedPrice && GameManager.chainsawUnlocked && GameManager.totalEmployees > 0)
        {
            upgradeEmployeeMoveSpeedButton.gameObject.SetActive(true);
            upgradeEmployeeMoveSpeedButton.interactable = true;
        }
        // If player drops below the amount of cash needed to buy the upgrade, set the button to
        // not be interactable
        else if (GameManager.cashOnHand < GameManager.employeeMoveSpeedPrice)
        {
            upgradeEmployeeMoveSpeedButton.interactable = false;
        }
    }

    /// <summary>
    /// Description:
    /// Increases the attack power and subtracts money from the player's cash on hand when this button is clicked.
    /// Modifies the attackPower and attackPowerPrice stats in the GameManager
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeAxePowerButtonClicked()
    {
        GameManager.attackPower += GameManager.attackPower * GameManager.attackPowerMultiplier;
        GameManager.SubtractMoney(GameManager.attackPowerPrice);
        GameManager.attackPowerPrice += GameManager.attackPowerPrice * GameManager.priceIncreaseMultiplier;
        UpdateUI();

    }

    /// <summary>
    /// Description:
    /// Increases the attack swing speed and subtracts money from the player's cash on hand when this button is clicked.
    /// Modifies the attackSpeed and attackSpeedPrice stats in the GameManager
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeAxeSpeedButtonClicked()
    {
        GameManager.attackSpeed += GameManager.attackSpeed * GameManager.attackSpeedMultiplier;
        GameManager.SubtractMoney(GameManager.attackSpeedPrice);
        GameManager.attackSpeedPrice += GameManager.attackSpeedPrice * GameManager.priceIncreaseMultiplier;
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// Increases the value of trees and subtracts money from the player's cash on hand when this button is clicked.
    /// Modifies the treeValue and treeValuePrice stats in the GameManager
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeTreeValueButtonClicked()
    {
        GameManager.treeValue += GameManager.treeValue * GameManager.treeValueMultiplier;
        GameManager.SubtractMoney(GameManager.treeValuePrice);
        GameManager.treeValuePrice += GameManager.treeValuePrice * GameManager.priceIncreaseMultiplier;
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// Sets chainsawUnlocked bool to true and subtracts money from the player's cash on hand when this button is clicked.
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeChainsawButtonClicked()
    {
        GameManager.SubtractMoney(GameManager.chainsawPrice);
        GameManager.SwitchToChainsaw();
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// When clicked, this button tells the EmployeeSpawnArea to instantiate a new employee, and tells
    /// the GameManager to increase the total employee count by 1 and to invoke EmployeePay
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void HireEmployeeButtonClicked()
    {
        GameObject.Find("EmployeeSpawnArea").GetComponent<SpawnEmployee>().SpawnNewEmployee();
        GameManager.StartPayingEmployees();
        GameManager.totalEmployees += 1;
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// Increases the tree spawn rate and subtracts money from the player's cash on hand when this button is clicked.
    /// Modifies the treeSpawnRate and saplingPrice stats in the GameManager
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeSaplingButtonClicked()
    {
        GameManager.treeSpawnRate -= GameManager.treeSpawnRate * GameManager.treeSpawnRateMultiplier;
        GameManager.SubtractMoney(GameManager.saplingPrice);
        GameManager.saplingPrice += GameManager.saplingPrice * GameManager.priceIncreaseMultiplier;
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// Increases the employee movement speed and subtracts money from the player's cash on hand when this button is clicked.
    /// Modifies the employeeMoveSpeed and employeeMoveSpeedPrice stats in the GameManager
    /// Input: None
    /// Return: void (no return)
    /// </summary>
    public void UpgradeEmployeeMoveSpeedButtonClicked()
    {
        GameManager.employeeMoveSpeed += GameManager.employeeMoveSpeed * GameManager.employeeMoveSpeedMultiplier;
        GameManager.SubtractMoney(GameManager.employeeMoveSpeedPrice);
        GameManager.employeeMoveSpeedPrice += GameManager.employeeMoveSpeedPrice * GameManager.priceIncreaseMultiplier;
        UpdateUI();
    }

    /// <summary>
    /// Description:
    /// Keeps button text up to date with current upgrade prices
    /// Input: none
    /// Return: void
    /// </summary>
    public void UpdatePrices()
    {
        upgradeAxePowerButton.GetComponentInChildren<Text>().text = "Sharpen Axe: " + GameManager.attackPowerPrice.ToString("C2", GameManager.ci);
        upgradeAxeSpeedButton.GetComponentInChildren<Text>().text = "Increase Swing Speed: " + GameManager.attackSpeedPrice.ToString("C2", GameManager.ci);
        upgradeTreeValueButton.GetComponentInChildren<Text>().text = "Increase Lumber Quality: " + GameManager.treeValuePrice.ToString("C2", GameManager.ci);
        upgradeChainsawButton.GetComponentInChildren<Text>().text = "Purchase Chainsaw: " + GameManager.chainsawPrice.ToString("C2", GameManager.ci);
        hireEmployeeButton.GetComponentInChildren<Text>().text = "Hire Employee: " + GameManager.employeePrice.ToString("C2", GameManager.ci) + "/second";
        upgradeSaplingButton.GetComponentInChildren<Text>().text = "Genetically Modify Saplings: " + GameManager.saplingPrice.ToString("C2", GameManager.ci);
        upgradeEmployeeMoveSpeedButton.GetComponentInChildren<Text>().text = "\"Motivate\" Employees: " + GameManager.employeeMoveSpeedPrice.ToString("C2", GameManager.ci);
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
        ActivateButtons();
        UpdatePrices();
    }
}
