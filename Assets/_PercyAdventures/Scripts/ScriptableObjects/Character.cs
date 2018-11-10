using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : ScriptableObject {

    public string characterName;
    public Sprite idPicture;
    [TextArea(3, 10)]
    public string description;
    public int hash;

}
