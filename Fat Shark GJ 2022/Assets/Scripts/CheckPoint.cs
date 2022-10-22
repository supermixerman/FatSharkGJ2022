using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] bool isWin;
    public Vector2 GetRespawnLocation(){
        return gameObject.transform.position;
    }

    public bool IsWinCheck(){
        return isWin;
    }
}
