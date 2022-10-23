using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour // : MonoBehaviour
{
    public bool hasBomb;

    public void RecievePowerup(int type)
    {
        if (type == 0)
        {
            hasBomb = true;
        }
    }

    public void ActivateBomb()
    {
        if (!hasBomb)
        {
            return;
        }

    }
}
