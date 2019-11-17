using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBauble : MonoBehaviour, IBaubleItem
{

    public string Name
    {
        get
        {
            return "Fire Baubel";
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnBaubelPickup()
    {
        Destroy(this);
    }
}
