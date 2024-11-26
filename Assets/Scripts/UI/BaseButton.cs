using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButton : MonoBehaviour
{
    [SerializeField] protected Color highlightColor;
    [SerializeField] protected Color pressedColor;


    public void OnSpriteClick()
    {
        Debug.Log("Test");
    }
}
