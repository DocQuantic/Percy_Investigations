using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllDialogues))]
public class AllDialoguesEditor : Editor {

    private static string[] allDialoguesNames;

    public static string[] AllDialoguesNames
    {
        get
        {
            // If the description array doesn't exist yet, set it.
            if (allDialoguesNames == null)
            {
                SetAllDialoguesNames();
            }
            return allDialoguesNames;
        }
        private set { allDialoguesNames = value; }
    }

    private DialogueEditor[] dialogueEditors;
    private AllDialogues allDialogues;
    private string newDialogueName = "New Dialogue";


    private const string creationPath = "Assets/Resources/AllDialogues.asset";
    private const float buttonWidth = 30.0f;

    private void OnEnable()
    {
        allDialogues = (AllDialogues)target;

        if (allDialogues.dialogues == null)
        {
            allDialogues.dialogues = new Dialogue[0];
        }

        if (dialogueEditors == null)
        {
            CreateEditors();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < dialogueEditors.Length; i++)
        {
            DestroyImmediate(dialogueEditors[i]);
        }

        dialogueEditors = null;
    }

    private static void SetAllDialoguesNames()
    {
        AllDialoguesNames = new string[TryGetDialoguesLength()];

        for (int i = 0; i < AllDialoguesNames.Length; i++)
        {
            AllDialoguesNames[i] = TryGetDialogueAt(i).name;
        }
    }

    public override void OnInspectorGUI()
    {
        if (dialogueEditors.Length != TryGetDialoguesLength())
        {
            for (int i = 0; i < dialogueEditors.Length; i++)
            {
                DestroyImmediate(dialogueEditors[i]);
            }

            CreateEditors();
        }

        for (int i = 0; i < dialogueEditors.Length; i++)
        {
            dialogueEditors[i].OnInspectorGUI();
        }

        if (TryGetDialoguesLength() > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();

        newDialogueName = EditorGUILayout.TextField(GUIContent.none, newDialogueName);

        if (GUILayout.Button("+", GUILayout.Width(buttonWidth)))
        {
            AddDialogue(newDialogueName);
            newDialogueName = "New Dialogue";
        }

        EditorGUILayout.EndHorizontal();
    }

    private void CreateEditors()
    {
        dialogueEditors = new DialogueEditor[allDialogues.dialogues.Length];

        for (int i = 0; i < TryGetDialoguesLength(); i++)
        {
            dialogueEditors[i] = CreateEditor(TryGetDialogueAt(i)) as DialogueEditor;
            dialogueEditors[i].editorType = DialogueEditor.EditorType.AllDialoguesAsset;
        }
    }

    [MenuItem("Assets/Create/AllDialogues")]
    private static void CreateAllDialoguesAsset()
    {
        if (AllDialogues.Instance)
        {
            return;
        }

        AllDialogues instance = CreateInstance<AllDialogues>();
        AssetDatabase.CreateAsset(instance, creationPath);

        AllDialogues.Instance = instance;

        instance.dialogues = new Dialogue[0];
    }

    private void AddDialogue(string dialogueName)
    {
        if (!AllDialogues.Instance)
        {
            Debug.Log("AllDialogues asset has not been created yet.");
            return;
        }

        Dialogue newDialogue = DialogueEditor.CreateDialogue(dialogueName);

        newDialogue.name = dialogueName;

        Undo.RecordObject(newDialogue, "Created new Dialogue.");

        AssetDatabase.AddObjectToAsset(newDialogue, AllDialogues.Instance);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newDialogue));

        ArrayUtility.Add(ref AllDialogues.Instance.dialogues, newDialogue);

        EditorUtility.SetDirty(AllDialogues.Instance);

        SetAllDialoguesNames();
    }

    public static void RemoveDialogue(Dialogue dialogue)
    {
        if (!AllDialogues.Instance)
        {
            Debug.Log("AllDialogues asset has not been created yet.");
            return;
        }

        Undo.RecordObject(dialogue, "Removed Dialogue.");

        ArrayUtility.Remove(ref AllDialogues.Instance.dialogues, dialogue);

        DestroyImmediate(dialogue, true);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(AllDialogues.Instance);

        SetAllDialoguesNames();
    }

    public static int TryGetDialogueIndex(Dialogue dialogue)
    {
        for (int i = 0; i < TryGetDialoguesLength(); i++)
        {
            if (dialogue.name == TryGetDialogueAt(i).name)
            {
                return i;
            }
        }

        return -1;
    }

    public static Dialogue TryGetDialogueAt(int index)
    {
        Dialogue[] allDialogues = AllDialogues.Instance.dialogues;

        if (allDialogues == null || allDialogues[0] == null)
        {
            return null;
        }

        if (index > allDialogues.Length)
        {
            return allDialogues[0];
        }

        return allDialogues[index];
    }

    public static int TryGetDialoguesLength()
    {
        if (AllDialogues.Instance.dialogues == null)
        {
            return 0;
        }

        return AllDialogues.Instance.dialogues.Length;
    }
}
