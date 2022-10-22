using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class BallControl : MonoBehaviour
{
    [SerializeField][Range(0, 1f)]private float _strikeModifier;
    [SerializeField] GameObject _strikeIndicator;
    private Vector2 _respawn;
    private Rigidbody2D _rb;

    public UnityEvent onBallStop;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Strike(Vector2 strike)
    {
        _rb.AddForce(strike * _strikeModifier, ForceMode2D.Impulse);
    }

    private void CheckForStop()
    {
        if (_rb.velocity == Vector2.zero)
        {
            //and has been 0 for a while?
            onBallStop.Invoke();
        }
    }

    public void DrawStrikeIndicator(Vector2 origin, Vector2 position)
    {
        Vector2 scaleVector = origin - position;
        float scale = scaleVector.magnitude;
        Debug.Log(scale);

        _strikeIndicator.SetActive(true);
        _strikeIndicator.transform.position = position;
        _strikeIndicator.transform.localScale = new Vector2(scale, scale);
    }

    public void HideStrikeIndicator()
    {
        _strikeIndicator.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Respawn")){
            CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();
            _respawn = checkPoint.GetRespawnLocation();

            if (checkPoint.IsWinCheck()){
                GameManager.gameManager.Victory();
            }
        }
    }
}

