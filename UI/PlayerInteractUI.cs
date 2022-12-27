using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Player playerInteract;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI interactTextMesh;

    private void Update()
    {
        if (playerInteract.GetInteractableObject() != null && !dialogPanel.activeSelf)
            ShowNPC(playerInteract.GetInteractableObject());
        else
            HideNPC();
    }
    private void ShowNPC(NPC npc)
    {
        containerGameObject.SetActive(true);
        interactTextMesh.text = npc.GetInteractText();
    }
    private void HideNPC()
    {
        containerGameObject.SetActive(false);
    }

}
