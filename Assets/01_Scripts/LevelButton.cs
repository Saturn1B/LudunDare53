using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    float moneyNeeded;
    [SerializeField]
    int sceneToLoad;

    [SerializeField]
    GameObject lockLevel;

    private void Awake()
    {
        lockLevel = transform.GetChild(1).gameObject;
    }

    public void LaunchLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnEnable()
    {
        if(!lockLevel)
            lockLevel = transform.GetChild(1).gameObject;

        float currentMoney = PlayerPrefs.HasKey("PlayerMoney") ? PlayerPrefs.GetFloat("PlayerMoney") : 0;

        if (currentMoney >= moneyNeeded)
        {
            lockLevel.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            lockLevel.SetActive(true);
            GetComponent<Button>().interactable = false;
            lockLevel.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = moneyNeeded + "€";
        }
    }
}
