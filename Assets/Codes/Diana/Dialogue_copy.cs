using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana
{
    [System.Serializable]

    public class Dialogue_copy
    {
        public string name;
        [TextArea(3, 10)]

        public string[] sentences;

        public DialogueChoice[] choices; // Choices for dialogue
    }

}