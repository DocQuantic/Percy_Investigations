using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SceneReaction))]
public class SceneReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Scene Reaction";
    }

}
