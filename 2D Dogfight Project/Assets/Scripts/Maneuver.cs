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


    //Preview parameters
    [SerializeField]
    public bool _Preview;
    Vector2 movement;

    Quaternion _currentRotation;
    float _Rotation;    

    private void Start()
    {
        maneuversManager = transform.GetComponentInParent<ManeuversManager>();
        //hand = maneuversManager.player.hand;
        _currentRotation = transform.rotation;
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
                        foreach (Transform child in transform)
                        {
                            if (child.CompareTag("Card"))
                            {
                                child.SetParent(newCard.transform.parent);
                                child.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                                _card = null;
                            }                                
                        }
                    }
                    break;

                case "HandSlot":
                    hand.RemoveCard(newCard);
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
        planeOutline.transform.Translate(GetMovement());

        CardAsset ca = _card.GetComponent<CardManager>().cardAsset;
        _Rotation = ca._rotation;

        Quaternion quaternion = Quaternion.AngleAxis(_Rotation, Vector3.forward);
        planeOutline.transform.rotation = planeOutline.transform.rotation * quaternion;        
    }


    public Vector2 GetMovement()
    {
        if (!ContainCard())
            Debug.LogWarning("GetMovement was called but no card is attached to" + name);

        //GET CARD DATA
        CardAsset ca = _card.GetComponent<CardManager>().cardAsset;        

        float scale = 50f;

        Vector2 movement;
        //CardAsset ca = transform.GetComponentInChildren<CardManager>().cardAsset;
        float Xmove = (-ca._lowStartPoint.x + ca._lowEndPoint.x) / scale;
        float Ymove = (-ca._lowStartPoint.y + ca._lowEndPoint.y) / scale;

        //Change vector regarding starting orientation
        switch (ca.orientation.ToString())
        {
            case "South":
                movement = new Vector2(Xmove, Ymove);
                break;

            case "West":
                movement = new Vector2(-Ymove, Xmove);
                break;

            case "East":
                movement = new Vector2(Ymove, -Xmove);
                break;

            case "North":
                movement = new Vector2(-Xmove, -Ymove);
                break;

            default:
                Debug.Log("Card orientation not recognized");
                movement = new Vector2(Xmove, Ymove);
                break;
        }

        return movement;
    }


}


