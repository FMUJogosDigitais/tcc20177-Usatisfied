using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.Localization
{
    public class LanguagePlayerPref : IDontDestroy<LanguagePlayerPref>
    {

        const string PLAYERLANGUAGE = "playerLanguage";

        public static void SetPlayerLanguage(LocalizationManager.Languages lang)
        {
            PlayerPrefs.SetString(PLAYERLANGUAGE, lang.ToString());
        }
        public static string GetPlayerLanguage()
        {
            return PlayerPrefs.GetString(PLAYERLANGUAGE);
        }
        public static LocalizationManager.Languages GetSetPlayerLanguage()
        {
            string l = PlayerPrefs.GetString(PLAYERLANGUAGE);
            LocalizationManager.Languages lang;
            if (EnumParse.TryParseEnum<LocalizationManager.Languages>(l, out lang))
            {
                return lang;
            }
            else
            {
                return LocalizationManager.defaultLang;
            }
        }
        public static bool IsSetPlayerLanguage()
        {
            return (PlayerPrefs.GetString(PLAYERLANGUAGE) != "") ? true : false;
        }

        public static void ResetPlayerLanguage()
        {
            PlayerPrefs.DeleteKey(PLAYERLANGUAGE);
        }
    }
}
