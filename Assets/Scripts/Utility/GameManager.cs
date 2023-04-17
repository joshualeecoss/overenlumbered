using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

/// <summary>
/// Singleton class that manages the game stats and ui elements
/// </summary>
public class GameManager : MonoBehaviour
{
    // The global instance for all other scripts to reference
    public static GameManager instance = null;

    #region References
    [Header("References")]
    [Tooltip("The UIManager component which manages the current scene's UI")]
    public static UIManager uiManager = null;
    [Tooltip("The player gameObject")]
    public GameObject gameManagerPlayer = null;

    // static getter for the player object
    public static GameObject player
    {
        get { return instance.gameManagerPlayer; }
    }
    #endregion

    #region Stats
    [Header("Stats")]
    [Tooltip("The number of trees the player has cut down")]
    [SerializeField] private int gameManagerTreesDestroyed = 0;
    [Tooltip("The amount of money the player has")]
    [SerializeField] private float gameManagerCashOnHand = 0;
    [Tooltip("The current time between attack swings (in seconds)")]
    [SerializeField] private float gameManagerAttackSpeed;
    [Tooltip("The current damage done by the attack")]
    [SerializeField] private float gameManagerAttackPower;
    [Tooltip("Price per tree chopped down")]
    [SerializeField] private float gameManagerTreeValue;
    [Tooltip("Tree spawn rate")]
    [SerializeField] private float gameManagerTreeSpawnRate;
    [Tooltip("Starting health for each tree")]
    [SerializeField] private float gameManagerTreeHealth;
    [Tooltip("Current number of employees")]
    [SerializeField] private int gameManagerTotalEmployees = 0;
    [Tooltip("Current employee movement speed")]
    [SerializeField] private float gameManagerEmployeeMoveSpeed;
    [Tooltip("Current rate at which the timer depletes")]
    [SerializeField] private float gameManagerTimerRate;
    [Tooltip("Current tree threshold to increase timer rate")]
    [SerializeField] private int gameManagerTimerTreeThreshold;
    public float gameManagerTimeToGameOver;

    public static float timeToGameOver
    {
        get { return instance.gameManagerTimeToGameOver; }
        set { instance.gameManagerTimeToGameOver = value; }
    }

    // CultureInfo for formatting dollar amounts in US currency
    private CultureInfo gameManagerCI = new CultureInfo("en-US");

    // Static getter for the culture info used for dollar formatting
    public static CultureInfo ci
    {
        get { return instance.gameManagerCI; }
    }

    // Static getter/setter for number of trees destroyed by player
    public static int treesDestroyed
    {
        get { return instance.gameManagerTreesDestroyed; }
        set { instance.gameManagerTreesDestroyed = value; }
    }

    // Static getter/setter for time between attack swings
    public static float attackSpeed
    {
        get { return instance.gameManagerAttackSpeed; }
        set { instance.gameManagerAttackSpeed = value; }
    }

    // Static getter/setter for attack damage
    public static float attackPower
    {
        get { return instance.gameManagerAttackPower; }
        set { instance.gameManagerAttackPower = value; }
    }


    // Static getter/setter for tree value (dollars) 
    public static float treeValue
    {
        get { return instance.gameManagerTreeValue; }
        set { instance.gameManagerTreeValue = value; }
    }

    // Static getter/setter for tree value (dollars) 
    public static float treeHealth
    {
        get { return instance.gameManagerTreeHealth; }
        set { instance.gameManagerTreeHealth = value; }
    }

    // Static getter/setter for tree spawn rate
    public static float treeSpawnRate
    {
        get { return instance.gameManagerTreeSpawnRate; }
        set { instance.gameManagerTreeSpawnRate = value; }
    }

    // Static getter/setter for cash on hand (dollars)
    public static float cashOnHand
    {
        get { return instance.gameManagerCashOnHand; }
        set { instance.gameManagerCashOnHand = value; }
    }

    // Static getter/setter for the total employees
    public static int totalEmployees
    {
        get { return instance.gameManagerTotalEmployees; }
        set { instance.gameManagerTotalEmployees = value; }
    }

    // Static getter/setter for employee movement speed
    public static float employeeMoveSpeed
    {
        get { return instance.gameManagerEmployeeMoveSpeed; }
        set { instance.gameManagerEmployeeMoveSpeed = value; }
    }

    // Static getter/setter for the timer depletion rate
    public static float timerRate
    {
        get { return instance.gameManagerTimerRate; }
        set { instance.gameManagerTimerRate = value; }
    }

    public static int timerTreeThreshold
    {
        get { return instance.gameManagerTimerTreeThreshold; }
        set { instance.gameManagerTimerTreeThreshold = value; }
    }

    // Bool depending on whether employee payment has started
    private static bool payingEmployees = false;
    #endregion

    #region Multipliers
    [Header("Multipliers")]
    [Tooltip("Multiplier for Attack Power Upgrade")]
    [SerializeField] private float gameManagerAttackPowerMultiplier;
    [Tooltip("Multiplier for Attack Speed Upgrade")]
    [SerializeField] private float gameManagerAttackSpeedMultiplier;
    [Tooltip("Multiplier for Tree Spawn Rate Upgrade")]
    [SerializeField] private float gameManagerTreeSpawnRateMultiplier;
    [Tooltip("Multiplier for Tree Value Upgrade")]
    [SerializeField] private float gameManagerTreeValueMultiplier;
    [Tooltip("Multiplier for Employee Move Speed")]
    [SerializeField]
    private float gameManagerEmployeeMoveSpeedMultiplier;
    [Tooltip("Multiplier to increase upgrade prices")]
    [SerializeField] private float gameManagerPriceIncreaseMultiplier;


    // Static getter/setter for attack power multiplier
    public static float attackPowerMultiplier
    {
        get { return instance.gameManagerAttackPowerMultiplier; }
        set { instance.gameManagerAttackPowerMultiplier = value; }
    }

    // Static getter/setter for attack speed multiplier
    public static float attackSpeedMultiplier
    {
        get { return instance.gameManagerAttackSpeedMultiplier; }
        set { instance.gameManagerAttackSpeedMultiplier = value; }
    }

    // Static getter/setter for tree spawn rate multiplier
    public static float treeSpawnRateMultiplier
    {
        get { return instance.gameManagerTreeSpawnRateMultiplier; }
        set { instance.gameManagerTreeSpawnRateMultiplier = value; }
    }

    // Static getter/setter for tree value multiplier
    public static float treeValueMultiplier
    {
        get { return instance.gameManagerTreeValueMultiplier; }
        set { instance.gameManagerTreeValueMultiplier = value; }
    }

    // Static getter/setter for price increase multiplier
    public static float priceIncreaseMultiplier
    {
        get { return instance.gameManagerPriceIncreaseMultiplier; }
        set { instance.gameManagerPriceIncreaseMultiplier = value; }
    }

    // Static getter/setter for employee move speed multiplier
    public static float employeeMoveSpeedMultiplier
    {
        get { return instance.gameManagerEmployeeMoveSpeedMultiplier; }
        set { instance.gameManagerEmployeeMoveSpeedMultiplier = value; }
    }
    #endregion

    #region Upgrade Prices
    [Header("Upgrade Prices")]
    [Tooltip("Price for Attack Power Upgrade")]
    [SerializeField] private float gameManagerAttackPowerPrice;
    [Tooltip("Price for Attack Speed Upgrade")]
    [SerializeField] private float gameManagerAttackSpeedPrice;
    [Tooltip("Price for the Lumber Value Increase Upgrade")]
    [SerializeField] private float gameManagerTreeValuePrice;
    [Tooltip("Price for Chainsaw Upgrade")]
    [SerializeField] private float gameManagerChainsawPrice;
    [Tooltip("Price for hiring an employee")]
    [SerializeField] private float gameManagerEmployeePrice;
    [Tooltip("Price for upgrading tree spawn rate")]
    [SerializeField] private float gameManagerSaplingPrice;
    [Tooltip("Price for upgrading employee move speed")]
    [SerializeField] private float gameManagerEmployeeMoveSpeedPrice;

    // Static getter/setter for attack power upgrade price
    public static float attackPowerPrice
    {
        get { return instance.gameManagerAttackPowerPrice; }
        set { instance.gameManagerAttackPowerPrice = value; }
    }

    // Static getter/setter for attack speed upgrade price
    public static float attackSpeedPrice
    {
        get { return instance.gameManagerAttackSpeedPrice; }
        set { instance.gameManagerAttackSpeedPrice = value; }
    }

    // Static getter/setter for tree value increase price
    public static float treeValuePrice
    {
        get { return instance.gameManagerTreeValuePrice; }
        set { instance.gameManagerTreeValuePrice = value; }
    }

    // Static getter/setter for the chainsaw upgrade price
    public static float chainsawPrice
    {
        get { return instance.gameManagerChainsawPrice; }
        set { instance.gameManagerChainsawPrice = value; }
    }

    // Static getter/setter for hiring employee price
    public static float employeePrice
    {
        get { return instance.gameManagerEmployeePrice; }
        set { instance.gameManagerEmployeePrice = value; }
    }

    // Static getter/setter for upgrading sapling price
    public static float saplingPrice
    {
        get { return instance.gameManagerSaplingPrice; }
        set { instance.gameManagerSaplingPrice = value; }
    }

    // Static getter/setter for employee move speed price
    public static float employeeMoveSpeedPrice
    {
        get { return instance.gameManagerEmployeeMoveSpeedPrice; }
        set { instance.gameManagerEmployeeMoveSpeedPrice = value; }
    }
    #endregion

    #region Unlocks
    [Header("Unlocks")]
    [Tooltip("Is the chainsaw unlocked?")]
    [SerializeField] private bool gameManagerChainsawUnlocked = false;

    // Static getter/setter for the chainsaw unlocked bool
    public static bool chainsawUnlocked
    {
        get { return instance.gameManagerChainsawUnlocked; }
        set { instance.gameManagerChainsawUnlocked = value; }
    }
    #endregion

    /// <summary>
    /// Description: 
    /// Standard Unity function that's called before start when this instance 
    /// is first loaded
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Awake()
    {
        // When this component is first added or activated, set up the global reference
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// Description:
    /// Standard Unity function called before the first update
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Start()
    {
        gameState = GameState.MainMenu;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }

    }

    /// <summary>
    /// Description:
    /// Handles main menu state
    /// </summary>
    void HandleMainMenu()
    {
        Time.timeScale = 0;
        uiManager.MainMenu();
    }

    /// <summary>
    /// Description: Handles game state
    /// </summary>
    void HandleGame()
    {
        Time.timeScale = 1;
        IncreaseTimerRateFromThreshold();
        if (timeToGameOver <= 0)
        {
            uiManager.GameOver();
        }
    }

    /// <summary>
    /// Description: Handles pause state
    /// </summary>
    void HandlePause()
    {
        Time.timeScale = 0;
        uiManager.Pause();
    }

    /// <summary>
    /// Description:
    /// Handles Game over state
    /// </summary>
    void HandleGameOver()
    {
        Time.timeScale = 0;
        uiManager.GameOver();
    }


    /// <summary>
    /// Increases timer rate after so many trees are destroyed
    /// </summary>
    private bool canIncrease = true;
    private void IncreaseTimerRateFromThreshold()
    {
        if (treesDestroyed > 0 && treesDestroyed % timerTreeThreshold == 0 && canIncrease)
        {
            canIncrease = false;
            StartCoroutine("AddToRate", 0.05f);
        }
        if (treesDestroyed % 2 != 0)
        {
            canIncrease = true;
        }
    }

    private IEnumerator AddToRate(float increase)
    {
        timerRate += increase;
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Description:
    /// Sends out a message to UI elements to update
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public static void UpdateUIElements()
    {
        if (instance != null && uiManager != null)
        {
            uiManager.UpdateUI();
        }
    }

    /// <summary>
    /// Description:
    /// Adds a number to the number of trees cut down, stored in the gameManager
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public static void AddTreeDestroyed()
    {
        treesDestroyed += 1;
        UpdateUIElements();
    }

    /// <summary>
    /// Description:
    /// Adds value to the player's cash on hand stored in the gameManager
    /// Input: float value
    /// Return: void (no return)
    /// </summary>
    /// <param name="value">The amount of money to add to the player's
    ///                     cash on hand</param>
    public static void AddMoney(float value)
    {
        cashOnHand += value;
        UpdateUIElements();
    }

    /// <summary>
    /// Description:
    /// Removes value from the player's cash on hand stored in the gameManager
    /// Input: float value
    /// Return: void (no return)
    /// </summary>
    /// <param name="value">The amount of money to subtract from the player's
    ///                     cash on hand</param>
    public static void SubtractMoney(float value)
    {
        cashOnHand -= value;
        UpdateUIElements();
    }

    /// <summary>
    /// Description:
    /// Subtracts the employee pay from the player's cashOnHand and updates the UI
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public void EmployeePay()
    {
        gameManagerCashOnHand -= gameManagerEmployeePrice * gameManagerTotalEmployees;
        UpdateUIElements();
    }

    /// <summary>
    /// When payingEmployees is true, starts to invokerepeating EmployeePay, taking employee price
    /// out of cash on hand once per second
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    public static void StartPayingEmployees()
    {
        if (!payingEmployees)
        {
            instance.InvokeRepeating("EmployeePay", 1f, 1f);
            payingEmployees = true;
        }
    }

    /// <summary>
    /// Switches to chainsaw
    /// </summary>
    public static void SwitchToChainsaw()
    {
        player.GetComponentInChildren<RangeTrigger>().SetState(RangeTrigger.WeaponType.Chainsaw);
        chainsawUnlocked = true;
    }

    public enum GameState
    {
        MainMenu, Game, Pause, GameOver
    }
    public static GameState gameState;

}
