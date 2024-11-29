using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(UIManager)) as UIManager;
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this as UIManager;
    }

    //[SerializeField] private Button buttonLeft;
    //[SerializeField] private Button buttonRight;

    [SerializeField] private GameObject root;

    [SerializeField] private SpriteRenderer noteImg;
    [SerializeField] private TextMeshPro noteName;
    [SerializeField] private TextMeshPro noteDescription;

    public static List<NoteInfo> noteInfos = new List<NoteInfo>();

    public struct NoteInfo
    {
        public string noteNameInfo;

        public string noteDescriptionInfo;

        public Sprite noteSpriteInfo;
    }

    private int currPageIndex = 0;

    void Start()
    {
        ShowStoryPage(currPageIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            root.SetActive(!root.activeSelf);
            if (root.activeSelf)
            {
                ShowStoryPage(currPageIndex);
            }
        }
    }

    public static void AddStoryPage(string name,string description,Sprite sprite)
    {
        NoteInfo info = new NoteInfo();

        info.noteNameInfo = name;
        info.noteDescriptionInfo = description;
        info.noteSpriteInfo = sprite;

        noteInfos.Add(info);
    }

    private void ShowStoryPage(int pageIndex)
    {
        if (noteInfos == null)
        {
            return;
        }

        if (pageIndex <= 0)
        {
            pageIndex = 0;
        }
        else if (pageIndex >= noteInfos.Count)
        {
            pageIndex = noteInfos.Count - 1;
        }

        if (noteInfos.Count > pageIndex)
        {
            noteImg.sprite = noteInfos[pageIndex].noteSpriteInfo;
            noteName.text = noteInfos[pageIndex].noteNameInfo;
            noteDescription.text = noteInfos[pageIndex].noteDescriptionInfo;
        }
    }

    public void StoryPageUp()
    {
        currPageIndex += 1;
        ShowStoryPage(currPageIndex);
    }

    public void StoryPageDown()
    {
        currPageIndex -= 1;
        ShowStoryPage(currPageIndex);
    }


}
