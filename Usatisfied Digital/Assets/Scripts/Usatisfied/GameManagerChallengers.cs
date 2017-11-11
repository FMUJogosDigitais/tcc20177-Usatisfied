using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManagerChallengers : IDontDestroy<GameManagerChallengers>
{
    GameManagerTimeline gmtl;
    [SerializeField]
    private GameObject buttonChallenger;
    [Range(0, 1f)]
    public float porcentageChance = .5f;
    public int maxChallengerDay = 5;
    private static int actulChallegerInDay = 0;


    private void OnEnable()
    {
        SetInitialReference();
        gmtl.EventAddActionInList += AddChallengerOnDrop;
    }
    void SetInitialReference()
    {
        gmtl = GameManagerTimeline.GetInstance();
    }

    public static void ResetActualChallengerInDay()
    {
        actulChallegerInDay = 0;
    }
    public GameObject GetButtonChallenger()
    {
        return buttonChallenger;
    }
    public void AddChallengerOnDrop(Transform parent)
    {
        ModelActions[] challengers = GameManager.GetInstance().GetChallenger();
        float chanceForChallenger = GameManager.GetInstance().TotalDay/ GameManager.roundsDaysGame;
        if (chanceForChallenger > 0 && actulChallegerInDay <= maxChallengerDay)
        {
            // começam os desafios
            for (int x=0; x < chanceForChallenger; x++)
            {
                float r = Random.Range(0,1f);
                //TODO: Aumentar a probabilidade conforme a resiliencia quebra
                float newchance = porcentageChance * chanceForChallenger;
                if (newchance >= r)
                {
                    int chanrand = Random.Range(0,challengers.Length);
                    challengers[chanrand].duration = gmtl.GetDuration(challengers[chanrand].duration);
                    if(gmtl.DayDuration < GameManagerTimeline.maxHour)
                    {
                        ConfigureChallengers(challengers[chanrand], parent);
                    }
                    
                }
            }

        }

    }

    private void ConfigureChallengers(ModelActions challenger, Transform parent)
    {
        // tem tempo paara adicionar na linha do tempo
        //TODO: Fazer ativar as animações para os stress.

        challenger.duration = (challenger.duration > (GameManagerTimeline.maxHour - gmtl.DayDuration)) ? GameManagerTimeline.maxHour - gmtl.DayDuration : challenger.duration;
        gmtl.AddActionInList(challenger);
        AddActionInParent(parent);
        gmtl.DayDuration = gmtl.GetListActionInDay().Sum(action => action.duration) + challenger.duration;
        challenger.actionUse += 1;
        actulChallegerInDay += 1;
        Debug.Log("Arrumou Problema");
    }
    private void AddActionInParent(Transform cont)
    {
        int childs = cont.childCount;
        GameObject go = Instantiate<GameObject>(buttonChallenger, cont);
        go.GetComponent<TimeLineButtons>().RefrashAddList(childs);
        go.transform.SetAsFirstSibling();
    }
    private void OnDisable()
    {
        gmtl.EventAddActionInList -= AddChallengerOnDrop;
    }

}