using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : IDontDestroy<AnimationManager> {

    public Animator player;
    
    // Quads
    public GameObject terra;
    public GameObject grama;
    public GameObject calcada;
    public GameObject predio;
    public GameObject chuva;
    public GameObject prediosNoite;

    // Elementos de cena
    public GameObject restScene;
    public GameObject funScene;
    public GameObject feedScene;
    public GameObject careerScene;
    public GameObject healthScene;
    public GameObject chuvaScene;

    // Ganhando satisfação
    public GameObject satisfationStars;

    // Use this for initialization
    void Start () {
        SetActionAnimation(ModelActions.ActionType.Sleep);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StatisfactionEarningAnimation()
    {
        satisfationStars.GetComponent<Animator>().SetTrigger("satisfactionEarn");
    }

    public void SetActionAnimation(ModelActions.ActionType actionType, string subAnimation = "")
    {
        float sorteio = Random.Range(0f, 10f);
        ModelActions.ActionType aninToSet = actionType;
        TurnOffScenaries();
        SetQuadsMove(0f);

        switch (aninToSet)
        {
            case ModelActions.ActionType.Sleep:
                {
                    restScene.SetActive(true);
                    SetNight();
                    player.SetTrigger("Rest");
                    break;
                }

            case ModelActions.ActionType.Sports:
                {
                    if (StressManager.esporteEstressada)
                    {
                        player.SetTrigger("StressRun");
                        SetQuadsMove(0.3f);
                        StressManager.esporteEstressada = false;
                    }
                    else if (sorteio <= 5f)
                    {
                        player.SetTrigger("Walk");
                        SetQuadsMove(0.1f);
                    }
                    else if (sorteio >= 5f)
                    {
                        player.SetTrigger("Run");
                        SetQuadsMove(0.3f);
                    }                   
                    
                    break;
                }

            case ModelActions.ActionType.Fun:
                {
                    funScene.SetActive(true);
                    player.SetTrigger("Fun");
                    break;
                }

            case ModelActions.ActionType.Career:
                {
                    careerScene.SetActive(true);
                    player.SetTrigger("Career");
                    break;
                }

            case ModelActions.ActionType.Health:
                {
                    healthScene.SetActive(true);
                    player.SetTrigger("Health");
                    break;
                }

            case ModelActions.ActionType.Feed:
                {
                    feedScene.SetActive(true);
                    if (StressManager.alimentacaoEstressada == true)
                    {
                        player.SetTrigger("StressedFeed");
                        StressManager.alimentacaoEstressada = false;
                    }
                    else
                    {                                              
                        player.SetTrigger("Feed");
                    }
                        break;
                }

            case ModelActions.ActionType.Challenger:
                {
                    if (subAnimation != "")
                    {
                        switch (subAnimation)
                        {
                            case "Des_VizinhoBarulhento":
                                restScene.SetActive(true);
                                SetNight();
                                player.SetTrigger("Des_VizinhoBarulhento");
                                break;

                            case "Des_Transito":
                                player.SetTrigger("Des_Transito");
                                break;
                            case "Des_Preguica":
                                funScene.SetActive(true);
                                player.SetTrigger("Des_Preguica");
                                break;
                            case "Des_Chuva":
                                SetRain();
                                SetNight();
                                player.SetTrigger("Des_Chuva");
                                break;
                        }
                    }
                    break;     
                }
        }
    }

    private void TurnOffScenaries() {
        restScene.SetActive(false);
        funScene.SetActive(false);
        feedScene.SetActive(false);
        careerScene.SetActive(false);
        healthScene.SetActive(false);
        chuvaScene.SetActive(false);
        chuva.SetActive(false);
        prediosNoite.SetActive(false);
        predio.SetActive(true);
    }

    private void SetNight()
    {
        prediosNoite.SetActive(true);
        predio.SetActive(false);
    }

    private void SetRain()
    {
        chuva.SetActive(true);
        chuvaScene.SetActive(true);
    }

    private void SetQuadsMove(float speed) {

        terra.GetComponent<TextureMove>().scrollSpeed = speed;
        grama.GetComponent<TextureMove>().scrollSpeed = speed;
        calcada.GetComponent<TextureMove>().scrollSpeed = speed;
        predio.GetComponent<TextureMove>().scrollSpeed = speed / 10;

    }

    public void TestButton(string test)
    {
        switch (test)
        {
            case "sleep":
                {
                    SetActionAnimation(ModelActions.ActionType.Sleep);
                    break;
                }

            case "sports":
                {
                    SetActionAnimation(ModelActions.ActionType.Sports);
                    break;
                }

            case "sports_estressado":
                {
                    StressManager.esporteEstressada = true;
                    SetActionAnimation(ModelActions.ActionType.Sports);
                    break;
                }

            case "fun":
                {
                    SetActionAnimation(ModelActions.ActionType.Fun);
                    break;
                }
            case "career":
                {
                    SetActionAnimation(ModelActions.ActionType.Career);
                    break;
                }
            case "health":
                {
                    SetActionAnimation(ModelActions.ActionType.Health);
                    break;
                }
            case "feed":
                {
                    SetActionAnimation(ModelActions.ActionType.Feed);
                    break;
                }
            case "feed_estressado":
                {
                    StressManager.alimentacaoEstressada = true;
                    SetActionAnimation(ModelActions.ActionType.Feed);
                    break;
                }
            case "vizinho":
                {
                    StressManager.isVizinho = true;
                    SetActionAnimation(ModelActions.ActionType.Challenger);
                    break;
                }
            case "transito":
                {
                    StressManager.isTransito = true;
                    SetActionAnimation(ModelActions.ActionType.Challenger);
                    break;
                }
            case "preguica":
                {
                    StressManager.isPreguica = true;
                    SetActionAnimation(ModelActions.ActionType.Challenger);
                    break;
                }
            case "chuva":
                {
                    StressManager.isChuva = true;
                    SetActionAnimation(ModelActions.ActionType.Challenger);
                    break;
                }
            case "satisfaction":
                {
                    StatisfactionEarningAnimation();
                    break;
                }
                
        }
    }
}
