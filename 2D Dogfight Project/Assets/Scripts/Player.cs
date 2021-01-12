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
            //Draw a card and place it in the hand
            GameObject card = deck.DrawCard();
            hand.PlaceCard(card); 
        }
    }
}
