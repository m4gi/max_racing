using Game.Scripts.Data;
using Game.Scripts.Shop;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private CarDataSO carData;

    [SerializeField] private ShopItem[] shopItems;

    void Start()
    {
        InitShop();
    }

    private void InitShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].Initialize(carData.carData[i], UpdateAllItem);
        }
    }

    private void UpdateAllItem()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].ForceUpdateItem();
        }
    }
}