using System;
using DanielLochner.Assets.SimpleScrollSnap;
using Game.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mapTitle;
    [SerializeField] private Button startButton;

    [SerializeField] private SimpleScrollSnap mapScrollSnap;
    [SerializeField] private RawImage mapRawImageTemplate;

    private MapDataSO MapData => LocalDataPlayer.Instance.MapData;
    
    private bool isInitialized = false;

    private void Start()
    {
        InitMap();
        mapScrollSnap.OnPanelCentered.AddListener(OnMapCentered);
    }

    private void OnEnable()
    {
        if(!isInitialized) return;
        var mapData = LocalDataPlayer.Instance.MapData;
        var maps = mapData.Maps;
        int index = LocalDataPlayer.Instance.GetUnlockMaps().Count - 1;
        mapScrollSnap.GoToPanel(index);
        mapTitle.SetText(maps[index].mapTitle);
        LocalDataPlayer.Instance.CurrentMapData = maps[index];
    }

    private void InitMap()
    {
        var mapData = LocalDataPlayer.Instance.MapData;
        var maps = mapData.Maps;
        for (int i = 0; i < maps.Length; i++)
        {
            var newMap = Instantiate(mapRawImageTemplate);
            newMap.texture = maps[i].renderTexture;
            newMap.gameObject.SetActive(true);
            newMap.name = "Map " + i;
            mapScrollSnap.AddToBack(newMap.gameObject);
        }
        int index = LocalDataPlayer.Instance.GetUnlockMaps().Count - 1;
        mapScrollSnap.GoToPanel(index);
        Debug.Log("Index: " + index);
        mapTitle.SetText(maps[index].mapTitle);
        isInitialized = true;
    }

    private void OnMapCentered(int centeredPanel, int selectedPanel)
    {
        var mapData = LocalDataPlayer.Instance.MapData;
        var maps = mapData.Maps;
        mapTitle.SetText(maps[centeredPanel].mapTitle);
        bool isDoneMap = LocalDataPlayer.Instance.HasUnlockMaps(maps[centeredPanel].mapId);
       startButton.interactable = isDoneMap;

       LocalDataPlayer.Instance.CurrentMapData = maps[centeredPanel];
    }
}