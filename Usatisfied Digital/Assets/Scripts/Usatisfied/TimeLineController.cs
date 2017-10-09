using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{

    // Use this for initialization
    [Range(0,1f)]
    public float timeProgressBar;
    public Transform content;
    public static bool inEdit = false;
    protected GameManagerTimeline gmtl;
    public Text totalDayText;
    private int totalDay = 0;
    public int TotalDay {
        get { return totalDay; }
        set { totalDay = value;
            totalDayText.text = totalDay.ToString();
        }
    }

    protected virtual void OnEnable()
    {
        inEdit = false;
        SetInitialReferences();
        if (gmtl.CountDaylist() <= 0)
        {
            NavigationManager.GetInstance().ToggleEditTimeline();
        }
        else
        {
            CreateList(content);
            StartCoroutine(DayCycle());
        }
    }

    private void Start()
    {
        TotalDay = 0;
    }
    protected void SetInitialReferences()
    {
        gmtl = GameManagerTimeline.GetInstance();
    }
    protected void ClearContent(Transform cont)
    {
        StopAllCoroutines();
        int i = cont.childCount;
        for (int x = 0; x < i; x++)
        {
            Destroy(cont.GetChild(x).gameObject);
        }
    }
    protected void CreateList(Transform cont)
    {
        if (gmtl.CountDaylist() > 0)
        {
            ClearContent(cont);
            int i = gmtl.CountDaylist();
            float totalDay = 0;
            for (int x = 0; x < i; x++)
            {
                GameObject go = Instantiate<GameObject>(gmtl.buttonAction, cont);
                go.GetComponent<TimeLineButtons>().RefrashAddList(x);
                if (inEdit)
                    go.transform.SetAsFirstSibling();
                else
                    go.transform.SetAsLastSibling();
                totalDay += gmtl.GetListActionInDay(x).duration;
            }

            gmtl.SetTextDayTime(totalDay);
        }
    }

    private void FinishAction(TimeLineButtons button)
    {
        button.SetResiliencesFinal();
    }

    protected virtual void OnDisable()
    {
        inEdit = true;
        StopAllCoroutines();
        StopCoroutine(DayCycle());
        StopCoroutine(DayCycle());
    }

    IEnumerator DayCycle()
    {
        int numberchild = content.childCount;
        
        while (numberchild > 0)
        {
            if (inEdit == false)
            {
                TimeLineButtons firstChild = content.GetChild(0).GetComponent<TimeLineButtons>();
                if (firstChild)
                {
                    float minuteDuration = firstChild.myaction.duration * timeProgressBar;
                    ModelActions.ActionType type = firstChild.myaction.actionType;
                    AnimationManager.GetInstance().SetAnimation(type);
                    //TODO: Pausar a barra, apra reiniciar quando for para a edição.
                    StartCoroutine(firstChild.BarProgress(minuteDuration));
                    //Debug.Log(firstChild.daylistRef);
                    yield return new WaitForSeconds(minuteDuration);
                    Destroy(content.GetChild(0).gameObject);
                    GameManagerTimeline.GetInstance().RemoveActionInList(0);
                    FinishAction(firstChild);
                }
                //TODO: Colocar o numero de vezes que a ação foi usada
                numberchild -= 1;
            }
            yield return null;
        }
        TotalDay += 1;
        gmtl.DayDuration = 0;
    }
}
