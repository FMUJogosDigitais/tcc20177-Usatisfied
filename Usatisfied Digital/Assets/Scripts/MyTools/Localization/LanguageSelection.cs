using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Utils.Localization
{
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
            LanguageChangeButton lbuton = btn.GetComponent<LanguageChangeButton>();
            if (lbuton)
            {
                lbuton.languages = lng;
                //Debug.Log(LocalizationManager.GetLanguageName(lng));
                lbuton.SetInitialText(LocalizationManager.GetLanguageName(lng));
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
