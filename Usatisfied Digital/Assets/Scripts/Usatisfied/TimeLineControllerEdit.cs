using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeLineControllerEdit : TimeLineController, IDropHandler
{
    public Transform contentEdit;
    private void OnEnable()
    {
        inEdit = true;
        ListActionInTimeLine(contentEdit);
    }
    public void OnDrop(PointerEventData eventData)
    {
        inEdit = true;
        Presets_Buttons preset = eventData.pointerDrag.GetComponent<Presets_Buttons>();
        GameManagerTimeline gmtl = GameManagerTimeline.GetInstance();
        if (preset && gmtl.dayDuration > 0)
        {
            //Debug.Log(preset.presetID);
            ModelActions action = new ModelActions(GameManager.GetInstance().GetTemplates(preset.presetID));
            GameManagerTimeline.GetInstance().AddActionInList(action);
            ListActionInTimeLine(contentEdit);
        }
    }

    void Start()
    {
        int i = GameManagerTimeline.GetInstance().CountDaylist();
        if (i > 0)
        {
            inEdit = true;
            ListActionInTimeLine(contentEdit);
        }
    }

    private void OnDisable()
    {
        inEdit = false;
    }
}
