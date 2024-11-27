using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    //[SerializeField] private GameObject door;
    //private Collider2D doorCollider;

    private GameObject walls;

    private GameObject front;

    private bool entered = false;
    public bool Entered
    {
        get { return entered; }
        set
        {
            if (value)
            {
                EnterRoom();
            }
            else
            {
                ExitRoom();
            }

            entered = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        walls = transform.Find("Walls").gameObject;
        front = transform.Find("Front").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterRoom()
    {
        walls.SetActive(true);
        SetFrontLayer("Wall");
        
    }

    public void ExitRoom()
    {
        walls.SetActive(false);
        SetFrontLayer("Default");
    }

    private void SetFrontLayer(string layerName)
    {
        SpriteRenderer sr = front.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = layerName;

        SpriteRenderer[] spriteRenderers = front.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    

}
