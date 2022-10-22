using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class BallControl : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] private float _strikeModifier;
    [SerializeField][Range(0, 1f)] private float _strikeIndicatorScaleModifier;
    [SerializeField][Range(0, 1f)] private float _minimumVelocity;
    [SerializeField][Range(0.5f, 5f)] private float _strikeIndicatorOffset;
    [SerializeField] GameObject _strikeIndicator;
    private Vector2 _respawn;
    private Rigidbody2D _rb;
    private bool _isMoving;

    public UnityEvent onBallStoppedMoving;
    public UnityEvent onBallStartedMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Strike(Vector2 strike)
    {
        _rb.AddForce(strike * _strikeModifier, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        CheckForStop();
    }

    private void CheckForStop()
    {
        if (_rb.velocity.magnitude > _minimumVelocity) // l�gg till threshold
        {
            if (_isMoving)
            {
                _rb.velocity = Vector2.zero;
                onBallStoppedMoving.Invoke();
                Debug.Log("A ball stopped moving");
                transform.rotation = Quaternion.identity;
                _isMoving = false;
            }
        }
        else
        {
            if (!_isMoving)
            {
                onBallStartedMoving.Invoke();
                Debug.Log("A ball started moving");
            }
            _isMoving = true;
        }
    }

    public void DrawStrikeIndicator(Vector2 origin, Vector2 position)
    {
        Vector2 scaleVector = origin - position;
        float scale = scaleVector.magnitude;
        scale = scale * _strikeIndicatorScaleModifier;

        scaleVector.Normalize();

        _strikeIndicator.SetActive(true);

        _strikeIndicator.transform.localPosition = - scaleVector * (scale + _strikeIndicatorOffset);
        if (scaleVector.x <= 0)
        {
        _strikeIndicator.transform.rotation = Quaternion.Euler(Vector3.forward * Vector2.Angle(Vector2.up, scaleVector));
        }
        else
        {
            _strikeIndicator.transform.rotation = Quaternion.Euler(Vector3.forward * Vector2.Angle(Vector2.down, scaleVector));
        }

        _strikeIndicator.transform.localScale = new Vector2(scale * 0.5f, scale);
    }

    public void HideStrikeIndicator()
    {
        _strikeIndicator.SetActive(false);
    }

    public void SetWeight(float newWeight)
    {
        _rb.mass = newWeight;
    
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

