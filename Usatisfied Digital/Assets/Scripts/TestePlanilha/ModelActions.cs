using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ModelActions  {
    public string name;
    public bool isBase;
    [Range(0f,1)]
    public float fisicoMulti;
    [Range(0f, 1)]
    public float mentalMulti;
    [Range(0f, 1)]
    public float socialMulti;
    [Range(0f, 1)]
    public float emocionalMulti;

    [Range(1, 24)]
    public int duration;
    [Header("Valor Por hora")]
    public float fisicoHourAction;
    public float mentalHourAction;
    public float socialHourAction;
    public float emocionalHourAction;
    [Header("Valor no Dia")]
    public float fisicoOnDay;
    public float mentalOnDay;
    public float socialOnDay;
    public float emocionalOnDay;

    ModelActions() { }
    public ModelActions(ModelActions template)
    {
        if (template != null) {
            this.name = template.name;
            this.isBase = template.isBase;
            this.fisicoMulti = template.fisicoMulti;
            this.mentalMulti = template.mentalMulti;
            this.socialMulti = template.socialMulti;
            this.emocionalMulti = template.emocionalMulti;
            this.duration = template.duration;
            this.fisicoHourAction = template.fisicoHourAction;
            this.mentalHourAction = template.mentalHourAction;
            this.emocionalHourAction = template.emocionalHourAction;
            this.socialHourAction = template.socialHourAction;
            this.fisicoOnDay = template.fisicoOnDay;
            this.mentalOnDay = template.mentalOnDay;
            this.socialOnDay = template.socialOnDay;
            this.emocionalOnDay = template.emocionalOnDay;
        }
    }

    
}
