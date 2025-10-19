using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "MapDataSO", menuName = "Game/Data/MapDataSO", order = 0)]
    public class MapDataSO : ScriptableObject
    {
        public MapData[] Maps;
    }

    [System.Serializable]
    public class MapData
    {
        public string mapId;
        public string mapTitle;
        public int numberOfCar;
        public RenderTexture renderTexture;
    }
}