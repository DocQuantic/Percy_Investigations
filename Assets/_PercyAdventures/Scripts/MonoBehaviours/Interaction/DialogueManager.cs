using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public delegate void onDialogueChanged(string sentence, Character npc);
    public onDialogueChanged OnDialogueChangedCallback;
    public delegate void onDialogueStop();
    public onDialogueStop OnDialogueStopCallback;

    #region Singleton

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private PlayerController playerController;

    private Queue<string> sentences;
    private Queue<Character> characters;

    private void Start()
    {
        sentences = new Queue<string>();
        characters = new Queue<Character>();
    }

    private void OnEnable()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        else
        {
            playerController = null;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController = null;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerController.isInteracting = true;

        sentences.Clear();
        characters.Clear();

        for(int i=0; i < dialogue.sentences.Count; i++)
        {
            string sentence = dialogue.sentences[i];
            Character character = dialogue.characters[i];

            sentences.Enqueue(sentence);
            characters.Enqueue(character);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            StopDialogue();
            return;
        }

        string currentSentence = sentences.Dequeue();
        Character currentCharacter = characters.Dequeue();
        OnDialogueChangedCallback.Invoke(currentSentence, currentCharacter);
    }

    public void StopDialogue()
    {
        OnDialogueStopCallback.Invoke();
        playerController.isInteracting = false;
    }

}
