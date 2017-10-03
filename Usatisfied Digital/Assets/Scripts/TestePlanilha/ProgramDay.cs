using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Usatisfied;

namespace Usatisfied
{
	
public class ProgramDay : MonoBehaviour {

    public List<ModelActions> listActions;
    public GameObject botao;

    private void Start()
    {
        foreach (ModelActions action in ManagerActions.GetInstance().actions) 
		{
        	GameObject go = Instantiate<GameObject>(botao, transform);
        	go.name = action.name;
        	go.GetComponent<Button>().onClick.AddListener(()=> AddActionToList(action));
        	go.GetComponentInChildren<Text>().text = action.name;
        }
    }

    public void AddActionToList(ModelActions template)
    {
        listActions.Add(new ModelActions(template));
    }

    public void Totalduration()
    {
        float total = listActions.Sum(item => item.duration);
        Debug.Log(total);
        //Checagem de retorno do valor.....
    }
    
	public void TotalSumResiliencesDay()
    {

        // Tratamento dormir
        float totalDormir = 24 - listActions.Sum(item => item.duration);
        float restValue = totalDormir * BasePontuacao.GetInstance().sleepPhour;
        Debug.Log("dormir: " + totalDormir);        


        // Tratamento da resiliencia "Fisico"
        float totalfisico = listActions.Sum(item=> item.physic);
        Debug.Log("Fisico: "+ totalfisico);
        PhysicsResController.GetInstance().DealWithResValue(totalfisico, restValue);        


		// Tratamento da resiliencia "Mental"
		float totalMental = listActions.Sum(item => item.mental);
        Debug.Log("Mental: " + totalMental);
        MentalResController.GetInstance().DealWithResValue(totalMental, restValue);
        

        // Tratamento da resiliencia "Emocional"
        float totalEmocional = listActions.Sum(item => item.emotional);
        Debug.Log("Emocional: " + totalEmocional);
        EmotionalResController.GetInstance().DealWithResValue(totalEmocional, restValue);
    

        // Tratamento da resiliencia "Social"
        float totalSocial = listActions.Sum(item => item.social);
        Debug.Log("Social: " + totalSocial);
        SocialResController.GetInstance().DealWithResValue(totalSocial, restValue);


    }
 }
		
}

