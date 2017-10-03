using UnityEngine;
using System;
[System.Serializable]

public class ModelActions
{
    public string name;
    public enum ActionType { Sleep, Feed, Career, Fun, Sports, Health, Schedule }
    public Sprite icon;
    public ActionType actionType;
    public bool isBase;
    public bool isActive = true;
    public bool isStressed = false;
    [Range(0f, 1)]
    public float fisicoMulti;
    [Range(0f, 1)]
    public float mentalMulti;
    [Range(0f, 1)]
    public float socialMulti;
    [Range(0f, 1)]
    public float emocionalMulti;

    [Range(1, 24 * 60)]
    public float duration;
    public int actionUse;
    public int satisfaction;

    [Header("Valor no Dia")]
    public float physic, mental, social, emotional;

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
            this.emocionalMulti = template.emocionalMulti;
            this.duration = template.duration;
            this.physic = template.physic;
            this.mental = template.mental;
            this.social = template.social;
            this.emotional = template.emotional;
            this.actionUse = template.actionUse;
            this.icon = template.icon;
            this.actionType = template.actionType;
            this.satisfaction = template.satisfaction;
        }
        GeneradeResilience();
    }

    public void GeneradeResilience()
    {
        // a duração foi feita em minutos mas os calculos são em base de hora, então a duração é dividido por 60
        float durationInHour = duration / 60;
        physic = (GameManager.GetInstance().fisicoPerHour * fisicoMulti) * durationInHour;
        mental = (GameManager.GetInstance().mentalPerHour * mentalMulti) * durationInHour;
        social = (GameManager.GetInstance().socialPerHour * socialMulti) * durationInHour;
        emotional = (GameManager.GetInstance().emocionalPerHour * emocionalMulti) * durationInHour;
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
}
