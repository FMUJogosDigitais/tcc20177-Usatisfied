using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Usatisfied.Navigation;

public class GameManagerTimeline : IDontDestroy<GameManagerTimeline>
{

    public GameObject buttonAction;
    [SerializeField]
    private List<ModelActions> listActionInDay;
    public static float maxHour = 24f * 60; //duracao em minutos
    public Text dayDurationText;
    [SerializeField]
    private float dayDuration;
    public float DayDuration{
        get { return dayDuration; }
        set {
            dayDuration = value;
            dayDurationText.text = GetStringHour(maxHour - dayDuration);
        }
    }

    public float durationScale = 5;
    public float resilienceMaxForSatisfation = 100;

    public delegate void ChangeValueEventHandler(float allmaxvaue);
    public event ChangeValueEventHandler EventDayTotalChange;

    public delegate void AddActionEventHandler(Transform cont);
    public event AddActionEventHandler EventAddActionInList;

    public delegate void RemoveActionEventHandler(int idde);
    public event RemoveActionEventHandler EventRemoveActionInList;

    public override void Awake()
    {
        base.Awake();
        dayDurationText.text = GetStringHour(maxHour);
    }
    public void CallOnDayDurationChange( float allmaxvaue)
    {
        if (EventDayTotalChange != null)
        {
            EventDayTotalChange(allmaxvaue);
        }
    }
    public void SetTextDayTime(float time)
    {
        dayDurationText.text = GetStringHour(maxHour - time);
    }


    public int CountDaylist()
    {
        if (listActionInDay != null)
            return listActionInDay.Count;
        else
            return 0;
    }
    public List<ModelActions> GetListActionInDay()
    {
        return listActionInDay;
    }

    public ModelActions GetListActionInDay(int idde)
    {
        return listActionInDay[idde];
    }
    public void AddActionInList(ModelActions action)
    {
        listActionInDay.Add(action);
    }

    public void CallEventAddActionInList( Transform cont)
    {
        if (EventAddActionInList != null)
        {
            EventAddActionInList(cont);
        }
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
        return duration = (duration % durationScale != 0) ? (Mathf.Ceil(duration / durationScale) * durationScale) : duration;
    }

    public void SetUpDuration(int idde, float duration)
    {
        listActionInDay[idde].duration = duration;
    }
}
