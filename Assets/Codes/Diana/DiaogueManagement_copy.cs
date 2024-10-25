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
        public Button[] choiceButtons;
        public Dialogue_copy currentDialogue;
        private Queue<string> sentences;
        private ProgressBarsControl progressBarControl;

        // Use this for initialization
        void Start()
        {
            sentences = new Queue<string>();
            progressBarControl = FindObjectOfType<ProgressBarsControl>(); 

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                int choiceIndex = i; 
                choiceButtons[i].onClick.AddListener(() => SelectChoice(choiceIndex));
            }
        }

        public void StartDialogue(Dialogue_copy dialogue)
        {
        if (dialogue == null)
        {
            Debug.LogError("Dialogue_copy object is null!");
            return;
        }

            Debug.Log("Starting dialogue with: " + dialogue.dialogueName);
            nameText.text = dialogue.dialogueName;
            currentDialogue = dialogue;

            if (sentences == null)
            {
                sentences = new Queue<string>();
            }

            sentences.Clear();
            Debug.Log("Sentences queue cleared.");

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
                Debug.Log("Enqueued sentence: " + sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            Debug.Log("Sentences.Count is of type: " + sentences.Count.GetType());
            Debug.Log("Sentences left: " + sentences.Count);
            string sentence = sentences.Dequeue(); // Dequeue sentence
            Debug.Log("Displayed sentence: " + sentence);
            
            dialogueText.text = sentence;
            Debug.Log("Sentences after dequeue: " + sentences.Count);

            if (sentences.Count == 0)
            {
                Debug.Log("No more sentences left, now calling DisplayChoices...");
                DisplayChoices(); 
            }
        }


        void DisplayChoices()
        {
            Debug.Log("Displaying choices...");
            for (int i = 0; i < currentDialogue.choices.Length; i++)
            {
                Transform choiceParent = choiceTexts[i].transform.parent;
                choiceTexts[i].transform.parent.gameObject.SetActive(true); // Ensure the parent holding the text is active
                choiceTexts[i].text = currentDialogue.choices[i].playerDialogue + 
                                      " (Cost: " + currentDialogue.choices[i].coinCost + " coins)";
            }
        }

        public void SelectChoice(int choiceIndex)
        {
            Debug.Log("Selected choice index: " + choiceIndex);
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

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
}