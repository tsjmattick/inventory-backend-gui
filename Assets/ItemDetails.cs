using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDetails : MonoBehaviour
{
    public Collectible info;
    private void OnTriggerEnter(Collider other)
    {
        // send the item details to the inventory listener, update quantity if not supplied.  
        // if a negative number is sent over, for whatever reason, then that is ok for now.
        GameEvents.Current.ItemPickupTriggerEnter(info, info.amount == 0 ? 1:info.amount);
        Destroy(this.gameObject);
    }

}
