using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuversManager : MonoBehaviour
{
    public delegate void OnCardDrop();
    public static event OnCardDrop onCardDrop;

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

        /*
        foreach(KeyValuePair<int, Vector2> valuePair in cardsVector)
        {           
            Debug.Log("Key:" + valuePair.Key + " , Value: " + valuePair.Value);            
        }*/

        SetUpLastPlane();

    }

    public void SetUpLastPlane()
    {
        foreach (var maneuver in maneuversSlots)
        {
            if (!maneuver._containCard)
            {
                return;
            }

            int i = maneuversSlots.IndexOf(maneuver);
            GameObject lastPlane = PlaneLastPosition(i);
            maneuver.planeOutline.transform.position = lastPlane.transform.position;
            maneuver.planeOutline.transform.rotation = lastPlane.transform.rotation;
            
            maneuver.UpdateOutline();
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
