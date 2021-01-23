using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    [SerializeField]
    private GameObject _cardPrefab = null;
    public List<CardAsset> cards = new List<CardAsset>();
    

    void Awake()
    {
        cards.Shuffle();
    }

    public GameObject DrawCard()
    {
        CardAsset ca = cards[Random.Range(0, cards.Count)];
        
        GameObject _card = Instantiate(_cardPrefab);
        CardManager manager = _card.GetComponent<CardManager>();
        manager.ApplyCardAsset(ca);

        return _card;
    }

}
