using System.Collections;
using System.Collections.Generic;
using Utils.Localization;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private int nextMessage =20;
    private TextAnimated texAnimated;

    public GameObject[] referenceButton;

    private void Awake()
    {
        ClearReferencias();
    }

    private void Start()
    {
        texAnimated = TutorialManager.GetInstance().messagemBallon.GetComponentInChildren<TextAnimated>();
        if (TutorialManager.startTutorial)
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
    public void NextOnTap()
    {
        if (TutorialManager.startTutorial)
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
                    Debug.Log("Proxima");
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
        texAnimated.SetMessage("_Olá, Meu nome é Maíra, bem vindo ao U-Satisfied", TutorialManager.GetInstance().FinishCallbak);

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
                message = LocalizationManager.GetText("Preciso da sua ajuda para aprimorar minha vida e torna-la fantástica!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 2:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("para isso precisamos cuidar bem da nossa qualidade de vida");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 3:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("conto com sua experiencia para melhorar as minhas resiliências");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 4:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("por falar nelas deixe-me apresenta-las a você");
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
                message = LocalizationManager.GetText("Para superar desafios de intelecto ou psicologico eu preciso de resiliência mental");
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
                message = LocalizationManager.GetText("Para os desafios de força e saúde eu preciso de resiliência fisica");
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
                message = LocalizationManager.GetText("Uma boa convivencia com os outros eu preciso de resiliência social");
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
                message = LocalizationManager.GetText("e para me sentir confortavel e feliz comigo, preciso de resiliência emocional");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 13:
                ClearReferencias();
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("sempre que eu treinar uma resiliencia ao seu máximo eu fico muito SATISFEITA!");
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
                Debug.LogWarning("Colcoar mudança de animação aqui");
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Assim eu acumulo pontos de satisfações que no fim do dia vão me ajudar");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 16:
                Debug.LogWarning("Colcoar mudança de animação aqui");
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("a superar novos desafios. No fim SATISFAÇÃO é tudo que queremos, concorda?");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 17:
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                StartCoroutine(WaitASecond(1));
                TutorialManager.finishMessage = true;
                TutorialManager.ToogleButtonNextTutorial();
                break;
            case 18:
                Debug.LogWarning("Colcoar mudança de animação aqui");
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Bom, agora chega de conversa e vamos nos organizar.");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 19:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Vamos iniciar a programação do nosso primeiro dia");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 20:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(5);
                message = LocalizationManager.GetText("Nada melhor que começar o dia com um bom café da manhã");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 21:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Arraste o icone indicado para o painel abaixo!");
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
                message = LocalizationManager.GetText("Agora vamos ajustar o tempo da nossa refeição!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 24:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Vamos ajustar para 2:00 de refeição!");
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
                message = LocalizationManager.GetText("Esta vendo esta marcação? cada cor representa os ganhos em cada resiliencia");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 27:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("É importante manter o equilibrio entre todas as resiliênas, e mais");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 28:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Evitar os exageros, fazer algo por muito tempo pode ser um ESTRESSE");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 29:
                TutorialManager.ToggleMessage(true);
                ToggleReferences(8);
                message = LocalizationManager.GetText("Bom, vamos comer, por hoje só faremos isso!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 30:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Vamos apertar o botão de ação e seguir para executar as ações!");
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
                message = LocalizationManager.GetText("Que delicia! Note que as resiliencias lá em cima aumentaram!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 33:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Sempre que uma resiliencia encher sua barra durante o dia");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 34:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Ganhamos um ponto de satisfação, mas cuidado!");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 35:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("se a quantia acumulada ultrapassar a barra, vamos acumular estresse");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 36:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Vamos voltar a tela de edição! Clicando na tela");
                texAnimated.SetMessage(message, TutorialManager.GetInstance().FinishCallbak);
                break;
            case 37:
                TutorialManager.ToggleMessage(false);
                ClearReferencias();
                TutorialManager.pauseTutorial = true;
                TutorialManager.ToggleImagePanel(false);
                break;
            case 38:
                TutorialManager.ToggleMessage(true);
                message = LocalizationManager.GetText("Voltemos com o tutorial");
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
