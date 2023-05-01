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

    private void Awake()
    {
        if (Screen.width != 1920)
        {
            GridLayoutGroup grid = GetComponent<GridLayoutGroup>();

            Debug.Log(Screen.width);
            int xCoor = Mathf.FloorToInt(Screen.width * 0.15625f);
            int yCoor = Mathf.FloorToInt(xCoor * 1.3f);
            grid.cellSize = new Vector2(xCoor, yCoor);
            grid.enabled = false;
            grid.enabled = true;

            int yCoor2 = Mathf.FloorToInt(xCoor * .216f);
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(xCoor, yCoor2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        foreach (ContractScriptable contract in contracts)
        {
            GameObject go = Instantiate(contractPrefab);
            go.transform.SetParent(transform);
            go.GetComponent<ContractButton>().contract = contract;
            go.transform.GetChild(0).GetComponent<TMP_Text>().text = "CONTRACT " + '\n' + contract.contractTitle;
            if (!contract.done)
                go.transform.GetChild(3).GetComponent<TMP_Text>().text = "GAIN : " + contract.contractGain + "€";
            else
                go.transform.GetChild(3).GetComponent<TMP_Text>().text = "ALREADY COMPLETED";

            go.transform.GetChild(4).gameObject.SetActive(false);
            go.GetComponent<Button>().interactable = true;

            if (Screen.width != 1920)
            {
                float spacing = Screen.width * 0.0015f;

                go.transform.GetChild(2).GetComponent<VerticalLayoutGroup>().spacing = spacing;
            }

            foreach (BoxType box in contract.boxTypes)
            {
                GameObject go2 = Instantiate(boxTextPrefab);
                go2.transform.SetParent(go.transform.GetChild(2).transform);
                go2.GetComponent<TMP_Text>().text = "-" + box.boxName + '\n' + "x" + box.boxPos.Length;
                for (int i = 0; i < box.boxPos.Length; i++)
                {
                    go.GetComponent<ContractButton>().boxToSpawn.Add(box.boxPrefab);
                    go.GetComponent<ContractButton>().placeToSpawn.Add(box.boxPos[i]);
                }

                if (Screen.width != 1920)
                {
                    int xCoor = Mathf.FloorToInt(Screen.width * 0.145f);
                    int yCoor = Mathf.FloorToInt(xCoor * 0.20f);

                    go2.GetComponent<RectTransform>().sizeDelta = new Vector2(xCoor, yCoor);
                }
            }
        }
    }
}
