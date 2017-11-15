using System.Collections;
using System.Collections.Generic;
using Utils.Localization;
using UnityEngine.UI;
using UnityEngine;

public class TutorialManager : IDontDestroy<TutorialManager>
{

    public GameObject PanelTutorial;
    public GameObject faceGirl;
    public GameObject messagemBallon;
    public GameObject buttonNext;
    

    public bool startTutorial = false;
    static int tutorialFase = 0;
    public static int tutorialFinal = 50;
    public float delayStart = 3f;
    public static bool finishMessage = false;
    public static bool pauseTutorial = false;
    Text myText;
    public override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        PlayerPrefTutorial.ResetTutorial();
#endif

        startTutorial = !SetPlayerPref.GetPlayerTutorial();
        if (startTutorial == false)
        {
            startTutorial = true;
            GameManager.TutorialMode = true;
            PlayerPrefTutorial.SetPlayerTutorial(tutorialFase);
            TooglePainelTutorial(true);
        }
        if (PlayerPrefTutorial.GetPlayerTutorial() > tutorialFinal)
        {
            TooglePainelTutorial(false);
        }
    }

    private void OnEnable()
    {
        ToggleFace(true);
        ToggleMessage(false);
        ToogleButtonNextTutorial(false);
    }
    
    public static void SetTutorialPhase(int nextfase)
    {
        tutorialFase = nextfase;
    }

    public static int GetTutorialFase()
    {
        return tutorialFase;
    }
    public static void FinishTutorial()
    {
        tutorialFase = tutorialFinal;
        GameManager.TutorialMode = false;
        PlayerPrefTutorial.SetPlayerTutorial(tutorialFinal);
        TooglePainelTutorial(false);
    }

    public void FinishCallbak(bool finish)
    {
       ToogleButtonNextTutorial();
        finishMessage = true;
    }
    public static void ToggleImagePanel(bool set)
    {
        GetInstance().PanelTutorial.GetComponent<Image>().enabled = set;
    }
    public static void ToggleMessage(bool set)
    {
        int anim = (set == true) ?2:0;
        AnimationManager.GetInstance().FaceChange(anim);
        GetInstance().messagemBallon.SetActive(set);
    }

    public static void ToggleMessage()
    {
        int anim = (GetInstance().messagemBallon.activeSelf == false) ? 2 : 0;
        AnimationManager.GetInstance().FaceChange(anim);
        GetInstance().messagemBallon.SetActive(!GetInstance().messagemBallon.activeSelf);
    }

    public static void ToggleFace(bool set)
    {
        GetInstance().faceGirl.SetActive(set);
    }

    public static void ToggleFace()
    {
        GetInstance().faceGirl.SetActive(!GetInstance().faceGirl.activeSelf);
    }

    public static void TooglePainelTutorial()
    {
        GetInstance().PanelTutorial.SetActive(!GetInstance().PanelTutorial.activeSelf);
    }
    public static void TooglePainelTutorial(bool set)
    {
        GetInstance().PanelTutorial.SetActive(set);
    }
    public static void ToogleButtonNextTutorial()
    {
        GetInstance().buttonNext.SetActive(!GetInstance().buttonNext.activeSelf);
    }
    public static void ToogleButtonNextTutorial(bool set)
    {
        GetInstance().buttonNext.SetActive(set);
    }
}

public class PlayerPrefTutorial : MonoBehaviour
{
    const string TUTORIALFINISH = "tutorialFinish";

    public static void SetPlayerTutorial(int tutorialPhase)
    {
        PlayerPrefs.SetInt(TUTORIALFINISH, tutorialPhase);
    }
    public static int GetPlayerTutorial()
    {
        return PlayerPrefs.GetInt(TUTORIALFINISH);
    }
    public static void ResetTutorial()
    {
        PlayerPrefs.DeleteKey(TUTORIALFINISH);
    }

}
