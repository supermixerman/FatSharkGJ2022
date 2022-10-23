using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int _powerupType;

    public int GetPowerupType()
    {
        return _powerupType;
    }
}
