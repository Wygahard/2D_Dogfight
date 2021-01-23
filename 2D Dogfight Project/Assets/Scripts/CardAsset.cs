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
    
    //public string _name;    
    [SerializeField] private Vector2 _lowStartPoint;
    [SerializeField] private Vector2 _lowEndPoint;
    [SerializeField] private Vector2 _HighStartPoint;    
    [SerializeField] private Vector2 _HighEndPoint;
    [SerializeField] private float _rotation;

    public Vector2 movement;
    public Quaternion rotation;

    public enum DeckNumber
    {
        A,
        B,
        C1,
        C2,
        D1,
        D2
    }

    //Movement type
    public enum Symbol
    {
        Straight,   //0
        Right,      //1
        Left,       //2
        Descent,    //3
        Turn        //4
    }

    //Starting point origin
    public enum Orientation
    {
        South, //0
        West, //1
        East, //2
        North //3
    }

    public Vector2 GetMovement()
    {        
        float scale = 50f;
        
        //CardAsset ca = transform.GetComponentInChildren<CardManager>().cardAsset;
        float Xmove = (_lowEndPoint.x -_lowStartPoint.x) / scale;
        float Ymove = (_lowEndPoint.y - _lowStartPoint.y) / scale;

        //Change vector regarding starting orientation
        switch (orientation.ToString())
        {
            case "South":
                movement = new Vector2(Xmove, Ymove);
                break;

            case "West":
                movement = new Vector2(-Ymove, Xmove);
                break;

            case "East":
                movement = new Vector2(Ymove, -Xmove);
                break;

            case "North":
                movement = new Vector2(-Xmove, -Ymove);
                break;

            default:
                Debug.Log("Card orientation not recognized");
                movement = new Vector2(Xmove, Ymove);
                break;
        }
        return movement;
    }


    public Quaternion GetRotation()
    {
        Quaternion quaternion = Quaternion.AngleAxis(_rotation, Vector3.forward);
        return quaternion;
    }


}

