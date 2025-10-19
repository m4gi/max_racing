using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;

    public void SetName(string text)
    {
        userName.text = text;
    }
}
