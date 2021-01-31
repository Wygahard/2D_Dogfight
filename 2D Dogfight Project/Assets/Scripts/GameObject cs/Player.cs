using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{ 
    public Deck deck;
    public Hand hand;
    public GameObject plane;

    private void OnEnable()
    {
        GameManager.Instance.OnBeginTurn += BeginTurn;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnBeginTurn -= BeginTurn;
    }

    //Action to do in Begin Turn
    private void BeginTurn()
    {
        DrawCardStarter();
    }

    private void DrawCardStarter()
    {
        StartCoroutine(DrawCardRoutine());
    }

    IEnumerator DrawCardRoutine()
    {
        if(GameManager.Instance.turncount == 1)
        {
            //First turn, we wait for animation to finish and draw 7 cards
            yield return new WaitForSecondsRealtime(1.2f);
            for (int i = 0; i < 7; i++)
            {
                yield return new WaitForSecondsRealtime(.5f);
                DrawCard();
            }            
        }
        else
        {
            //Other turns, we draw 3 cards
            yield return new WaitForSecondsRealtime(1.2f);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSecondsRealtime(.5f);
                DrawCard();
            }
        }        
    }

    private void DrawCard()
    {
        GameObject card = deck.DrawCard();
        hand.PlaceCard(card);
    }    
}
