using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerActions : IDontDestroy<ManagerActions> {

    public ModelActions[] actions;

    private void Start()
    {
        TotalResiliencesbyDay();
    }

    private void Update()
    {
        //ResiliencesPerHour();
    }

    public void TotalResiliencesbyDay()
    {
        foreach (ModelActions action in actions)
        {
            action.physic = (action.fisicoMulti * BasePontuacao.GetInstance().fisicoPhour) * action.duration;
            action.mental = (action.mentalMulti * BasePontuacao.GetInstance().mentalPhour) * action.duration;
            action.emotional = (action.emotionalMulti * BasePontuacao.GetInstance().emocionalPhour) * action.duration;
            action.social = (action.socialMulti * BasePontuacao.GetInstance().socialPhour) * action.duration;
        }
    }
}
