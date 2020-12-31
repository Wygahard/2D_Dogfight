using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
 
    public Deck deck;
    public Hand hand;
    public GameObject plane;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var myHand = hand.GetComponent<Hand>();
            var myDeck = deck.GetComponent<Deck>();

            myHand.PlaceCard(myDeck);
           
        }
    }
}
