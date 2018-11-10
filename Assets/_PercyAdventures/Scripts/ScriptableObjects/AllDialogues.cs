using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDialogues : ScriptableObject {

    public Dialogue[] dialogues;

    public static AllDialogues instance;

    private const string loadPath = "AllDialogues";

    public static AllDialogues Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<AllDialogues>();
            }
            if (!instance)
            {
                instance = Resources.Load<AllDialogues>(loadPath);
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

}
