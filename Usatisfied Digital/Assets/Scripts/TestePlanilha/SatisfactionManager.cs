using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
	
public class SatisfactionManager : MonoBehaviour {

	public int satisfacaoTotal;
	public int satisfacaoFisico;
	public int satisfacaoMental;
	public int satisfacaoSocial;
	public int satisfacaoEmocional;

	public float resFisicoAc;
	public float resMentalAc;
	public float resEmocionalAc;
	public float resSocialAc;
	
	public float estRecover = 80f;
	public float estFisico;
		public bool fisicoEstressado = false;
	public float estMental;
		public bool mentalEstressado = false;
	public float estSocial;
		public bool socialEstressado = false;
	public float estEmocional;
		public bool emocionalEstressado = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SomaSatisfacao()
	{
		satisfacaoTotal = satisfacaoFisico + satisfacaoMental + satisfacaoSocial + satisfacaoEmocional;
	}

		public void TratamentoDeResiliencia(GameManager.Resiliences resName, float res){

		float resValue = res;

		switch (resName) 
		{
			case GameManager.Resiliences.Phisycs:
				resFisicoAc += resValue; 
				if (resFisicoAc >= 100) {
					if (!fisicoEstressado) {
						satisfacaoFisico++;
					}
					estFisico += resFisicoAc - 100;
					resFisicoAc = 0;
				}
			break;

			case GameManager.Resiliences.Mental:
				resMentalAc += resValue; 
				if (resMentalAc >= 100) {
					estMental += resMentalAc - 100;
					resMentalAc = 0;
					satisfacaoMental++;
				}
			break;

			case GameManager.Resiliences.Social:
				resSocialAc += resValue; 
				if (resSocialAc >= 100) {
					estSocial += resSocialAc - 100;
					resSocialAc = 0;
					satisfacaoSocial++;
				}
			break;

			case GameManager.Resiliences.Emotional:
				resEmocionalAc += resValue; 
				if (resEmocionalAc >= 100) {
					estEmocional += resEmocionalAc - 100;
					resEmocionalAc = 0;
					satisfacaoEmocional++;
				}
			break;
		}

		SomaSatisfacao ();
		CheckStressRes ();
	}

		public void CheckStressRes(){

			// Checagam da resiliencia "Fisico"
			if (estFisico >= 100) {
				estFisico = 100;
				fisicoEstressado = true;
			} else if (fisicoEstressado) {
				if (estFisico <= estRecover) {
					fisicoEstressado = false;
				}
			}

			// Checagam da resiliencia "Mental"
			if (estMental >= 100) {
				estMental = 100;
				mentalEstressado = true;
			} else if (mentalEstressado) {
				if (estMental <= estRecover) {
					mentalEstressado = false;
				}
			}

			// Checagam da resiliencia "Social"
			if (estSocial >= 100) {
				estSocial = 100;
				socialEstressado = true;
			} else if (socialEstressado) {
				if (estSocial <= estRecover) {
					socialEstressado = false;
				}
			}

			// Checagam da resiliencia "Emocional"
			if (estEmocional >= 100) {
				estEmocional = 100;
				emocionalEstressado = true;
			} else if (emocionalEstressado) {
				if (estEmocional <= estRecover) {
					emocionalEstressado = false;
				}
			}
		}
}
}