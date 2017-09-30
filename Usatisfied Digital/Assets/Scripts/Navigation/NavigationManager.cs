using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public GameObject[] panels;

    private void Start()
    {
        ClearPanels();
        TogglePanel(0);
    }

    public void TogglePanel(int panel)
    {
        ClearPanels();
        panels[panel].SetActive(true);
    }

    private void ClearPanels()
    {
        int i = panels.Length;
        for (int x = 0; x< i; x++)
        {
            panels[x].SetActive(false);
        }
    }
}
