using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mijung
{

    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManagement>().StartDialogue(dialogue);
        }
        public void Start()
        {
            // Keeps the gameobject alive on scene changes
            DontDestroyOnLoad(gameObject);

            // Find the dialogue manager and plays the start dialogue
            FindObjectOfType<DialogueManagement>().StartDialogue(dialogue);
        }
    }
}