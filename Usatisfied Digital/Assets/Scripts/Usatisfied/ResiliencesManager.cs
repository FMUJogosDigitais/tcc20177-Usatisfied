using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResiliencesManager : MonoBehaviour
{
    public bool isStress = false;
    public Image resilienceImage;
    public Image stressImage;
    public Image iconeResilience;
    public Image imgReceip;
    public GameManager.Resiliences resiliences;
    public Color stressedColor;
    Color normalColor;
    [SerializeField]
    int maxresilience = 100;
    [SerializeField]
    int stressOut = 80;
    [Range(0, 1f)]
    [SerializeField] float reduceStressed = 0;

    [Range(0, 1f)]
    [SerializeField]
    public float reduceGainStressed = .5f;
    [Range(0, 1f)]
    [SerializeField]
    float reducePartialGainStressed = .1f;

    [Range(0, 100)]
    [SerializeField]
    int totalResilience = 0;
    public int TotalResilience
    {
        get { return totalResilience; }
        set
        {
            totalResilience += SetTotalResilience(value);
            if (totalResilience < 0)
            {
                TotalStress = totalResilience * -1;
                totalResilience = 0;
            }
            ChangefillBar(resilienceImage, (float)totalResilience / 100);
            //Debug.Log(resiliences.ToString() + " Gerou: " + totalResilience);
            if (totalResilience >= maxresilience)
            {
                GameManagerResilience.GetInstance().TotalSatisfaction = SetTotalSatisfaction(totalResilience);
                TotalStress = totalResilience;
                //Debug.Log("RETORNO "+ SetTotalSatisfaction(totalResilience));
                totalResilience = 0;
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
            //Debug.Log(resiliences.ToString() + " Passou Stress: " + value);
            totalStress += SetTotalStress(value);
            if (totalStress < 0)
            {
                isStress = false;
                StressActions();
                totalStress = 0;
            }
            //Debug.Log(resiliences.ToString() + " Gerou Stress: " + totalStress);
            if (totalStress > maxresilience)
            {
                isStress = true;
                StressActions();
                //Debug.Log(resiliences.ToString() + " Gerou Stress Extressando: " + totalStress);
                totalStress = maxresilience;
            }
           // Debug.Log(totalStress);
            ChangefillBar(stressImage, (float)totalStress / 100, false);
        }
    }

    private GameManagerResilience gmr;

    private void Awake()
    {
        normalColor = iconeResilience.color;
    }

    private void OnEnable()
    {
        SetInitialReferences();
        gmr.EventUpdateResiliences += ChangeResiliences;
        gmr.EventRecoveryStress += RecoveryStress;
    }
    void SetInitialReferences()
    {
        gmr = GameManagerResilience.GetInstance();
        
    }

    private int SetTotalResilience(int val)
    {
        float subval = (1f - reduceStressed) * (float)val;
        val = Mathf.RoundToInt(subval);
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
            switch (resiliences)
            {
                case GameManager.Resiliences.Emotional:
                    GameManagerResilience.emotionaltimes += 1;
                    break;
                case GameManager.Resiliences.Mental:
                    GameManagerResilience.mentalTimes += 1;
                    break;
                case GameManager.Resiliences.Phisycs:
                    GameManagerResilience.physicsTimes += 1;
                    break;
                case GameManager.Resiliences.Social:
                    GameManagerResilience.socialTimes += 1;
                    break;

            }
            gmr.GainSatisfation();
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
        gmr.SetResilienceStressed(resiliences, true);
        iconeResilience.color = stressedColor;
        reduceStressed = 0;
        if (this.isStress == true)
        {
            reduceStressed = reduceGainStressed;
        }
        switch (resiliences)
        {
            case GameManager.Resiliences.Phisycs:
                reduceStressed += (gmr.mentalStressed) ?reducePartialGainStressed:0;
                reduceStressed += (gmr.emotionalStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.socialStressed) ? reducePartialGainStressed : 0;
                break;
            case GameManager.Resiliences.Mental:
                reduceStressed += (gmr.physicsStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.emotionalStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.socialStressed) ? reducePartialGainStressed : 0;
                break;
            case GameManager.Resiliences.Emotional:
                reduceStressed += (gmr.mentalStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.physicsStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.socialStressed) ? reducePartialGainStressed : 0;
                break;
            case GameManager.Resiliences.Social:
                reduceStressed += (gmr.mentalStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.physicsStressed) ? reducePartialGainStressed : 0;
                reduceStressed += (gmr.emotionalStressed) ? reducePartialGainStressed : 0;
                break;
        }
        //TODO: Colocar animaçõe e efeitos que indicam que a resiliencia estressou;

    }

    private void ChangeResiliences(GameManager.Resiliences res, int totalval)
    {
        if (res == resiliences)
        {
            TotalResilience = totalval;
        }
    }

    private void RecoveryStress(int recovery)
    {
        if (totalStress > 0)
        {
            totalStress -= recovery;
            totalStress = (totalStress < 0) ? 0 : totalStress;
            stressImage.fillAmount = (float)totalStress / 100;
            if (totalStress <= stressOut)
            {
                iconeResilience.color = normalColor;
                isStress = false;
            }
        }
    }
    private void OnDisable()
    {
        gmr.EventUpdateResiliences -= ChangeResiliences;
        gmr.EventRecoveryStress -= RecoveryStress;
    }

    private void ChangefillBar(Image fill, float total, bool reset = true)
    {
        StartCoroutine(BarProgressResili(fill, total, reset));

    }

    IEnumerator BarProgressResili(Image fill, float total, bool reset)
    {
        float i = 0;
        float rate = 1 / total;
        float start = fill.fillAmount;
        while (i < 1)
        {
            i += Time.deltaTime * rate;

            fill.fillAmount = Mathf.Lerp(start, total, i);
            yield return null;
        }
        if (fill.fillAmount >= 1 && reset)
        {
            fill.fillAmount = 0;
        }
        else if(fill.fillAmount >= 1 && !reset)
        {
            fill.fillAmount = 1;
        }
    }

}
