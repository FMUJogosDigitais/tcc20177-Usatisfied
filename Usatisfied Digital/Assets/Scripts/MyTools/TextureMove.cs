using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMove : MonoBehaviour {
    public bool _x = true;
    public bool _y;
    public float scrollSpeed = 0.5F;
    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
        float offsetX = 0;
        float offsetY = 0;
        if (_x == true)
        {
            offsetX = Time.time * scrollSpeed;
        }
        if(_y == true)
        {
            offsetY = Time.time * scrollSpeed;
        }
        //float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
       // rend.sortingOrder = 4;
    }
}
