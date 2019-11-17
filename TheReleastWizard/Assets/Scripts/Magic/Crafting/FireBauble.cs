using UnityEngine;

public class FireBauble : MonoBehaviour, IBaubleItem
{
    public string Name
    {
        get
        {
            return "Fire Bauble";
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

    public void BaublePickup()
    {
        Destroy(gameObject);
    }
}
