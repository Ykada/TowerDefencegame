using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Uihandler : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Text loadingInfoText;
    [SerializeField] private string sceneToLoad = "MAP2";
    private static bool shadersAlreadyWarmed = false;

    private void Start()
    {
        if (loadingInfoText != null)
            loadingInfoText.text = "Initializing...";

        if (shadersAlreadyWarmed)
        {
            if (loadingInfoText != null)
                loadingInfoText.text = "Shaders already warmed — skipping...";

            Invoke(nameof(LoadNextScene), 1f);
        }
        else
        {
            Invoke(nameof(LoadNextScene), 1f);
            //Invoke(nameof(StartShaderRendering), 0.5f);
        }
    }

    private void StartShaderRendering()
    {
        if (loadingInfoText != null)
            loadingInfoText.text = "Warming up shaders...";

        Shader.WarmupAllShaders();

        Camera cam = Camera.main;
        if (cam != null)
            cam.renderingPath = RenderingPath.DeferredShading;

        if (Shader.Find("Hidden/Internal-ScreenSpaceShadows") != null)
        {
            Shader.globalMaximumLOD = 600;
            if (loadingInfoText != null)
                loadingInfoText.text = "Shaders warmed up (with shadows).";
        }
        else
        {
            Shader.globalMaximumLOD = 300;
            if (loadingInfoText != null)
                loadingInfoText.text = "Shaders warmed up (no shadows).";
        }

        shadersAlreadyWarmed = true;

        Invoke(nameof(LoadNextScene), 1f);
    }

    private void LoadNextScene()
    {
        if (loadingInfoText != null)
            loadingInfoText.text = "Loading Scene...";

        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        SceneManager.LoadScene(sceneToLoad);
    }
}
