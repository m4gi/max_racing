using System;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "CarDataSO", menuName = "Game/Data/CarDataSO", order = 0)]
    public class CarDataSO : ScriptableObject
    {
        public CarData[] carData;
    }

    [Serializable]
    public class CarData
    {
        public string carId;
        public string carName;
        public int price;
    }
}