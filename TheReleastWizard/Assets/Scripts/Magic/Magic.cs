using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public int damage = 2;

    protected PlayerController owner;

    public void SetOwner(PlayerController pc)
    {
        owner = pc;
    }
}
