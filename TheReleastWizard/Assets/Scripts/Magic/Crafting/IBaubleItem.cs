using System;
using UnityEngine;

public interface IBaubleItem
{
    string Name { get; }

    Sprite Image { get; }

    void OnBaubelPickup();
}

public class BaubleEventArgs : EventArgs
{
    public IBaubleItem item;

    public BaubleEventArgs(IBaubleItem incoming)
    {
        item = incoming;
    }
}

