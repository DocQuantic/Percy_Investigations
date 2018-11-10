using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Sentence {

    [XmlAttribute("string")]
    public string sentence;

    [XmlAttribute("character")]
    public string character;

}
