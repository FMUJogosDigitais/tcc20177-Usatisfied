using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utils.Localization;

public class TimeLineControllerEdit : TimeLineController, IDropHandler
{
    public Transform contentEdit;

    private void Awake()
    {

    }

    protected override void OnEnable()
    {
        inEdit = true;
        GameManager.StartGame = false;
        StopAllCoroutines();
        SetInitialReferences();
        ClearContent(contentEdit);
        CreateList(contentEdit);
        SetTutorial();
    }

    public void OnDrop(PointerEventData eventData)
    {
        inEdit = true;
        Presets_Buttons preset = eventData.pointerDrag.GetComponent<Presets_Buttons>();
        if (preset != null && GameManager.TutorialMode && preset.presetID != 1)
        {
            TutorialManager.ToggleImagePanel(true);
            TutorialManager.ToggleMessage(true);
            string message = LocalizationManager.GetText("Let's try the food first!");
            TutorialManager.GetInstance().messagemBallon.GetComponentInChildren<TextAnimated>().SetMessage(message, FinishCallbak);
            return;
        }
        if (preset != null && GameManagerTimeline.maxHour - gmtl.DayDuration > 0)
        {
            if (GameManager.TutorialMode && preset.presetID == 1)
            {
                TutorialManager.ToggleImagePanel(true);
                TutorialManager.ToggleMessage(true);
                TutorialManager.pauseTutorial = false;
                string message = LocalizationManager.GetText("Very well now let's set the time for our meal");
                TutorialManager.GetInstance().messagemBallon.GetComponentInChildren<TextAnimated>().SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
            }
            ModelActions action = new ModelActions(GameManager.GetInstance().GetTemplates(preset.presetID));
            action.duration = gmtl.GetDuration(action.duration); // corrige o valor de acordo com o salto;
            float dayduration = gmtl.GetListActionInDay().Sum(x => x.duration) + action.duration;
            //Debug.Log(alltime);
            if (GameManagerTimeline.maxHour - gmtl.DayDuration > 0)
            {
                gmtl.DayDuration = dayduration;
                //action.modelId = gmtl.CountDaylist();
                gmtl.AddActionInList(action);
                AddActionInParent(contentEdit);
                gmtl.CallEventAddActionInList(contentEdit);
            }
        }
    }

    private void AddActionInParent(Transform cont)
    {
        int childs = cont.childCount;
        GameObject go = Instantiate<GameObject>(gmtl.buttonAction, cont);
        go.GetComponent<TimeLineButtons>().RefrashAddList(childs);
        go.transform.SetAsFirstSibling();
    }

    public void FinishCallbak(bool finish)
    {
        TutorialManager.ToogleButtonNextTutorial();
        TutorialManager.finishMessage = true;
    }

    private void SetTutorial()
    {
        //Debug.Log(TutorialManager.GetTutorialFase());
        if (GameManager.TutorialMode && TutorialManager.GetTutorialFase() == 37)
        {
            AnimationManager.GetInstance().FaceChange(0);
            TutorialController tutorialController = FindObjectOfType<TutorialController>();
            TutorialManager.ToggleImagePanel(true);
            TutorialManager.ToggleMessage(true);
            TutorialManager.pauseTutorial = false;
            TutorialManager.finishMessage = true;
            TutorialManager.ToogleButtonNextTutorial();
            tutorialController.NextOnTap();
        }
    }
    protected override void OnDisable()
    {
        inEdit = false;
        GameManager.StartGame = true;
    }
}