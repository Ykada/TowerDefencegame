using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Button returnbutton;
    [SerializeField] private Button returntomenu;
    [SerializeField] private string mainMenuSceneName;

    private void Start()
    {
        if (returnbutton != null)
            returnbutton.onClick.AddListener(ToggleMenu);
        if (returntomenu != null)
            returntomenu.onClick.AddListener(ReturnToMainMenu);
        if (menuUI != null)
            menuUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    private void ToggleMenu()
    {
        if (menuUI != null)
            menuUI.SetActive(!menuUI.activeSelf);
        if (menuUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }
    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);

    }
}

