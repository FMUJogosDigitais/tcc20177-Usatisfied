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
    public ModelActions myaction;
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
        name = myName.text = myaction.name;
        myName.GetComponent<LanguageText>().ChangeInitialReference(myaction.name);
        myDuration.gameObject.SetActive(TimeLineController.inEdit);
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
        imgStress.fillAmount = tstress;
    }

    public void SetResiliencesFinal()
    {
        GameManagerResilience.GetInstance().CallEventUpdateResiliences((int)myaction.physic, (int)myaction.mental, (int)myaction.social, (int)myaction.emotional);
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

    private void OnDisable()
    {
        gmtl.EventDayTotalChange -= ChangeDayTime;
    }
  
}
