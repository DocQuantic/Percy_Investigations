using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(AllCharacters))]
public class AllCharactersEditor : Editor {

    private static string[] allCharactersNames;

    public static string[] AllCharactersNames
    {
        get
        {
            // If the description array doesn't exist yet, set it.
            if (allCharactersNames == null)
            {
                SetAllCharactersNames();
            }
            return allCharactersNames;
        }
        private set { allCharactersNames = value; }
    }

    private CharacterEditor[] characterEditors;
    private AllCharacters allCharacters;
    private string newCharacterName = "New Character";


    private const string creationPath = "Assets/Resources/AllCharacters.asset";
    private const float buttonWidth = 30.0f;

    private void OnEnable()
    {
        allCharacters = (AllCharacters)target;

        if (allCharacters.characters == null)
        {
            allCharacters.characters = new Character[0];
        }

        if (characterEditors == null)
        {
            CreateEditors();
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < characterEditors.Length; i++)
        {
            DestroyImmediate(characterEditors[i]);
        }

        characterEditors = null;
    }

    private static void SetAllCharactersNames()
    {
        AllCharactersNames = new string[TryGetCharactersLength()];

        for(int i = 0; i < AllCharactersNames.Length; i++)
        {
            AllCharactersNames[i] = TryGetCharacterAt(i).characterName;
        }
    }

    public override void OnInspectorGUI()
    {
        if (characterEditors.Length != TryGetCharactersLength())
        {
            for(int i = 0; i < characterEditors.Length; i++)
            {
                DestroyImmediate(characterEditors[i]);
            }

            CreateEditors();
        }

        for(int i = 0; i < characterEditors.Length; i++)
        {
            characterEditors[i].OnInspectorGUI();
        }

        if (TryGetCharactersLength() > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();

        newCharacterName = EditorGUILayout.TextField(GUIContent.none, newCharacterName);

        if(GUILayout.Button("+", GUILayout.Width(buttonWidth)))
        {
            AddCharacter(newCharacterName);
            newCharacterName = "New Character";
        }

        EditorGUILayout.EndHorizontal();
    }

    private void CreateEditors()
    {
        characterEditors = new CharacterEditor[allCharacters.characters.Length];

        for(int i = 0; i < TryGetCharactersLength(); i++)
        {
            characterEditors[i] = CreateEditor(TryGetCharacterAt(i)) as CharacterEditor;
            characterEditors[i].editorType = CharacterEditor.EditorType.AllCharactersAsset;
        }
    }

    [MenuItem("Assets/Create/AllCharacters")]
    private static void CreateAllCharactersAsset()
    {
        if (AllCharacters.Instance)
        {
            return;
        }

        AllCharacters instance = CreateInstance<AllCharacters>();
        AssetDatabase.CreateAsset(instance, creationPath);

        AllCharacters.Instance = instance;

        instance.characters = new Character[0];
    }

    private void AddCharacter(string characterName)
    {
        if (!AllCharacters.Instance)
        {
            Debug.Log("AllCharacters asset has not been created yet.");
            return;
        }

        Character newCharacter = CharacterEditor.CreateCharacter(characterName);

        newCharacter.name = characterName;

        Undo.RecordObject(newCharacter, "Created new Character.");

        AssetDatabase.AddObjectToAsset(newCharacter, AllCharacters.Instance);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newCharacter));

        ArrayUtility.Add(ref AllCharacters.Instance.characters, newCharacter);

        EditorUtility.SetDirty(AllCharacters.Instance);

        SetAllCharactersNames();
    }

    public static void RemoveCharacter(Character character)
    {
        if (!AllCharacters.Instance)
        {
            Debug.Log("AllCharacters asset has not been created yet.");
            return;
        }

        Undo.RecordObject(character, "Removed Character.");

        ArrayUtility.Remove(ref AllCharacters.Instance.characters, character);

        DestroyImmediate(character, true);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(AllCharacters.Instance);

        SetAllCharactersNames();
    }

    public static int TryGetCharacterIndex(Character character)
    {
        for (int i = 0; i < TryGetCharactersLength(); i++)
        {
            if (character.hash == TryGetCharacterAt(i).hash)
            {
                return i;
            }
        }

        return -1;
    }

    public static Character TryGetCharacterAt(int index)
    {
        Character[] allCharacters = AllCharacters.Instance.characters;

        if (allCharacters == null || allCharacters[0] == null)
        {
            return null;
        }

        if (index > allCharacters.Length)
        {
            return allCharacters[0];
        }

        return allCharacters[index];
    }

    public static int TryGetCharactersLength()
    {
        if (AllCharacters.Instance.characters == null)
        {
            return 0;
        }

        return AllCharacters.Instance.characters.Length;
    }

}
