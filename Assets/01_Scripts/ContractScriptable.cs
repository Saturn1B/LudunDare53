using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Contract", menuName = "ScriptableObjects/Contract")]
public class ContractScriptable : ScriptableObject
{
    public string contractTitle;
    public float contractGain;
    public ContractType contractType;
    public BoxType[] boxTypes;
    public bool done;

    public float hours;
    public float mins;
    public float secs;
}

public enum ContractType
{
    EASY,
    MEDIUM,
    HARD
}

[System.Serializable]
public class BoxType
{
    public string boxName;
    public GameObject boxPrefab;
    public Vector2[] boxPos;
}
