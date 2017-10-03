using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSortingLayers : MonoBehaviour {

    public string sortingLayer;
    public int orderLayer;

    private void Awake()
    {
        SetSortingLayer();
    }

    [ContextMenu("Update sorting Layer")]
    void UpdateSortingLayerSettings()
    {
        SetSortingLayer();
    }

     private void SetSortingLayer()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.sortingLayerName = sortingLayer;
        rend.sortingOrder = orderLayer;
    }
}
