using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Diana
{

    public class DialogueManagement_copy : MonoBehaviour
    {

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI[] choiceTexts;
        private Dialogue_copy currentDialogue;
        private Queue<string> sentences;
        private ProgressBarsControl progressBarControl;

        // Use this for initialization
        void Start()
        {
            sentences = new Queue<string>();
            progressBarControl = FindObjectOfType<ProgressBarsControl>(); 
        }

        public void StartDialogue(Dialogue_copy dialogue)
        {
            // Debugging to check for missing references
        if (dialogue == null)
        {
            Debug.LogError("Dialogue_copy object is null!");
            return;
        }

            Debug.Log("Starting dialogue with: " + dialogue.dialogueName);
            nameText.text = dialogue.dialogueName;

            nameText.text = dialogue.dialogueName;
            currentDialogue = dialogue;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                DisplayChoices();
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;

        }

        void DisplayChoices()
        {
            for (int i = 0; i < currentDialogue.choices.Length; i++)
            {
                choiceTexts[i].text = currentDialogue.choices[i].playerDialogue +
                                      " (Cost: " + currentDialogue.choices[i].coinCost + " coins)";
            }
        }

        public void SelectChoice(int choiceIndex)
        {
            float choiceCost = currentDialogue.choices[choiceIndex].coinCost;

            // Check if player has enough money for the selected choice
            if (progressBarControl.DecreaseMoney(choiceCost)) // Successfully deducted money
            {
                // Reward social gems
                float gemReward = currentDialogue.choices[choiceIndex].socialGemReward;
                progressBarControl.IncreaseSocial(gemReward);

                // Reward academic gems 
                float academicReward = currentDialogue.choices[choiceIndex].academicGemReward;
                progressBarControl.IncreaseGPA(academicReward);

                // Reward money gems
                float moneyReward = currentDialogue.choices[choiceIndex].moneyGemReward;
                progressBarControl.IncreaseMoney(moneyReward);

                // Show the friend's response
                dialogueText.text = currentDialogue.choices[choiceIndex].friendResponse;

                EndDialogue(); // End the conversation after choice is made
            }
            else
            {
                // If not enough money, display an error message
                dialogueText.text = "Not enough money!";
            }
        }
        void EndDialogue()
        {
            Debug.Log("End of Conversation");
        }
    }
}