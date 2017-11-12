using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Utils.Localization;

public class TimeLineController : MonoBehaviour
{

    // Use this for initialization
    [Range(0, 1f)]
    public float timeProgressBar;
    public Transform content;
    public static bool inEdit = false;
    protected GameManagerTimeline gmtl;

    private void Awake()
    {
        SetInitialReferences();
    }

    protected virtual void OnEnable()
    {
        inEdit = false;
        GameManager.StartGame = true;
        StopAllCoroutines();
        SetInitialReferences();
        if (gmtl.CountDaylist() <= 0)
        {
            NavigationManager.GetInstance().ToggleEditTimeline();
        }
        else
        {
            NavigationManager.tapOn = false;
            CreateList(content);
            StartCoroutine(DayCycle());
        }
    }

    private void Start()
    {
        GameManager.GetInstance().TotalDay = 0;
    }
    protected void SetInitialReferences()
    {
        gmtl = GameManagerTimeline.GetInstance();
    }
    protected void ClearContent(Transform cont)
    {
        StopAllCoroutines();
        int i = cont.childCount;
        for (int x = 0; x < i; x++)
        {
            Destroy(cont.GetChild(x).gameObject);
        }
    }
    protected void CreateList(Transform cont)
    {
        if (gmtl.CountDaylist() > 0)
        {
            ClearContent(cont);
            int i = gmtl.CountDaylist();
            float totalDay = 0;
            for (int x = 0; x < i; x++)
            {
                GameObject button = gmtl.buttonAction;
                if (gmtl.GetListActionInDay(x).actionType == ModelActions.ActionType.Challenger)
                {
                    button = GameManagerChallengers.GetInstance().GetButtonChallenger();
                }
                GameObject go = Instantiate<GameObject>(button, cont);
                go.GetComponent<TimeLineButtons>().RefrashAddList(x);
                if (inEdit)
                    go.transform.SetAsFirstSibling();
                else
                    go.transform.SetAsLastSibling();
                totalDay += gmtl.GetListActionInDay(x).duration;
            }

            gmtl.SetTextDayTime(totalDay);
        }
    }

    private void FinishAction(TimeLineButtons button)
    {
        button.SetResiliencesFinal();
        
    }
    private void AddNewDay()
    {
        //TODO: Aqui o dia termina e é somado um dia no total;
        GameManager.GetInstance().TotalDay += 1;
        GameManagerChallengers.ResetActualChallengerInDay();
        gmtl.DayDuration = 0;
        AnimationManager.GetInstance().SetActionAnimation(ModelActions.ActionType.Sleep);
    }

    protected virtual void OnDisable()
    {
        inEdit = true;
        GameManager.StartGame = false;
        StopAllCoroutines();
    }

    IEnumerator DayCycle()
    {
        int numberchild = content.childCount;

        while (numberchild > 0)
        {
            if (inEdit == false)
            {
                TimeLineButtons firstChild = content.GetChild(0).GetComponent<TimeLineButtons>();
                if (firstChild)
                {
                    float minuteDuration = (firstChild.myaction.duration * timeProgressBar) / GameManager.GetInstance().debugSpeedyTime;
                    ModelActions.ActionType type = firstChild.myaction.actionType;
                    AnimationManager.GetInstance().SetActionAnimation(type, firstChild.myaction.animationCode);
                    //TODO: Pausar a barra, apra reiniciar quando for para a edição.
                    StartCoroutine(firstChild.BarProgress(minuteDuration));
                    //Debug.Log(firstChild.daylistRef);
                    yield return new WaitForSeconds(minuteDuration);
                    Destroy(content.GetChild(0).gameObject);
                    GameManagerTimeline.GetInstance().RemoveActionInList(0);
                    FinishAction(firstChild);
                }
                //TODO: Colocar o numero de vezes que a ação foi usada
                numberchild -= 1;
            }
            yield return null;
        }
        AddNewDay();
        TutorialFase();
        yield return new WaitForSeconds(1.5f);
        NavigationManager.GetInstance().ToggleLiderBoard();
    }

    void TutorialFase()
    {
        if (GameManager.TutorialMode == true)
        {
            TutorialController tutorialController = FindObjectOfType<TutorialController>();
            TutorialManager.ToggleImagePanel(true);
            TutorialManager.ToggleMessage(true);
            TutorialManager.pauseTutorial = false;
            TutorialManager.finishMessage = true;
            TutorialManager.ToogleButtonNextTutorial();
            tutorialController.NextOnTap();
        }
    }
}
