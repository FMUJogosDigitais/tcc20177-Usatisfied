using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usatisfied;
using UnityEngine.UI;

public class PhisycsController_btn : MonoBehaviour {

    // Use this for initialization
    private Image phisycsResBar;

    private void OnEnable()
    {
        //SatisfactionManager.GetInstance().EventUpdateResiliences += UpdatePhisycsRes;
    }
    void Start()
    {
        phisycsResBar = GetComponent<Image>();
        //TODO: Buscar o valor salvo (Playerpreference)
        UpdatePhisycsRes();
    }

    private void Update()
    {
        UpdatePhisycsRes();
    }

    public void UpdatePhisycsRes()
    {
        phisycsResBar.fillAmount = PhysicsResController.fisicoAcumulado / 100;
    }

    private void OnDisable()
    {
        //SatisfactionManager.GetInstance().EventUpdateResiliences -= UpdatePhisycsRes;
    }
}
