using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine;
using Utils.Localization;

public class GameManager : IDontDestroy<GameManager> {

    public enum Resiliences { Mental, Phisycs, Emotional, Social, Satisfaction }
    [Range(0f, 1f)]
    public float fisicoPerMin = .30f;
    [Range(0f, 1f)]
    public float mentalPerMin = .12f;
    [Range(0f, 1f)]
    public float emocionalPerMin = .22f;
    [Range(0f, 1f)]
    public float socialPerMin = .32f;
    [Range(0f, 1f)]
    public float sleppPerMin = .10f;

    public static bool TutorialMode = false;
    public float debugSpeedyTime = 2;
    public static int maxDaysGame = 30;
    public static int roundsDaysGame = 2;
    public Text totalDayText;
    [SerializeField]
    private int totalDay = 0;
    public int TotalDay
    {
        get { return totalDay; }
        set
        {
            totalDay = value;
            string message = LocalizationManager.GetText("Day #{0:00}");
            totalDayText.text = String.Format(message, totalDay);
        }
    }

    [SerializeField]
    private ModelActions[] templateActions;
    [SerializeField]
    private ModelActions[] templateChallengers;

    private static bool _startGame = false;
    public static bool _sleepAction { get; set; }

    public static bool StartGame
    {
        get { return _startGame; }
        set { _startGame = value; }
    }
    public static void ToggleStartGame()
    {
        _startGame = !_startGame;
    }

    public ModelActions GetTemplates(int idde)
    {
        return templateActions[idde];
    }
    public ModelActions[] GetTemplates()
    {
        return templateActions;
    }

    public ModelActions GetChallenger(int idde)
    {
        return templateChallengers[idde];
    }

    public ModelActions[] GetChallenger()
    {
        return templateChallengers;
    }

}
