using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Dictionary<IBaubleItem, int> inventory = new Dictionary<IBaubleItem, int>();

    public event EventHandler<BaubleEventArgs> BaublePickedUp;

    public void AddBauble(IBaubleItem item)
    {
        if (inventory.ContainsKey(item))
            inventory[item]++;
        else
            inventory.Add(item, 1);

        item.BaublePickup();

        BaublePickedUp?.Invoke(this, new BaubleEventArgs(item));
    }

    public Dictionary<IBaubleItem, int> GetInventory()
    {
        return inventory;
    }

    public void UpdateInventory(IBaubleItem item, int amt)
    {
        inventory[item] -= amt;
    }

}
