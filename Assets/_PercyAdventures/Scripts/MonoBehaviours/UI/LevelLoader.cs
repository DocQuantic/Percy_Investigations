using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    private SceneController sceneController;

    private void Start()
    {
        sceneController = SceneController.instance;
    }

    public void LoadLevel(string sceneName)
    {
        sceneController.FadeAndLoadScene(sceneName);
    }

    /*
    public void LoadLevel(int sceneIndex)
    {
        progressStatus.text = "0 %";
        progressSlider.value = 0;
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Debug.Log("Load");
    }
    */

    /*
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (operation.isDone == false)
        {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressStatus.text = Mathf.RoundToInt(progress * 100f) + " %";
            progressSlider.value = progress;

            yield return null;
        }
    }
    */
}
