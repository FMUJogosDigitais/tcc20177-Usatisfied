using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{

    // Use this for initialization
    public Transform content;
    public static bool inEdit = false;
    private void OnEnable()
    {
        inEdit = false;
        ListActionInTimeLine(content);
        if (GameManagerTimeline.GetInstance().CountDaylist() > 0)
        {
            StartActionsDay();
        }
    }
    void Start()
    {

        if (GameManagerTimeline.GetInstance().CountDaylist() > 0)
        {
            inEdit = false;
            ListActionInTimeLine(content);
        }
        else
        {
            NavigationManager.GetInstance().ToggleEditTimeline();
        }
    }

    protected void ListActionInTimeLine(Transform cont)
    {
        GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
        
        int i = gmtl.CountDaylist();
        int child = cont.childCount;
        for (int x = child; x < i; x++)
        {
            GameObject go = Instantiate<GameObject>(gmtl.buttonAction, cont);
            go.GetComponent<TimeLineButtons>().daylistRef = x;
            if (inEdit)
            {
                go.transform.SetAsFirstSibling();
            }
            else
            {
                go.transform.SetAsLastSibling();
            }
            
        }
    }

    private void StartActionsDay()
    {
        Debug.Log("rodando");
        int listSize = GameManagerTimeline.GetInstance().CountDaylist();
        StartCoroutine(ProgressAction());
    }

    IEnumerator ProgressAction()
    {
        int x = 0;
        do
        {
            Debug.Log("Começou "+ GameManagerTimeline.GetInstance().timeProgressInSec);
            GameManagerTimeline.GetInstance().RemoveActionInList(0);
            Debug.Log(content.GetChild(x).name);
            Transform child = content.GetChild(x);
            //child.gameObject.GetComponent<TimeLineButtons>().ProgressBar(GameManagerTimeline.GetInstance().timeProgressInSec, 1);
            yield return new WaitForSeconds(2);
            child.gameObject.SetActive(false);
            Debug.Log("Removeu: " + GameManagerTimeline.GetInstance().CountDaylist());
            x++;
        } while (GameManagerTimeline.GetInstance().CountDaylist() > 0);
    }
}
