using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IDontDestroy<GameManager> {

    public enum Resiliences { Mental, Phisycs, Emotional, Social, Satisfaction }
    public float fisicoPerMin = 30;
    public float mentalPerMin = 12;
    public float emocionalPerMin = 22;
    public float socialPerMin = 32;
    public float sleppPerMin = 10;

    [SerializeField]
    private ModelActions[] templateActions;

    private static bool _pauseGame = false;
    public static bool _sleepAction { get; set; }

    public static bool Pausegame()
    {
        return _pauseGame;
    }
    public static void TogglePauseGame()
    {
        _pauseGame = !_pauseGame;
    }

    public ModelActions GetTemplates(int idde)
    {
        return templateActions[idde];
    }
    public ModelActions[] GetTemplates()
    {
        return templateActions;
    }

}
