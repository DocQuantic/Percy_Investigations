using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacters : ScriptableObject {

    public Character[] characters;

    public static AllCharacters instance;

    private const string loadPath = "AllCharacters";

    public static AllCharacters Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<AllCharacters>();
            }
            if (!instance)
            {
                instance = Resources.Load<AllCharacters>(loadPath);
            }
            if (!instance)
            {
                Debug.LogError("AllCharacters has not been created yet.  Go to Assets > Create > AllCharacters.");
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public static Character FindCharacterByName(string characterName)
    {
        Character character = null;

        for(int i = 0; i < AllCharacters.Instance.characters.Length; i++)
        {
            if (instance.characters[i].characterName == characterName)
            {
                character = instance.characters[i];
            }
        }

        if (character == null)
        {
            return null;
        }

        return character;
    }

}
