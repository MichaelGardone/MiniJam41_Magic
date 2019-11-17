using System;
using UnityEngine;

public interface IBaubleItem
{
    string Name { get; }

    Sprite Image { get; }

    void BaublePickup();
}

public class BaubleEventArgs : EventArgs
{
    public IBaubleItem item;

    public BaubleEventArgs(IBaubleItem item)
    {
        this.item = item;
    }
}

