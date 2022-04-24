using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObject/Config", order = 1)]
public class ConfigScriptableObject : ScriptableObject
{
    public bool isDebug;
    public bool isDemo;
}
