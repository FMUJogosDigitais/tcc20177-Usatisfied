using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Localization;
namespace Usatisfied
{

    public class ActionsManager_Presets : MonoBehaviour
    {
        public GameObject buttonPreset;
        private List<TActions> actionsPreset;

        private void OnEnable()
        {
            Init();
            GetActionsPresets();
        }
        private void Init()
        {
            actionsPreset = GameManager.GetInstance().actionsPreset;
        }
        public void GetActionsPresets()
        {
            DestroyList();
            TActions.ActionsType gameManager_actualAction = GameManager.GetInstance().actualAction;
            int n = actionsPreset.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (actionsPreset[i].actionType == gameManager_actualAction)
                    {
                        GameObject gb = Instantiate<GameObject>(buttonPreset, this.transform) as GameObject;
                        ActionsManager_PresetsButtons actionManagerButton = gb.GetComponent<ActionsManager_PresetsButtons>();
                        actionManagerButton.SetupButton(actionsPreset[i]);
                    }
                }
            }
        }
        private void OnDisable()
        {
            DestroyList();
        }

        private void DestroyList()
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
