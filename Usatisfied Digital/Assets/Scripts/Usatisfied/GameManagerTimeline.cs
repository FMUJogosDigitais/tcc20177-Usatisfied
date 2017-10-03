using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Usatisfied.Navigation;

public class GameManagerTimeline : IDontDestroy<GameManagerTimeline>
{
    public float timeProgressInSec = 1f;
    public GameObject buttonAction;
    [SerializeField]
    private List<ModelActions> listActionInDay;
    private float maxHour = 24f * 60; //duracao em minutos
    [SerializeField]
    private Text dayDurationTotal;
    public float dayDuration;
    public float durationScale = 5;
    public float resilienceMaxForSatisfation = 100;

    public delegate void SliderEventHandler(float max);
    public event SliderEventHandler EventSetNewMaxValue;

    public override void Awake()
    {
        base.Awake();
        dayDuration = maxHour;
        SetDayDuration(dayDuration);
    }

    public void SetDayDuration(float value)
    {
        dayDurationTotal.text = GetStringHour(value);
    }

    public void CallEventSetNewMaxValue(float max)
    {
        if (EventSetNewMaxValue != null)
        {
            EventSetNewMaxValue(max);
        }
    }

    public int CountDaylist()
    {
        return listActionInDay.Count;
    }

    public ModelActions GetListActionInDay(int idde)
    {
        return listActionInDay[idde];
    }
    public void AddActionInList(ModelActions action)
    {
        if (dayDuration > 0)
            listActionInDay.Add(action);
    }
    public void RemoveActionInList(int idde)
    {
        listActionInDay.RemoveAt(idde);
    }
    public string GetStringHour(float minutes)
    {
        float hour = minutes / 60;
        float min = minutes % 60;
        return String.Format("{0:00}:{1:00}", (int)hour, min);
    }
    public float GetDuration(float duration)
    {
        duration = (duration % durationScale != 0) ? (Mathf.Ceil(duration / durationScale) * durationScale) : duration;
        if (dayDuration - duration >= 0)
        {
            return duration;
        }
        return 0;
    }

    public void SetUpDuration(int idde, float duration)
    {
        listActionInDay[idde].duration = duration;
    }
}
