using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILoadingFinisher : MonoBehaviour
{
    [SerializeField] private GameObject startingstuff;
    [SerializeField] private GameObject UIfinishloader;
    [SerializeField] private Text gameloadingdatatext;

    private void Start()
    {
        FinishLoading();
        gameloadingdatatext.text = "Prepairing For Shaders...";
    }
    public void FinishLoading()
    {
        Shader.WarmupAllShaders();
        Shader.globalMaximumLOD = 300;
        Shader.FindAnyObjectByType<Camera>().renderingPath = RenderingPath.DeferredShading;

        if (Shader.Find("Hidden/Internal-ScreenSpaceShadows") != null)
        {
            Shader.globalMaximumLOD = 600;
            Shader.FindAnyObjectByType<Camera>().renderingPath = RenderingPath.DeferredShading;
        }
        else
        {
            Shader.globalMaximumLOD = 300;
            Shader.FindAnyObjectByType<Camera>().renderingPath = RenderingPath.DeferredShading;
        }
        gameloadingdatatext.text = "Shaders Finished";
        Invoke("DisableLoader", 1f);
    }
    private void DisableLoader()
    {
        UIfinishloader.SetActive(false);
        startingstuff.SetActive(true);
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}
