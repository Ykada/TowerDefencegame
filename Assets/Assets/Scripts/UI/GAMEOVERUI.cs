using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAMEOVERUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returntomenu;
    [SerializeField] private string gamescene;
    [SerializeField] private string mainMenuSceneName;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        returntomenu.onClick.AddListener(ReturnToMainMenu);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(gamescene);
    }
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
