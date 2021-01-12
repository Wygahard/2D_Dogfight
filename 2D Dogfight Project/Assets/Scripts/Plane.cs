using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    
    GameObject[] maneuvers;

    private float scale;

    float Xmove;
    float Ymove;
    Vector2 movement;
    
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

            //Change vector regarding starting orientation
            switch (ca.orientation.ToString())
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
            

            //Move the model
            transform.Translate(movement);

            //Rotate the model
            _Rotation = ca._rotation;
            Quaternion quaternion = Quaternion.AngleAxis(_Rotation, Vector3.forward);
            transform.rotation = transform.rotation * quaternion;

                      
            
            


        }
        
        
    }
}
