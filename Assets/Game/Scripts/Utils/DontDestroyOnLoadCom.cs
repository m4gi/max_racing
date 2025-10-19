using System;
using UnityEngine;

public class DontDestroyOnLoadCom : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
