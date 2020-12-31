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
    public bool _containCard;

    public GameObject planeOutline;
    //public GameObject planeLastKnownPosition = new GameObject();


    //Preview parameters
    [SerializeField]
    public bool _Preview;
    Vector2 movement;

    Quaternion _currentRotation;
    float _Rotation;


    public void OnEnable()
    {
        ManeuversManager.onCardDrop += UpdateOutline;
    }

    public void OnDisable()
    {
        ManeuversManager.onCardDrop -= UpdateOutline;
    }

    private void Start()
    {
        maneuversManager = transform.GetComponentInParent<ManeuversManager>();
        //hand = maneuversManager.player.hand;
        _currentRotation = transform.rotation;
        _containCard = false;        
    }

    
    public void Update()
    {        
        if (_containCard && _Preview)
        {
            planeOutline.SetActive(true);
        }
        else
        {
            planeOutline.SetActive(false);
        }
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop" + name);
        foreach (Transform child in transform)
        {
            if (child.tag == "Card")
            {
                _containCard = true;
                return;
            }
        }
        _containCard = false;

        if (eventData.pointerDrag != null && !_containCard && eventData.pointerDrag.gameObject.tag == "Card")
        {
            //eventData.pointerDrag.GetComponent<Transform>().transform.position = transform.position;
            GameObject card = eventData.pointerDrag.gameObject;
            hand.RemoveCard(card);
            card.GetComponent<Transform>().SetParent(transform);
            _containCard = true;

            //GET CARD DATA
            CardAsset ca = transform.GetComponentInChildren<CardManager>().cardAsset;
            _Rotation = ca._rotation;
            movement = GetMovement(ca);
            
            //UPDATE VECTOR LIST, WILL REFRESH OUTLINE POSITIONS
            maneuversManager.UpdateVectorList(movement, numberInList);
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
        //SET STARTING POINT
        //planeOutline.transform.position = planeLastKnownPosition.transform.position;
        //planeOutline.transform.rotation = planeLastKnownPosition.transform.rotation;
        
        planeOutline.transform.Translate(movement);
        Quaternion quaternion = Quaternion.AngleAxis(_Rotation, Vector3.forward);
        planeOutline.transform.rotation = planeOutline.transform.rotation * quaternion;
        
    }


    public Vector2 GetMovement(CardAsset ca)
    {
        float scale = 100f;

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


