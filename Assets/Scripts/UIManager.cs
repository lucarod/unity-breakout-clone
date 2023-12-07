using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void HandleGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.GameOver)
        {
            EnableGameOverMenu();
        }
    }
}
