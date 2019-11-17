using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    [SerializeField] ScrollRect viewer;
    [SerializeField] Button buttonPrefab;

    Inventory inventory;
    Dictionary<IBaubleItem, int> inv;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inv = inventory.GetInventory();
        foreach(IBaubleItem b in inv.Keys)
        {
            Button buttonb = Instantiate(buttonPrefab);
            buttonb.transform.parent = viewer.content.transform;
        }
    }

    void Update()
    {
        
    }
}
