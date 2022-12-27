using UnityEngine;

public class NpcQuestGiver : MonoBehaviour
{
    public QuestGiver questGiver;
    private bool windowOpened;

    private void Start()
    {
        questGiver = GetComponent<QuestGiver>();
        windowOpened = false;

    }

    void Update()
    {
        if (DialogueSystem.Instance.dialogEnded && !questGiver.currentQuest.isActive)
        {
            questGiver.OpenQuestWindow();
            if (Input.GetKey(KeyCode.N) && !questGiver.currentQuest.isActive)
            {
                DialogueSystem.Instance.dialogEnded = false;
                questGiver.AcceptQuest();

            }
            if (Input.GetKey(KeyCode.Escape))
            {
                DialogueSystem.Instance.dialogEnded = false;
                questGiver.CloseQuestWindow();
            }
          
        }

    }
    
}

