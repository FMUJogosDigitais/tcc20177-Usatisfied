using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usatisfied;
using UnityEngine.UI;

public class SocialController_btn : MonoBehaviour {
    // Use this for initialization
    private Image resBar;

    private void OnEnable()
    {
        //SatisfactionManager.GetInstance().EventUpdateResiliences += UpdateThisRes;
    }
    void Start()
    {
        resBar = GetComponent<Image>();
        //TODO: Buscar o valor salvo (Playerpreference)
        UpdateThisRes();
    }

    private void Update()
    {
        UpdateThisRes();
    }

    public void UpdateThisRes()
    {
        resBar.fillAmount = SocialResController.socialAcumulado / 100;
    }

    private void OnDisable()
    {
        //SatisfactionManager.GetInstance().EventUpdateResiliences -= UpdateThisRes;
    }
}
