using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;
    private void Awake()
    {
        Current = this;
    }

    public event Action<Collectible, int> OnItemPickupTriggerEnter;

    public void ItemPickupTriggerEnter(Collectible collectible, int id)
    {
        OnItemPickupTriggerEnter?.Invoke(collectible,id);
    }
}