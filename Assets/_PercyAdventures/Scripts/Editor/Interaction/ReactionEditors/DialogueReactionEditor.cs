using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(DialogueReaction))]
public class DialogueReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Dialogue Reaction";
    }

}
