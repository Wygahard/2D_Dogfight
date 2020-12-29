﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<GameObject> cardsInHand = new List<GameObject>();
    [SerializeField] private GameObject[] slots;

    private void Start()
    {
        DeactivateEmptySlot();
        GameManager.Instance.onEndTurn += FreezeCards;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.onEndTurn -= FreezeCards;
    }

    public void PlaceCard(Deck deck)
    {
        //card equal slots is counted as true even without <=
        if (cardsInHand.Count < slots.Length)
        {
            foreach (GameObject slot in slots)
            {
                //Take the next inactive slot
                if (slot.activeInHierarchy == false)
                {
                    slot.SetActive(true);
                    GameObject _card = deck.GetComponent<Deck>().DrawCard();

                    cardsInHand.Add(_card);
                    _card.transform.SetParent(slot.transform);
                    _card.transform.position = slot.transform.position;
                    return;
                }
            }

        }
        else
        {
            Debug.Log("Hand limit reached");
        }
    }

    public void RemoveCard(GameObject card)
    {
        cardsInHand.Remove(card);
        foreach(GameObject g in cardsInHand)
        {
            g.transform.SetParent(slots[cardsInHand.IndexOf(g)].transform);
            g.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        DeactivateEmptySlot();
    }

    private void DeactivateEmptySlot()
    {
        //deactivate empty slot
        foreach (GameObject slot in slots)
        {
            if (slot.activeInHierarchy && slot.transform.childCount == 0)
            {
                slot.SetActive(false);
            }
        }
    }

    private void FreezeCards()
    {
        Debug.Log("Cards are frozen!");
    }
}