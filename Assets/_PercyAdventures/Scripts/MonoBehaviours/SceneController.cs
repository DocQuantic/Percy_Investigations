using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    #region Singleton

    public static SceneController instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        dialogueManager = DialogueManager.instance;
    }

    #endregion

    public CanvasGroup fadingCanvasGroup;
    public float fadingDuration = 1.0f;

    public bool isFading;

    private DialogueManager dialogueManager;

    private void Start()
    {
        if (dialogueManager.enabled)
        {
            dialogueManager.enabled = false;
        }

        fadingCanvasGroup.alpha = 1.0f;

        //StartCoroutine(Fade(0.0f));

        dialogueManager.enabled = true;
    }

    public void FadeAndLoadScene(SceneReaction sceneReaction)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneReaction.sceneName));
        }
    }

    public void FadeAndLoadScene(string sceneName)
    {
        //if (!isFading)
        //{
        StartCoroutine(FadeAndSwitchScenes(sceneName));
        //}
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        dialogueManager.enabled = false;

        Debug.Log("Loading");

        yield return StartCoroutine(Fade(1.0f));

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        yield return StartCoroutine(Fade(0.0f));

        dialogueManager.enabled = true;
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        fadingCanvasGroup.blocksRaycasts = true;

        float fadingSpeed = Mathf.Abs(fadingCanvasGroup.alpha - finalAlpha) / fadingDuration;

        while (!Mathf.Approximately(finalAlpha, fadingCanvasGroup.alpha))
        {
            fadingCanvasGroup.alpha = Mathf.MoveTowards(fadingCanvasGroup.alpha, finalAlpha, fadingSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;
        fadingCanvasGroup.blocksRaycasts = false;
    }

    public void LoadMap()
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes("Map"));
        }
    }

}
