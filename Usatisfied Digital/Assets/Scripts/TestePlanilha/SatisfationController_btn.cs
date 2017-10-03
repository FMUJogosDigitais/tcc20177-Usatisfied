using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Usatisfied;

public class SatisfationController_btn : MonoBehaviour {

    // Use this for initialization
    private Text totalSatisfation;

    private void OnEnable()
    {
       // SatisfactionManager.GetInstance().EventUpdateResiliences += UpdateSatisfation;
    }
    void Start () {
        totalSatisfation = GetComponentInChildren<Text>();
        //TODO: Buscar o valor salvo (Playerpreference)
        UpdateSatisfation();
    }

    private void Update()
    {
        UpdateSatisfation();
    }

    public void UpdateSatisfation()
    {
        totalSatisfation.text = SatisfactionManager.SomaSatisfacao().ToString();
    }

    private void OnDisable()
    {
       // SatisfactionManager.GetInstance().EventUpdateResiliences -= UpdateSatisfation;
    }
}
