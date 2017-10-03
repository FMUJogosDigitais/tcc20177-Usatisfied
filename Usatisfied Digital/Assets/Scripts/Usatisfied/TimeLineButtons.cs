using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utils.Localization;

public class TimeLineButtons : MonoBehaviour
{
    public int daylistRef;
    //ModelActions myaction;
    public Image myicon;
    public Text myName;
    public Text myTime;
    public Slider myDuration;
    public Slider myProgress;
    [Header("Resiliences")]
    public Image imgPhysic;
    public Image imgMental;
    public Image imgSocial;
    public Image imgEmotional;
    public Image imgStress;

    private float actualDayDuration;

    GameManagerTimeline gmtl;
    private void OnEnable()
    {
        //SetButtonTimeline();
        InitialReferences();
        gmtl.EventSetNewMaxValue += ChangeMaxValues;
    }

    private void Start()
    {
        //Debug.Log(daylistRef);
        SetButtonTimeline();
    }

    private void InitialReferences()
    {
        gmtl = GameManagerTimeline.GetInstance();
    }

    private void SetButtonTimeline(int idde = -1)
    {
        GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
        idde = (idde < 0) ? daylistRef : idde;
        ModelActions action = gmtl.GetListActionInDay(idde);
        actualDayDuration = gmtl.dayDuration + action.duration;
        myicon.sprite = action.icon;
        name = action.name;
        myName.GetComponent<LanguageText>().ChangeInitialReference(action.name);
        action.duration = gmtl.GetDuration(action.duration);
        //Debug.Log(action.duration);
        myTime.text = gmtl.GetStringHour(action.duration);
        myDuration.value = action.duration / gmtl.durationScale;
        myDuration.gameObject.SetActive(TimeLineController.inEdit);
        myProgress.gameObject.SetActive(!TimeLineController.inEdit);
        gmtl.dayDuration -= action.duration;
        gmtl.SetDayDuration(gmtl.dayDuration);
        action.GeneradeResilience();
        SetResilienceImage();
    }

    public void SlideChangeHour(int idde = -1)
    {
        GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
        idde = (idde < 0) ? daylistRef : idde;
        ModelActions action = gmtl.GetListActionInDay(idde);
        float durationInMin = myDuration.value * gmtl.durationScale;
        float restday = (durationInMin - action.duration);
        myTime.text = gmtl.GetStringHour(durationInMin);
        action.duration = durationInMin;
        gmtl.dayDuration -= restday;
        gmtl.SetDayDuration(gmtl.dayDuration);
        action.GeneradeResilience();
        gmtl.CallEventSetNewMaxValue(gmtl.dayDuration);
        SetResilienceImage();
    }

    private void SetResilienceImage(int idde = -1)
    {
        GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
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
        imgStress.fillAmount = tstress;
    }

    public float ProgressBar(float speed, float value)
    {
         return Mathf.Lerp(myProgress.value, value, Time.deltaTime * speed);
    }

    private void ChangeMaxValues(float max)
    {
        myDuration.maxValue = ((myDuration.value * gmtl.durationScale) + max) / gmtl.durationScale;
    }
    private void OnDisable()
    {
        gmtl.EventSetNewMaxValue += ChangeMaxValues;
    }

}
