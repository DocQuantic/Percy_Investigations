using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    public enum EditorType
    {
        DialogueAsset, AllDialoguesAsset
    }

    public EditorType editorType;

    private Dialogue dialogue;

    private SerializedProperty dialogueTextAssetProperty;
    private SerializedProperty dialogueSentencesProperty;

    private const string dialoguePropTextAssetName = "textFile";
    private const string dialoguePropCharactersName = "characters";
    private const string dialoguePropSentencesName = "sentences";
    private const float descriptionWidthPadding = 65.0f;
    private const float descriptionHeightPadding = 1.0f;
    private const float dialogueButtonWidth = 30.0f;

    private void OnEnable()
    {
        dialogue = (Dialogue)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        dialogueTextAssetProperty = serializedObject.FindProperty(dialoguePropTextAssetName);
        dialogueSentencesProperty = serializedObject.FindProperty(dialoguePropSentencesName);
    }

    public override void OnInspectorGUI()
    {
        switch (editorType)
        {
            case EditorType.AllDialoguesAsset:
                AllDialoguesAssetGUI();
                break;
            case EditorType.DialogueAsset:
                DialogueAssetGUI();
                break;
            default:
                throw new UnityException("Unknown CharacterEditor.EditorType.");
        }
    }

    private void AllDialoguesAssetGUI()
    {
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUI.indentLevel++;

        // Display the description of the Condition.
        EditorGUILayout.LabelField(dialogue.name);

        // Display a button showing a '-' that if clicked removes this Condition from the AllConditions asset.
        if (GUILayout.Button("-", GUILayout.Width(dialogueButtonWidth)))
            AllDialoguesEditor.RemoveDialogue(dialogue);

        EditorGUI.indentLevel--;
        EditorGUILayout.EndHorizontal();
    }

    private void DialogueAssetGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUI.indentLevel++;

        dialogueTextAssetProperty.objectReferenceValue = EditorGUILayout.ObjectField("Text File", dialogueTextAssetProperty.objectReferenceValue, typeof(TextAsset), false) as TextAsset;
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Dialogue"))
        {
            if (dialogueTextAssetProperty.objectReferenceValue == null)
            {
                dialogue.characters = null;
                dialogue.sentences = null;
            }
            else
            {
                GenerateDialogueFromXML((TextAsset)dialogueTextAssetProperty.objectReferenceValue);
            }
        }
        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

        for (int i = 0; i < dialogueSentencesProperty.arraySize; i++)
        {
            DialogueElementGUI(i);
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private void DialogueElementGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField(dialogue.characters[index].characterName, EditorStyles.boldLabel);
        GUIStyle sentenceStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true,
            fixedWidth = Screen.width - descriptionWidthPadding
        };
        sentenceStyle.fixedHeight = sentenceStyle.CalcHeight(new GUIContent(dialogue.sentences[index]), Screen.width) * descriptionHeightPadding;
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField(dialogue.sentences[index], sentenceStyle);
        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
    }

    public void GenerateDialogueFromXML(TextAsset textAsset)
    {
        DialogueXML dialogueXML = DialogueXML.LoadDialogueFromXML(textAsset);

        dialogue.characters = new List<Character>();
        dialogue.sentences = new List<string>();

        if (dialogueXML == null)
            return;

        dialogue.name = textAsset.name;
        for (int i = 0; i < dialogueXML.sentences.Count; i++)
        {
            Character currentCharacter = AllCharacters.FindCharacterByName(dialogueXML.sentences[i].character);
            if (currentCharacter == null)
                throw new UnityException(dialogueXML.sentences[i].character + " was not created yet.");
            dialogue.characters.Add(currentCharacter);
            dialogue.sentences.Add(dialogueXML.sentences[i].sentence);
        }

        dialogueXML = null;
    }

    public static Dialogue CreateDialogue()
    {
        Dialogue newDialogue = CreateInstance<Dialogue>();

        string blankDialogueName = "No Name set.";

        Dialogue globalDialogue = AllDialoguesEditor.TryGetDialogueAt(0);
        newDialogue.name = globalDialogue != null ? globalDialogue.name : blankDialogueName;

        return newDialogue;
    }

    public static Dialogue CreateDialogue(string dialogueName)
    {
        Dialogue newDialogue = CreateInstance<Dialogue>();

        newDialogue.name = dialogueName;
        return newDialogue;
    }

}
