using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    public string npcName;
    [SerializeField] private GameObject dialoguePanel;

    public List<string> dialogueLines = new List<string>();

    private Text dialogueText, nameText;

    private int dialogueIndex;

    public bool dialogEnded { get; set; }

    void Awake()
    {
        dialogueText = dialoguePanel.transform.Find("Message").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("Author").GetChild(0).GetComponent<Text>();
        dialoguePanel.SetActive(false);
        dialogEnded = false;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ContinueDialogue();
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
        }
        this.npcName = npcName;

        Debug.Log(dialogueLines.Count);
        CreateDialogue();

    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
            Debug.Log(dialogueIndex);
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogEnded = true;

        }
    }
}
