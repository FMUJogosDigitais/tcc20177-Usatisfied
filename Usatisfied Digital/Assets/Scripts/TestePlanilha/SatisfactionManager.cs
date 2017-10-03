using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
	
public class SatisfactionManager : IDontDestroy<SatisfactionManager> {

	public static int satisfacaoTotal;
	public static int satisfacaoFisico;
	public static int satisfacaoMental;
	public static int satisfacaoSocial;
	public static int satisfacaoEmocional;

	public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventUpdateResiliences;
    
    public void CallEventUpdateResiliences()
    {
        if (EventUpdateResiliences != null)
        {
            EventUpdateResiliences();
        }
    }

    public static int SomaSatisfacao()
	{
	    return satisfacaoTotal = satisfacaoFisico + satisfacaoMental + satisfacaoSocial + satisfacaoEmocional;
	}

       

    public void TratamentoDeResiliencia(GameManager.Resiliences resName, float res){

		    SomaSatisfacao ();
            CallEventUpdateResiliences();
        }


    }
  


}