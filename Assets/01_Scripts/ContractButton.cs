using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractButton : MonoBehaviour
{
    public List<GameObject> boxToSpawn = new List<GameObject>();
    public List<Vector2> placeToSpawn = new List<Vector2>();

    public void SelectContract()
    {
        transform.parent.gameObject.SetActive(false);
        //TO DO start game
        for (int i = 0; i < boxToSpawn.Count; i++)
        {
            GameObject box = Instantiate(boxToSpawn[i], new Vector3(placeToSpawn[i].x, placeToSpawn[i].y, 0), Quaternion.identity);
        }
    }
}
