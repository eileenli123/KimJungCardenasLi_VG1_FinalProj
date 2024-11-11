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
        private Dialogue_copy currentDialogue;
        private Queue<string> sentences;
        private ProgressBarsControl progressBarControl;
        public delegate void DialogueEndHandler();
        public event DialogueEndHandler OnDialogueEnd;

        void Start()
        {
            sentences = new Queue<string>();
            progressBarControl = FindObjectOfType<ProgressBarsControl>();
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                int choiceIndex = i; 
                choiceButtons[i].onClick.AddListener(() => SelectChoice(choiceIndex));
                Debug.Log("Parent Position: " + choiceButtons[i].transform.parent.position);
                Debug.Log("Button Position: " + choiceButtons[i].GetComponent<RectTransform>().anchoredPosition);
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
        Debug.Log("Sentences left: " + sentences.Count);

        // Dequeue a sentence if there are any left
        string sentence = sentences.Dequeue();
        Debug.Log("Displayed sentence: " + sentence);
        
        dialogueText.text = sentence;

        // Log the updated count after dequeuing
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
                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = currentDialogue.choices[i].playerDialogue + 
                                    " (Cost: " + currentDialogue.choices[i].coinCost + " coins)";
                Debug.Log("Displaying choice: " + currentDialogue.choices[i].playerDialogue); // Log the choice being displayed
            }
        }


        public void SelectChoice(int choiceIndex)
        {
            Debug.Log("Button clicked: " + choiceIndex);
            int choiceCost = currentDialogue.choices[choiceIndex].coinCost;

            // Check if player has enough money for the selected choice
            if (progressBarControl.DecreaseCoins(choiceCost)) // Successfully deducted money
            {
                float socialReward = currentDialogue.choices[choiceIndex].socialGemReward;
                progressBarControl.IncreaseSocial(socialReward);

                float academicReward = currentDialogue.choices[choiceIndex].academicGemReward;
                progressBarControl.IncreaseGPA(academicReward);

                float moneyReward = currentDialogue.choices[choiceIndex].moneyGemReward;
                progressBarControl.IncreaseCoins((int)moneyReward);

                dialogueText.text = currentDialogue.choices[choiceIndex].friendResponse;

                // Update the coin count display after making a choice
                progressBarControl.UpdateCoinCountText();

                EndDialogue(); 
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
            
            OnDialogueEnd?.Invoke();

            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
