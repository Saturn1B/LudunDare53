using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public ContractScriptable currentContract;
    public bool hasControl;
    bool win;
    bool loose;
    [SerializeField]
    GameObject LoosePanel, WinPanel;

    public void LooseGame()
    {
        if (win)
            return;

        loose = true;
        FindObjectOfType<SimpleCarController>().Brake();
        hasControl = false;
        Time.timeScale = .4f;
        currentContract = null;
        FindObjectOfType<LevelTimer>().isCounting = false;
        LoosePanel.SetActive(true);
    }

    public void WinGame()
    {
        if (loose)
            return;

        LevelTimer levelTimer = FindObjectOfType<LevelTimer>();

        win = true;
        hasControl = false;
        Time.timeScale = .4f;

        FindObjectOfType<SimpleCarController>().Brake();
        levelTimer.isCounting = false;
        if (currentContract.done)
        {
            if(currentContract.hours > levelTimer.curruntHourTime)
            {
                currentContract.hours = levelTimer.curruntHourTime;
                currentContract.mins = levelTimer.curruntMinTime;
                currentContract.secs = levelTimer.curruntTime;
            }
            else if(currentContract.hours == levelTimer.curruntHourTime)
            {
                if (currentContract.mins > levelTimer.curruntMinTime)
                {
                    currentContract.hours = levelTimer.curruntHourTime;
                    currentContract.mins = levelTimer.curruntMinTime;
                    currentContract.secs = levelTimer.curruntTime;
                }
                else if (currentContract.mins == levelTimer.curruntMinTime)
                {
                    if (currentContract.secs > levelTimer.curruntTime)
                    {
                        currentContract.hours = levelTimer.curruntHourTime;
                        currentContract.mins = levelTimer.curruntMinTime;
                        currentContract.secs = levelTimer.curruntTime;
                    }
                }
            }

            WinPanel.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text +=  "0€";
        }
        else
        {
            currentContract.hours = levelTimer.curruntHourTime;
            currentContract.mins = levelTimer.curruntMinTime;
            currentContract.secs = levelTimer.curruntTime;

            PlayerPrefs.SetFloat("PlayerMoney", PlayerPrefs.HasKey("PlayerMoney") ? PlayerPrefs.GetFloat("PlayerMoney") + currentContract.contractGain : currentContract.contractGain);
            WinPanel.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text += currentContract.contractGain + "€";
        }

        currentContract.done = true;
        WinPanel.SetActive(true);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
