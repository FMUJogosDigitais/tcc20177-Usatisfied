using System;
using UnityEngine;
using Utils.Localization;

public class TimerLoadScene : MonoBehaviour {

    public float waitTimeInSaconds;
    public int loadScene;
    public int[] skipScene;

    private LoadSceneManager loadSceneManager;

    private void OnEnable()
    {
        SetInitialReferences();
    }
    void SetInitialReferences()
    {
        try
        {
            loadSceneManager = FindObjectOfType<LoadSceneManager>();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            loadSceneManager = null;
        }
    }
    public void InitTimer()
    {
        if (loadSceneManager != null)
        {
            if (LanguagePlayerPref.IsSetPlayerLanguage() && Array.IndexOf(skipScene, loadScene) >= 0)
                loadScene++;
            loadSceneManager.LoadScene(loadScene, waitTimeInSaconds);
        }
    }
}
