using UnityEngine;
using System;

[System.Serializable]
public class ModelActions 
{
    public string name;
    public enum ActionType { Sleep, Feed, Career, Fun, Sports, Health, Schedule, Challenger }
    public Sprite icon;
    public ActionType actionType;
    public bool isBase;
    public bool isActive = true;
    public bool isStressed = false;
    [Range(0f, 1)]
    public float stressRate;
    [Header("Resiliences Multiplicadores")]
    [Range(0f, 1)]
    public float fisicoMulti;
    [Range(0f, 1)]
    public float mentalMulti;
    [Range(0f, 1)]
    public float socialMulti;
    [Range(0f, 1)]
    public float emotionalMulti;

    [Range(1, 24 * 60)]
    public float duration;
    public int actionUse = 0;
    public int satisfaction;

    [Header("Valor no Dia")]
    public float physic;
    public float mental;
    public float social;
    public float emotional;

    [Header("Challenge Sets")]
    public string animationCode;
    public int satisfactionCost = 1;
    [Range(0f, 1)]
    public float chanceRate;

    ModelActions() { }
    public ModelActions(ModelActions template)
    {
        if (template != null)
        {
            this.name = template.name;
            this.isBase = template.isBase;
            this.isActive = template.isActive;
            this.isStressed = template.isStressed;
            this.fisicoMulti = template.fisicoMulti;
            this.mentalMulti = template.mentalMulti;
            this.socialMulti = template.socialMulti;
            this.emotionalMulti = template.emotionalMulti;
            this.duration = template.duration;
            this.physic = template.physic;
            this.mental = template.mental;
            this.social = template.social;
            this.emotional = template.emotional;
            this.actionUse = template.actionUse;
            this.icon = template.icon;
            this.actionType = template.actionType;
            this.satisfaction = template.satisfaction;
            this.animationCode = template.animationCode;
            this.satisfactionCost = template.satisfactionCost;
            this.chanceRate = template.chanceRate;
        }
        GeneradeResilience();
    }

    public void GeneradeResilience()
    {
        physic = (GameManager.GetInstance().fisicoPerMin * fisicoMulti) * duration;
        mental = (GameManager.GetInstance().mentalPerMin * mentalMulti) * duration;
        social = (GameManager.GetInstance().socialPerMin * socialMulti) * duration;
        emotional = (GameManager.GetInstance().emocionalPerMin * emotionalMulti) * duration;
    }
    public float GetResilienceAction(float baseAction, float value, float maxponts)
    {
        if (value > maxponts)
        {
            value = maxponts;
        }
        float totalResilience = (baseAction * value) / maxponts;
        return totalResilience;
    }

    public float GetRecoveryStressSleepAction(float maxponts)
    {
        if (actionType == ActionType.Sleep)
        {
            float sleep = GameManager.GetInstance().sleppPerMin * duration;
            sleep = (sleep > maxponts) ? maxponts : GameManager.GetInstance().sleppPerMin * duration;
            return sleep / maxponts;
        }
        return 0;
    }
    public float GetStressResilienceAction(float baseAction, float value, float maxponts)
    {
        if (actionType != ActionType.Sleep)
        {
            float stress = value - maxponts;
            if (stress >= 0)
            {
                float totalStress = GetResilienceAction(baseAction / 4, stress, maxponts);
                return totalStress;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }

    public void SetModel()
    {
        throw new NotImplementedException();
    }
}