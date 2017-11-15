using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardButtons : MonoBehaviour {

    [SerializeField]
    Image avtar;
    [SerializeField]
    Text satisfidText, emotional, social, mental, physic;

    ModelLeaderBoard myCard;

    public void SetCard(ModelLeaderBoard card)
    {
        //criacard
        avtar.sprite = card.myface;
        satisfidText.text = card.mySatisfaction.ToString();
        emotional.text = card.myEmotional.ToString();
        social.text = card.mySocial.ToString();
        mental.text = card.myMental.ToString();
        physic.text = card.myPhysics.ToString();
    }

}
