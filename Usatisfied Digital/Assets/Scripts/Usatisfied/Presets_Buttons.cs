using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils.Localization;

public class Presets_Buttons : UIScrollAndDrag
{
    public int presetID;
    [SerializeField]
    private Text namePreset;
    [SerializeField]
    private Image iconPreset;
    //public ModelActions myaction;

    private void OnEnable()
    {
        SetupButton(presetID);
    }

    public void SetupButton(int idde)
    {
        ModelActions action = GameManager.GetInstance().GetTemplates(idde);
        name = action.name;
        namePreset.GetComponent<LanguageText>().ChangeInitialReference(action.name);
        namePreset.text = LocalizationManager.GetText(action.name);
        iconPreset.sprite = action.icon;
    }

    public override void BeginDrag(PointerEventData eventData)
    {

        base.BeginDrag(eventData);
        draggedItem = this;
        //TODO: Fazer o som de saida e alertar o painel que esta acontecendo um drag

    }

    protected override void CreateDragIcon(string iconname)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        icon = Instantiate<GameObject>(this.gameObject, canvas.transform, true);
        //Destroy(icon.GetComponent<Presets_Buttons>());
        CanvasGroup canvasgroup = icon.AddComponent<CanvasGroup>();
        canvasgroup.blocksRaycasts = false;
        canvasgroup.interactable = false;
        canvasgroup.interactable = true;
        icon.transform.SetAsLastSibling();
        icon.GetComponent<Image>().raycastTarget = false;
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        iconRect.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y);
    }
}
