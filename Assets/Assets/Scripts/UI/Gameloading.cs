using UnityEditor.SearchService;
using UnityEngine;

public class Gameloading : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private GameObject loadingScreen;

    private void Start()
    {
        StartCoroutine(DisableAfterDelay());
    }

    private System.Collections.IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        loadingScreen.SetActive(false);
    }
}