using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Maneuver : MonoBehaviour, IDropHandler
{
    private Player player;
    private Hand hand;
    private bool _containCard;


    private void Start()
    {
        player = GetComponentInParent(typeof(Player)) as Player;
        hand = player.hand;
        _containCard = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
       Debug.Log("OnDrop" + name);

        if (eventData.pointerDrag != null && !_containCard && eventData.pointerDrag.gameObject.tag == "Card")

        {
            _containCard = true;
            var card = eventData.pointerDrag.gameObject;
            card.GetComponent<Transform>().SetParent(transform.GetChild(0));
            //eventData.pointerDrag.GetComponent<Transform>().transform.position = transform.position;
            hand.RemoveCard(card);
        }
    }
}
