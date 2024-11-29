using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StoryNote : AbstractCollectible
{
    [SerializeField] private string noteName;

    [SerializeField] private string noteDescription;

    [SerializeField] private Sprite noteSprite;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected override void Collect()
    {
        UIManager.AddStoryPage(noteName,noteDescription,noteSprite);
        
        this.gameObject.SetActive(false);
    }
}
