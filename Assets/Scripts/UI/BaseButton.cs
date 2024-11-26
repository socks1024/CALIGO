using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButton : MonoBehaviour
{
    protected SpriteRenderer sprite;

    [SerializeField] protected Color defaultColor = Color.white;
    [SerializeField] protected Color highlightColor = Color.white;
    [SerializeField] protected Color pressedColor = Color.white;

    protected bool highlighted = false;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnSpriteMouseEnter()
    {
        sprite.color = highlightColor;
        highlighted = true;
    }

    public void OnSpriteMouseExit()
    {
        sprite.color = defaultColor;
        highlighted = false;
    }

    public void OnSpriteMouseDown()
    {
        sprite.color = pressedColor;
    }

    public void OnSpriteMouseUp()
    {
        if (highlighted)
        {
            sprite.color = highlightColor;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }

    protected void ClickEvent()
    {

    }
}
