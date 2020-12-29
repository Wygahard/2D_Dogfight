using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Maneuver")]

public class CardAsset : ScriptableObject
{
    // this object will hold the info about the most general card
    [Header("General info")]
    public Sprite _artwork;
    public Symbol _symbol;
    public Orientation orientation;

    public enum Orientation
    {
        Vertical, //0
        Horizontal //1
    }
    
    //public string _name;    
    public Vector2 _lowStartPoint;
    public Vector2 _lowEndPoint;
    public Vector2 _HighStartPoint;    
    public Vector2 _HighEndPoint;

    public float _rotation;
    
    public enum Symbol
    {
        Straight,   //0
        Right,      //1
        Left,       //2
        Descent,    //3
        Turn        //4
    }
    
    public enum DeckNumber
    {
        A,
        B,
        C1,
        C2,
        D1,
        D2
    }
    
}

