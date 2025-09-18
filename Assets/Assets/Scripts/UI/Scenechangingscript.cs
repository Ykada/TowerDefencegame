using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenechangingscript : MonoBehaviour
{
    [SerializeField] private Button startbutton;

    private void Start()
    {
        startbutton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
