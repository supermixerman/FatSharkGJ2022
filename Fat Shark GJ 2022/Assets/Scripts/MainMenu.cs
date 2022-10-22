using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Dropdown dropDown;

    public void SetTotalPlayers(){
        gameData.totalPlayers = dropDown.value + 1;
        Debug.Log("Total Players: " + gameData.totalPlayers);
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
