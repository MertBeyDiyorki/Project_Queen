using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Skor Arayüzü")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Paneller")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject nameInputPanel;

    [Header("Girdi Alaný")]
    [SerializeField] private TMP_InputField nameInputField;

    private bool isPaused = false;
    private bool isGameOver = false;

    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScoreUI;
        PlayerCollision.OnGameOver += ShowGameOverUI;
        PlayerInputHandler.OnPause += TogglePause;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScoreUI;
        PlayerCollision.OnGameOver -= ShowGameOverUI;
        PlayerInputHandler.OnPause -= TogglePause;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (nameInputPanel != null) nameInputPanel.SetActive(false);
        if (scoreText != null)
        {
            if (PlayerPrefs.GetInt("ChallengeMode", 0) == 1)
                scoreText.color = Color.red;
            else
                scoreText.color = Color.white;
        }

        UpdateScoreUI(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateScoreUI(int newScore) => scoreText.text = newScore.ToString();

    private void ShowGameOverUI()
    {
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TogglePause()
    {
        if (isGameOver) return;

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pausePanel != null) pausePanel.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            if (pausePanel != null) pausePanel.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void RestartGame()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (nameInputPanel != null) nameInputPanel.SetActive(true);
    }

    public void ConfirmNameAndRestart()
    {
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {
            PlayerPrefs.SetString("PlayerName", nameInputField.text);
            PlayerPrefs.Save();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}