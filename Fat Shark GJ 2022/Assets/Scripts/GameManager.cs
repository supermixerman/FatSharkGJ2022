using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab, mainCamera, winnerScreen, inGameUI;
    [SerializeField] int playerAmount, turn;
    [SerializeField] List<GameObject> playerList;
    [SerializeField] List<Transform> spawnLocations;
    [SerializeField] List<Color> playerColorsList;
    [SerializeField] GameData gameData;

    public bool victory;
    private bool _waitForNextTurn;

    UIText gameUI;

    public static GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameUI = inGameUI.GetComponent<UIText>();
        StartGame(gameData.totalPlayers);
        _waitForNextTurn = true;
    }

    private void LateUpdate()
    {
        Debug.Log(_waitForNextTurn);
        if (AllBallsStopped() && InputManager.inputManager.GetDidAction() && !_waitForNextTurn)
        {
            NextTurn();
        }
    }

    public void StartGame(int players){
        playerAmount = players;
        SpawnPlayers(playerAmount);
        TurnStart();
        //menuCanvas.SetActive(false);
        //gameCanvas.SetActive(true);
    }

    public GameObject GetCurrentPlayer(){
        return playerList[turn];
    }

    public void SpawnPlayers(int amount){
        if (amount > spawnLocations.Count) //There can not be more players than the amount of spawn locations.
        {
            Debug.LogError("Player Amount is too large. Add more Spawn Locations or lower the value. Changed player amount to the amount of spawn locations.");
            amount = spawnLocations.Count;
            playerAmount = spawnLocations.Count;
        }
        for (int i = 0; i < amount; i++)
        {
            playerList.Add(Instantiate(playerPrefab, spawnLocations[i].position, Quaternion.identity));
            playerList[i].gameObject.name = "Player " + (i+1);
            Debug.Log(playerList[i].gameObject.name);
            if (playerColorsList.Count >= playerList.Count){ //Set player color
                playerList[i].GetComponent<SpriteRenderer>().color = playerColorsList[i];
            }
        }
    }

    public void CameraFollow(Transform target){
        Debug.Log("Camera Target: "+target);
        mainCamera.GetComponent<CameraFollow>().SetTarget(playerList[turn].transform);
        //mainCamera.transform.position = target.position;
    }

    public void TurnStart(){
        turn = 0;
        UpdateGameUI();
        CameraFollow(playerList[turn].transform);
        InputManager.inputManager.SetActiveBall(playerList[turn].GetComponent<BallControl>());
        //turnText.text = "Turn: "+playerList[0].name;
    }

    public void NextTurn(){
        turn++;
        if (turn >= playerList.Count){
            Debug.Log("Reset turn");
            turn = 0;
        }
        Debug.Log("Turn num = "+turn);
        UpdateGameUI();
        CameraFollow(playerList[turn].transform);
        InputManager.inputManager.SetActiveBall(playerList[turn].GetComponent<BallControl>());
        _waitForNextTurn = true;
    }

    public void Victory(){
        victory = true;
        winnerScreen.SetActive(true);
        winnerScreen.GetComponent<UIText>().SetText("Winner: " + playerList[turn].name);
        Debug.Log("A ball has won the game");
    }

    public void UpdateGameUI(){
        gameUI.SetTextColor(playerColorsList[turn]);
        gameUI.SetText(playerList[turn].name);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ResetGame(){
        SceneManager.LoadScene(0);
    }

    private bool AllBallsStopped()
    {
        foreach (GameObject ball in playerList)
        {
            BallControl ballControl = ball.GetComponent<BallControl>();
            if (ballControl.GetIsMoving())
            {
                return false;
            }
            else if (ballControl.GetHasBomb()) 
            { 
                return false; 
            }
        }
        return true;
    }

    public void StartWaitForTurn(float time=0.5f)
    {
        _waitForNextTurn = true;
        StartCoroutine(WaitForTurn(time));
    }

    private IEnumerator WaitForTurn(float time=0.5f)
    {
        yield return new WaitForSeconds(time);
        _waitForNextTurn = false;
    }
}
