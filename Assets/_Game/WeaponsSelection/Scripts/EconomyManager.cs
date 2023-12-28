using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;
    [SerializeField]
    private int cash;

    private void Awake()
    {
        Instance = this;
    }

    public int GetCurrentCash()
    {
        return cash;
    }

    public void AddCash(int addAmount)
    {
        cash += addAmount;
    }
}
