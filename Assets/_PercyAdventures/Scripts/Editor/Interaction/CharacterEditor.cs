using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor {

    public enum EditorType
    {
        CharacterAsset, AllCharactersAsset
    }

    public EditorType editorType;

    private Character character;

    public SerializedProperty characterNameProperty;
    public SerializedProperty idPictureProperty;
    public SerializedProperty descriptionProperty;
    public SerializedProperty hashProperty;

    private const string characterPropCharacterNameName = "characterName";
    private const string characterPropIdPictureName = "idPicture";
    private const string characterPropDescriptionName = "description";
    private const string characterPropHashName = "hash";
    private const float characterButtonWidth = 30.0f;
    private const int descriptionWidthPadding = 60;
    private const float descriptionHeightPadding = 1;

    private void OnEnable()
    {
        character = (Character)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        characterNameProperty = serializedObject.FindProperty(characterPropCharacterNameName);
        idPictureProperty = serializedObject.FindProperty(characterPropIdPictureName);
        descriptionProperty = serializedObject.FindProperty(characterPropDescriptionName);
        hashProperty = serializedObject.FindProperty(characterPropHashName);
    }

    public override void OnInspectorGUI()
    {
        switch (editorType)
        {
            case EditorType.AllCharactersAsset:
                AllCharactersAssetGUI();
                break;
            case EditorType.CharacterAsset:
                CharacterAssetGUI();
                break;
            default:
                throw new UnityException("Unknown CharacterEditor.EditorType.");
        }
    }

    private void AllCharactersAssetGUI()
    {
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUI.indentLevel++;

        // Display the description of the Condition.
        EditorGUILayout.LabelField(character.characterName);

        // Display a button showing a '-' that if clicked removes this Condition from the AllConditions asset.
        if (GUILayout.Button("-", GUILayout.Width(characterButtonWidth)))
            AllCharactersEditor.RemoveCharacter(character);

        EditorGUI.indentLevel--;
        EditorGUILayout.EndHorizontal();
    }

    private void CharacterAssetGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.LabelField(characterNameProperty.stringValue, EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        idPictureProperty.objectReferenceValue = EditorGUILayout.ObjectField("Picture", idPictureProperty.objectReferenceValue, typeof(Sprite), false) as Sprite;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Biography");
        GUIStyle descriptionStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true,
            fixedWidth = Screen.width - descriptionWidthPadding
        };
        descriptionStyle.fixedHeight = descriptionStyle.CalcHeight(new GUIContent(descriptionProperty.stringValue), Screen.width) * descriptionHeightPadding;
        descriptionProperty.stringValue = EditorGUILayout.TextArea(descriptionProperty.stringValue, descriptionStyle);

        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    public static Character CreateCharacter()
    {
        Character newCharacter = CreateInstance<Character>();

        string blankCharacterName = "No Name set.";

        Character globalCharacter = AllCharactersEditor.TryGetCharacterAt(0);
        newCharacter.characterName = globalCharacter != null ? globalCharacter.characterName : blankCharacterName;

        SetHash(newCharacter);
        return newCharacter;
    }

    public static Character CreateCharacter(string characterName)
    {
        Character newCharacter = CreateInstance<Character>();

        newCharacter.characterName = characterName;
        SetHash(newCharacter);
        return newCharacter;
    }

    private static void SetHash(Character character)
    {
        character.hash = Animator.StringToHash(character.characterName);
    }

}
