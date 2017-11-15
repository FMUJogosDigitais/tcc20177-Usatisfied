using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPref : IDontDestroy<SetPlayerPref> {

    const string PLAYERTUTORIAL = "playerTutorial";

    public Toggle toggleTutorial;

    public static void SetPlayerTutorial(bool tutorial)
    {
        PlayerPrefsX.SetBool(PLAYERTUTORIAL, tutorial);
    }
    public static bool GetPlayerTutorial()
    {
        return PlayerPrefsX.GetBool(PLAYERTUTORIAL);
    }

    public static void ResetTutorial()
    {
        PlayerPrefs.DeleteKey(PLAYERTUTORIAL);
    }


    private void OnEnable()
    {
        toggleTutorial.isOn = PlayerPrefsX.GetBool(PLAYERTUTORIAL);
    }

    public void ButtonToggle()
    {
        PlayerPrefsX.SetBool(PLAYERTUTORIAL, toggleTutorial.isOn);
    }
}
