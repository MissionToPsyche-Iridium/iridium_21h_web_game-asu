using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text timerText;
    public Slider greenBar;
    public GameObject successUI;
    public GameObject failUI;

    [Header("Buttons (Before Game)")]
    public Button startButton;

    [Header("Buttons (During Game)")]
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public Button instructionButton;         // Button that shows instructions mid-game
    public Button duringGameMenuButton;      // 'Main Menu' during the game
    public Button restartButton;             // Now also available during the game

    [Header("Buttons (After Game)")]
    public Button afterGameMenuButton;       // 'Main Menu' after the game

    [Header("Instruction Panel")]
    public GameObject instructionsPanel;     // The panel that displays instructions
    public Button backToGameButton;          // Button to resume from instructions

    [Header("Game Settings")]
    public float totalTime = 30f;
    private float currentTime;
    private float currentProgress = 0f;

    private bool gameActive = false;
    private bool isPaused = false;

    void Start()
    {
        // Hide success/fail at startup
        if (successUI) successUI.SetActive(false);
        if (failUI) failUI.SetActive(false);

        // Show Start + Instructions at the very beginning
        if (startButton) startButton.gameObject.SetActive(true);
        if (instructionsPanel) instructionsPanel.SetActive(true);

        // Hide in-game buttons
        if (pauseButton) pauseButton.gameObject.SetActive(false);
        if (instructionButton) instructionButton.gameObject.SetActive(false);
        if (duringGameMenuButton) duringGameMenuButton.gameObject.SetActive(false);
        if (restartButton) restartButton.gameObject.SetActive(false);

        // Hide after-game button
        if (afterGameMenuButton) afterGameMenuButton.gameObject.SetActive(false);

        // If you have a "Back to Game" button on instructions, ensure it's hidden or shown
        if (backToGameButton) backToGameButton.gameObject.SetActive(false);

        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (gameActive && !isPaused)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                FailGame();
            }
            UpdateTimerDisplay();
        }
    }

    public void UpdateProgress(bool correct)
    {
        if (!gameActive) return;

        if (correct)
        {
            currentProgress += 0.05f;
            if (greenBar) greenBar.value = currentProgress;
            if (currentProgress >= 1f)
            {
                WinGame();
            }
        }
        else
        {
            Debug.Log("Wrong Element!");
            if (greenBar) greenBar.value = currentProgress;
        }
    }

    void WinGame()
    {
        gameActive = false;
        if (successUI) successUI.SetActive(true);

        // Hide in-game buttons
        if (pauseButton) pauseButton.gameObject.SetActive(false);
        if (instructionButton) instructionButton.gameObject.SetActive(false);
        if (duringGameMenuButton) duringGameMenuButton.gameObject.SetActive(false);
        if (restartButton) restartButton.gameObject.SetActive(false);

        // Show after-game button
        if (afterGameMenuButton) afterGameMenuButton.gameObject.SetActive(true);

        Debug.Log("You Win!");
    }

    void FailGame()
    {
        gameActive = false;
        if (failUI) failUI.SetActive(true);

        // Hide in-game buttons
        if (pauseButton) pauseButton.gameObject.SetActive(false);
        if (instructionButton) instructionButton.gameObject.SetActive(false);
        if (duringGameMenuButton) duringGameMenuButton.gameObject.SetActive(false);
        if (restartButton) restartButton.gameObject.SetActive(false);

        // Show after-game button
        if (afterGameMenuButton) afterGameMenuButton.gameObject.SetActive(true);

        Debug.Log("Game Over!");
    }

    // ====== BUTTON METHODS ======

    public void OnStartGame()
    {
        Debug.Log("OnStartGame() called.");
        // Hide Start + Instructions panel
        if (startButton) startButton.gameObject.SetActive(false);
        if (instructionsPanel) instructionsPanel.SetActive(false);

        // Show in-game buttons
        if (pauseButton) pauseButton.gameObject.SetActive(true);
        if (instructionButton) instructionButton.gameObject.SetActive(true);
        if (duringGameMenuButton) duringGameMenuButton.gameObject.SetActive(true);
        if (restartButton) restartButton.gameObject.SetActive(true);

        // If there's a "Back to Game" button on the instructions panel, hide it
        if (backToGameButton) backToGameButton.gameObject.SetActive(false);

        // Reset
        gameActive = true;
        isPaused = false;
        currentTime = totalTime;
        currentProgress = 0f;
        if (greenBar) greenBar.value = 0f;
        if (successUI) successUI.SetActive(false);
        if (failUI) failUI.SetActive(false);

        // Set pause button text to "Pause" initially
        if (pauseButtonText) pauseButtonText.text = "Pause";

        Debug.Log("Game Started!");
    }

    // Toggle play/pause with one button
    public void OnTogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseButtonText) pauseButtonText.text = "Play";
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseButtonText) pauseButtonText.text = "Pause";
            Debug.Log("Game Resumed");
        }
    }

    // Show instructions mid-game, also pause
    public void OnShowInstructions()
    {
        if (!gameActive) return; // If the game hasn't started, do nothing
        // Show the instructions panel
        if (instructionsPanel) instructionsPanel.SetActive(true);

        // Show "Back to Game" button if it exists
        if (backToGameButton) backToGameButton.gameObject.SetActive(true);

        // Pause the game
        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Instructions shown, game paused.");
    }

    // Hide instructions and resume
    public void OnBackToGame()
    {
        if (instructionsPanel) instructionsPanel.SetActive(false);
        if (backToGameButton) backToGameButton.gameObject.SetActive(false);

        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Instructions hidden, game resumed.");
    }

    public void OnMainMenuButton()
    {
        Debug.Log("Go to Main Menu (placeholder)...");
    }

    // Pressing "Restart" at any time -> Full reset to initial state
    public void OnRestartGame()
    {
        Debug.Log("OnRestartGame() called, returning to initial state.");

        Time.timeScale = 1f; // Make sure time is running
        gameActive = false;
        isPaused = false;

        currentTime = totalTime;
        currentProgress = 0f;
        if (greenBar) greenBar.value = 0f;
        if (successUI) successUI.SetActive(false);
        if (failUI) failUI.SetActive(false);

        // Hide all in-game and after-game buttons
        if (pauseButton) pauseButton.gameObject.SetActive(false);
        if (instructionButton) instructionButton.gameObject.SetActive(false);
        if (duringGameMenuButton) duringGameMenuButton.gameObject.SetActive(false);
        if (restartButton) restartButton.gameObject.SetActive(false);
        if (afterGameMenuButton) afterGameMenuButton.gameObject.SetActive(false);

        // Show Start + Instructions again
        if (startButton) startButton.gameObject.SetActive(true);
        if (instructionsPanel) instructionsPanel.SetActive(true);

        // Hide "Back to Game" if it exists
        if (backToGameButton) backToGameButton.gameObject.SetActive(false);

        // Timer display
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (!timerText) return;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
