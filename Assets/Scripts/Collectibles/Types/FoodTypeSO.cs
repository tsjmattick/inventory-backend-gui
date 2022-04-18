using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Collectibles/Food Item")]
public class FoodTypeSO : Collectible
{
    public int healthValue;
    public string foodItemID;
    public string foodName;
    public FoodTypeSO()
    {
        
    }

    public void OnValidate()
    {
        foodName = this.name;
    }

    public void OnEnable()
    {
        itemType = ItemType.Food;
        foodName = this.name;
        foodItemID = $"item_{itemType}_{foodName}_{Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 12)}";
    }

    public void OnDisable()
    {
        
    }
}
