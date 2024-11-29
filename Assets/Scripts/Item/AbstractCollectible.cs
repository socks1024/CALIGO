using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCollectible : MonoBehaviour
{
    protected Collider2D cld;

    protected bool isTouching = false;

    // Start is called before the first frame update
    protected void Start()
    {
        cld = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetButtonDown("Act") && isTouching)
        {
            Collect();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

    protected abstract void Collect();
}
