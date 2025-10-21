using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Data;
using MoreMountains.Tools;
using Newtonsoft.Json;
using UnityEngine;

public class LocalDataPlayer : MMPersistentSingleton<LocalDataPlayer>
{
    public MapDataSO MapData;
    public List<int> PricePool;
    public bool IsGameStarted { get; set; } = false;
    
    public MapData CurrentMapData { get; set; }

    public class LocalUserData
    {
        public bool SoundOn { get; set; } = true;
        public bool MusicOn { get; set; } = true;

        public int Gold { get; set; } = 0;

        public string CurrentSelectedCar { get; set; } = "HotFire";

        public List<string> Cars { get; set; } = new List<string> { "HotFire" };
        
        public List<string> MapUnlocks { get; set; } = new List<string> { "map_1" };

        public override string ToString()
        {
            return $"LocalUserData {Gold} - {CurrentSelectedCar} -  {Cars[0]}";
        }
    }

    private LocalUserData _localUserData;

    public Action<int> OnGoldChanged;
    public Action<bool,bool> OnSoundChanged;

    public LocalUserData LocalData
    {
        get
        {
            if (_localUserData == null)
            {
                if (!LoadData())
                    _localUserData = new LocalUserData();
            }

            return _localUserData;
        }
    }

    public void AddGoldByRank(int rank)
    {
        AddGold(PricePool[rank]);
    }

    public void AddGold(int amount)
    {
        LocalData.Gold += amount;
        OnGoldChanged?.Invoke(LocalData.Gold);
        SaveData();
    }

    public bool MinusGold(int amount)
    {
        if (LocalData.Gold < amount) return false;
        LocalData.Gold -= amount;
        OnGoldChanged?.Invoke(LocalData.Gold);
        SaveData();
        return true;
    }
    
    public int GetGold()
    {
        return LocalData.Gold;
    }

    public void SaveData()
    {
        string saveJson = JsonConvert.SerializeObject(_localUserData);
        PlayerPrefs.SetString("LocalUserData", saveJson);
        PlayerPrefs.Save();
    }

    public bool LoadData()
    {
        if (!PlayerPrefs.HasKey("LocalUserData")) return false;
        string saveJson = PlayerPrefs.GetString("LocalUserData");
        _localUserData = JsonConvert.DeserializeObject<LocalUserData>(saveJson);
        return true;
    }

    public void AddCarDeck(string itemId)
    {
        if (LocalData.Cars.Contains(itemId)) return;
        LocalData.Cars.Add(itemId);
        SaveData();
    }

    public bool HasCar(string itemId)
    {
        return LocalData.Cars.Contains(itemId);
    }

    public bool GetSound => LocalData.SoundOn;
    
    public bool GetMusic => LocalData.MusicOn;
    
    public void SetSound(bool isOn)
    {
        LocalData.SoundOn = isOn;
        OnSoundChanged?.Invoke(LocalData.SoundOn, LocalData.MusicOn);
        SaveData();
    }
    
    public void SetMusic(bool isOn)
    {
        LocalData.MusicOn = isOn;
        OnSoundChanged?.Invoke(LocalData.SoundOn, LocalData.MusicOn);
        SaveData();
    }

    public bool HasUnlockMaps(string mapId)
    {
        return LocalData.MapUnlocks.Contains(mapId);
    }

    public List<string> GetUnlockMaps()
    {
        return LocalData.MapUnlocks.Distinct().ToList();
    }
    public void AddMap(string mapId)
    {
        if (LocalData.MapUnlocks.Contains(mapId)) return;
        LocalData.MapUnlocks.Add(mapId);
        LocalData.MapUnlocks = LocalData.MapUnlocks.Distinct().ToList();
        SaveData();
    }

    public string GetNextMapID()
    {
        int totalMap = GetUnlockMaps().Count;
        if (totalMap < MapData.Maps.Length)
        {
            return MapData.Maps[totalMap].mapId;
        }

        return string.Empty;
    }
}