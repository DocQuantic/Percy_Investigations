using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour {

    public GameObject dialogue;
    public GameObject dialogueText;
    public Text characterName;
    public Image characterPicture;
    public Text dialogueSentence;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.instance;
        dialogueManager.OnDialogueChangedCallback += UpdateDialogue;
        dialogueManager.OnDialogueStopCallback += StopDialogue;
    }

    private void UpdateDialogue(string sentence, Character character)
    {
        if (!dialogue.activeSelf)
        {
            dialogue.SetActive(true);
        }

        characterName.text = character.characterName;
        characterPicture.sprite = character.idPicture;
        dialogueSentence.text = sentence;
    }

    private void StopDialogue()
    {
        dialogueText.SetActive(false);
    }
}
