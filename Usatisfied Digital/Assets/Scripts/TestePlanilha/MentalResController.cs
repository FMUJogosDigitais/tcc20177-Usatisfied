using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
    public class MentalResController : IDontDestroy<MentalResController>
    {

        public static float mentalAcumulado = 0;
        public float estMental;              

        public void DealWithResValue(float resValue, float sleepValue)
        {
            float res = resValue;
            float sleep = sleepValue;

            res *= CheckStressPun();
            mentalAcumulado += res;
            
            if (mentalAcumulado >= 100)
            {
                estMental += mentalAcumulado - 100;
                CheckStress();

                if (!StressManager.mentalEstressado)
                {
                    SatisfactionManager.satisfacaoMental++;
                }

                mentalAcumulado = 0;
            }

            else if (mentalAcumulado < 100)
            {
                estMental -= sleep;
            }

            if (estMental < 0) { estMental = 0f; }

            SatisfactionManager.GetInstance().CallEventUpdateResiliences();

        }

        private float CheckStressPun()
        {

            float estResPunicao = 1f;

            if (StressManager.fisicoEstressado)
            { estResPunicao -= 0.1f; }

            if (StressManager.mentalEstressado)
            { estResPunicao -= 0.5f; }

            if (StressManager.socialEstressado)
            { estResPunicao -= 0.1f; }

            if (StressManager.emocionalEstressado)
            { estResPunicao -= 0.1f; }

            return estResPunicao;
        }

        private void CheckStress()
        {
            if (estMental >= 100)
            {
                estMental = 100;
                StressManager.mentalEstressado = true;
            }
            else if (StressManager.mentalEstressado)
            {
                if (estMental <= BasePontuacao.GetInstance().stressRecover)
                {
                    StressManager.mentalEstressado = false;
                } 
            }
        }

    }
}
