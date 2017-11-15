using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour {

    private void OnEnable()
    {
        GameManagerLeaderBoard.GetInstance().PopulateList(GameManagerResilience.GetInstance().TotalSatisfaction);
    }
}
