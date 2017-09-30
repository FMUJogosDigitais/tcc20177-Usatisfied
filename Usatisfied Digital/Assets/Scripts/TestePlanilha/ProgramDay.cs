using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        int total = listActions.Sum(item => item.duration);
        Debug.Log(total);
        //Checagem de retorno do valor.....
    }
    
	public void TotalSumResiliencesDay()
    {
		SatisfactionManager sm = GetComponent<SatisfactionManager> ();
		
		// Tratamento da resiliencia "Fisico"
		float totalfisico = listActions.Sum(item=> item.fisicoOnDay);
        Debug.Log("Fisico: "+ totalfisico);
		sm.TratamentoDeResiliencia (GameManager.Resiliences.Phisycs, totalfisico);


		// Tratamento da resiliencia "Mental"
		float totalMental = listActions.Sum(item => item.mentalOnDay);
        Debug.Log("Mental: " + totalMental);
		sm.TratamentoDeResiliencia (GameManager.Resiliences.Mental, totalMental);

		// Tratamento da resiliencia "Emocional"
		float totalEmocional = listActions.Sum(item => item.emocionalOnDay);
        Debug.Log("Emocional: " + totalEmocional);
		sm.TratamentoDeResiliencia (GameManager.Resiliences.Emotional, totalEmocional);
		
		// Tratamento da resiliencia "Social"
		float totalSocial = listActions.Sum(item => item.socialOnDay);
        Debug.Log("Social: " + totalSocial);
		// Checa se gerou satisfação
		sm.TratamentoDeResiliencia (GameManager.Resiliences.Social, totalSocial);

	}
 }
		
}

