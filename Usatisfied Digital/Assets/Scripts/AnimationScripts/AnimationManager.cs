using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : IDontDestroy<AnimationManager> {

    public Animator player;
    public GameObject terra;
    public GameObject grama;
    public GameObject calcada;
    public GameObject predio;
    public GameObject restScene;
    public GameObject funScene;
    public GameObject feedScene;
    public GameObject careerScene;
    public GameObject healthScene;
        
    // Use this for initialization
    void Start () {
        SetAnimation(ModelActions.ActionType.Sleep);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAnimation(ModelActions.ActionType actionType)
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
                    player.SetTrigger("Rest");
                    break;
                }

            case ModelActions.ActionType.Sports:
                {
                    if (sorteio <= 5f)
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
                    player.SetTrigger("Feed");
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
                    SetAnimation(ModelActions.ActionType.Sleep);
                    break;
                }

            case "sports":
                {
                    SetAnimation(ModelActions.ActionType.Sports);
                    break;
                }
            case "fun":
                {
                    SetAnimation(ModelActions.ActionType.Fun);
                    break;
                }
            case "career":
                {
                    SetAnimation(ModelActions.ActionType.Career);
                    break;
                }
            case "health":
                {
                    SetAnimation(ModelActions.ActionType.Health);
                    break;
                }
            case "feed":
                {
                    SetAnimation(ModelActions.ActionType.Feed);
                    break;
                }
        }
    }
}
