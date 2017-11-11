﻿using System.Collections;
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

    public bool physicsStressed;
    public bool mentalStressed;
    public bool emotionalStressed;
    public bool socialStressed;

    [SerializeField] static int totalSatisfaction = 0;
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

    public delegate void RecoverySleepEventHandler(int totalvalue);
    public event RecoverySleepEventHandler EventRecoveryStress;

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

    public void CallEventRecoveryStress(int recovery)
    {
        if (EventRecoveryStress != null)
        {
                EventRecoveryStress( recovery);
        }
    }
    public override void Awake()
    {
        base.Awake();
        panelSatisfation.GetComponentInChildren<Text>().text = totalSatisfaction.ToString();
    }
    private void Start()
    {
        panelSatisfation.GetComponentInChildren<Text>().text = totalSatisfaction.ToString();
    }
    public void GainSatisfation()
    {
        AnimationManager.GetInstance().StatisfactionEarningAnimation();
        //Debug.Log("YUPYY!!!! Satisfaction!!!");
    }

    public void SetResilienceStressed(GameManager.Resiliences resilience, bool stress)
    {
        switch (resilience)
        {
            case GameManager.Resiliences.Mental:
                mentalStressed = stress;
                break;
            case GameManager.Resiliences.Phisycs:
                physicsStressed = stress;
                break;
            case GameManager.Resiliences.Emotional:
                emotionalStressed = stress;
                break;
            case GameManager.Resiliences.Social:
                socialStressed = stress;
                break;

        }
    }
}
