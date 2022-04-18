using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Item", menuName = "Collectibles/Material Item")]
public class MaterialsTypeSO : Collectible
{
    public string materialItemID;
    public string materialName;
    public Type materialType;
    public Quality materialQuality;
    public QualityType materialQualityType;
    public void OnValidate()
    {
        materialName = this.name;
    }

    public void OnEnable()
    {
        itemType = ItemType.Material;
        materialName = this.name;
        materialItemID = $"item_{itemType}_{materialName}_{Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 12)}";
    }
}


