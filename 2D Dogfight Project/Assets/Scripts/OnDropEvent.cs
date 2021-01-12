using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[CreateAssetMenu(fileName = "OnDropEvent", menuName = "Game /Management")]
public class OnDropEvent : ScriptableObject
{
    public event UnityAction onDropEvent;

    public void DropHappened()
    {
        Debug.Log("DropHappened was called");

        if (onDropEvent != null)
        {
            onDropEvent.Invoke();
        }
        else
        {
            Debug.LogWarning("A Drop Event was requested but is empty");
        }
            
    }


}
