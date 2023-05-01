using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : MonoBehaviour
{
    public ContractScriptable[] myContract;

    private static SOManager instance = null;
    public static SOManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
