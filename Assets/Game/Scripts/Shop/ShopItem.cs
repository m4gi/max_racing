using System;
using Game.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TextMeshProUGUI cost;
        [SerializeField]
        private Button buyButton;
        [SerializeField]
        private Button selectButton;
        [SerializeField]
        private GameObject selectedButton;

        private string itemId;
        private int carPrice;
        private string itemName;
        private Action itemAction;
        
        private void Start()
        {
            buyButton.onClick.AddListener(BuyButtonOnClick);
            selectButton.onClick.AddListener(SelectButtonOnClick);
        }

        private void OnDestroy()
        {
            buyButton.onClick.RemoveListener(BuyButtonOnClick);
            selectButton.onClick.RemoveListener(SelectButtonOnClick);
        }

        private void SelectButtonOnClick()
        {
            LocalDataPlayer.Instance.LocalData.CurrentSelectedCar = itemId;
            itemAction?.Invoke();
        }

        private void BuyButtonOnClick()
        {
            bool success = LocalDataPlayer.Instance.MinusGold(carPrice);
            if (success)
            {
                LocalDataPlayer.Instance.AddCarDeck(itemId);
                LocalDataPlayer.Instance.LocalData.CurrentSelectedCar = itemId;
                itemAction?.Invoke();
            }
        }

        public void Initialize(CarData carData, Action action = null)
        {
            itemId = carData.carId;
            carPrice = carData.price;
            itemName = carData.carName;
            itemAction = action;

            title.SetText(carData.carName);
            cost.SetText(StringUltils.FormatNumber(carData.price));

            var localData = LocalDataPlayer.Instance;
            bool hasCar = localData.HasCar(itemId);
            bool isSelected = localData.LocalData.CurrentSelectedCar == itemId;

            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);
            selectedButton.SetActive(false);

            if (!hasCar)
            {
                buyButton.gameObject.SetActive(true);
            }
            else if (isSelected)
            {
                selectedButton.SetActive(true);
            }
            else
            {
                selectButton.gameObject.SetActive(true);
            }
        }

        public void ForceUpdateItem()
        {
            var localData = LocalDataPlayer.Instance;
            bool hasCar = localData.HasCar(itemId);
            bool isSelected = localData.LocalData.CurrentSelectedCar == itemId;

            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);
            selectedButton.SetActive(false);

            if (!hasCar)
            {
                buyButton.gameObject.SetActive(true);
            }
            else if (isSelected)
            {
                selectedButton.SetActive(true);
            }
            else
            {
                selectButton.gameObject.SetActive(true);
            }
        }
    }
}