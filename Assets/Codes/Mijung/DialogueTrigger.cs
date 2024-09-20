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
    }
}