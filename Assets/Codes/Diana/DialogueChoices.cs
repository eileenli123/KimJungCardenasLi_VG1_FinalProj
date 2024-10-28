using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana{
[System.Serializable]
    public class DialogueChoice
    {
        public string playerDialogue; 
        public int coinCost;   
        public int socialGemReward; 
        public float academicGemReward;
        public float healthGemReward;
        public float moneyGemReward; 
        public string friendResponse; 
    }
}