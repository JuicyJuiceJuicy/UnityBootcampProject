using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Dialogues", menuName = "Datum/Dialogues")]
public class Dialogues : ScriptableObject
{
    [FormerlySerializedAs("dialogue")]
    public Dialogue[] dialogue;
    public List<Dialogue> list;

    public class Dialogue
    {
        public float time;
        public string ko;
    }
}
