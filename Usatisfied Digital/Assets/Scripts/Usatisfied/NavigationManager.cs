using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : IDontDestroy<NavigationManager>
{
    public GameObject[] panels;
    public static bool tapOn = true;
    /// <summary>
    /// 0 - index
    /// 1 - presets
    /// 2 - edit timeline
    /// 3 - liderboards
    /// </summary>
    ///

    private void Start()
    {
        ToggleIndex();
    }

    private void ClearPanels()
    {
        int i = panels.Length;
        for (int x = 0; x < i; x++)
        {
            panels[x].SetActive(false);
        }
    }

    void TogglePanel(int idde)
    {
        panels[idde].SetActive(true);
    }

    void TogglePanel(int[] iddes)
    {
        int i = iddes.Length;
        for (int x = 0; x < i; x++)
        {
            panels[x].SetActive(true);
        }
    }

    public void ToggleIndex()
    {
        ClearPanels();
        TogglePanel(0);
    }
    public void ToggleEditTimeline()
    {
        ClearPanels();
        TogglePanel(1);
        TogglePanel(2);
    }
    public void ToggleLiderBoard()
    {
        ClearPanels();
        TogglePanel(3);
        GameManagerLeaderBoard.GetInstance().PopulateList(GameManagerResilience.GetInstance().TotalSatisfaction);
    }
}
