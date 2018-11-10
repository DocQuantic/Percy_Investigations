using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReaction : Reaction {

    private SceneController sceneController;

    public string sceneName;

    protected override void SpecificInit()
    {
        sceneController = SceneController.instance;
    }


    protected override void ImmediateReaction()
    {
        sceneController.FadeAndLoadScene(this);
    }

}
