using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResiliencesManager : MonoBehaviour
{
    public bool isStress = false;
    public Image resilienceImage;
    public Image stressImage;
    public GameManager.Resiliences resiliences;
    [SerializeField]
    int maxresilience = 100;

    [Range(0, 100)]
    [SerializeField]
    int totalResilience = 0;
    public int TotalResilience
    {
        get { return totalResilience; }
        set
        {
            totalResilience += SetTotalResilience(value);
            resilienceImage.fillAmount = (float)totalResilience / 100;
            Debug.Log(resiliences.ToString() + " Gerou: " + totalResilience);
            if (totalResilience > maxresilience)
            {
                GameManagerResilience.GetInstance().TotalSatisfaction = SetTotalSatisfaction(totalResilience);
                TotalStress = totalResilience;
                //Debug.Log("RETORNO "+ SetTotalSatisfaction(totalResilience));
                resilienceImage.fillAmount = totalResilience = 0;
            }
        }
    }

    [SerializeField]
    int totalStress = 0;
    public int TotalStress
    {
        get { return totalStress; }
        set
        {
            Debug.Log(resiliences.ToString() + " Passou Stress: " + value);
            totalStress += SetTotalStress(value);
            Debug.Log(resiliences.ToString() + " Gerou Stress: " + totalStress);
            if (totalStress > maxresilience)
            {
                isStress = true;
                StressActions();
                Debug.Log(resiliences.ToString() + " Gerou Stress Extressando: " + totalStress);
                totalStress = maxresilience;
            }
            //Debug.Log(totalStress);
            stressImage.fillAmount = (float)totalStress / 100;
        }
    }

    private GameManagerResilience gmr;

    private void OnEnable()
    {
        SetInitialReferences();
        gmr.EventUpdateResiliences += ChangeResiliences;
    }
    void SetInitialReferences()
    {
        gmr = GameManagerResilience.GetInstance();
    }

    private int SetTotalResilience(int val)
    {

        int sub = (int)val / 100;
        return (sub < 1) ? val % 100 : val;
    }

    private int SetTotalStress(int val)
    {
        if (val > 100)
        {
            int sub = val - 100;
            return SetTotalResilience(sub);
        }
        else
        {
            return 0;
        }
    }

    private int SetTotalSatisfaction(int val)
    {
        if (!isStress)
        {
            GameManagerResilience.GetInstance().GainSatisfation();
            return 1;
        }
        else
            return 0;
    }

    // Use this for initialization
    void Start()
    {
        totalResilience = 0;
        totalStress = 0;
        resilienceImage.fillAmount = 0;
        stressImage.fillAmount = 0;
    }

    void StressActions()
    {
        //TODO: Colocar animaçõe e efeitos que indicam que a resiliencia estressou;
    }

    private void ChangeResiliences(GameManager.Resiliences res, int totalval)
    {
        if (res == resiliences)
        {
            TotalResilience = totalval;
        }
    }
    private void OnDisable()
    {
        gmr.EventUpdateResiliences -= ChangeResiliences;
    }

}
