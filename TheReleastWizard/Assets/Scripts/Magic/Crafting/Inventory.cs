using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private const int SLOTS = 100;

    private Dictionary<IBaubleItem, int> items = new Dictionary<IBaubleItem, int>();

    public event EventHandler<BaubleEventArgs> baubleGrabbed;

    public void AddBauble(IBaubleItem item)
    {
        if (items.Count < SLOTS)
        {
            Collider r = (item as MonoBehaviour).GetComponent<Collider>();
            if(r.enabled)
            {
                r.enabled = false;
                if(items.ContainsKey(item))
                {
                    items[item]++;
                }
                else
                {
                    items.Add(item, 1);
                }

                item.OnBaubelPickup();

                baubleGrabbed?.Invoke(this, new BaubleEventArgs(item));
            }
        }
    }

}
