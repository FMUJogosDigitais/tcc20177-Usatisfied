using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{
    public class SocialResController : IDontDestroy<SocialResController>
    {

        public static float socialAcumulado = 0;
        public float estsocial;              

        public void DealWithResValue(float resValue, float sleepValue)
        {
            float res = resValue;
            float sleep = sleepValue;

            res *= CheckStressPun();
            socialAcumulado += res;
            
            if (socialAcumulado >= 100)
            {
                estsocial += socialAcumulado - 100;
                CheckStress();

                if (!StressManager.socialEstressado)
                {
                    SatisfactionManager.satisfacaoSocial++;
                }

                socialAcumulado = 0;
            }

            else if (socialAcumulado < 100)
            {
                estsocial -= sleep;
            }

            if (estsocial < 0) { estsocial = 0f; }

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
            { estResPunicao -= 0.5f; }

            if (StressManager.emocionalEstressado)
            { estResPunicao -= 0.1f; }

            return estResPunicao;
        }

        private void CheckStress()
        {
            if (estsocial >= 100)
            {
                estsocial = 100;
                StressManager.socialEstressado = true;
            }
            else if (StressManager.socialEstressado)
            {
                if (estsocial <= BasePontuacao.GetInstance().stressRecover)
                {
                    StressManager.socialEstressado = false;
                } 
            }
        }

    }
}
