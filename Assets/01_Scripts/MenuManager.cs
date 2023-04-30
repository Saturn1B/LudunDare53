using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainPanel, LevelPanel;
    [SerializeField]
    TMPro.TMP_Text moneyText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelSelection()
    {
        MainPanel.SetActive(false);
        LevelPanel.SetActive(true);
        moneyText.text = (PlayerPrefs.HasKey("PlayerMoney") ? PlayerPrefs.GetFloat("PlayerMoney") : 0) + "€";
    }

    public void BackToMain(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
