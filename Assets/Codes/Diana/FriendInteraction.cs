using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Diana;

public class FriendInteraction : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameTagText;
    public DialogueManagement_copy dialogueManager;
    public Dialogue_copy dialogueData;

    // Check when player enters the trigger area of the friend sprite
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            dialogueUI.SetActive(true);
            dialogueManager.StartDialogue(dialogueData); 

            nameTagText.text = dialogueData.name;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueUI.SetActive(false);
        }
    }
}
