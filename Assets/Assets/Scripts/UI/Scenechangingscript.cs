using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scenechangingscript : MonoBehaviour
{
    [SerializeField] private Button startbutton;
    [SerializeField] private float delayBeforeChange = 3f;
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        startbutton.onClick.AddListener(OnStartButtonClicked);
    }
    private void OnBeforeTransformParentChanged(string scenename)
    {
        string scenena = sceneToLoad;
    }
    private void OnStartButtonClicked()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene(sceneToLoad);
    }
}
