using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Uihandler.cs
// Manages the UI loading screen and shader warm-up process before loading the main game scene.
// Ykada_Hiroka
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

            Invoke(nameof(LoadNextScene), 5f);
        }
        else
        {
            loadingInfoText.text = "Preparing For shaders...";
            //Invoke(nameof(LoadNextScene), 5f);
            Invoke(nameof(StartShaderRendering), 5f);
        }
    }

    void StartShaderRendering()
    {
        if (loadingInfoText != null)
            loadingInfoText.text = "Warming up shaders...";
        //Shader.WarmupAllShaders();
        //shadersAlreadyWarmed = true;
        if (loadingInfoText != null)
            loadingInfoText.text = "Awaiting Game...";
        Invoke(nameof(LoadNextScene), 8f);
    }

    private void LoadNextScene()
    {
        if (loadingInfoText != null)
            loadingInfoText.text = "Loading Game";


        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        SceneManager.LoadScene(sceneToLoad);
    }
}

// © 2025 YKΛDΛ_. All rights reserved.