using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Dialogue")]
public class DialogueXML {

    [XmlArray("Sentences")]
    [XmlArrayItem("Sentence")]
    public List<Sentence> sentences = new List<Sentence>();

    public static DialogueXML LoadDialogueFromXML(TextAsset textAsset)
    {
        TextAsset xml = textAsset;

        XmlSerializer serializer = new XmlSerializer(typeof(DialogueXML));

        StringReader reader = new StringReader(xml.text);

        DialogueXML dialogue = serializer.Deserialize(reader) as DialogueXML;

        reader.Close();

        if (dialogue == null)
            return null;

        return dialogue;
    }

}
