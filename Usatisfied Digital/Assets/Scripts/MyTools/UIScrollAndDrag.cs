using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// Namespace:      Tools
/// Class:          UIScrollAndDrag
/// Description:    Behaviour of Drag and Scroll
/// 
/// Author:         Renato Innocenti                    Date: 05/20/2017
/// Notes:          not attach - inheritance new class
/// Revision History:
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0
/// Name: Renato Innocenti           Date:05/23/2017        Description: v1.0.1 - add Render mode Camera and Overlap
/// </summary>
/// 

public class UIScrollAndDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    /// <summary>
    /// VARIAVEIS PUBLICAS
    /// </summary>
    [Header("UI Scroll And Drag")]
    public bool cameraSpace = true;
    [HideInInspector]
    public GameObject icon;
    [HideInInspector]
    public UIScrollAndDrag draggedItem;
    /// <summary>
    /// vARIAVEIS PRIVADAS
    /// </summary>
    private float mouseOffsetX = -2;
    private float mouseOffsetY = 2;
    /// <summary>
    /// Controles para diferenciar o scroll do drag
    /// </summary>
    private ScrollRect parentScrollRect;
    private bool otherScroll;
    private bool scrollHorizontal;
    /// <summary>
    /// INICIALIZA O PAINEL QUE PODE SER DRAG E SCROLL
    /// </summary>
    private void Awake()
    {
        // verifica qual é o scroll pai do botão e coloca ele na variavel parentScrollRect
        if (!parentScrollRect)
        {
            parentScrollRect = GetComponentInParent<ScrollRect>();
            if (parentScrollRect)
                scrollHorizontal = parentScrollRect.horizontal; // se for true o scroll é horizontal se for false o scroll é vertical NAO FUNCIONA EM AMBOS
        }
    }
    /// <summary>
    /// METODO INICIA QUANDO UM DRAG TEM INICIO
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        float horizontal = Mathf.Abs(eventData.position.x - eventData.pressPosition.x);
        float vertical = Mathf.Abs(eventData.position.y - eventData.pressPosition.y);
        if (scrollHorizontal)
        {
            //Scroll é Horizontal
            if (horizontal > vertical)
            {
                //Se estou movendo na horizontal ativo o scroll
                OnScrollStart(eventData);
                return;
            }
            //Metodos ativados só quando for horizontal o Scroll
        }
        else
        {
            //Scroll é na vertical
            if (vertical > horizontal)
            {
                // Se estou movendo na vertical ativo o scroll
                OnScrollStart(eventData);
                return;
            }
            // Metodos ativados só quando for Vertical;
        }
        // Se não é Scroll Use a ação BeginDrag deste botão;
        CreateDragIcon(this.name);
        BeginDrag(eventData);
    }
    /// <summary>
    /// QUANDO UM DRAG EFETIVAMENTE ESTA ACONTECENDO
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (otherScroll && draggedItem == null)
        {
            // NÃO ESTOU DRAGANDO UM ITEM VALIDO
            parentScrollRect.OnDrag(eventData);
            parentScrollRect.SendMessage("OnDrag", eventData);
            return;
        }
        if (icon != null && draggedItem)
        {
            Canvas mycanvas = GetComponentInParent<Canvas>();
            // estou draggando um item VALIDO
            if (mycanvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                Vector3 screenPoint = Input.mousePosition;
                screenPoint.z = 10.0f; //distance of the plane from the camera
                icon.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            }
            else
            {
                Vector3 mouseP = Input.mousePosition;
                icon.transform.position = new Vector3(mouseP.x + mouseOffsetX, mouseP.y + mouseOffsetY);
            }


            Drag(eventData);
        }
    }
    /// <summary>
    /// QUANDO UM DRAG SE ENCERRA
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // se for um fim de drag ATIVADO PELO SCROLL
        if (otherScroll && draggedItem == null)
        {
            otherScroll = false;
            parentScrollRect.OnEndDrag(eventData);
            parentScrollRect.SendMessage("OnEndDrag", eventData);
            return;
        }
        // É UM DRAG VALIDO
        if (icon != null && draggedItem)
        {
            EndDrag(eventData);
        }
    }
    /// <summary>
    /// QUANDO UM SCROLL ESTA EFETIVAMENTE ATIVADO
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public virtual void OnScroll(PointerEventData eventData)
    {
        ((IScrollHandler)parentScrollRect).OnScroll(eventData);
        otherScroll = true;
        draggedItem = null;
    }
    /// <summary>
    /// FUNÇÃO EXTENDIDA DO ONSCROLL
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public virtual void OnScrollStart(PointerEventData eventData)
    {
        otherScroll = true;
        parentScrollRect.OnBeginDrag(eventData);
        parentScrollRect.SendMessage("OnBeginDrag", eventData);
        draggedItem = null;
    }
    /// <summary>
    /// FUNÇÃO EXTENDIDA DO ONBEGINDRAG
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void BeginDrag(PointerEventData eventData)
    {
        otherScroll = false;
        //draggedItem = this;
    }
    /// <summary>
    /// FUNÇÃO EXTENIDA DO ONDRAG
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public virtual void Drag(PointerEventData eventData)
    {

    }
    /// <summary>
    /// FUNÇÃO EXTENDIDA DO ONENDDRAG
    /// </summary>
    /// <param name="eventData">Paramentros do EventSystem</param>
    public virtual void EndDrag(PointerEventData eventData)
    {
        if (icon != null)
        {
            Destroy(icon);
        }
        otherScroll = true;
        draggedItem = null;
    }
    /// <summary>
    /// FUNÇÃO DE AUXILIO CRIANDO UM ICONE DO OBEJETO DURANTE O DRAG JUNTO AO MOUSE
    /// </summary>
    /// <param name="iconname">Paramentros do EventSystem</param>
    protected virtual void CreateDragIcon(string iconname)
    {
        icon = new GameObject("icon_" + iconname);
        Image image = icon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.preserveAspect = GetComponent<Image>().preserveAspect;
        image.raycastTarget = false;
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        //CONFIGURA O TAMANHO DO ICONE
        iconRect.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 2,
                                         GetComponent<RectTransform>().sizeDelta.y / 2);
        Canvas canvas = GetComponentInParent<Canvas>();
        // PEGA O CANVAS E COLOCA O OBJETO NELE
        if (canvas != null)
        {
            // PARA PODER EXIBIR NA FRENTE DO GUI PRECISA SER O ULTIMO FILHO
            icon.transform.SetParent(canvas.transform, true);
            icon.transform.SetAsLastSibling();
        }
    }
    /// <summary>
    /// FORÇA AS UIS A RECALCULAR SEUS FORMATOS
    /// </summary>
    /// <param name="allroots">ARRAY COM OS RECTTRANSFORM A SEREM RECONSTRUIDOS</param>
    private void RebuildUI(RectTransform[] allroots)
    {
        foreach (RectTransform rec in allroots)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rec);
        }
    }
}
