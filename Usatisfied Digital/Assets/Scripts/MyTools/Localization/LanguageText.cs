using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyTools.Localization
{
    public class LanguageText : MonoBehaviour
    {

        public string deafultPlural;
        public float qntPlural;
        private string textLocalize;
        private LocalizationManager localizationManager;

        private void OnEnable()
        {
            SetInitialReferences();
            localizationManager.OnChangeLanguage += OnChangeLanguage;
        }
        private void OnDisable()
        {
            localizationManager.OnChangeLanguage -= OnChangeLanguage;
        }
        private void SetInitialReferences()
        {
            localizationManager = FindObjectOfType<LocalizationManager>();
            textLocalize = GetComponent<Text>().text;
        }
        // Use this for initialization

        private void Start()
        {
            OnChangeLanguage(localizationManager);
        }
        void OnChangeLanguage(LocalizationManager lang)
        {
            if (deafultPlural != null && deafultPlural != "") {
                GetComponent<Text>().text = LocalizationManager.GetText(textLocalize);
            }
            else
            {
                GetComponent<Text>().text = LocalizationManager.GetText(textLocalize, deafultPlural, qntPlural);
            }
            
        }
    }
}
