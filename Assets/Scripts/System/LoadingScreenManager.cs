﻿// LoadingScreenManager
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// for Nowhere Prophet (http://www.noprophet.com)
//
// Licensed under GNU General Public License v3.0
// http://www.gnu.org/licenses/gpl-3.0.txt

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{

    [Header("Loading Visuals")]
    public Image loadingIcon;
    public Image loadingDoneIcon;
    public Text loadingText;
    public Image progressBar;
    public Image fadeOverlay;
    public CharacterController cc;

    [Header("Timing Settings")]
    public float waitOnLoadEnd = 0.25f;
    public float fadeDuration = 0.25f;

    [Header("Loading Settings")]
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority;

    [Header("Other")]
    // If loading additive, link to the cameras audio listener, to avoid multiple active audio listeners
    public AudioListener audioListener;

    AsyncOperation operation;
    Scene currentScene;

    public static Vector3 spawnPosition;
    public static int sceneToLoad = -1;
    // IMPORTANT! This is the build index of your loading scene. You need to change this to match your actual scene index
    static int loadingSceneIndex = 7;

    public static void LoadScene(int levelNum, Vector3 spawnTo)
    {
        GameData.data.map = levelNum;
        spawnPosition = spawnTo;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        sceneToLoad = levelNum;
        SceneManager.LoadScene(loadingSceneIndex);
    }

    void Start()
    {
        if (sceneToLoad < 0)
            return;

        fadeOverlay.gameObject.SetActive(true); // Making sure it's on so that we can crossfade Alpha
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoad));
    }

    private IEnumerator LoadAsync(int levelNum)
    {
        ShowLoadingVisuals();

        yield return null;

        FadeIn();
        StartOperation(levelNum);

        float lastProgress = 0f;

        // operation does not auto-activate scene, so it's stuck at 0.9
        while (DoneLoading() == false)
        {
            yield return null;

            if (Mathf.Approximately(operation.progress, lastProgress) == false)
            {
                progressBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }

        if (loadSceneMode == LoadSceneMode.Additive)
            audioListener.enabled = false;

        ShowCompletionVisuals();

        yield return new WaitForSeconds(waitOnLoadEnd);

        FadeOut();

        yield return new WaitForSeconds(fadeDuration);

        if (loadSceneMode == LoadSceneMode.Additive)
        {
            SceneManager.UnloadSceneAsync(currentScene.name);
        }
            
        else
        {
            operation.allowSceneActivation = true;
        }
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void StartOperation(int levelNum)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
    }

    void FadeOut()
    {
        fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
    }

    void ShowLoadingVisuals()
    {
        loadingIcon.gameObject.SetActive(true);
        loadingDoneIcon.gameObject.SetActive(false);

        progressBar.fillAmount = 0f;
        loadingText.text = "LOADING...";
    }

    void ShowCompletionVisuals()
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(true);

        progressBar.fillAmount = 1f;
        loadingText.text = "DONE";
        loadingText.color = new Color(223, 255, 161);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Level Loaded" + scene.name + " at:" + spawnPosition);
            if(scene.name != "LoadingScreen")
            {
                GameObject.FindWithTag("Player").transform.position = spawnPosition;
            }
            
        }

}