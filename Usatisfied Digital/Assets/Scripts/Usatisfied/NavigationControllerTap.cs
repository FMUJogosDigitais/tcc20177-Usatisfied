using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usatisfied.Navigation;
namespace Usatisfied.Navigation {
    public class NavigationControllerTap : MonoBehaviour {

        public void TapToEditTimeLine()
        {
            if (GameManagerTimeline.GetInstance().CountDaylist() <= 0)
            {
                GameManager.ToggleStartGame();
                NavigationManager.GetInstance().ToggleEditTimeline();
            }
            
        }
        public void TapToIndex()
        {
            GameManager.ToggleStartGame();
            NavigationManager.GetInstance().ToggleIndex();
        }
    }
}
