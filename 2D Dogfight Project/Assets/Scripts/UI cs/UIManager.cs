using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject turnMessage;

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
        DisplayNbTurn();
    }

    public void DisplayNbTurn()
    {
        turnMessage.GetComponentInChildren<Text>().text = "Turn " + GameManager.Instance.turncount;
        turnMessage.SetActive(true);        
    }
}
