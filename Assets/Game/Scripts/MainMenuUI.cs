using System;
using System.Linq;
using DanielLochner.Assets.SimpleScrollSnap;
using Game.Scripts.Utils;
using MoreMountains.HighroadEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [Header("Game UI")] 
    [SerializeField] private GameObject startScreenUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject mapSelectionUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject guideUI;

    [Space(3)] [SerializeField] private Button enterGameButton;
    [SerializeField] private Button mapSelectionButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button raceButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button guideButton;
    
    [Space]
    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] SimpleScrollSnap mapSelectionSnap;

    private int mapSelected = 0;
    
    private void Start()
    {
        InitUIFirst();
        InitButtonListener();
        mapSelectionSnap.OnPanelCentered.AddListener(OnMapCentered);
        LocalDataPlayer.Instance.OnGoldChanged += OnGoldUpdate;
        OnGoldUpdate(LocalDataPlayer.Instance.GetGold());
    }

    private void OnDestroy()
    {
        LocalDataPlayer.Instance.OnGoldChanged -= OnGoldUpdate;
    }

    private void InitButtonListener()
    {
        enterGameButton.onClick.AddListener(EnterGameButtonOnClick);
        mapSelectionButton.onClick.AddListener(MapSelectionButtonOnClick);
        settingButton.onClick.AddListener(SettingButtonOnClick);
        raceButton.onClick.AddListener(StartRaceButtonOnClick);
        shopButton.onClick.AddListener(ShopButtonOnClick);
        guideButton.onClick.AddListener(GuideButtonOnClick);
    }

    private void GuideButtonOnClick()
    {
        guideUI.SetActive(true);
    }

    private void ShopButtonOnClick()
    {
        shopUI.SetActive(true);
    }

    private void StartRaceButtonOnClick()
    {
        string mapName = LocalLobbyManager.Instance.AvailableTracksSceneName[mapSelected];
        string currentCar = LocalDataPlayer.Instance.LocalData.CurrentSelectedCar;
        int currentPosition = 0;

        var listCars = LocalLobbyManager.Instance.AvailableVehiclesPrefabs;
        var car = listCars.FirstOrDefault(x => x.gameObject.name == currentCar);
        LocalLobbyManager.Instance.AddPlayer(new LocalLobbyPlayer
        {
            Position = currentPosition,
            Name = "Me",
            VehicleName = car.name,
            VehicleSelectedIndex = -1,
            IsBot = false
        });
        
        currentPosition++;
        var botCars = ListUtils.GetRandomElements<GameObject>(listCars.ToList(), 3);
        foreach (var bot in botCars)
        {
            if (bot.GetComponent<BaseController>() != null && bot.GetComponent<VehicleAI>() != null)
            {
                LocalLobbyManager.Instance.AddPlayer(new LocalLobbyPlayer
                {
                    Position = currentPosition,
                    Name = UsernameGenerator.Generate(),
                    VehicleName = bot.name,
                    VehicleSelectedIndex = -1,
                    IsBot = true
                });
                currentPosition++;
            }
        }
        LoadingSceneManager.LoadScene(mapName);
    }

    private void InitUIFirst()
    {
        bool isGameStart = LocalDataPlayer.Instance.IsGameStarted;
        startScreenUI.SetActive(!isGameStart);
        mainMenuUI.SetActive(isGameStart);
    }

    private void EnterGameButtonOnClick()
    {
        LocalDataPlayer.Instance.IsGameStarted = true;
        startScreenUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    private void SettingButtonOnClick()
    {
        settingsUI.SetActive(true);
    }

    private void MapSelectionButtonOnClick()
    {
        Debug.Log("map SelectionUI");
        mapSelectionUI.SetActive(true);
    }
    
    private void OnMapCentered(int centeredPanel, int selectedPanel)
    {
        mapSelected = centeredPanel;
    }

    private void OnGoldUpdate(int gold)
    {
        goldText.SetText(StringUltils.FormatNumber(gold));
    }
}