using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Echo", menuName = "ScriptableObject/Echo", order = 4)]
public class EchoScriptableObject : ScriptableObject
{
    public EchoSave[] echosaves;
}
