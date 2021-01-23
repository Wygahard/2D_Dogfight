using UnityEngine;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
//[ExecuteInEditMode]
[RequireComponent(typeof(Sprite))]
public class CardManager : MonoBehaviour {

    private CardAsset cardAsset;
    [Header("Image References")]
    public Sprite CardGraphicImage;
    public string Symbol;
    public Vector2 movement;
    public Quaternion rotation;

    
    [Header("Movement References")]
    public RectTransform LowStartPoint;
    public RectTransform LowEndPoint;
    public RectTransform HighStartPoint;
    public RectTransform HighEndPoint;
    public Transform PlaneZ;

    private Vector2 lowStart;
    private Vector2 lowEnd;
    private Vector2 highStart;
    private Vector2 highEnd;
    private float rotationDegree;

    public int positionInHand;

    public bool _DebugMode;
    public Text Infos;

    /*
    void Awake()
    {
        if (cardAsset != null && !_DebugMode)
            ApplyCardAsset();
    }
    */
   
    public void ApplyCardAsset(CardAsset ca)
    {
        //Image
        this.GetComponent<Image>().sprite = ca._artwork;
        this.Symbol = ca._symbol.ToString();
        this.movement = ca.GetMovement();
        this.rotation = ca.GetRotation();

        /*
        //Start and End point
        LowStartPoint.localPosition = cardAsset._lowStartPoint;
        LowEndPoint.localPosition = cardAsset._lowEndPoint;
        HighStartPoint.localPosition = cardAsset._HighStartPoint;
        HighEndPoint.localPosition = cardAsset._HighEndPoint;

        rotation = cardAsset._rotation;
        */
    }

    public void Update()
    {
        if (_DebugMode)
        {
            Infos.enabled = true;
            lowStart = LowStartPoint.anchoredPosition;
            lowEnd = LowEndPoint.anchoredPosition;
            highStart = HighStartPoint.anchoredPosition;
            highEnd = HighEndPoint.anchoredPosition;
            rotationDegree = PlaneZ.transform.rotation.z;

            Infos.text = cardAsset.name + "\r\nCoordinates \r\nLow Start: " + lowStart + "\r\nLow End: " + lowEnd + "\r\nHigh Start: " + highStart + "\r\nHigh End: " + highEnd + "\r\nrotation: " + rotation;
        }
        else
        {
            Infos.enabled = false;
        }
    }

}
