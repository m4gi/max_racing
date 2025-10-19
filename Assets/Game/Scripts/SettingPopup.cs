using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    
    [SerializeField] private GameObject soundOnGameObject;
    [SerializeField] private GameObject soundOffGameObject;
    [SerializeField] private GameObject musicOnGameObject;
    [SerializeField] private GameObject musicOffGameObject;
    
    private LocalDataPlayer LocalData => LocalDataPlayer.Instance;

    void Start()
    {
        soundButton.onClick.AddListener(SoundButtonOnClick);
        musicButton.onClick.AddListener(MusicButtonOnClick);
        LocalData.OnSoundChanged += UpdateStatus;
    }

    private void OnEnable()
    {
        bool isSoundOn = LocalData.GetSound;
        bool isMusicOn = LocalData.GetMusic;
        UpdateStatus(isSoundOn, isMusicOn);
    }

    private void OnDestroy()
    {
        soundButton.onClick.RemoveListener(SoundButtonOnClick);
        musicButton.onClick.RemoveListener(MusicButtonOnClick);
        LocalData.OnSoundChanged -= UpdateStatus;
    }

    private void MusicButtonOnClick()
    {
        LocalData.SetMusic(!LocalData.GetMusic);
    }

    private void SoundButtonOnClick()
    {
        LocalData.SetSound(!LocalData.GetSound);
    }

    private void UpdateStatus(bool isSoundOn, bool isMusicOn)
    {
        soundOnGameObject.SetActive(isSoundOn);
        soundOffGameObject.SetActive(!isSoundOn);
        
        musicOnGameObject.SetActive(isMusicOn);
        musicOffGameObject.SetActive(!isMusicOn);
    }
}