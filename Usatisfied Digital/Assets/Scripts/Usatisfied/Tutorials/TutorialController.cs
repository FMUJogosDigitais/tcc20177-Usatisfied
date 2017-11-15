using System.Collections;
using System.Collections.Generic;
using Utils.Localization;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private int nextMessage;
    private TextAnimated texAnimated;

    public GameObject[] referenceButton;

    private void Awake()
    {
        ClearReferencias();
    }

    private void Start()
    {
        texAnimated = TutorialManager.GetInstance().messagemBallon.GetComponentInChildren<TextAnimated>();
        if (TutorialManager.GetInstance().startTutorial)
        {
            TutorialManager.ToogleButtonNextTutorial(false);
            Invoke("MessageFirst", TutorialManager.GetInstance().delayStart);
        }
    }
    void ClearReferencias()
    {
        int i = referenceButton.Length;
        for (int x=0; x< i; x++) {
            referenceButton[x].SetActive(false);
        }
    }

    void ToggleReferences(int idde)
    {
        ClearReferencias();
        referenceButton[idde].SetActive(true);
    }

    public int GetTutorialPhase()
    {
        return nextMessage;
    }
    public void NextOnTap()
    {
        if (TutorialManager.GetInstance().startTutorial)
        {
            if (nextMessage <= TutorialManager.tutorialFinal && TutorialManager.finishMessage == true)
            {
                TutorialManager.finishMessage = false;
                TutorialManager.ToogleButtonNextTutorial();
                TutorialManager.ToggleMessage();
                if (TutorialManager.pauseTutorial == false)
                {
                    nextMessage++;
                    TutorialManager.SetTutorialPhase(nextMessage);
                    NextMessagens(nextMessage);
                }
                else
                {
                    TutorialManager.ToggleImagePanel(false);
                }
            }
        }
    }

    void MessageFirst()
    {
        TutorialManager.ToggleMessage(true);
        AnimationManager.GetInstance().FaceChange(2);
        string message = LocalizationManager.GetText("_Hello, My name is Maira, welcome to U-Satisfied");
        texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);

    }

    public void NextMessagens(int number)
    {
        string message = "";

        switch (number)
        {
            case 0:
                break;
            case 1:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("I need your help to improve my life and make it fantastic!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 2:
                TutorialManager.ToggleMessage(true);

                message = LocalizationManager.GetText("for this we need to take care of our quality of life");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 3:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("I hope with your experience to improve my resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 4:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("and speaking on them let me introduce to you");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 5:
                TutorialManager.ToggleMessage(false);
                ToggleReferences(0);
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 6:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("To overcome intellect or psychological challenges I need mental resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 7:
                TutorialManager.ToggleMessage(false);
                ToggleReferences(1);
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 8:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("For strength and health challenges I need physical resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 9:
                TutorialManager.ToggleMessage(false);
                ToggleReferences(2);
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 10:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("For a good coexistence with others I need social resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 11:
                TutorialManager.ToggleMessage(false);
                ToggleReferences(3);
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 12:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("and to feel myself comfortable and happy, I need emotional resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 13:
                ClearReferencias();
                TutorialManager.ToggleMessage(true);
                AnimationManager.GetInstance().FaceChange(4);
                message = LocalizationManager.GetText("whenever I fillup a resilience to its maximum I get very SATISFIED!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 14:
                TutorialManager.ToggleMessage(false);
                ToggleReferences(4);
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 15:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("So I accumulate satisfaction points that at the end of the day will help me");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 16:
                TutorialManager.ToggleMessage(true);
                AnimationManager.GetInstance().FaceChange(4);
                message = LocalizationManager.GetText("to meet new challenges. At the end SATISFACTION is all we want, is it?");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 17:;
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 18:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Well, now enough talk and let's organize ourselves.");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 19:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Let's start programming our first day");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 20:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(5);
                message = LocalizationManager.GetText("Nothing better than starting the day with a good breakfast");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 21:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Drag the indicated icon to the panel below!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 22:
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                TutorialManager.pauseTutorial = true;
                TutorialManager.ToggleImagePanel(false);
                break;
            case 23:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(6);
                message = LocalizationManager.GetText("Now let's adjust the time of our meal!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 24:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Let's adjust for 2:00 meal hours!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 25:
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                TutorialManager.pauseTutorial = true;
                TutorialManager.ToggleImagePanel(false);
                break;
            case 26:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(7);
                message = LocalizationManager.GetText("Do you see this markup? each color represents the gains in each resilience");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 27:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("It's important to keep the balance between all the resilience, and more");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 28:
                AnimationManager.GetInstance().FaceChange(5);
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Avoid the hype, do something for a long time can be a STRESS");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 29:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(8);
                message = LocalizationManager.GetText("That's it, let's do it, for today we will only to do this!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 30:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Let's hit the action button and follow to perform the actions!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 31:
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                TutorialManager.pauseTutorial = true;
                TutorialManager.ToggleImagePanel(false);
                break;
            case 32:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Delicious! Note that the resiliencies up there have increased!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 33:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Whenever a resilience fills your bar during the day");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 34:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("We won a point of satisfaction, but attention!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 35:
                TutorialManager.ToggleMessage(true);
                AnimationManager.GetInstance().FaceChange(6);
                message = LocalizationManager.GetText("if the amount exceeds the bar, we will accumulate stress");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 36:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("I think I can leave everything under your care! But before some tips");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 37:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("The action of sleeping, recovering the stressed resilience, then");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 38:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("It is important in the day there is a sleeping action");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 39:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("to remove an action double click on it!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 40:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("sometimes there will be some unexpected challenges");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 41:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("but do not worry, with resilience we can overcome");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 42:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("We're ready to go!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            default:
                // Fim do processo
                TutorialManager.FinishTutorial();
                break;
        }
    }

    
    IEnumerator WaitASecond(float w)
    {
        yield return new WaitForSeconds(w);
    }
}
