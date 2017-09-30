using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Localization;

namespace Usatisfied {
    public class ActionsManager_StartButtons : MonoBehaviour {

        // Use this for initialization
        public TActions.ActionsType actionType;

        void Start() {
            gameObject.name = LocalizationManager.GetText(actionType.ToString());
            transform.GetChild(0).GetComponentInChildren<Image>().sprite = ActionsManager.GetInstance().actionIcones[(int)actionType];
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(() => GameManager.GetInstance().actualAction = actionType);
            btn.onClick.AddListener(() => NavigationManager.GetInstance().NavToPresetDisplay());
            // Enviar o tipo para construir a lista.
            
        }

    }
}