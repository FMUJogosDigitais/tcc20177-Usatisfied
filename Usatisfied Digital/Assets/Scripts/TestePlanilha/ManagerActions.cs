using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerActions : IDontDestroy<ManagerActions> {

    public ModelActions[] actions;

    private void Start()
    {
        ResiliencesPerHour();
        TotalResiliencesbyDay();
    }

    private void Update()
    {
        //ResiliencesPerHour();
    }

    public void ResiliencesPerHour()
    {
        foreach (ModelActions action in actions)
        {
            action.fisicoHourAction = action.fisicoMulti * BasePontuacao.GetInstance().fisicoPhour;
            action.mentalHourAction = action.mentalMulti * BasePontuacao.GetInstance().mentalPhour;
            action.emocionalHourAction = action.emocionalMulti * BasePontuacao.GetInstance().emocionalPhour;
            action.socialHourAction = action.socialMulti * BasePontuacao.GetInstance().socialPhour;
        }
    }

    public void TotalResiliencesbyDay()
    {
        foreach (ModelActions action in actions)
        {
            action.fisicoOnDay = action.fisicoHourAction * action.duration;
            action.mentalOnDay = action.mentalHourAction * action.duration;
            action.emocionalOnDay = action.emocionalHourAction * action.duration;
            action.socialOnDay = action.socialHourAction * action.duration;
        }
    }
}
