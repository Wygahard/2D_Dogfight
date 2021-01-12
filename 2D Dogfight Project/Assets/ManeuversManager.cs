using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManeuversManager : MonoBehaviour
{
    [SerializeField] private OnDropEvent _onDropEvent;

    public List<Maneuver> maneuversSlots = new List<Maneuver>();
    public Dictionary<int, Vector2> cardsVector = new Dictionary<int, Vector2>();
    public Player player;
    public Hand hand;
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent(typeof(Player)) as Player;
        hand = player.hand;
        plane = player.plane;

        GetManeuversSlots();
    }

    private void OnEnable()
    {
        _onDropEvent.onDropEvent += DropHappened;
    }


    private void OnDisable()
    {
        _onDropEvent.onDropEvent -= DropHappened;
    }

    public void DropHappened()
    {
        SetUpLastPlane();
    }


    public void GetManeuversSlots()
    {
        //Clean List of Maneuvers in case we want to update it in game
        if (maneuversSlots != null)
            maneuversSlots.Clear();

        foreach (Transform child in transform)
        {
            Maneuver maneuverChild = child.GetComponent<Maneuver>();
            if (maneuverChild != null)
            {
                maneuversSlots.Add(maneuverChild);
                int i = maneuversSlots.IndexOf(maneuverChild);
                maneuverChild.UpdateNumberList(i);
                maneuverChild.hand = hand;
            }
        }
    }


    public void UpdateVectorList(Vector2 movement, int numberInList)
    {
        if (cardsVector.ContainsKey(numberInList))
            cardsVector.Remove(numberInList);
        
        cardsVector.Add(numberInList, movement);

        //Get Dictionnary value for debugging
        /*
        foreach(KeyValuePair<int, Vector2> valuePair in cardsVector)
        {           
            Debug.Log("Key:" + valuePair.Key + " , Value: " + valuePair.Value);            
        }
        */
    }

    public void SetUpLastPlane()
    {
        bool _isPreviousSlotFull = true;

        foreach (var maneuver in maneuversSlots)
        {
            
            if (maneuver.ContainCard())
            {                
                int i = maneuversSlots.IndexOf(maneuver);
                UpdateVectorList(maneuver.GetMovement(), i);

                GameObject lastPlane = PlaneLastPosition(i);
                
                maneuver.planeOutline.transform.position = lastPlane.transform.position;
                maneuver.planeOutline.transform.rotation = lastPlane.transform.rotation;                

                if (_isPreviousSlotFull)
                {
                    maneuver.planeOutline.SetActive(true);
                }
                else
                {
                    maneuver.planeOutline.SetActive(false);
                }

                maneuver.UpdateOutline();
            }
            else
            {
                _isPreviousSlotFull = false;
                maneuver.planeOutline.SetActive(false);
            }            
        }
    }

    public GameObject PlaneLastPosition(int numberInList)
    {
        //First in List, starting point is the plane
        if(numberInList == 0)
        {
            return plane;
        }
        else
        {
            return maneuversSlots[numberInList-1].planeOutline;
        }        
    }


    
}
