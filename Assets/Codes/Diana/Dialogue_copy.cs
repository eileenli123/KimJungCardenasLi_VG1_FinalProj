using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Diana
{
    [System.Serializable]

    public class Dialogue_copy : MonoBehaviour
    {
        public string dialogueName;
        [TextArea(3, 10)]

        public string[] sentences;

        public DialogueChoice[] choices; // Choices for dialogue
    }

}