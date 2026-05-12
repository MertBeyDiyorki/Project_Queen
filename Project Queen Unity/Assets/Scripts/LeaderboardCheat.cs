using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LeaderboardCheat : MonoBehaviour
{
    [Header("Þifre Ayarlarý")]
    [Tooltip("Tabloyu sýfýrlayacak gizli þifre")]
    [SerializeField] private string cheatCode = "mertbey";

    private int currentIndex = 0;

    private void OnEnable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput += OnTextInput;
        }
    }

    private void OnDisable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }
    }

    private void OnTextInput(char c)
    {
        if (char.ToLower(c) == cheatCode[currentIndex])
        {
            currentIndex++;

            if (currentIndex >= cheatCode.Length)
            {
                ResetLeaderboard();
                currentIndex = 0;
            }
        }
        else
        {
            if (char.ToLower(c) == cheatCode[0])
            {
                currentIndex = 1;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    private void ResetLeaderboard()
    {
        PlayerPrefs.DeleteKey("LocalLeaderboard");
        PlayerPrefs.DeleteKey("HighScores");
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}