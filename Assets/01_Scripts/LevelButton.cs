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

    [SerializeField] float hours;
    [SerializeField] float mins;
    [SerializeField] float secs;

    [SerializeField] ContractScriptable[] levelContract;

    private void Awake()
    {
        lockLevel = transform.GetChild(1).gameObject;
    }

    public void LaunchLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void FindBestTime()
    {
        bool noneDone = true;
        List<ContractScriptable> doneContract = new List<ContractScriptable>();
        foreach (ContractScriptable contract in levelContract)
        {
            if (contract.done)
            {
                noneDone = false;
                doneContract.Add(contract);
            }
        }

        if (noneDone)
        {
            transform.GetChild(2).gameObject.SetActive(false);
            return;
        }

        transform.GetChild(2).gameObject.SetActive(true);
        TMPro.TMP_Text bestTimeText = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();

        Debug.Log(doneContract.Count);

        //HOUR
        List<ContractScriptable> bestHourContract = new List<ContractScriptable>();
        foreach (ContractScriptable contract in doneContract)
        {
            if (bestHourContract.Count == 0)
            {
                bestHourContract.Add(contract);
            }
            else
            {
                /*foreach(ContractScriptable c in bestHourContract)
                {
                    if(contract.hours < c.hours)
                    {
                        bestHourContract.Clear();
                        bestHourContract.Add(contract);
                        break;
                    }
                    else if(contract.hours == c.hours)
                    {
                        bestHourContract.Add(contract);
                    }
                }*/
                for (int i = 0; i < bestHourContract.Count; i++)
                {
                    if(contract.hours < bestHourContract[i].hours)
                    {
                        bestHourContract.Clear();
                        bestHourContract.Add(contract);
                        break;
                    }
                    else if(contract.hours == bestHourContract[i].hours)
                    {
                        bestHourContract.Add(contract);
                        break;
                    }
                }
            }
        }

        Debug.Log(bestHourContract.Count);


        //MIN
        List<ContractScriptable> bestMinContract = new List<ContractScriptable>();
        foreach (ContractScriptable contract in bestHourContract)
        {
            if (bestMinContract.Count == 0)
            {
                bestMinContract.Add(contract);
            }
            else
            {
                for (int i = 0; i < bestMinContract.Count; i++)
                {
                    if (contract.mins < bestMinContract[i].mins)
                    {
                        bestMinContract.Clear();
                        bestMinContract.Add(contract);
                        break;
                    }
                    else if (contract.mins == bestMinContract[i].mins)
                    {
                        bestMinContract.Add(contract);
                        break;
                    }
                }
            }
        }

        Debug.Log(bestMinContract.Count);


        //SEC
        List<ContractScriptable> bestSecContract = new List<ContractScriptable>();
        foreach (ContractScriptable contract in bestMinContract)
        {
            if (bestSecContract.Count == 0)
            {
                bestSecContract.Add(contract);
            }
            else
            {
                for (int i = 0; i < bestSecContract.Count; i++)
                {
                    if (contract.secs < bestSecContract[i].secs)
                    {
                        bestSecContract.Clear();
                        bestSecContract.Add(contract);
                        Debug.Log("better time found");
                        break;
                    }
                    else if (contract.secs == bestSecContract[i].secs)
                    {
                        bestSecContract.Add(contract);
                        break;
                    }
                }
            }
        }

        hours = bestSecContract[0].hours;
        mins = bestSecContract[0].mins;
        secs = bestSecContract[0].secs;

        if (hours > 0)
            bestTimeText.text = hours.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00.00");
        else
            bestTimeText.text = mins.ToString("00") + ":" + secs.ToString("00.00");
    }

    private void OnEnable()
    {
        FindBestTime();

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
