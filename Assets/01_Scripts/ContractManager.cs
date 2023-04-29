using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractManager : MonoBehaviour
{
    [SerializeField]
    ContractScriptable[] contracts;
    [SerializeField]
    GameObject contractPrefab;
    [SerializeField]
    GameObject boxTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach(ContractScriptable contract in contracts)
        {
            GameObject go = Instantiate(contractPrefab);
            go.transform.SetParent(transform);
            go.GetComponent<ContractButton>().contract = contract;
            go.transform.GetChild(0).GetComponent<TMP_Text>().text = "Contract " + contract.contractTitle;
            go.transform.GetChild(3).GetComponent<TMP_Text>().text = "GAIN : " + contract.contractGain + "€";
            if (!contract.done)
            {
                go.transform.GetChild(4).gameObject.SetActive(false);
                go.GetComponent<Button>().interactable = true;
            }
            foreach(BoxType box in contract.boxTypes)
            {
                GameObject go2 = Instantiate(boxTextPrefab);
                go2.transform.SetParent(go.transform.GetChild(2).transform);
                go2.GetComponent<TMP_Text>().text = "-" + box.boxName + " : x" + box.boxPos.Length;
                for (int i = 0; i < box.boxPos.Length; i++)
                {
                    go.GetComponent<ContractButton>().boxToSpawn.Add(box.boxPrefab);
                    go.GetComponent<ContractButton>().placeToSpawn.Add(box.boxPos[i]);
                }
            }
        }
    }
}
