using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private GameObject[] maneuvers;
    [SerializeField] private float scale;

    float Xmove;
    float Ymove;
    [SerializeField]
    float _Rotation;


    private void Start()
    {
        GameManager.Instance.onEndTurn += MovePlane;
        transform.position = new Vector2(0,-4);
        scale = 100f;       
    }

    private void OnDisable()
    {
        GameManager.Instance.onEndTurn -= MovePlane;
    }


    private void MovePlane()
    {
        StartCoroutine(MovePlaneRoutine());
    }

    IEnumerator MovePlaneRoutine()
    {
        foreach (GameObject m in maneuvers)
        {
            yield return new WaitForSecondsRealtime(1f);
                      
            CardAsset ca = m.transform.GetComponentInChildren<CardManager>().cardAsset;
            Xmove = (-ca._lowStartPoint.x + ca._lowEndPoint.x) / scale;
            Ymove = (-ca._lowStartPoint.y + ca._lowEndPoint.y) / scale;
            Vector2 movement = new Vector2(Xmove, Ymove);

            _Rotation = ca._rotation;

            Quaternion quaternion = Quaternion.AngleAxis(_Rotation, Vector3.forward);
            //Vector3 rotatedVector = quaternion * movement;
            //Debug.Log("Destination: X= " + transform.position.x + rotatedVector.x + ", Y= " + transform.position.y + rotatedVector.y);

            
            transform.Translate(movement);            
            //transform.Translate(rotatedVector);
            transform.rotation = transform.rotation * quaternion;


        }
        
        
    }
}
