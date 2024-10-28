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

        void Start()
        {
            sentences = new Queue<string>();
            progressBarControl = FindObjectOfType<ProgressBarsControl>();
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

            sentences.Clear();  // Ensure old sentences are cleared
            Debug.Log("Sentences queue cleared.");

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
                Debug.Log("Enqueued sentence: " + sentence);  // Make sure each sentence is added to the queue
            }

            DisplayNextSentence();
        }

    public void DisplayNextSentence()
    {
        // Check the current count of sentences in the queue
        Debug.Log("Sentences left: " + sentences.Count);

        // Dequeue a sentence if there are any left
        string sentence = sentences.Dequeue();
        Debug.Log("Displayed sentence: " + sentence);
        
        dialogueText.text = sentence;

        // Log the updated count after dequeuing
        Debug.Log("Sentences after dequeue: " + sentences.Count);
        
        // Now, check if sentences.Count has reached zero AFTER displaying the sentence
        if (sentences.Count == 0)
        {
            Debug.Log("No more sentences left, now calling DisplayChoices...");
            DisplayChoices(); // Ensure DisplayChoices gets called when no more sentences remain
        }
    }

        void DisplayChoices()
        {
            Debug.Log("Displaying choices...");

            if (currentDialogue.choices == null || currentDialogue.choices.Length == 0)
            {
                Debug.LogError("No choices available in currentDialogue!");
                return;
            }

            // Ensure the choices are visible
            for (int i = 0; i < currentDialogue.choices.Length; i++)
            {
                Transform choiceParent = choiceTexts[i].transform.parent;
                choiceTexts[i].transform.parent.gameObject.SetActive(true); // Ensure the parent holding the text is active
                choiceTexts[i].text = currentDialogue.choices[i].playerDialogue + 
                                    " (Cost: " + currentDialogue.choices[i].coinCost + " coins)";
                Debug.Log("Displaying choice: " + currentDialogue.choices[i].playerDialogue); // Log the choice being displayed
            }
        }


        public void SelectChoice(int choiceIndex)
        {
            float choiceCost = currentDialogue.choices[choiceIndex].coinCost;

            // Check if player has enough money for the selected choice
            if (progressBarControl.DecreaseCoins((int)choiceCost)) // Successfully deducted money
            {
                // Reward social gems
                float gemReward = currentDialogue.choices[choiceIndex].socialGemReward;
                progressBarControl.IncreaseSocial(gemReward);

                // Reward academic gems 
                float academicReward = currentDialogue.choices[choiceIndex].academicGemReward;
                progressBarControl.IncreaseGPA(academicReward);

                // Reward money gems
                float moneyReward = currentDialogue.choices[choiceIndex].moneyGemReward;
                progressBarControl.IncreaseCoins((int)moneyReward);

                // Show the friend's response
                dialogueText.text = currentDialogue.choices[choiceIndex].friendResponse;

                // Update the coin count display after making a choice
                progressBarControl.UpdateCoinCountText();

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
