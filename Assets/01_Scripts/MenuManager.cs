using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainPanel, LevelPanel, CreditsPanel, OptionsPanel;
    [SerializeField]
    TMPro.TMP_Text moneyText;

    [SerializeField] Slider soundSlider, musicSlider;
    [SerializeField] AudioMixer soundMixer, musicMixer;
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

    public void Credits()
    {
        MainPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void Options()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);

        float soundValue;
        soundMixer.GetFloat("Sound", out soundValue);
        soundValue = Mathf.Pow(10, (soundValue / 20));
        soundSlider.value = soundValue;

        float musicValue;
        musicMixer.GetFloat("Music", out musicValue);
        musicValue = Mathf.Pow(10, (musicValue / 20));
        musicSlider.value = musicValue;
    }

    public void BackToMain(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void SetSoundLevel(float sliderValue)
    {
        soundMixer.SetFloat("Sound", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void QuitGame()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }
}
