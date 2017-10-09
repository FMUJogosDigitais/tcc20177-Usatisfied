using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerResilience : IDontDestroy<GameManagerResilience>
{
    public GameObject panelPhysics;
    public GameObject panelMental;
    public GameObject panelEmotional;
    public GameObject panelSocial;
    public GameObject panelSatisfation;

    [SerializeField]
    static int totalSatisfaction;
    public int TotalSatisfaction
    {
        get { return totalSatisfaction; }
        set
        {
            totalSatisfaction += value;
            panelSatisfation.GetComponentInChildren<Text>().text = totalSatisfaction.ToString();
        }
    }

    public delegate void ChangeResilienceEventHandler(GameManager.Resiliences resilience, int totalvalue);
    public event ChangeResilienceEventHandler EventUpdateResiliences;

    public void CallEventUpdateResiliences(int physic = 0, int mental = 0, int social = 0, int emotional = 0)
    {
        if (EventUpdateResiliences != null)
        {
            if (physic != 0)
                EventUpdateResiliences(GameManager.Resiliences.Phisycs, physic);
            if (mental != 0)
                EventUpdateResiliences(GameManager.Resiliences.Mental, mental);
            if (social != 0)
                EventUpdateResiliences(GameManager.Resiliences.Social, social);
            if (emotional != 0)
                EventUpdateResiliences(GameManager.Resiliences.Emotional, emotional);
        }
    }

    private void Start()
    {
        panelSatisfation.GetComponentInChildren<Text>().text = totalSatisfaction.ToString();
    }
    public void GainSatisfation()
    {
        Debug.Log("YUPYY!!!! Satisfaction!!!");
    }
}
