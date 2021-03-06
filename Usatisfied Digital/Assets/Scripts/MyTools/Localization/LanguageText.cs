﻿using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Utils.Localization
{
    public class LanguageText : MonoBehaviour
    {
        public bool isTextAnimation = false;
        public enum FormatOutput { None, LowerCase, Uppercase, FirstUpper}
        public FormatOutput formatOutput;
        public string[] deafultPlural;
        public float qntPlural =1;
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
            
        }
        // Use this for initialization

        private void Start()
        {
            textLocalize = GetComponent<Text>().text;
            OnChangeLanguage(localizationManager);
        }

        public void ChangeInitialReference(string text)
        {
            textLocalize = text;
            OnChangeLanguage(localizationManager);
        }
        void OnChangeLanguage(LocalizationManager lang)
        {
            //Debug.Log(LocalizationManager.GetText(textLocalize, deafultPlural, qntPlural));
            if (!isTextAnimation)
            {
                GetComponent<Text>().text = LocalizationManager.GetText(textLocalize, deafultPlural, qntPlural);

                switch (formatOutput)
                {
                    case FormatOutput.FirstUpper:
                        GetComponent<Text>().text = GetComponent<Text>().text.First().ToString().ToUpper() + GetComponent<Text>().text.Substring(1);
                        break;
                    case FormatOutput.LowerCase:
                        GetComponent<Text>().text = GetComponent<Text>().text.ToLower();
                        break;
                    case FormatOutput.Uppercase:
                        GetComponent<Text>().text = GetComponent<Text>().text.ToUpper();
                        break;
                }
            }
        }
    }
}
