using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scenechangingscript : MonoBehaviour
{
    [SerializeField] private Button startbutton;
    [SerializeField] private float delayBeforeChange = 3f;

    private void Start()
    {
        startbutton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("GameScene");
    }
}
