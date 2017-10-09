using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeLineControllerEdit : TimeLineController, IDropHandler
{
    public Transform contentEdit;

    protected override void OnEnable()
    {
        inEdit = true;
        StopAllCoroutines();
        SetInitialReferences();
        //gmtl.EventAddActionInList += CreateList;
        ClearContent(contentEdit);
        CreateList(contentEdit);
        //gmtl.SetTextDayTime();
    }

    public void OnDrop(PointerEventData eventData)
    {
        inEdit = true;
        Presets_Buttons preset = eventData.pointerDrag.GetComponent<Presets_Buttons>();
        if (preset && GameManagerTimeline.maxHour - gmtl.DayDuration > 0)
        {
            ModelActions action = new ModelActions(GameManager.GetInstance().GetTemplates(preset.presetID));
            action.duration = gmtl.GetDuration(action.duration); // corrige o valor de acordo com o salto;
            gmtl.DayDuration = gmtl.GetListActionInDay().Sum(x => x.duration) + action.duration;
            //Debug.Log(alltime);
            if (GameManagerTimeline.maxHour - gmtl.DayDuration > 0)
            {
                gmtl.AddActionInList(action);
                AddActionInParent(contentEdit);
                //gmtl.CallEventAddActionInList(contentEdit);
            }
        }
    }

    private void AddActionInParent(Transform cont)
    {
        int i = gmtl.CountDaylist();
        int childs = cont.childCount;
        GameObject go = Instantiate<GameObject>(gmtl.buttonAction, cont);
        go.GetComponent<TimeLineButtons>().RefrashAddList(childs);
        go.transform.SetAsFirstSibling();
    }
    protected override void OnDisable()
    {
        inEdit = false;
        //gmtl.EventAddActionInList -= CreateList;
    }
}