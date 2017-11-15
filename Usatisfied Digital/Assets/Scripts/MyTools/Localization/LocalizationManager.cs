using System.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Utils.Localization
{
    /// <summary>
    /// Opera a tradução dos componentes
    /// TODO: Testar o plural como array!
    /// </summary>
    public class LocalizationManager : IDontDestroy<LocalizationManager>
    {
        public enum Languages { en_US, pt_BR, es_ES }
        public static Languages defaultLang = Languages.en_US;
        public static Languages language = Languages.en_US;

        private static Languages[] avariableLanguages;
        private static string resourceLanguagesFiles = "Languages/";
        private static string resourceFlagsFiles = "Sprites/Flags/";

        private enum ReadPhase { None, Singular, Plural, TSingular, TPlural }
        private static Dictionary<string, string[]> textTable;
        public bool IsInitialized
        {
            get { return initialized; }
        }
        private static int nplural = 0;
        private static string fplural = "";
        private bool initialized = false;

        public delegate void ChangeLanguageEventHandler(LocalizationManager thisLanguage);
        public event ChangeLanguageEventHandler OnChangeLanguage;

        public override void Awake()
        {
            base.Awake();
            avariableLanguages = GetAvailableLanguages();
            language = (LanguagePlayerPref.IsSetPlayerLanguage()) ? LanguagePlayerPref.GetSetPlayerLanguage() : defaultLang;
        }
        public void ChangeLanguage(Languages lng)
        {
            language = lng;
            LoadFile(lng);
            // dispara todos os listens para a troca de texto;
            if (IsInitialized && OnChangeLanguage != null)
            {
                OnChangeLanguage(this);
            }
        }
        public static void SetNewLanguage(Languages lng)
        {
            language = lng;
            GetInstance().OnChangeLanguage(GetInstance());
#if !UNITY_EDITOR
                LanguagePlayerPref.SetPlayerLanguage(lng);
#endif
        }
        public static string GetText(string key)
        {
            string result = key;
            //Debug.Log(result);
            //Debug.Log(language);
            if (key != null && textTable != null)
            {
                if (textTable.ContainsKey(key))
                {
                    result = (string)textTable[key][0];

                    //Debug.Log("Key: "+ key);
                }
            }
            return (result.Trim() != "") ? (string)result : key;
        }
        public static string GetText(string key, string[] plural, float val)
        {
            //Debug.Log(key);
            string result = "0";
            int aplural = PluralForm(val, fplural);
            //Debug.Log(aplural);
            if (aplural > 0 && plural.Length > aplural)
            {
                result = plural[aplural];
                if (textTable != null)
                {
                    ;
                    if (textTable.ContainsKey(plural[aplural]))
                    {
                        //Debug.Log((string)textTable[plural[aplural]][aplural]);
                        result = (string)textTable[plural[aplural]][aplural];
                    }
                }
            }
            else
            {
                //Debug.Log(key);
                result = GetText(key);
            }

            return (result.Trim() != "") ? (string)result : key;
        }

        public static Languages[] GetAvailableLanguages()
        {
            if (avariableLanguages == null)
            {
                List<Languages> listAvariableLang = new List<Languages>();
                Languages selectedLanguage;
#if UNITY_ANDROID || UNITY_IOS

                TextAsset[] languageFiles = Resources.LoadAll<TextAsset>(resourceLanguagesFiles);
                int ilang = languageFiles.Length;
                for (int i = 0; i < ilang; i++)
                {
                    if (EnumParse.TryParseEnum<Languages>(Path.GetFileNameWithoutExtension(languageFiles[i].name), out selectedLanguage))
                    {
                        listAvariableLang.Add(selectedLanguage);
                    }
                }
                listAvariableLang.Sort();
                avariableLanguages = listAvariableLang.ToArray();
#else
                string[] languageFiles = Directory.GetFiles(GetPoPath());
                int ilang = languageFiles.Length;
                for (int i = 0; i < ilang; i++)
                {
                    if (Path.GetExtension(languageFiles[i]) != ".po")
                        continue;
                    if (EnumParse.TryParseEnum<Languages>(Path.GetFileNameWithoutExtension(languageFiles[i]), out selectedLanguage))
                    {
                        listAvariableLang.Add(selectedLanguage);
                    }
                }
                listAvariableLang.Sort();
                avariableLanguages = listAvariableLang.ToArray();
#endif
            }
            return avariableLanguages;
        }
        public static Sprite GetLanguageFlag(Languages lng)
        {
            string newlng = lng.ToString().Replace("_", "-");
            //Debug.Log(newlng);
            CultureInfo newCulture = new CultureInfo(newlng);
            //Debug.Log(newCulture.DisplayName);
            newlng = (newCulture.Name.Length > 3) ? newCulture.Name.Substring(newCulture.Name.Length - 2) : newCulture.Name;
            newlng.ToLower();
            //Debug.Log(resourceFlagsFiles + newlng);
            return Resources.Load<Sprite>(resourceFlagsFiles + newlng);
        }
        public static string GetLanguageName(Languages lng)
        {
            string newlng = lng.ToString().Replace("_", "-");
            CultureInfo newCulture = new CultureInfo(newlng);
            return newCulture.EnglishName;
        }
        private static string GetPoPath()
        {
            string fullpath = resourceLanguagesFiles;
            return Path.Combine(Application.streamingAssetsPath, fullpath);
        }
        private string GetPoFile(Languages lang)
        {
            string file = lang.ToString().Replace("-", "_");
            string fullpath = resourceLanguagesFiles + file + ".po";
            return Path.Combine(Application.streamingAssetsPath, fullpath);
        }

        //#if UNITY_ANDROID || UNITY_EDITOR
        //        string fullpath = resourceLanguagesFiles + file + ".po";
        //        TextAsset textAsset = Resources.Load<TextAsset>(resourceLanguagesFiles + "pt_BR.po");
        //        Debug.Log(textAsset);
        #region NOT CHANGE
        private void LoadFile(Languages lang)
        {
            ReadPhase phase = ReadPhase.None;
            if (lang == defaultLang)
            {
                //if (textTable != null)
                textTable = new Dictionary<string, string[]>();
                textTable.Clear();
                language = defaultLang;
                phase = ReadPhase.None;
                OnChangeLanguage(this);
                initialized = true;
                return;
            }
            if (Array.IndexOf<Languages>(avariableLanguages, lang) >= 0 && lang != defaultLang)
            {
                //possui o arquivo, e não é a linguagem padrão então TRADUZIR
                initialized = false;// INDICA que não completou a tradução
#if UNITY_ANDROID || UNITY_IOS
                string file = lang.ToString().Replace("-", "_") + ".po";
                string textAsset = Resources.Load<TextAsset>(resourceLanguagesFiles + file).text;
#else
                string fullpath = GetPoFile(lang);
                string textAsset = File.ReadAllText(fullpath, System.Text.Encoding.UTF8);
#endif

                //Debug.Log(fullpath);

                //Debug.Log(textAsset);
                if (textAsset != null)
                {
                    //Debug.LogWarning("[LanguageManager] loading: " + fullpath);
                    if (textTable == null)
                    {
                        textTable = new Dictionary<string, string[]>();
                    }
                    textTable.Clear();
                    StringReader reader = new StringReader(textAsset);
                    string key = null;
                    string plural = null;
                    string val = null;
                    int number = 0;
                    string[] valp = new string[1];
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //Debug.Log("Linha: " + line);
                        if (line.StartsWith("\"Plural-Forms: nplurals="))
                        {
                            nplural = System.Int32.Parse(line.Substring(line.IndexOf("nplurals=") + 9, line.IndexOf(";") - (line.IndexOf("nplurals=") + 9)));
                            fplural = line.Substring(line.IndexOf("plural=") + 7, line.IndexOf(";", line.IndexOf("plural=")) - (line.IndexOf("plural=") + 7));

                        }
                        else if (line.StartsWith("\""))
                        {
                            // é continuação do texto
                            //Debug.Log("Continuação: " + line);
                            if (phase != ReadPhase.None)
                                switch (phase)
                                {
                                    case ReadPhase.Singular:
                                        key += line.Substring(1, line.Length - 2);
                                        break;
                                    case ReadPhase.Plural:
                                        plural += line.Substring(1, line.Length - 2);
                                        break;
                                    case ReadPhase.TSingular:
                                        val += line.Substring(1, line.Length - 2);
                                        break;
                                    case ReadPhase.TPlural:
                                        valp[number] += line.Substring(1, line.Length - 2);
                                        break;
                                    default:
                                        phase = ReadPhase.None;
                                        break;
                                }
                        }
                        else if (line.StartsWith("msgid \""))
                        {
                            phase = ReadPhase.Singular;
                            key = line.Substring(7, line.Length - 8);
                            //Debug.Log("Key: "+key);
                        }
                        else if (line.StartsWith("msgid_plural \""))
                        {
                            phase = ReadPhase.Plural;
                            plural = line.Substring(14, line.Length - 15);
                            //Debug.Log("Achei o plural: "+plural);
                        }
                        else if (line.StartsWith("msgstr \""))
                        {
                            phase = ReadPhase.TSingular;
                            val = line.Substring(8, line.Length - 9);
                            //Debug.Log("Valor: "+val);
                        }
                        else if (line.StartsWith("msgstr["))
                        {
                            phase = ReadPhase.TPlural;
                            string sub = line.Substring(7, 1);
                            if (System.Int32.TryParse(sub.Trim(), out number))
                            {
                                if (valp == null)
                                {
                                    //Debug.Log("Valp é nulo: "+ nplural);
                                    valp = new string[nplural];
                                    //Debug.Log("guarda no numero: " + number);
                                    valp[number] = line.Substring(line.IndexOf("\"") + 1, line.Length - (line.IndexOf("\"") + 2));
                                    // Debug.Log("No numero: " + number + " tem o valor: "+ valp[number]);
                                }
                                else
                                {
                                    // Debug.Log("Valp NAO nulo");
                                    valp[number] = line.Substring(line.IndexOf("\"") + 1, line.Length - (line.IndexOf("\"") + 2));
                                    // Debug.Log("No numero: " + number + " tem o valor: " + valp[number]);
                                }
                            }
                        }
                        else
                        {
                            AddLocation(key, val, plural, valp);
                            plural = key = val = null;
                            valp = null;
                        }
                    }
                    AddLocation(key, val, plural, valp);
                    reader.Close();
                }
                else
                {
                    Debug.LogWarning("[LanguageManager] " + lang.ToString() + " file not found.");
                    return;
                }
                initialized = true;
            }

        }
        private void AddLocation(string key, string val = null, string plural = null, string[] valp = null)
        {
            //Debug.Log("Key: " + key + " Value: "+ val);
            if (key != null && val != null)
            {
                if (key != "" && !textTable.ContainsKey(key))
                {
                    string[] myval = new string[1];
                    myval[0] = val;
                    textTable.Add(key, myval);
                    //Debug.Log("Key: " + key + " Value: " + val);
                }
            }

            if (key != null && plural != null && valp.Length > 1)
            {
                if (plural != "" && !textTable.ContainsKey(plural))
                {
                    string[] myval = new string[1];
                    myval[0] = valp[0];
                    textTable.Add(key, myval);
                    textTable.Add(plural, valp);
                }
            }
        }
        private static int PluralForm(float n, string form)
        {
            int plural = 0;
            switch (form)
            {
                case "0":
                    return 0;
                case "(n > 1)":
                    return plural = (n > 1) ? 1 : 0;
                case "(n != 1)":
                    return plural = (n != 1) ? 1 : 0;
                case "(n==0 ? 0 : n==1 ? 1 : n==2 ? 2 : n%100>=3 && n%100<=10 ? 3 : n%100>=11 ? 4 : 5)":
                    return plural = (n == 0) ? 0 : (n == 1) ? 1 : (n == 2) ? 2 : (n % 100 >= 3 && n % 100 <= 10) ? 3 : (n % 100 >= 11) ? 4 : 5;
                case "(n%10==1 && n%100!=11 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1) ? 0 : (n>=2 && n<=4) ? 1 : 2":
                    return plural = (n == 1) ? 0 : (n >= 2 && n <= 4) ? 1 : 2;
                case "(n==1) ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2":
                    return plural = (n == 1) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1) ? 0 : (n==2) ? 1 : (n != 8 && n != 11) ? 2 : 3":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n != 8 && n != 11) ? 2 : 3;
                case "n==1 ? 0 : n==2 ? 1 : (n>2 && n<7) ? 2 :(n>6 && n<11) ? 3 : 4":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n > 2 && n < 7) ? 2 : (n > 6 && n < 11) ? 3 : 4;
                case "(n==1 || n==11) ? 0 : (n==2 || n==12) ? 1 : (n > 2 && n < 20) ? 2 : 3":
                    return plural = (n == 1 || n == 11) ? 0 : (n == 2 || n == 12) ? 1 : (n > 2 && n < 20) ? 2 : 3;
                case "(n%10!=1 || n%100==11)":
                    return plural = (n % 10 != 1 || n % 100 == 11) ? 1 : 0;
                case "(n==1) ? 0 : (n==2) ? 1 : (n == 3) ? 2 : 3":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n == 3) ? 2 : 3;
                case "(n%10==1 && n%100!=11 ? 0 : n%10>=2 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n%10==1 && n%100!=11 ? 0 : n != 0 ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n != 0) ? 1 : 2;
                case "n%10==1 && n%100!=11 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "n==1 || n%10==1 ? 0 : 1":
                    return plural = (n == 1 || n % 10 == 1) ? 0 : 1;
                case "(n==0 ? 0 : n==1 ? 1 : 2)":
                    return plural = (n == 0) ? 0 : (n == 1) ? 1 : 2;
                case "(n==1 ? 0 : n==0 || ( n%100>1 && n%100<11) ? 1 : (n%100>10 && n%100<20 ) ? 2 : 3)":
                    return plural = (n == 1) ? 0 : (n == 0 || (n % 100 > 1 && n % 100 < 11)) ? 1 : (n % 100 > 10 && n % 100 < 20) ? 2 : 3;
                case "(n==1 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n == 1) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1 ? 0 : (n==0 || (n%100 > 0 && n%100 < 20)) ? 1 : 2)":
                    return plural = (n == 1) ? 0 : (n == 0 || (n % 100 > 0 && n % 100 < 20)) ? 1 : 2;
                case "(n%100==1 ? 0 : n%100==2 ? 1 : n%100==3 || n%100==4 ? 2 : 3)":
                    return plural = (n % 100 == 1) ? 0 : (n % 100 == 2) ? 1 : (n % 100 == 3 || n % 100 == 4) ? 2 : 3;
                default:
                    return plural;
            }
        }
#endregion
    }
}
