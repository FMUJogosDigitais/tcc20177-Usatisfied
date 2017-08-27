using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Namespace:      Tools
/// Class:          ScreenSize
/// Description:    Reconhece o tamanho da tela.
/// Author:         Renato Innocenti                    Date: 05/20/2017
/// Notes:          no Attach;
/// Revision History:
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0
/// </summary>
///
public static class ScreenSize
{
    /// <summary>
    /// Retorna um Vetor2 com a altura e largura da tela em Unity points;
    /// </summary>
    public static Vector2 CamSize
    {
        get
        {
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            return new Vector2(width, height);
        }
    }
}
