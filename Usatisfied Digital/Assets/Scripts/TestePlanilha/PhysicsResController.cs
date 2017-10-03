using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
    public class PhysicsResController : IDontDestroy<PhysicsResController>
    {

        public static float fisicoAcumulado = 0;
        public float estFisico;              

        public void DealWithResValue(float resValue, float sleepValue)
        {
            float res = resValue;
            float sleep = sleepValue;

            res *= CheckStressPun();
            fisicoAcumulado += res;
            
            if (fisicoAcumulado >= 100)
            {
                estFisico += fisicoAcumulado - 100;
                CheckStress();

                if (!StressManager.fisicoEstressado)
                {
                    SatisfactionManager.satisfacaoFisico++;
                }

                fisicoAcumulado = 0;
            }

            else if (fisicoAcumulado < 100)
            {
                estFisico -= sleep;
            }

            if (estFisico < 0) { estFisico = 0f; }

            SatisfactionManager.GetInstance().CallEventUpdateResiliences();

        }

        private float CheckStressPun()
        {

            float estFisicoPunicao = 1f;

            if (StressManager.fisicoEstressado)
            { estFisicoPunicao -= 0.5f; }

            if (StressManager.mentalEstressado)
            { estFisicoPunicao -= 0.1f; }

            if (StressManager.socialEstressado)
            { estFisicoPunicao -= 0.1f; }

            if (StressManager.emocionalEstressado)
            { estFisicoPunicao -= 0.1f; }

            return estFisicoPunicao;
        }

        private void CheckStress()
        {
            if (estFisico >= 100)
            {
                estFisico = 100;
                StressManager.fisicoEstressado = true;
            }
            else if (StressManager.fisicoEstressado)
            {
                if (estFisico <= BasePontuacao.GetInstance().stressRecover)
                {
                    StressManager.fisicoEstressado = false;
                } 
            }
        }

    }
}
