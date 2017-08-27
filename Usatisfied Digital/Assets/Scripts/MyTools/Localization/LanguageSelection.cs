using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace MyTools.Localization
{
    [RequireComponent(typeof(Canvas))]
    public class LanguageSelection : MonoBehaviour
    {

        public bool startConfiguration;
        public Transform myContent;
        public GameObject myLanguageButton;

        private LoadSceneManager loadSceneManager;

        private void Start()
        {
            SetInitialReferences();
            LoadSelectLanguage();
        }
        private void SetInitialReferences()
        {
            loadSceneManager = FindObjectOfType<LoadSceneManager>();
        }
        private void LoadSelectLanguage()
        {
            LocalizationManager.Languages[] allLang = LocalizationManager.GetAvailableLanguages();
            SetButton(myLanguageButton, LocalizationManager.defaultLang);

            for (int i = 0; i < allLang.Length; i++)
            {
                SetButton(myLanguageButton, allLang[i]);
            }
        }
        private void SetButton(GameObject go, LocalizationManager.Languages lng)
        {
            GameObject btn = Instantiate<GameObject>(go, myContent);
            btn.name = lng.ToString();
            btn.GetComponentInChildren<Text>().text = LocalizationManager.GetLanguageName(lng);
            btn.GetComponent<Image>().sprite = LocalizationManager.GetLanguageFlag(lng);
            LanguageChangeButton lbuton = btn.GetComponent<LanguageChangeButton>();
            if (lbuton)
            {
                lbuton.languages = lng;
                lbuton.SetInitialText(btn.GetComponentInChildren<Text>().text);
            }
        }
        public void SetNewLanguage()
        {
            LocalizationManager.SetNewLanguage(LocalizationManager.language);
            if (loadSceneManager != null && startConfiguration == true)
            {
                loadSceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }

        }
    }
}
