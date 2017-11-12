using UnityEngine;

[System.Serializable]

public class ModelLeaderBoard {

    public Sprite myface;
    public int mySatisfaction, myPhysics, myMental, mySocial, myEmotional;
    public bool isPlayer = false;
    public enum Persona { Looser, Easy, Copycat, Challenger }
    [Header("OnlyPersonas")]
    public Persona myPersona;
    public int myPosition;
}
