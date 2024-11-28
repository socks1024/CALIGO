using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StoryNote : AbstractCollectible
{
    [SerializeField] private string noteName;

    [SerializeField] private string noteDescription;

    [SerializeField] private Sprite noteSprite;

    public struct NoteInfo
    {
        public string noteName;

        public string noteDescription;

        public Sprite noteSprite;

        public NoteInfo(string noteName, string noteDescription, Sprite noteSprite)
        {
            this.noteName = noteName;
            this.noteDescription = noteDescription;
            this.noteSprite = noteSprite;
        }
    }

    public NoteInfo noteInfo;

    private new void Start()
    {
        base.Start();

        noteInfo = new NoteInfo(noteName, noteDescription, noteSprite);
    }

    private new void Update()
    {
        base.Update();
    }

    protected override void Collect()
    {
        UIManager.Instance.AddStoryPage(noteInfo);
        this.gameObject.SetActive(false);
    }    
}
