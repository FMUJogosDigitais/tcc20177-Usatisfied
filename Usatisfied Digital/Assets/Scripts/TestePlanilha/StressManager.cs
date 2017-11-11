using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressManager : IDontDestroy<StressManager>
{
    //resiliencias estressadas
    public static bool fisicoEstressado = false;
    public static bool mentalEstressado = false;
    public static bool socialEstressado = false;
    public static bool emocionalEstressado = false;

    // Ações Estressadas
    public static bool alimentacaoEstressada = false;
    public static bool esporteEstressada = false;

    // Ações de desafio
    public static bool isVizinho = false;
    public static bool isTransito = false;
    public static bool isPreguica = false;
    public static bool isChuva = false;

}
