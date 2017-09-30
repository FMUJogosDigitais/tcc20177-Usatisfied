using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Localization;

namespace Usatisfied
{
	public class ActionManager_Display : MonoBehaviour {

		// Objetos de display

		// identificação
		public Image actionIcon;
		public Text actionLabel;
		public Text presetName;
        public Text newActionName;

        // Resiliencias
        public Image emocionalResilience;
		public Image physicResilience;
		public Image socialResilience;
		public Image mentalResilience;

        // Horas
        public Text txtStartHour;
        public Text txtFinishHour;
        public Slider inicialTime;
		public Slider finalTime;

		// lista de ações
		private List<TActions> actionsPreset;
        private TActions ActualDisplayAction;

        private ActionsManager_Presets actionManagerPresets;

		void InitialReferences()
		{
			actionsPreset = GameManager.GetInstance ().actionsPreset;
            actionManagerPresets = FindObjectOfType<ActionsManager_Presets>();

        }

		// Use this for initialization
		void OnEnable () 
		{
			InitialReferences ();
			GetActionInfo ();
		}
        private void Start()
        {
            InitialReferences();
        }


        void GetActionInfo()
		{
			int n = actionsPreset.Count;
			if (n > 0) 
			{
				for (int i = 0; i<= n; i++)
				{
					if (actionsPreset[i].actionType == GameManager.GetInstance().actualAction)
					{
						UpdateDisplay (actionsPreset [i]);
						break;
					}
				}
			}
		}

		public void GetActionInfo(TActions action)
		{
			UpdateDisplay (action);
		}

		void UpdateDisplay(TActions action)
		{
            // Identificação da ação
            ActualDisplayAction = new TActions(action);
			actionIcon.sprite = action.GetIcone(action.actionType);
			actionLabel.text = action.actionType.ToString ();
			presetName.text = action.name;

			// atualiza o display das resiliencias
			emocionalResilience.fillAmount = action.actionEmotional;
			physicResilience.fillAmount = action.actionPhisycs;
			socialResilience.fillAmount = action.actionSocial;
			mentalResilience.fillAmount = action.actionMental;

			// atualiza hora inicial e final
			inicialTime.value = action.actionStart;
			finalTime.value = action.actionFinish;
		}

        public void SetActionsDisplay()
        {
            // Identificação da ação
            ActualDisplayAction.name = newActionName.text;
            // atualiza o display das resiliencias
            //ActualDisplayAction.actionEmotional = emocionalResilience.fillAmount;
            //ActualDisplayAction.actionPhisycs = physicResilience.fillAmount;
            //ActualDisplayAction.actionSocial = socialResilience.fillAmount;
            //ActualDisplayAction.actionMental = mentalResilience.fillAmount;

            // atualiza hora inicial e final
            ActualDisplayAction.actionStart = inicialTime.value;
            ActualDisplayAction.actionFinish = finalTime.value;
            if (newActionName.text != null && newActionName.text != "") {
                actionsPreset.Add(ActualDisplayAction);
                actionManagerPresets.GetActionsPresets();
                UpdateDisplay(ActualDisplayAction);
            }
            else
            {
                Debug.Log("Fazer um popup de e pedir para preencher com um novo nome");
            }
            
        }

        public void ChangeStartNumber()
        {
            float finalvalue = finalTime.value;
            txtStartHour.text = inicialTime.value.ToString();
            if (finalvalue < inicialTime.value)
            {
                finalTime.value = finalvalue = inicialTime.value;
            }

        }
        public void ChangeFinishNumber()
        {
            float finalvalue = finalTime.value;
            if (finalvalue < inicialTime.value)
            {
                finalTime.value = finalvalue = inicialTime.value;
            }

            txtFinishHour.text = finalvalue.ToString();
        }
    }
}


