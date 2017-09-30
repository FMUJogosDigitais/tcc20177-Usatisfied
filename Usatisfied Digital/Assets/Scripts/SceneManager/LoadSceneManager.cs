using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils.Localization;
/// <summary>
/// Namespace:      Tools
/// Class:          LoadSceneManager
/// Description:    Control All Scane transitions.
/// Author:         Renato Innocenti                    Date: 05/20/2017
/// Notes:          Attach on gameObject with control scenes (ScaneManager);
/// Revision History:
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0.1 - Skip Load if alradey done
/// Name: Renato Innocenti           Date:08/26/2017        Description: v1.1 - Call by another Scripts
/// </summary>
///
public class LoadSceneManager : IDontDestroy<LoadSceneManager>
{
    [Header("LoadSceneManager")]
    #region Public Variables
    //public string sceneName;
    [Tooltip("This object must be disabled")]
    public GameObject loadingScreenObject;
    public Image loadBar;
    [Tooltip("This object must be disabled")]
    public Image fadeScreen;
    [Range(0f, 1f)]
    public float fadeTimeIn = 1f;
    [Range(0f, 1f)]
    public float fadeTimeOut = 1f;
    #endregion

    #region Private Variables
    private AsyncOperation async;
    enum Fade { In, Out };
    #endregion
    public void LoadScene(int idScene, float time = 0)
    {
        if ((SceneManager.sceneCountInBuildSettings -1) >= idScene)
        {
            StartCoroutine(LoadingScreen(idScene, time));
        }
    }
    /// <summary>
    /// Controla a tela de loading
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingScreen(int idScene, float time = 0)
    {
        if (time > 0)
            yield return new WaitForSeconds(time);
        async = SceneManager.LoadSceneAsync(idScene);
        async.allowSceneActivation = false;

        FadeOut();
        yield return new WaitForSeconds(fadeTimeOut);

        if (async.progress < 0.88f)
        {
            if (loadingScreenObject)
            {
                loadingScreenObject.SetActive(true);
                FadeIn();
                yield return new WaitForSeconds(fadeTimeIn);
                fadeScreen.transform.parent.gameObject.SetActive(false);
                //loadscreen
                while (async.isDone == false)
                {
                    loadBar.fillAmount = async.progress;
                    if (async.progress == 0.9f)
                    {
                        loadBar.fillAmount = 1;
                        FadeOut();
                        yield return new WaitForSeconds(fadeTimeOut);
                        loadingScreenObject.SetActive(false);
                        async.allowSceneActivation = true;
                        FadeIn();
                        yield return new WaitForSeconds(fadeTimeIn);
                        fadeScreen.transform.parent.gameObject.SetActive(false);
                    }
                    yield return null;
                }
            }

        }
        else
        {
            async.allowSceneActivation = true;
            FadeIn();
            yield return new WaitForSeconds(fadeTimeIn);
            if (fadeScreen)
                fadeScreen.transform.parent.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Controla a transição de quando a cena aparece na tela
    /// </summary>
    void FadeIn()
    {
        StartCoroutine(FadeAudio(fadeTimeIn - 0.2f, Fade.In));
        if (fadeScreen)
        {
            fadeScreen.canvasRenderer.SetAlpha(1.0f);
            fadeScreen.CrossFadeAlpha(0.0f, fadeTimeIn, false);
        }

    }
    /// <summary>
    /// Controla a transição de quando a cena desaparece da tela
    /// </summary>
    void FadeOut()
    {
        StartCoroutine(FadeAudio(fadeTimeOut - 0.2f, Fade.Out));
        if (fadeScreen)
        {
            fadeScreen.canvasRenderer.SetAlpha(0.0f);
            fadeScreen.transform.parent.gameObject.SetActive(true);
            fadeScreen.CrossFadeAlpha(1.0f, fadeTimeOut, false);
        }

    }
    /// <summary>
    /// Controla a entrada e saida do som durante as transições
    /// </summary>
    /// <param name="timer"> tempo entre as transições</param>
    /// <param name="fadeType">tempo de transição</param>
    /// <returns></returns>
    IEnumerator FadeAudio(float timer, Fade fadeType)
    {
        float start = fadeType == Fade.In ? 0.0F : 1.0F;
        float end = fadeType == Fade.In ? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F / timer;

        while (i <= 1.0F)
        {
            i += step * Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(step * Time.deltaTime);
        }
    }
}
