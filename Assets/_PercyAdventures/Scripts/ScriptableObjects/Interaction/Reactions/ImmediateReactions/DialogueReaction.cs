using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReaction : Reaction {

    public Dialogue dialogue;

    protected override void ImmediateReaction()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

}
