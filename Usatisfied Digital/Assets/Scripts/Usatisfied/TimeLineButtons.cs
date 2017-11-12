using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utils.Localization;

public class TimeLineButtons : MonoBehaviour, IPointerDownHandler
{
    public int daylistRef;
    public ModelActions myaction;
    public Image myicon;
    public Text myName;
    public Text myTime;
    public Slider myDuration;
    public Slider myProgress;
    public Text satisfactionChallenger;
    [Header("Resiliences")]
    public Image imgPhysic;
    public Image imgMental;
    public Image imgSocial;
    public Image imgEmotional;
    public Image imgStress;
    public Color colorStress;
    public Color colorRecovery;

    private float actualDayDuration;
    private bool stoptutor = false;

    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    //this is how long in seconds to allow for a double click
    float delay = 0.25f;

    GameManagerTimeline gmtl;

    private void OnEnable()
    {
        // vai ativar sempre que dropar todos os botoes
        //Debug.Log("Ativou o botão" + daylistRef);
        StopAllCoroutines();
        SetInitialReferences();
        gmtl.EventDayTotalChange += ChangeDayTime;
        //RefrashAddList(daylistRef);

    }

    protected void SetInitialReferences()
    {
        gmtl = GameManagerTimeline.GetInstance();
    }

    public void RefrashAddList(int refList)
    {
        daylistRef = refList;
        //ModelActions action = gmtl.GetListActionInDay(daylistRef);
        myaction = gmtl.GetListActionInDay(daylistRef);
        myicon.sprite = myaction.icon;
        //Debug.Log(myaction.name);
        myName.GetComponent<LanguageText>().ChangeInitialReference(myaction.name);
        name = myName.text = LocalizationManager.GetText(myaction.name);
        //name = myName.text = myaction.name;
        myDuration.gameObject.SetActive(TimeLineController.inEdit);
        if (myaction.actionType == ModelActions.ActionType.Challenger)
        {
            myDuration.gameObject.SetActive(false);
            satisfactionChallenger.text = String.Format("-{0:00}", myaction.satisfactionCost);
        }

        myDuration.value = myaction.duration / gmtl.durationScale;
        myProgress.gameObject.SetActive(!TimeLineController.inEdit);
        myTime.text = gmtl.GetStringHour(myaction.duration);
        float maxAllduration = GameManagerTimeline.maxHour - gmtl.DayDuration;
        gmtl.CallOnDayDurationChange(maxAllduration);
        myaction.GeneradeResilience();
        SetResilienceImage();
    }
    public void SlideChangeHour()
    {
        myaction = gmtl.GetListActionInDay(daylistRef);

        if (GameManager.TutorialMode && TutorialManager.GetTutorialFase() == 25 && myDuration.value > 120 / gmtl.durationScale)
        {
            if (stoptutor == false)
            {
                stoptutor = true;
                TutorialPhase();
            }

        }
        else

        if (myaction.actionType != ModelActions.ActionType.Challenger)
        {
            float diference = (myDuration.value * gmtl.durationScale) - myaction.duration;
            //Debug.Log(diference);
            myaction.duration = myDuration.value * gmtl.durationScale;
            myTime.text = gmtl.GetStringHour(myaction.duration);
            gmtl.DayDuration += diference;
            float maxAllduration = GameManagerTimeline.maxHour - gmtl.DayDuration;
            gmtl.CallOnDayDurationChange(maxAllduration);
            myaction.GeneradeResilience();
            SetResilienceImage();
        }
    }

    public void ChangeDayTime(float allMaxDuration)
    {
        //Debug.Log(allMaxDuration + myaction.duration);
        myDuration.maxValue = (allMaxDuration + myaction.duration) / gmtl.durationScale;
    }
    private void SetResilienceImage(int idde = -1)
    {
        //GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
        idde = (idde < 0) ? daylistRef : idde;
        ModelActions action = gmtl.GetListActionInDay(idde);
        imgPhysic.fillAmount = action.GetResilienceAction(.25f, action.physic, gmtl.resilienceMaxForSatisfation);
        imgMental.fillAmount = action.GetResilienceAction(.25f, action.mental, gmtl.resilienceMaxForSatisfation);
        imgSocial.fillAmount = action.GetResilienceAction(.25f, action.social, gmtl.resilienceMaxForSatisfation);
        imgEmotional.fillAmount = action.GetResilienceAction(.25f, action.emotional, gmtl.resilienceMaxForSatisfation);

        float tstress = action.GetStressResilienceAction(1, action.physic, gmtl.resilienceMaxForSatisfation);
        tstress += action.GetStressResilienceAction(1, action.mental, gmtl.resilienceMaxForSatisfation);
        tstress += action.GetStressResilienceAction(1, action.social, gmtl.resilienceMaxForSatisfation);
        tstress += action.GetStressResilienceAction(1, action.emotional, gmtl.resilienceMaxForSatisfation);
        imgStress.color = colorStress;
        imgStress.fillAmount = tstress;

        if (action.actionType == ModelActions.ActionType.Sleep)
        {
            float recovery = action.GetRecoveryStressSleepAction(gmtl.resilienceMaxForSatisfation);
            imgStress.color = colorRecovery;
            imgStress.fillAmount = recovery;
            //Debug.Log(recovery);
        }

    }

    public void SetResiliencesFinal()
    {
        myaction.actionUse += 1;
        if (myaction.actionType != ModelActions.ActionType.Challenger)
        {
            GameManagerResilience.GetInstance().CallEventUpdateResiliences((int)myaction.physic, (int)myaction.mental, (int)myaction.social, (int)myaction.emotional);
            if (myaction.actionType == ModelActions.ActionType.Sleep)
            {
                float recovery = GameManager.GetInstance().GetResiliencePerMin(GameManager.Resiliences.Recovery) * myaction.duration;
                GameManagerResilience.GetInstance().CallEventRecoveryStress((int)recovery);
            }
        }
        else
        {
            GameManagerResilience.GetInstance().CallEventUpdateResiliences(-(int)myaction.physic, -(int)myaction.mental, -(int)myaction.social, -(int)myaction.emotional);
            if (myaction.satisfactionCost > 0)
            {
                if (GameManagerResilience.GetInstance().TotalSatisfaction - myaction.satisfactionCost >= 0)
                {
                    GameManagerResilience.GetInstance().TotalSatisfaction = - myaction.satisfactionCost;
                }
                else
                {
                    GameManagerResilience.ResetSatisfaction();
                }
            }
        }
        
    }
    public IEnumerator BarProgress(float time)
    {
        float rate = 1 / time;
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            myProgress.value = Mathf.Lerp(0, 1, i);
            yield return null;
        }
    }

    private void RemoveButtom(GameObject mybuttom)
    {
        GameManagerTimeline.GetInstance().DebugDayList();
        Debug.Log("------------------");
        if (TimeLineController.inEdit == true && myaction.actionType != ModelActions.ActionType.Challenger)
        {
            //Debug.Log(mybuttom.name);
            gmtl.RemoveActionInList(this.daylistRef);
            Destroy(this.gameObject);
        }
        GameManagerTimeline.GetInstance().DebugDayList();
    }

    private void TutorialPhase()
    {
        if (myDuration.value >= 120 / gmtl.durationScale)
        {
            TutorialManager.ToggleImagePanel(true);
            TutorialManager.ToggleMessage(true);
            TutorialManager.pauseTutorial = false;
            TutorialManager.GetInstance().messagemBallon.GetComponentInChildren<TextAnimated>().SetMessage("Muito bem! Vamos ter uma boa refeição!", TutorialManager.GetInstance().FinishCallbak);

        }
    }
    private void OnDisable()
    {
        gmtl.EventDayTotalChange -= ChangeDayTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TimeLineController.inEdit != true) return;
        if (!one_click)
        {
            one_click = true;
            timer_for_double_click = Time.time + delay;
        }
        else if (one_click && timer_for_double_click > Time.time)
        {
            RemoveButtom(eventData.pointerCurrentRaycast.gameObject);
            one_click = false;
        }
        else
        {
            one_click = false;
            timer_for_double_click = 0;
        }
    }
}
