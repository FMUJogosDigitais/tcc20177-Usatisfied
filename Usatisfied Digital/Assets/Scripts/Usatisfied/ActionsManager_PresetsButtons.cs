using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Localization;

namespace Usatisfied {
    public class ActionsManager_PresetsButtons : MonoBehaviour {
        ActionManager_Display actionDisplay;
        public void SetupButton(TActions act)
        {
            TActions action = new TActions(act);
            Button btn = GetComponent<Button>();
            gameObject.name = action.name;
            transform.GetChild(0).GetComponentInChildren<Image>().sprite = action.actionIcons;
            GetComponentInChildren<Text>().text = LocalizationManager.GetText(action.name);
            actionDisplay = FindObjectOfType<ActionManager_Display>();
            btn.onClick.AddListener(() => actionDisplay.GetActionInfo(action));
        }

    }
}