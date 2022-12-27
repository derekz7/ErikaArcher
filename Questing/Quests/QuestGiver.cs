
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest[] quest;
    public Quest currentQuest;
    public Player player;
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI diamondText;

    private void Start()
    {
        currentQuest = quest[0];
    }
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = currentQuest.title;
        descriptionText.text = currentQuest.description;
        expText.text = currentQuest.experieneReward.ToString();
        diamondText.text = currentQuest.goldReward.ToString();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        currentQuest.isActive = true;
        player.quest = currentQuest;
    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void NextQuest()
    {
        if (currentQuest.id < quest.Length - 1)
        {
            currentQuest = quest[currentQuest.id + 1];
            Debug.Log("Next quest: " + currentQuest.id);
        }
        else
        {
            currentQuest = null;
        }

    }
}

