using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usatisfied.Navigation;
namespace Usatisfied.Navigation {
    public class NavigationControllerTap : MonoBehaviour {

        public void TapToEditTimeLine()
        {
            NavigationManager.GetInstance().ToggleEditTimeline();
            GameManager.TogglePauseGame();
        }
        public void TapToIndex()
        {
            NavigationManager.GetInstance().ToggleIndex();
            GameManager.TogglePauseGame();
        }
    }
}
