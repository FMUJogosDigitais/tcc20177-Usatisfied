using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLeaderBoard : IDontDestroy<GameManagerLeaderBoard>
{
    public GameObject buttonLeaderBoard;
    public Transform contentlist;
    [SerializeField]
    Sprite playerFace;
    [SerializeField]
    Sprite[] personasFace;
    [SerializeField]
    int maxLeaderboards = 5;
    [SerializeField]
    int maxSatisfactionPoints = 200;
    [SerializeField]
    int mediaSatisfationDay = 3;
    [SerializeField]
    int minSatisfactionPoints = 1;
    [SerializeField]
    int maxResiliencePoints = 50;
    [SerializeField]
    int minResiliencePoints = 5;
    [SerializeField]
    int playerPosition;
    [SerializeField]
    ModelLeaderBoard[] otherPersonas;
    [SerializeField]
    List<ModelLeaderBoard> listLeaderBoard;

    private void Start()
    {
        listLeaderBoard = new List<ModelLeaderBoard>();
        if (otherPersonas.Length <= 0)
        {
            otherPersonas = new ModelLeaderBoard[maxLeaderboards - 1];
            CreatePersonas();
        }      
    }

    void CreatePersonas()
    {
        if (otherPersonas != null)
        {
            ReShuffle(personasFace);
            int i = maxLeaderboards - 1;
            for (int x = 0; x < i; x++)
            {
                otherPersonas[x] = new ModelLeaderBoard();
                otherPersonas[x].myface = personasFace[x];
                otherPersonas[x].myPersona = (ModelLeaderBoard.Persona)x;
            }
        }
    }

    ModelLeaderBoard Player()
    {
        ModelLeaderBoard player = new ModelLeaderBoard();
        player.isPlayer = true;
        player.myface = playerFace;
        player.myMental = GameManagerResilience.mentalTimes;
        player.myPhysics = GameManagerResilience.physicsTimes;
        player.mySocial = GameManagerResilience.socialTimes;
        player.myEmotional = GameManagerResilience.emotionaltimes;
        player.mySatisfaction = GameManagerResilience.GetInstance().TotalSatisfaction;
        return player;
    }

    public void PopulateList(int satisfation)
    {
        ClearBoard();
        int totalday = GameManager.GetInstance().TotalDay;
        if (totalday >= 1)
        {
            
            otherPersonas[0].mySatisfaction = Random.Range(0, minSatisfactionPoints * totalday);
            otherPersonas[1].mySatisfaction = Random.Range(otherPersonas[0].mySatisfaction + 1, mediaSatisfationDay * totalday);
            otherPersonas[2].mySatisfaction = Random.Range(otherPersonas[1].mySatisfaction + 1, (mediaSatisfationDay * totalday) + minSatisfactionPoints);
            otherPersonas[3].mySatisfaction = Random.Range(otherPersonas[2].mySatisfaction + 1, (mediaSatisfationDay * totalday) + mediaSatisfationDay);

            ResiliencePoints(otherPersonas[0]);
            ResiliencePoints(otherPersonas[1]);
            ResiliencePoints(otherPersonas[2]);
            ResiliencePoints(otherPersonas[3]);

            listLeaderBoard.Add(Player());
            listLeaderBoard.AddRange(otherPersonas);

            listLeaderBoard = listLeaderBoard.OrderBy(x => x.mySatisfaction).ThenBy(x=>x.myEmotional).ThenBy(x => x.mySocial).ThenBy(x => x.myPhysics).ToList();

            for (int x = listLeaderBoard.Count -1 ; x>=0; x--)
            {
                GameObject go = Instantiate<GameObject>(buttonLeaderBoard, contentlist);
                go.GetComponent<LeaderBoardButtons>().SetCard(listLeaderBoard[x]);
            }
        }
        if(!GameManager.GameOver)
            NavigationManager.tapOn = true;
    }
    void ClearBoard()
    {
        listLeaderBoard = new List<ModelLeaderBoard>();
        foreach (Transform child in contentlist)
        {
            Destroy(child.gameObject);
        }
    }

    void ResiliencePoints(ModelLeaderBoard persona)
    {
        int satisf = persona.mySatisfaction;
        for (int x = 0; x < 4; x++)
        {
            if (satisf > 0)
            {
                switch (x)
                {
                    case 0:
                        persona.myEmotional = Random.Range(0, satisf +1);
                        satisf -= persona.myEmotional;
                        break;
                    case 1:
                        persona.mySocial = Random.Range(0, satisf+1);
                        satisf -= persona.mySocial;
                        break;
                    case 2:
                        persona.myPhysics = Random.Range(0, satisf+1);
                        satisf -= persona.myPhysics;
                        break;
                    case 3:
                        persona.myMental = satisf;
                        satisf -= persona.myMental;
                        break;
                }
            }
        }
    }
    void ReShuffle(Sprite[] avatars)
    {
        int i = avatars.Length;
        for (int t = 0; t < i; t++)
        {
            Sprite tmp = avatars[t];
            int r = Random.Range(t, avatars.Length);
            avatars[t] = avatars[r];
            avatars[r] = tmp;
        }
    }
}
