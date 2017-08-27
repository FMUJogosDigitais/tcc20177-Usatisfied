using UnityEngine;
using System.Collections;
/// <summary>
/// Namespace:      Tools/Singleton
/// Class:          IDontDestroy
/// Description:    Singleton for instance wicth don't allow destroy
/// Author:         Renato Innocenti                    Date: 05/20/2017
/// Notes:          Not Attacht, only prevents gameobjects duplicated
/// Revision History:
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0
/// </summary>
/// 
public class IDontDestroy<Instance> : MonoBehaviour where Instance : IDontDestroy<Instance>
{
    private static Instance instance;

    [Header("Don't Destroy On Load")]
    public bool isPersistant = true;

    public virtual void Awake()
    {
        if (isPersistant)
        {
            if (!instance)
            {
                instance = this as Instance;
            }
            else
            {
                DestroyObject(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance = this as Instance;
        }
    }
    public static Instance GetInstance()
    {
        return instance;
    }
}
