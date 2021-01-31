using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManeuversManager : MonoBehaviour
{
    [SerializeField] private OnDropEvent _onDropEvent;

    public List<Maneuver> maneuversSlots = new List<Maneuver>();
    public Dictionary<int, Vector2> CardsVectorDic = new Dictionary<int, Vector2>();
    public Dictionary<int, Quaternion> CardsQuaternionDic = new Dictionary<int, Quaternion>();
   
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
        GameManager.Instance.OnEndTurn += TurnEnd;
    }

    private void OnDisable()
    {
        _onDropEvent.onDropEvent -= DropHappened;
        GameManager.Instance.OnEndTurn -= TurnEnd;
    }

    private void DropHappened()
    {
        SetUpLastPlane();
    }

    public void TriggerDropCheck()
    {
        _onDropEvent.DropHappened();
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


    public void UpdateVectorDict(Vector2 movement, int numberInList)
    {
        if (CardsVectorDic.ContainsKey(numberInList))
            CardsVectorDic.Remove(numberInList);
        
        CardsVectorDic.Add(numberInList, movement);

        //Get Dictionnary value for debugging
        /*
        foreach(KeyValuePair<int, Vector2> valuePair in cardsVector)
        {           
            Debug.Log("Key:" + valuePair.Key + " , Value: " + valuePair.Value);            
        }
        */
    }

    public void UpdateQuaternionDict(Quaternion rotation, int numberInList)
    {
        if (CardsQuaternionDic.ContainsKey(numberInList))
            CardsQuaternionDic.Remove(numberInList);

        CardsQuaternionDic.Add(numberInList, rotation);
    }


    public void SetUpLastPlane()
    {
        bool _isPreviousSlotFull = true;

        foreach (var maneuver in maneuversSlots)
        {
            
            if (maneuver.ContainCard())
            {                
                int i = maneuversSlots.IndexOf(maneuver);
                UpdateVectorDict(maneuver.CardMovement(), i);
                UpdateQuaternionDict(maneuver.CardRotation(), i);

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

    //Transfer Information to Plane
    private void TurnEnd()
    {
        plane.GetComponent<Plane>().Movements = new List<Vector2>(CardsVectorDic.Values);
        plane.GetComponent<Plane>().Rotations = new List<Quaternion>(CardsQuaternionDic.Values);
        CleanManeuvers();
    }

    private void CleanManeuvers()
    {
        foreach (var maneuver in maneuversSlots)
        {
            if (maneuver.ContainCard())
            {
                maneuver._card = null;
                maneuver.ContainCard();
            }
            else
            {
                Debug.LogError("Maneuver Slot " + maneuver + " should have a card");
            }
        }
    }
}
