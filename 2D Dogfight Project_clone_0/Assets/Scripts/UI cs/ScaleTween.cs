using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleTween : MonoBehaviour
{
    public void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .3f).setDelay(.5f).setOnComplete(OnClose);
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), .5f).setDelay(.8f).setOnComplete(Hide);
    }

    void Hide()
    {
        gameObject.SetActive(false);        
    }
}
