using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    //[SerializeField] private Button buttonLeft;
    //[SerializeField] private Button buttonRight;

    [SerializeField] private GameObject root;

    [SerializeField] private SpriteRenderer noteImg;
    [SerializeField] private TextMeshPro noteName;
    [SerializeField] private TextMeshPro noteDescription;

    private List<StoryNote.NoteInfo> noteInfos;

    private int currPageIndex = 0;

    void Start()
    {
        ShowStoryPage(currPageIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            root.SetActive(!root.activeInHierarchy);
        }
    }

    public void AddStoryPage(StoryNote.NoteInfo noteInfo)
    {
        noteInfos.Add(noteInfo);
    }

    private void ShowStoryPage(int pageIndex)
    {
        if (pageIndex <= 0)
        {
            pageIndex = 0;
        }

        if (pageIndex >= noteInfos.Count)
        {
            pageIndex = noteInfos.Count - 1;
        }

        if (noteInfos.Count > pageIndex)
        {
            noteImg.sprite = noteInfos[pageIndex].noteSprite;
            noteName.text = noteInfos[pageIndex].noteName;
            noteDescription.text = noteInfos[pageIndex].noteDescription;
        }
    }

    public void StoryPageUp()
    {
        ShowStoryPage(currPageIndex + 1);
    }

    public void StoryPageDown()
    {
        ShowStoryPage(currPageIndex - 1);
    }


}
