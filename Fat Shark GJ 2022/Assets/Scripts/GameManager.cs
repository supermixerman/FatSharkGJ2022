using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab, mainCamera;
    [SerializeField] int playerAmount, turn;
    [SerializeField] List<GameObject> playerList;
    [SerializeField] Transform spawnLocation;
    [SerializeField] List<Color> playerColorsList;
    
    public static GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null){
            gameManager = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPlayers(){
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList.Add(Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity));
            playerList[i].gameObject.name = "Player " + (i+1);
            Debug.Log(playerList[i].gameObject.name);
            if (playerColorsList.Count >= playerList.Count){ //Set player colour
                //playerList[i].GetComponent<Player>().SetPlayerColor(playerColorsList[i]);
            }
            playerAmount += 1;
        }
    }

    public void CameraFollow(Transform target){
        Debug.Log("Camera Target: "+target);
        mainCamera.transform.position = target.position;
    }

    public void TurnStart(){
        turn = 0;
        //characterController.SetActivePlayer(playerList[0]);
        CameraFollow(playerList[0].transform);
        //turnText.text = "Turn: "+playerList[0].name;
    }
}
