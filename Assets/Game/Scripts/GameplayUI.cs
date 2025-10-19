    using MoreMountains.HighroadEngine;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button confirmPopupButton;
    
    [SerializeField] private GameObject confirmPopup;
    [SerializeField] private Button yesButton;
    
    void Start()
    {
        backToMenuButton.onClick.AddListener(ReturnMainMenu);
        confirmPopupButton.onClick.AddListener(ShowConfirmPopupOnClick);
        yesButton.onClick.AddListener(ReturnMainMenu);
    }

    private void ShowConfirmPopupOnClick()
    {
        confirmPopup.SetActive(true);
    }

    private void ReturnMainMenu()
    {
        LoadingSceneManager.LoadScene("MainMenu");
    }
}
