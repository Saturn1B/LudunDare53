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
    [SerializeField]
    GameObject LoosePanel, WinPanel;

    public void LooseGame()
    {
        FindObjectOfType<SimpleCarController>().Brake();
        hasControl = false;
        Time.timeScale = .4f;
        currentContract = null;
        LoosePanel.SetActive(true);
    }

    public void WinGame()
    {
        FindObjectOfType<SimpleCarController>().Brake();
        hasControl = false;
        Time.timeScale = .4f;
        currentContract.done = true;
        WinPanel.SetActive(true);
        WinPanel.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text += currentContract.contractGain + "€";
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
