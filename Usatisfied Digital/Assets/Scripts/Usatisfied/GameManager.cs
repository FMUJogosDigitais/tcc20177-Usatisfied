using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine;
using Utils.Localization;

public class GameManager : IDontDestroy<GameManager> {

    public enum Resiliences { Mental, Phisycs, Emotional, Social, Satisfaction, Recovery }
    [SerializeField][Range(0f, 1f)]
    float physicPerMin = .30f;
    [SerializeField]
    [Range(0f, 1f)]
    float mentalPerMin = .12f;
    [SerializeField]
    [Range(0f, 1f)]
    float emotionalPerMin = .22f;
    [SerializeField]
    [Range(0f, 1f)]
    float socialPerMin = .32f;
    [SerializeField]
    [Range(0f, 1f)]
    float recoveryPerMin = .10f;

    public static bool TutorialMode = false;
    public float debugSpeedyTime = 2;
    [SerializeField] int maxDaysGame = 30;
    public int MaxDaysGame { get { return maxDaysGame; } }
    [SerializeField] int roundsDaysGame = 2;
    public int RoundsDaysGame { get { return roundsDaysGame; } }

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
    /// <summary>
    /// Retorna o valor da resiliencia por minuto
    /// </summary>
    /// <param name="resilience">0 fisico, 1 mental, 2 social, 3 emocional, </param>
    /// <returns></returns>
    public float GetResiliencePerMin(Resiliences resilience)
    {
        switch (resilience)
        {
            case Resiliences.Mental:
                return mentalPerMin;
            case Resiliences.Phisycs:
                return physicPerMin;
            case Resiliences.Social:
                return socialPerMin;
            case Resiliences.Emotional:
                return emotionalPerMin;
            case Resiliences.Recovery:
                return recoveryPerMin;
            default:
                return 0;
        }
    }

}
