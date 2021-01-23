using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Maneuver : MonoBehaviour, IDropHandler
{
    private ManeuversManager maneuversManager;
    public int numberInList;
    public Text numberToDisplay;
    public Hand hand;
    public GameObject _card;

    public GameObject planeOutline;
    //public GameObject planeLastKnownPosition = new GameObject();
    
    //Vector2 movement;
    //Quaternion rotation;
    float _Rotation;    

    private void Start()
    {
        maneuversManager = transform.GetComponentInParent<ManeuversManager>();
        //hand = maneuversManager.player.hand;
    }
       
    //planeOutline.SetActive(true);

    //CHECK IF CONTAINS CARD
    public bool ContainCard()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Card"))
            {
                _card = child.gameObject;
                return true;
            }
        }
        //Deactivate card if it's associate to another parent
        if (_card != null)
            _card = null;

        return false;
    }


    public void OnDrop(PointerEventData eventData)
    {
        GameObject newCard = eventData.pointerDrag.gameObject;

        if (eventData.pointerDrag != null && newCard.tag == "Card")
        {
            //IS THE CARD COMING FROM THE HAND OR ANOTHER SLOT
            switch (newCard.transform.parent.tag)
            {
                case "ManeuverSlot":
                    //Switch cards if there's already one in the slot
                    if(ContainCard())
                    {
                        _card.transform.SetParent(newCard.transform.parent);
                        _card.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                        _card = null;
                    }
                    break;

                case "HandSlot":                    
                    hand.RemoveCard(newCard);
                    //Switch cards if there's already one in the slot
                    if (ContainCard())
                    {
                        hand.PlaceCard(_card);
                        _card = null;
                    }
                    break;

                default:
                    Debug.LogError("Tag Parent of Card is not recognized");
                    break;
            }

            _card = newCard;
            _card.transform.SetParent(transform);
            
            maneuversManager.TriggerDropCheck();
        }
    }

    //CALLED BY MANEUVER MANAGER WITH GetManeuversSlots()
    public void UpdateNumberList(int i)
    {
        numberInList = i;
        numberToDisplay.text = (numberInList + 1).ToString();
    }


    //CALLED BY MANEUVER MANAGER WITH UpdateDic
    public void UpdateOutline()
    {
        //Maneuver Manager is in charge of setting the starting plane   
        planeOutline.transform.Translate(CardMovement());
        planeOutline.transform.rotation = planeOutline.transform.rotation * CardRotation();        
    }


    public Vector2 CardMovement()
    {
        if (!ContainCard())
            Debug.LogWarning("GetMovement was called but no card is attached to" + name);
        
        Vector2 movement = _card.GetComponent<CardManager>().movement;
        return movement;
    }

    public Quaternion CardRotation()
    {
        if (!ContainCard())
            Debug.LogWarning("GetRotation was called but no card is attached to" + name);

        Quaternion rotation = _card.GetComponent<CardManager>().rotation;

        return rotation;
    }
}


