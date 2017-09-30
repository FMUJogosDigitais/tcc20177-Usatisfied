using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Usatisfied
{

    public class NavigationManager : IDontDestroy<NavigationManager>
    {

        public GameObject actionsPanel;
        public GameObject timeLinePanel;
        public GameObject actionSetupPanel;
        public GameObject presetDisplay;

        void ToogleActionSetupPanel() {
            actionSetupPanel.SetActive(!actionSetupPanel.activeSelf);
        }
        void ToogleActionSetupPanel(bool toogle)
        {
            actionSetupPanel.SetActive(toogle);
        }

        void TooglePresetDisplay()
        {
            presetDisplay.SetActive(!presetDisplay.activeSelf);
        }
        void TooglePresetDisplay(bool toogle)
        {
            presetDisplay.SetActive(toogle);
        }

        void ToogleTimeLinePanel()
        {
            timeLinePanel.SetActive(!timeLinePanel.activeSelf);
        }
        void ToogleTimeLinePanel(bool toogle)
        {
            timeLinePanel.SetActive(toogle);
        }
        void ToogleActionPanel()
        {
            actionsPanel.SetActive(!actionsPanel.activeSelf);
        }
        void ToogleActionPanel(bool toogle)
        {
            actionsPanel.SetActive(toogle);
        }

        // Botões das ações
        public void NavToActionPanel()
        {
            ToogleActionSetupPanel(false);
            TooglePresetDisplay(false);
            ToogleTimeLinePanel(false);
            ToogleActionPanel(true);
        }
        public void NavToTimeLine()
        {
            ToogleActionSetupPanel(false);
            TooglePresetDisplay(false);
            ToogleTimeLinePanel(true);
            ToogleActionPanel(false);
        }
        public void NavToPresetDisplay()
        {
            ToogleActionSetupPanel(true);
            TooglePresetDisplay(true);
            ToogleTimeLinePanel(false);
            ToogleActionPanel(false);
        }
        public void NavToActionSetupPanel()
        {
            ToogleActionSetupPanel(true);
            TooglePresetDisplay(true);
            ToogleTimeLinePanel(false);
            ToogleActionPanel(false);
        }
    }
}