using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
    public class EmotionalResController : IDontDestroy<EmotionalResController>
    {

        public static float emotionalAcumulado = 0;
        public float estEmotional;              

        public void DealWithResValue(float resValue, float sleepValue)
        {
            float res = resValue;
            float sleep = sleepValue;

            res *= CheckStressPun();
            emotionalAcumulado += res;
            
            if (emotionalAcumulado >= 100)
            {
                estEmotional += emotionalAcumulado - 100;
                CheckStress();

                if (!StressManager.emocionalEstressado)
                {
                    SatisfactionManager.satisfacaoEmocional++;
                }

                emotionalAcumulado = 0;
            }

            else if (emotionalAcumulado < 100)
            {
                estEmotional -= sleep;
            }

            if (estEmotional < 0) { estEmotional = 0f; }

            SatisfactionManager.GetInstance().CallEventUpdateResiliences();

        }

        private float CheckStressPun()
        {

            float estResPunicao = 1f;

            if (StressManager.fisicoEstressado)
            { estResPunicao -= 0.1f; }

            if (StressManager.mentalEstressado)
            { estResPunicao -= 0.1f; }

            if (StressManager.socialEstressado)
            { estResPunicao -= 0.1f; }

            if (StressManager.emocionalEstressado)
            { estResPunicao -= 0.5f; }

            return estResPunicao;
        }

        private void CheckStress()
        {
            if (estEmotional >= 100)
            {
                estEmotional = 100;
                StressManager.emocionalEstressado = true;
            }
            else if (StressManager.emocionalEstressado)
            {
                if (estEmotional <= BasePontuacao.GetInstance().stressRecover)
                {
                    StressManager.emocionalEstressado = false;
                } 
            }
        }

    }
}
