using System.Collections;
using System.Collections.Generic;
using Utils.Localization;
using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(LanguageText))]
public class TextAnimated : MonoBehaviour
{

    Text myText;
    LanguageText myLanguageText;
    public float speedyText = .2f;

    public delegate void CallBack(bool finsih);
    private void OnEnable()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        myText = GetComponent<Text>();
        myLanguageText = GetComponent<LanguageText>();
    }

    private void Start()
    {
        myText.text = "";
    }
    public void SetMessage(string message, CallBack callBack)
    {
        myLanguageText.ChangeInitialReference(message);
        string newmessage = LocalizationManager.GetText(message);
        StartCoroutine(AnimateText(newmessage, callBack));
    }
    IEnumerator AnimateText(string strComplete, CallBack callBack)
    {
        int i = 0;
        myText.text = "";
        while (i < strComplete.Length)
        {
            myText.text += strComplete[i++];
            yield return new WaitForSeconds(speedyText);
        }
        callBack(true);
    }
}
