using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractButton : MonoBehaviour
{
    public List<GameObject> boxToSpawn = new List<GameObject>();
    public List<Vector2> placeToSpawn = new List<Vector2>();
    public ContractScriptable contract;

    public void SelectContract()
    {
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.hasControl = true;
        GameManager.Instance.currentContract = contract;
        for (int i = 0; i < boxToSpawn.Count; i++)
        {
            GameObject box = Instantiate(boxToSpawn[i], new Vector3(0, 4 + placeToSpawn[i].y, 1.5f * placeToSpawn[i].x), boxToSpawn[i].transform.rotation);
        }
        FindObjectOfType<LevelTimer>().isCounting = true;
    }
}
