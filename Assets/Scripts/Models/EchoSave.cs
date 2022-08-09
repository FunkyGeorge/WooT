using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EchoSave
{
    public int index;
    public string author;
    [TextArea(3, 5)] public string statement;
}
