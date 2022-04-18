using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tool Item", menuName = "Collectibles/Tool Item")]
public class ToolTypeSO : Collectible
{
    public string toolItemID;
    public string toolName;
    public Type toolType;
    public Quality toolQuality;
    public QualityType toolQualityType;
    

    public void OnValidate()
    {
        toolName = this.name;
    }

    public void OnEnable()
    {
        itemType = ItemType.Tool;
        toolName = this.name;
        toolItemID = $"item_{itemType}_{toolName}_{Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 12)}";
    }
}
