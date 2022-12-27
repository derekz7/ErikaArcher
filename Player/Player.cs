using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1000;
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int Experiene;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private float interactRange = 2f;

    public Quest quest;
    public GameObject currentQuest;
    public Text QuestTitle;
    public Text QuestDescription;



    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        quest = null;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        ShowQuest();

    }
    public void Interact()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC npc))
            {
                npc.Interact();
            }


        }
    }
    public NPC GetInteractableObject()
    {
        List<NPC> npcList = new List<NPC>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC npc))
            {
                npcList.Add(npc);
            }
        }
        NPC closestNPC = null;
        foreach (NPC n in npcList)
        {
            if (closestNPC == null)
            {
                closestNPC = n;
            }
            else
            {
                if (Vector3.Distance(transform.position, n.transform.position) <
                    Vector3.Distance(transform.position, closestNPC.transform.position))
                {
                    //gan nhat
                    closestNPC = n;
                }
            }
        }
        return closestNPC;
    }


    public void TakeDamage(int amount)
    {
     
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Take damage");
        }
    }
    private void Die()
    {
        Debug.Log("Player dead. Reset health");
        this.currentHealth = this.maxHealth;
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        Physics.SyncTransforms();
    }
    public void ShowQuest()
    {
        if (quest != null)
        {
            QuestTitle.text = quest.title;
            if (quest.goal.IsReached())
            {
                QuestDescription.fontSize = 16;
                QuestDescription.text = "Hoàn thành";
            }
            else
            {
                QuestDescription.fontSize = 14;
                QuestDescription.text = quest.description + " " + quest.goal.currentAmount + "/" + quest.goal.requiredAmount;

            }
            currentQuest.SetActive(true);

        }
        else
        {
            currentQuest.SetActive(false);
        }
    }
}
