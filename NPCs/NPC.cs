using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    [SerializeField]
    private string npcName;

    [SerializeField]
    private string interactText;

    [SerializeField]
    private string[] dialogQuest;

    [SerializeField]
    private string[] dialogQuestActived;

    [SerializeField]
    private string[] dialogCompleteQuest;

    [SerializeField]
    private string[] outOfQuestList;

    private QuestGiver questGiver;

    private string[] dialogue;
    private string[] dialogueActived;
    private string[] dialogueCompleteQuest;

    private void Start()
    {

        questGiver = GetComponent<QuestGiver>();
        dialogue = dialogQuest[questGiver.currentQuest.id].Split('|');
        dialogueActived = dialogQuestActived[questGiver.currentQuest.id].Split('|');
        dialogueCompleteQuest = dialogCompleteQuest[questGiver.currentQuest.id].Split('|');
    }

    public void SetQuestDialog()
    {
        questGiver.NextQuest();
        if (questGiver.currentQuest != null)
        {

            dialogue = dialogQuest[questGiver.currentQuest.id].Split('|');
            dialogueActived = dialogQuestActived[questGiver.currentQuest.id].Split('|');
            dialogueCompleteQuest = dialogCompleteQuest[questGiver.currentQuest.id].Split('|');
        }

    }


    public void Interact()
    {
        if (questGiver.currentQuest != null)
        {
            if (!questGiver.currentQuest.isActive && !questGiver.currentQuest.isCompleted) //chua dc nhan
            {
                DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
            }
            if (questGiver.currentQuest.isActive && !questGiver.currentQuest.isCompleted)
            {
                if (questGiver.currentQuest.goal.IsReached())
                {
                    questGiver.currentQuest.Complete();
                    DialogueSystem.Instance.AddNewDialogue(dialogueCompleteQuest, npcName);
                    SetQuestDialog();

                }
                else
                {
                    DialogueSystem.Instance.AddNewDialogue(dialogueActived, npcName);
                }
            }
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(outOfQuestList, npcName);
        }
       
        //Debug.Log("Interacting with NPCs");
    }

    public string GetInteractText()
    {
        return interactText;
    }

}
