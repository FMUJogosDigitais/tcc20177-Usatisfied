using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IDontDestroy<GameManager> {

    public enum Resiliences { Mental, Phisycs, Emotional, Social, Satisfaction }
    public float fisicoPerHour = 30;
    public float mentalPerHour = 12;
    public float emocionalPerHour = 22;
    public float socialPerHour = 32;
    public float sleppPerHour = 10;

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
