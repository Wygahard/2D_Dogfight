using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI_Overlay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    //
    private void OnEnable()
    {
        GameManager.Instance.OnBeginTurn += BeginTurn;
        GameManager.Instance.OnEndTurn += EndTurn;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnBeginTurn -= BeginTurn;
        GameManager.Instance.OnEndTurn -= EndTurn;
    }

    private void BeginTurn()
    {
        //Make Card Interactable or not
        canvasGroup.blocksRaycasts = true;
    }

    private void EndTurn()
    {
        canvasGroup.blocksRaycasts = false;
    }

    //MOVEMENT SECTION
    // distance from the center of this Game Object to the point where we clicked to start dragging 
    private Vector3 pointerDisplacement = Vector3.zero;

    private void Awake()
    {
       canvas = GetComponent<Canvas>();
       canvasGroup = GetComponent<CanvasGroup>();
       rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBegingDrag");
        pointerDisplacement = -transform.position + MouseInWorldCoords();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = MouseInWorldCoords();
        //Debug.Log(mousePos);
        rectTransform.anchoredPosition += eventData.delta; /// canvas.scaleFactor;
        canvas.overrideSorting = true;
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        canvas.overrideSorting = false;        
    }
    

    // returns mouse position in World coordinates for our GameObject to follow. 
    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
