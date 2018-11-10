using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour {
    
    public GameObject questionMenu;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.instance;
        dialogueManager.OnDialogueStopCallback += DisplayQuestions;
    }

    private void DisplayQuestions()
    {
        questionMenu.SetActive(true);
    }
}
